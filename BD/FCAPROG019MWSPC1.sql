USE [Cajas01]
GO

ALTER PROCEDURE FCAPROG019MWSPC1
	@Opcion INT							= NULL
	, @Programa INT						= NULL
	, @ClaveMaquina VARCHAR(10)			= NULL
	, @wFechaAnterior VARCHAR(10)		= NULL
	, @Turno INT						= NULL
	, @Fecha VARCHAR(10)				= NULL
	, @FechaF VARCHAR(10)				= NULL
	, @SinFechaProd BIT					= NULL
	, @OP VARCHAR(20)					= NULL
	, @Suaje VARCHAR(10)				= NULL
	, @Articulo VARCHAR(10)				= NULL
	, @TipoConsulta INT					= NULL
	, @MaquinaDesperdicio VARCHAR(10)	= NULL
	, @AplicaCajaRec BIT				= NULL
	, @EsUtilizado BIT					= NULL
	, @EsContabilizadoPLC BIT			= NULL
	, @EsProcesoAnterior BIT			= NULL
	, @ClaveSupervisor VARCHAR(10)		= NULL

	, @UsuarioERP VARCHAR(6)			= NULL
	, @ZonaERP VARCHAR(2)				= NULL
	, @FechaSinGuion BIT				= NULL
AS BEGIN
	SELECT @wFechaAnterior = CASE WHEN ISNULL(@wFechaAnterior, '') = '' THEN NULL ELSE @wFechaAnterior END;

	-- INICIA PRODUCCIÓN
	IF @Opcion = 1
	BEGIN
		DECLARE @wFecha DATETIME, @Formato VARCHAR(16)
		SELECT @Formato = CASE WHEN ISNULL(@FechaSinGuion, 0) = 1 THEN 'yyyyMMdd HH:mm' ELSE 'yyyy-MM-dd HH:mm' END
		SELECT @wFecha = Informatica.[dbo].[MnuFun002](1, @UsuarioERP, @ZonaERP, GETDATE());
		SELECT FORMAT(@wFecha, @Formato) AS Fecha;
	END
	-- BUSCAR PROGRAMA
	ELSE IF @Opcion = 2
	BEGIN
		SELECT TOP 10 A.Programa, A.[Clave Maquina] AS ClaveMaquina, A.Op, A.[Clave Proceso] AS ClaveProceso, A.[Piezas Corte] AS PiezasCorte, A.[Tipo Maquina] AS TipoMaquina,
			A.Cantidad, A.[Ultimo Proceso] AS UltimoProceso, A.[Pegado], A.[1er Color] AS PrimerColor, A.[2do Color] AS SegundoColor, A.[3er Color] AS TercerColor,
			A.[4to Color] AS CuartoColor, C.[Area unitaria] AS AreaUnitaria, C.[Peso Unitario] AS PesoUnitario, C.[Clave Articulo] AS ClaveArticulo,
			D.Descripcion AS Articulo, C.[Liberado Costos] AS LiberadoCostos
		FROM CmoDat020 A 
		JOIN CmoDat011 C ON A.op=C.op
		JOIN CmoTjCat004 D ON D.[clave articulo]= C.[clave articulo]
		WHERE A.[Tipo Maquina] != 'IM' AND C.[Liberado Costos] = 0
		--WHERE A.programa = @Programa
	END
	-- BUSCA PRODUCCIÓN
	ELSE IF @Opcion = 3
	BEGIN
		SELECT [Clave Maquina] AS ClaveMaquina, OP, FORMAT(Fecha, 'yyyy-MM-dd') AS Fecha, Turno
			, Cantidad, FORMAT([Fecha Sistema], 'yyyy-MM-dd HH:mm') AS FechaSistema, IdUnico
		FROM CmoDat021
		WHERE Programa = @Programa
		ORDER BY Fecha, Turno
	END
	-- OBTENER TRIPULACIONES DE MAQUINA
	ELSE IF @Opcion = 4
	BEGIN
		SELECT A.IdTripulacion, RTRIM(B.ClaveMaquina) + ' - ' + RTRIM(A.Nombre) AS Tripulacion
		FROM CmoCat071 A
		INNER JOIN CmoDat143 B ON B.IdTripulacion = A.IdTripulacion
		WHERE Estatus = 0 AND b.ClaveMaquina = @ClaveMaquina 
		ORDER BY Tripulacion
	END
	-- LEE PRODUCCIÓN PARTE 1
	ELSE IF @Opcion = 5
	BEGIN
		IF ISNULL(@wFechaAnterior, '') != ''
		BEGIN
			SELECT [Programa], [Turno], [Supervisor], [Clave Maquina] AS ClaveMaquina, [OP], FORMAT([Fecha], 'yyyy-MM-dd') AS Fecha, [Hora Inicio] AS HoraInicio, [Hora Termino] AS HoraTermino
				, [Cantidad], [PickUp], [Minutos Produccion] AS MinutosProduccion, [Desperdicio Acabados] AS DesperdicioAcabados, [LAMINADESPEG], [LAMINACOMBA]
				, [LAMINAMEDIDAS], [LAMINAIMPRES], [LAMINADIMENS], [LAMINAPEGAD], FORMAT([Fecha Sistema], 'yyyy-MM-dd') AS FechaSistema, [Usuario]
				, [Excedente], [ExpCostos], FORMAT([FechaExp], 'yyyy-MM-dd') AS FechaExp, [Laminas Desperdicio] AS LaminasDesperdicio, [Piezas Desperdicio] AS PiezasDesperdicio
				, [CanTinta1], [CanTinta2], [CanTinta3], [CanTinta4], [Tipo Parafina] AS TipoParafina, [SinPreparacion]
				, [VerificaAduana], [ProduccionPT], [MinutosFT], [ClaveInspector], [PesoLamina]
				, [PesoCaja], [Retrabajo], [MaqPA], [DespPA], [IdTripulacion], [DespProdEnProc]
				, [Motivo], [PreAlimentador], FORMAT([FechaINSERT], 'yyyy-MM-dd') AS FechaInsert, [DespCorrNoUtil], [ProdProcesoCap]
				, [DespPAUtil], [DespImpNoConPLC], [IdUnico], [ModuloInsert], [Posicion]
				, [DesEtFront], [DesEtBack], [CajasComProPLC], [ContabilizadoGolpesXSuaje]
				, [VerificaRuta], [MinutosComedor], [CajasRecEnProd]
			FROM CmoDat021
			WHERE ISNULL(Fecha, '') = ISNULL(@wFechaAnterior, '') AND Programa = @Programa 
				AND [Clave Maquina] = @ClaveMaquina AND Turno = @Turno
			ORDER BY Fecha
		END ELSE
		BEGIN
			SELECT [Programa], [Turno], [Supervisor], [Clave Maquina] AS ClaveMaquina, [OP], FORMAT([Fecha], 'yyyy-MM-dd') AS Fecha, [Hora Inicio] AS HoraInicio, [Hora Termino] AS HoraTermino
				, [Cantidad], [PickUp], [Minutos Produccion] AS MinutosProduccion, [Desperdicio Acabados] AS DesperdicioAcabados, [LAMINADESPEG], [LAMINACOMBA]
				, [LAMINAMEDIDAS], [LAMINAIMPRES], [LAMINADIMENS], [LAMINAPEGAD], FORMAT([Fecha Sistema], 'yyyy-MM-dd') AS FechaSistema, [Usuario]
				, [Excedente], [ExpCostos], FORMAT([FechaExp], 'yyyy-MM-dd') AS FechaExp, [Laminas Desperdicio] AS LaminasDesperdicio, [Piezas Desperdicio] AS PiezasDesperdicio
				, [CanTinta1], [CanTinta2], [CanTinta3], [CanTinta4], [Tipo Parafina] AS TipoParafina, [SinPreparacion]
				, [VerificaAduana], [ProduccionPT], [MinutosFT], [ClaveInspector], [PesoLamina]
				, [PesoCaja], [Retrabajo], [MaqPA], [DespPA], [IdTripulacion], [DespProdEnProc]
				, [Motivo], [PreAlimentador], FORMAT([FechaINSERT], 'yyyy-MM-dd') AS FechaInsert, [DespCorrNoUtil], [ProdProcesoCap]
				, [DespPAUtil], [DespImpNoConPLC], [IdUnico], [ModuloInsert], [Posicion]
				, [DesEtFront], [DesEtBack], [CajasComProPLC], [ContabilizadoGolpesXSuaje]
				, [VerificaRuta], [MinutosComedor], [CajasRecEnProd]
			FROM CmoDat021
			WHERE Programa = @Programa AND [Clave Maquina] = @ClaveMaquina AND Turno = @Turno
			ORDER BY Fecha
		END
	END
	-- COMBO SUPERVISORES
	ELSE IF @Opcion = 6
	BEGIN
		SELECT [Clave Supervisor] AS IdSupervisor, Nombre AS Supervisor
		FROM CmoCat012 
		WHERE Status = 0 
		ORDER BY Nombre
	END
	-- COMBO TIPO PARAFINA
	ELSE IF @Opcion = 7
	BEGIN
		SELECT IdParafina, Descripcion AS Parafina
		FROM CmoCat039
	END
	-- VERIFICAR DATOS ANTES DE GUARDAR
	ELSE IF @Opcion = 8
	BEGIN
		SELECT Cantidad 
		FROM CmoDat021 
		WHERE Programa = @Programa AND [Clave Maquina] = @ClaveMaquina
			AND Turno = @Turno AND Fecha = @Fecha
	END
	-- CATALOGO DE MAQUINAS
	ELSE IF @Opcion = 9
	BEGIN
		SELECT DISTINCT RTRIM ([Clave Maquina]) AS ClaveMaquina
        FROM CmoCat011
        WHERE Status = 0 AND [Tipo Maquina] IN ('AC', 'ES')
	END
	-- CARGA PROGRAMAS DE PAGINA L
	ELSE IF @Opcion = 10
	BEGIN
		IF ISNULL(@SinFechaProd, 0) = 1
		BEGIN
			SELECT A.Programa, A.[Clave Maquina] AS ClaveMaquina, A.Turno, A.OP, A.IdUnico, FORMAT(A.Fecha, 'yyyy-MM-dd') AS Fecha
			FROM CmoDat020 B
			JOIN CmoDat021 A on A.Programa = B.Programa
			JOIN CmoDat011 C on A.OP = C.OP
			WHERE B.[Fecha Programa] BETWEEN ISNULL(@Fecha, '') AND ISNULL(@FechaF, '') AND Turno = @Turno
				AND B.[Tipo Maquina] <> 'IM' AND C.[Liberado Costos] = 0 AND A.[Clave Maquina] = @ClaveMaquina
				AND A.Fecha IS NULL
			ORDER BY A.Fecha, A.Programa DESC
		END ELSE
		BEGIN
			SELECT A.Programa, A.[Clave Maquina] AS ClaveMaquina, A.Turno, A.OP, A.IdUnico, FORMAT(A.Fecha, 'yyyy-MM-dd') AS Fecha
			FROM CmoDat020 B
			JOIN CmoDat021 A on A.Programa = B.Programa
			JOIN CmoDat011 C on A.OP = C.OP
			WHERE B.[Fecha Programa] BETWEEN ISNULL(@Fecha, '') AND ISNULL(@FechaF, '') AND Turno = @Turno
				AND B.[Tipo Maquina] <> 'IM' AND C.[Liberado Costos] = 0 AND A.[Clave Maquina] = @ClaveMaquina
			ORDER BY A.Fecha, A.Programa DESC
		END
	END
	-- =========================================================================================================================================
	-- MODULO 2

	-- BUSCA PROGRAMA
	ELSE IF @Opcion = 11
	BEGIN
		SELECT A.[Clave Maquina] AS ClaveMaquina, A.OP, A.[Clave Proceso] AS ClaveProceso, A.[Piezas Corte] AS PiezasCorte, 
			A.Proceso1, A.Cantidad, A.[Ultimo Proceso] AS UltimoProceso, A.[Pegado], 
			A.[1er Color] AS PrimerColor, A.[2do Color] AS SegundoColor, A.[3er Color] AS TercerColor, A.[4to Color] AS CuartoColor, 
			C.[Area unitaria] AS AreaUnitaria, C.[Peso Unitario] AS PesoUnitario, C.[Clave Articulo] AS ClaveArticulo,
			A.[Tipo Maquina] AS TipoMaquina, E.Descripcion AS Articulo, A.Suaje AS SuajeOld, C.[Liberado Costos] AS LiberadoCostos, 
			ISNULL(A.NotasOperacion, '') AS NotasOperacion, A.Eficiencia, G.Eficiencia AS EficienciaAct, D.SinPreparacion, 
			D.CanTinta1, D.CanTinta2, D.CanTinta3, D.CanTinta4, ISNULL(D.Supervisor, '') AS Supervisor, F.Descripcion AS Proceso, 
			A.[Minutos Preparacion] AS MinPrep, A.[Velocidad Std] AS Velocidad, A.[Minutos Produccion] AS MinStdProd, D.MaqPA, D.DespPA,
			CASE
				WHEN ISNULL(E.Suaje, '') <> '' THEN RTRIM(E.Suaje)
				Else CASE
					WHEN ISNULL(E.Suaje50, '') <> '' THEN RTRIM(E.Suaje50)
					Else CASE
						WHEN ISNULL(E.Suaje2, '') <> '' THEN RTRIM(E.Suaje2)
						ELSE CASE
							WHEN ISNULL(E.Suaje66, '') <> '' THEN RTRIM(E.Suaje66)
							ELSE 'N/D'
						END
					END
				END
			END AS Suaje
		FROM CmoDat020 A
			JOIN CmoDat011 C ON A.op = C.op
			JOIN CmoDat021 D ON A.programa = D.programa
			JOIN CmoTjCat004 E ON C.[clave articulo] = E.[clave articulo]
			JOIN CmoCat010 F ON A.[Tipo Maquina] = F.[Tipo Maquina]
				AND A.[Clave Proceso] = F.[Clave Proceso]
			JOIN CmoCat011 G ON A.[Clave Maquina] = G.[Clave Maquina]
		WHERE A.programa = @Programa
	END
	-- CATALOGO DE CLAVE PREPARACION
	ELSE IF @Opcion = 12
	BEGIN
		SELECT RTRIM(A.[Clave Proceso]) AS ClaveProceso, RTRIM(B.Descripcion) AS Descripcion, A.[Tiempo Std] AS TiempoStd,
			RTRIM(A.[Clave Proceso]) + ' , ' + RTRIM(B.Descripcion) + ' , Min.Prep: ' + FORMAT(A.[Tiempo Std], '0') AS Compuesto
		FROM CmoDat018 A
		JOIN CmoCat010 B on A.[Tipo Maquina] + A.[Clave Proceso] = B.[Tipo Maquina] + B.[Clave Proceso]
		WHERE B.Status = 0 AND A.[clave maquina] = @ClaveMaquina
		ORDER BY A.[Clave Proceso]
	END
	-- VALIDAR DATOS DE MAQUINA
	ELSE IF @Opcion = 13
	BEGIN
		IF ISNULL(@TipoConsulta, 0) = 0
		BEGIN
			SELECT RTRIM(Programa) AS Programa, RTRIM(ClaveMaquinaDesp) AS ClaveMaquinaDesp, RTRIM(Turno) AS Turno, 
				ISNULL(EsUtilizado, 0) AS EsUtilizado, SUM(Cantidad) AS TotalDesperdicio, ISNULL(EsProcesoAnterior, 0) AS EsProcesoAnterior
			FROM CmoDat259 A
			WHERE A.Estatus = 0 
				AND OP = @OP
				AND Programa = @Programa
				AND Turno = @Turno
			GROUP BY Programa, ClaveMaquinaDesp, Turno, EsUtilizado, EsProcesoAnterior;
		END ELSE
		BEGIN
			SELECT RTRIM(Programa) AS Programa, RTRIM(ClaveMaquinaDesp) AS ClaveMaquinaDesp, RTRIM(Turno) AS Turno, ISNULL(EsUtilizado, 0) AS EsUtilizado, 
				SUM(ISNULL(Cantidad, 0)) AS TotalDesperdicio, ISNULL(EsProcesoAnterior, 0) AS EsProcesoAnterior, ISNULL(EsContabilizadoPlc, 0) AS EsContabilizadoPlc
			FROM CmoDat259 A
			Where A.Estatus = 0
				AND OP = @OP
				AND Programa = @Programa
				AND Turno = @Turno
			GROUP BY Programa, ClaveMaquinaDesp, Turno, EsUtilizado, EsProcesoAnterior, EsContabilizadoPlc;
		END

	END
	-- CARGA SUPERVISOR DE PROGRAMA Y TURNO
	ELSE IF @Opcion = 14
	BEGIN
		SELECT ISNULL(B.Supervisor, '') AS Supervisor
		FROM CMODAT020 A
		JOIN CmoDat021 B ON A.Programa = B.Programa
		WHERE A.Programa = @Programa
			AND B.Turno = @Turno
	END
	-- CARGA SUAJES
	ELSE IF @Opcion = 15
	BEGIN
		SELECT RTRIM(CodigoGrabadoSuaje) AS CodigoGrabadoSuaje, RTRIM(Descripcion) AS Descripcion,
			RTRIM(CodigoGrabadoSuaje) + ', ' + RTRIM(Descripcion) AS Compuesto
		FROM CMODAT337
		WHERE CveGraSua LIKE 'S-%'
			AND CveGraSua = @Suaje
		ORDER BY Descripcion
	END
	-- CARGA GRABADOS
	ELSE IF @Opcion = 16
	BEGIN
		SELECT RTRIM(CodigoGrabadoSuaje) AS CodigoGrabadoSuaje, RTRIM(Descripcion) AS Descripcion,
			RTRIM(CodigoGrabadoSuaje) + ', ' + RTRIM(Descripcion) AS Compuesto
		FROM CMODAT337 A
		JOIN CMODAT349 B ON A.CveGraSua = B.CodigoGrabado
		WHERE CveGraSua LIKE 'G-%'
			AND B.CodigoArticulo = @Articulo
	END
	-- MAQUINA PROCESO ANTERIOR
	ELSE IF @Opcion = 17
	BEGIN
		IF ISNULL(@ZonaERP, '') = '02'
		BEGIN
			SELECT RTRIM([Clave Maquina]) AS ClaveMaquina--, RTRIM(Nombre) AS Nombre
            FROM CmoCat011
            WHERE Status = 0 
			ORDER BY [Clave Maquina]
		END ELSE
		BEGIN
			SELECT RTRIM([Clave Maquina]) AS ClaveMaquina--, RTRIM(Nombre) AS Nombre
            FROM CmoCat011
            WHERE Troquela = 1  AND Status = 0 AND [Clave Maquina] <> 'SAT2T' 
			ORDER BY [Clave Maquina]
		END
	END
	-- OBTENER CANTIDAD DE CAJAS REC
	ELSE IF @Opcion = 18
	BEGIN
		SELECT SUM(ISNULL(Cantidad, 0)) AS Cantidad
		FROM CmoDat382
		WHERE OP = @OP
			AND CveMaquinaCap = @ClaveMaquina
			AND Programa = @Programa
	END
	-- DATOS MODAL DESPERDICIO MODULO 2
	ELSE IF @Opcion = 19
	BEGIN
		IF ISNULL(@AplicaCajaRec, 0) = 1
		BEGIN
			IF EXISTS(
				SELECT 1
				FROM CmoCat134 A
				LEFT JOIN CmoDat258 B ON A.IdConcepto = B.IdConcepto
					AND B.ClaveMaquina = @MaquinaDesperdicio
				LEFT JOIN CMODAT382 C ON A.IdConcepto = C.IdConcepto
					AND ISNULL(C.OP, @OP) = @OP
					AND ISNULL(C.Programa, @Programa) = @Programa
					AND ISNULL(C.CveMaquinaCap, @ClaveMaquina) = @ClaveMaquina
					AND ISNULL(C.Turno, @Turno) = @Turno
				WHERE a.Estatus = 0
					AND ISNULL(C.OP, @OP) = @OP
					AND ISNULL(C.Programa, @Programa) = @Programa
					AND ISNULL(C.CveMaquinaCap, @ClaveMaquina) = @ClaveMaquina
					AND ISNULL(C.Turno, @Turno) = @Turno
					AND ISNULL(A.AplicaRecuperacionCaja, @AplicaCajaRec) = @AplicaCajaRec
				--ORDER BY A.Concepto
			)
			BEGIN
				SELECT A.IdConcepto, RTRIM(A.Concepto) AS Concepto, ISNULL(C.Cantidad, 0) As Cantidad, 
					RTRIM(C.OP) AS OP, RTRIM(C.CveMaquinaCap) AS ClaveMaquinaCap, C.Programa, C.Turno
				FROM CmoCat134 A
				LEFT JOIN CmoDat258 B ON A.IdConcepto = B.IdConcepto
					AND B.ClaveMaquina = @MaquinaDesperdicio
				LEFT JOIN CMODAT382 C ON A.IdConcepto = C.IdConcepto
					AND ISNULL(C.OP, @OP) = @OP
					AND ISNULL(C.Programa, @Programa) = @Programa
					AND ISNULL(C.CveMaquinaCap, @ClaveMaquina) = @ClaveMaquina
					AND ISNULL(C.Turno, @Turno) = @Turno
				WHERE a.Estatus = 0
					AND ISNULL(C.OP, @OP) = @OP
					AND ISNULL(C.Programa, @Programa) = @Programa
					AND ISNULL(C.CveMaquinaCap, @ClaveMaquina) = @ClaveMaquina
					AND ISNULL(C.Turno, @Turno) = @Turno
					AND ISNULL(A.AplicaRecuperacionCaja, @AplicaCajaRec) = @AplicaCajaRec
				ORDER BY A.Concepto;
			END ELSE
			BEGIN
				SELECT A.IdConcepto, A.Concepto
				FROM Cajas01..CmoCat134 A
				LEFT JOIN Cajas01..CmoDat258 B ON A.IdConcepto = B.IdConcepto AND B.ClaveMaquina = @ClaveMaquina
				WHERE a.Estatus = 0
			END
		END ELSE
		BEGIN
			IF EXISTS(
				SELECT 1 
				FROM CmoCat134 A
				JOIN CmoDat258 B ON A.IdConcepto = B.IdConcepto
					AND B.ClaveMaquina = @MaquinaDesperdicio
				LEFT JOIN CmoDat259 C ON B.IdConcepto = C.IdConcepto
					AND B.ClaveMaquina = C.ClaveMaquinaDesp
					AND ISNULL(C.OP, @OP) = @OP
					AND ISNULL(C.Programa, @Programa) = @Programa
					AND ISNULL(C.claveMaquinaCap, @ClaveMaquina) = @ClaveMaquina
					AND ISNULL(C.Turno, @Turno) = @Turno
					AND ISNULL(C.EsUtilizado, @EsUtilizado) = @EsUtilizado
					AND ISNULL(C.EsContabilizadoPLC, @EsContabilizadoPLC) = @EsContabilizadoPLC
					AND ISNULL(C.EsProcesoAnterior, @EsProcesoAnterior) = @EsProcesoAnterior
					AND ISNULL(C.ClaveMaquinaDesp, @MaquinaDesperdicio) = @MaquinaDesperdicio
				WHERE A.Estatus = 0
					AND ISNULL(C.OP, @OP) = @OP
					AND ISNULL(C.Programa, @Programa) = @Programa
					AND ISNULL(C.claveMaquinaCap, @ClaveMaquina) = @ClaveMaquina
					AND ISNULL(C.Turno, @Turno) = @Turno
					AND ISNULL(C.EsUtilizado, @EsUtilizado) = @EsUtilizado
					AND ISNULL(C.EsContabilizadoPLC, @EsContabilizadoPLC) = @EsContabilizadoPLC
					AND ISNULL(C.EsProcesoAnterior, @EsProcesoAnterior) = @EsProcesoAnterior
					AND ISNULL(C.ClaveMaquinaDesp, @MaquinaDesperdicio) = @MaquinaDesperdicio
				--ORDER BY A.Concepto
			)
			BEGIN
				SELECT A.IdConcepto, RTRIM(A.Concepto) AS Concepto, ISNULL(C.Cantidad, 0) AS Cantidad, C.EsUtilizado, RTRIM(C.OP) AS OP, 
					RTRIM(C.ClaveMaquinaDesp) AS ClaveMaquinaDesp, RTRIM(C.Programa) AS Programa, RTRIM(C.Turno) AS Turno, 
					RTRIM(C.ClaveMaquinaCap) AS ClaveMaquinaCap, RTRIM(B.ClaveMaquina) AS MaquinaConcepto
				FROM CmoCat134 A
				JOIN CmoDat258 B ON A.IdConcepto = B.IdConcepto
					AND B.ClaveMaquina = @MaquinaDesperdicio
				LEFT JOIN CmoDat259 C ON B.IdConcepto = C.IdConcepto
					AND B.ClaveMaquina = C.ClaveMaquinaDesp
					AND ISNULL(C.OP, @OP) = @OP
					AND ISNULL(C.Programa, @Programa) = @Programa
					AND ISNULL(C.claveMaquinaCap, @ClaveMaquina) = @ClaveMaquina
					AND ISNULL(C.Turno, @Turno) = @Turno
					AND ISNULL(C.EsUtilizado, @EsUtilizado) = @EsUtilizado
					AND ISNULL(C.EsContabilizadoPLC, @EsContabilizadoPLC) = @EsContabilizadoPLC
					AND ISNULL(C.EsProcesoAnterior, @EsProcesoAnterior) = @EsProcesoAnterior
					AND ISNULL(C.ClaveMaquinaDesp, @MaquinaDesperdicio) = @MaquinaDesperdicio
				WHERE A.Estatus = 0
					AND ISNULL(C.OP, @OP) = @OP
					AND ISNULL(C.Programa, @Programa) = @Programa
					AND ISNULL(C.claveMaquinaCap, @ClaveMaquina) = @ClaveMaquina
					AND ISNULL(C.Turno, @Turno) = @Turno
					AND ISNULL(C.EsUtilizado, @EsUtilizado) = @EsUtilizado
					AND ISNULL(C.EsContabilizadoPLC, @EsContabilizadoPLC) = @EsContabilizadoPLC
					AND ISNULL(C.EsProcesoAnterior, @EsProcesoAnterior) = @EsProcesoAnterior
					AND ISNULL(C.ClaveMaquinaDesp, @MaquinaDesperdicio) = @MaquinaDesperdicio
				ORDER BY A.Concepto
			END ELSE
			BEGIN
				SELECT A.IdConcepto, A.Concepto
				FROM Cajas01..CmoCat134 A
				INNER JOIN Cajas01..CmoDat258 B ON A.IdConcepto = B.IdConcepto AND B.ClaveMaquina = @ClaveMaquina
				WHERE a.Estatus = 0
			END
		END
	END
	-- VALIDA DATOS SUPERVISOR AL GUARDAR
	ELSE IF @Opcion = 20
	BEGIN
		SELECT ISNULL(B.Supervisor, '') AS ClaveSupervisor, ISNULL(C.Nombre, '') AS NombreSupervisor,
			B.Turno, A.[Clave Maquina] AS ClaveMaquina, FORMAT(B.Fecha, 'yyyy-MM-dd') AS Fecha, A.Programa
		FROM CMODAT020 A
			INNER JOIN CmoDat021 B ON A.programa = B.programa
			INNER JOIN CmoCat012 C ON B.Supervisor = C.[clave supervisor]
		WHERE A.[Clave Maquina] = @ClaveMaquina
			AND B.Turno = @Turno
			AND B.Fecha = @Fecha
			AND B.Supervisor != @ClaveSupervisor
			AND A.Programa != @Programa
	END
END