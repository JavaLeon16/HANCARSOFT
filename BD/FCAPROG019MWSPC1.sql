USE [Cajas01]
GO

ALTER PROCEDURE FCAPROG019MWSPC1
	@Opcion INT						= NULL
	, @Programa INT					= NULL
	, @ClaveMaquina VARCHAR(10)		= NULL
	, @wFechaAnterior VARCHAR(10)	= NULL
	, @Turno INT					= NULL
	, @Fecha VARCHAR(10)			= NULL

	, @UsuarioERP VARCHAR(6)		= NULL
	, @ZonaERP VARCHAR(2)			= NULL
AS BEGIN
	-- INICIA PRODUCCIÓN
	IF @Opcion = 1
	BEGIN
		DECLARE @wFecha DATETIME
		SELECT @wFecha = Informatica.[dbo].[MnuFun002](1, @UsuarioERP, @ZonaERP, GETDATE());
		SELECT FORMAT(@wFecha, 'yyyy-MM-dd HH:mm') AS Fecha;
	END
	-- BUSCAR PROGRAMA
	ELSE IF @Opcion = 2
	BEGIN
		SELECT A.[Clave Maquina] AS ClaveMaquina, A.Op, A.[Clave Proceso] AS ClaveProceso, A.[Piezas Corte] AS PiezasCorte, A.[Tipo Maquina] AS TipoMaquina,
			A.Cantidad, A.[Ultimo Proceso] AS UltimoProceso, A.[Pegado], A.[1er Color] AS PrimerColor, A.[2do Color] AS SegundoColor, A.[3er Color] AS TercerColor,
			A.[4to Color] AS CuartoColor, C.[Area unitaria] AS AreaUnitaria, C.[Peso Unitario] AS PesoUnitario, C.[Clave Articulo] AS ClaveArticulo,
			D.Descripcion AS Articulo, C.[Liberado Costos] AS LiberadoCostos
		FROM CmoDat020 A 
		JOIN CmoDat011 C ON A.op=C.op
		JOIN CmoTjCat004 D ON D.[clave articulo]= C.[clave articulo]
		WHERE A.programa = @Programa
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
END
