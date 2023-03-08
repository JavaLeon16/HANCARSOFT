USE [Cajas01]
GO
/****** Object:  StoredProcedure [dbo].[FCAPROG019MWSPA2]    Script Date: 15/02/2023 02:35:32 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[FCAPROG019MWSPA2]
	@Opcion INT									= NULL
	, @Fecha VARCHAR(10)						= NULL
	, @HoraIni VARCHAR(5)						= NULL
	, @HoraFin VARCHAR(5)						= NULL
	, @Turno INT								= NULL
	, @Supervisor VARCHAR(10)					= NULL
	, @Minutos INT								= NULL
	, @DespCorrugadora INT						= NULL
	, @DespImpresora INT						= NULL
	, @DespAcabados INT							= NULL
	, @FechaNow VARCHAR(15)						= NULL
	, @UsuarioERP VARCHAR(6)					= NULL
	, @ZonaERP VARCHAR(2)						= NULL
	, @Parafina VARCHAR(10)						= NULL
	, @PesoLamina INT							= NULL
	, @PesoCaja INT								= NULL
	, @Retrabajo INT							= NULL
	, @ActCantidad INT							= NULL
	, @IdTripulacion INT						= NULL
	
	, @Programa INT								= NULL
	, @ClaveMaquina VARCHAR(10)					= NULL
	, @wFechaAnterior VARCHAR(10)				= NULL
	, @IdUnico INT								= NULL
	, @CMODAT021TD_001 CMODAT021TD_001			READONLY

	, @AplicaCajaRec BIT						= NULL
	, @ClaveMaquinaCap VARCHAR(10)				= NULL
	, @ClaveMaquinaDesp VARCHAR(10)				= NULL
	, @OP VARCHAR(10)							= NULL
	, @EsUtilizado BIT							= NULL
	, @EsProcesoAnterior BIT					= NULL
	, @EsContabilizadoPLC BIT					= NULL
	, @IdTipoDesp INT							= NULL
	, @CMODAT382_259TD_001 CMODAT382_259TD_001	READONLY

	, @IgnoraTiempo BIT							= NULL
    --, @IdUnico INT								= NULL
    --, @wFechaAnterior VARCHAR(10)				= NULL
    , @ChkProceso BIT							= NULL
    , @LSupervisor VARCHAR(20)					= NULL
    , @LSuaje VARCHAR(20)						= NULL
    , @LGrabado VARCHAR(20)						= NULL
    , @LMinutos INT								= NULL
    , @LProceso VARCHAR(20)						= NULL
    , @LVelocidad INT							= NULL
    , @LEficiencia INT							= NULL
    , @LMinStd INT								= NULL
    --, @Programa INT								= NULL
    , @MinStdProd INT							= NULL
    , @TxtDespCorrUtil INT						= NULL
    , @TxtDespImprUtil INT						= NULL
    , @TxtPesoLamina DECIMAL(18,2)				= NULL
    , @TxtPesoCaja DECIMAL(18,2)				= NULL
    , @TxtRetrabajo DECIMAL(18,2)				= NULL
    , @CmbMaquinaPA VARCHAR(10)					= NULL
    , @TxtDesPAUtul INT							= NULL
    , @Cant1 INT								= NULL
    , @Cant2 INT								= NULL
    , @Cant3 INT								= NULL
    , @Cant4 INT								= NULL
    , @TxtCantidad INT							= NULL
    , @TxtCantidadCajasRec INT					= NULL
    , @FecProduccion VARCHAR(10)				= NULL
    --, @HoraIni VARCHAR(5)						= NULL
    --, @HoraFin VARCHAR(5)						= NULL
    --, @Turno INT								= NULL
    --, @IdTripulacion INT						= NULL
    , @CmbMaquina VARCHAR(10)					= NULL
	, @ChkMP BIT								= NULL
AS BEGIN
	DECLARE @HoraLocal VARCHAR(16)
	DECLARE @Tmp AS TABLE (Fecha VARCHAR(16));
	-- GUARDAR INFORMACIÓN MÓDULO 3
	IF @Opcion = 1 
	BEGIN
		SELECT @ActCantidad = CASE WHEN ISNULL(@ActCantidad, 0) = 0 THEN NULL ELSE @ActCantidad END
		BEGIN TRANSACTION
		BEGIN TRY
			UPDATE CmoDat021 
				SET Fecha = @Fecha,
					[Hora Inicio] = @HoraIni,
					[Hora Termino] = @HoraFin,
					Turno = @Turno,
					Supervisor = @Supervisor,
					[Minutos Produccion] = @Minutos,
					LAMINADESPEG = @DespCorrugadora,
					LAMINACOMBA = 0,
					LAMINAMEDIDAS = 0,
					LAMINAIMPRES = @DespImpresora,
					LAMINADIMENS = 0,
					LAMINAPEGAD = 0,
					[Desperdicio Acabados] = @DespAcabados,
					[Fecha Sistema] = CAST(@FechaNow AS smalldatetime),
					Usuario = @UsuarioERP,
					[Tipo Parafina] = @Parafina,
					PesoLamina = @PesoLamina,
					PesoCaja = @PesoCaja,
					Retrabajo = @Retrabajo,
					Cantidad = @ActCantidad,
					IdTripulacion = @IdTripulacion
			WHERE Programa = @Programa AND [Clave Maquina] = @ClaveMaquina
				AND Turno = @Turno AND ISNULL(Fecha, '') = ISNULL(@wFechaAnterior, '')
				AND IdUnico = @IdUnico;
			COMMIT TRANSACTION;
		END TRY
		BEGIN CATCH
			SELECT ERROR_MESSAGE()
			ROLLBACK TRANSACTION;
		END CATCH
	END
	-- ACTUALIZAR PAGINA L
	ELSE IF @Opcion = 2
	BEGIN
		BEGIN TRANSACTION
		BEGIN TRY
			UPDATE CMODAT021
				SET Fecha = A.Fecha,
					Supervisor = A.Supervisor,
					IdTripulacion = A.IdTripulacion
			FROM @CMODAT021TD_001 A
			JOIN CMODAT021 B ON A.IdUnico = B.IdUnico;

			SELECT 'OK';
			COMMIT TRANSACTION;
		END TRY
		BEGIN CATCH
			SELECT ERROR_MESSAGE()
			ROLLBACK TRANSACTION;
		END CATCH
	END
	-- GUARDAR MODAL DESPERDICIOS MODULO 2
	ELSE IF @Opcion = 3
	BEGIN
		BEGIN TRANSACTION
		BEGIN TRY
			DECLARE @TmpReg AS TABLE (
				ID INT IDENTITY(1,1), IdConcepto INT, Concepto VARCHAR(200), Cantidad INT, EsUtilizado BIT, OP VARCHAR(10),
				ClaveMaquinaDesp VARCHAR(10), Programa INT, Turno INT, ClaveMaquinaCap VARCHAR(10), MaquinaConcepto VARCHAR(100)
			);
			DECLARE @ID INT, @Cantidad INT, @IdConcepto INT;

			INSERT INTO @TmpReg (IdConcepto, Concepto, Cantidad, EsUtilizado, OP, ClaveMaquinaDesp, Programa, Turno, ClaveMaquinaCap, MaquinaConcepto)
			SELECT IdConcepto, Concepto, Cantidad, EsUtilizado, OP, ClaveMaquinaDesp, Programa, Turno, ClaveMaquinaCap, MaquinaConcepto
			FROM @CMODAT382_259TD_001
			WHERE ISNULL(Concepto, '') != '';

			WHILE EXISTS(SELECT 1 FROM @TmpReg)
			BEGIN
				SELECT TOP 1 @ID = ID, @Cantidad = ISNULL(Cantidad, 0), @IdConcepto = IdConcepto
				FROM @TmpReg;

				SELECT @Cantidad = ISNULL(@Cantidad, 0);

				IF ISNULL(@AplicaCajaRec, 0) = 1
				BEGIN
					UPDATE CMODAT382 SET 
						Cantidad = ISNULL(@Cantidad, 0),
						Estatus = 0,
						FechaUpdate = GETDATE(),
						CveMaquinaCap = @ClaveMaquinaCap,
						UsuarioUpdate = @UsuarioERP
					WHERE IdConcepto = @IdConcepto AND CveMaquinaCap = @ClavemaquinaDesp AND Programa = @Programa 
						AND Turno = @Turno AND CveMaquinaCap = @ClaveMaquinaCap;

					IF @@ROWCOUNT = 0 AND ISNULL(@Cantidad, 0) > 0
					BEGIN
						INSERT INTO CMODAT382 ([OP],[Programa],[CveMaquinaCap],[Turno],[IdConcepto],[Cantidad],UsuarioInsert,FechaInsert,UsuarioUpdate,FechaUpdate,Estatus)
						VALUES (@OP, @Programa, @ClaveMaquinaDesp, @Turno, @IdConcepto, @Cantidad, @UsuarioERP, GETDATE(), NULL, NULL, 0);
					END
				END ELSE
				BEGIN
					UPDATE CmoDat259 SET 
						Cantidad = ISNULL(@Cantidad, 0),
						Estatus = 0,
						Fecha = GETDATE(),
						EsUtilizado = @EsUtilizado,
						EsProcesoAnterior = @EsProcesoAnterior,
						ClaveMaquinaCap = @ClaveMaquinaCap,
						EsContabilizadoPLC = @EsContabilizadoPLC,
						IdTipoDesp = @IdTipoDesp
					WHERE IdConcepto = @IdConcepto AND ClaveMaquinaDesp = @ClaveMaquinaDesp AND Programa = @Programa
						AND Turno = @Turno AND EsUtilizado = @EsUtilizado AND EsProcesoAnterior = @EsProcesoAnterior
						AND ClaveMaquinaCap = @ClaveMaquinaCap AND EsContabilizadoPLC = @EsContabilizadoPLC AND IdTipoDesp = @IdTipoDesp;

					IF @@ROWCOUNT = 0 AND ISNULL(@Cantidad, 0) > 0
					BEGIN
						INSERT INTO CmoDat259 ([OP],[Programa],[ClaveMaquinaDesp],[IdConcepto],[Cantidad],[Turno],[EsUtilizado],[EsProcesoAnterior],[ClaveMaquinaCap],[EsContabilizadoPLC],[IdTipoDesp])
						VALUES (@OP, @Programa, @ClaveMaquinaDesp, @IdConcepto, @Cantidad, @Turno, @EsUtilizado, @EsProcesoAnterior, @ClaveMaquinaCap, @EsContabilizadoPLC, @IdTipoDesp);
					END
				END

				DELETE FROM @TmpReg WHERE ID = @ID;
			END

			COMMIT TRANSACTION
		END TRY
		BEGIN CATCH
			ROLLBACK TRANSACTION;
		END CATCH
	END
	-- GUARDAR MODULO 2
	ELSE IF @Opcion = 4
	BEGIN
		DECLARE @lAfectados INT;
		BEGIN TRANSACTION
		BEGIN TRY
			INSERT INTO @Tmp (Fecha)
			EXEC dbo.FCAPROG019MWSPC1 @UsuarioERP = @UsuarioERP, @ZonaERP = @ZonaERP, @FechaSinGuion = 1

			SELECT TOP 1 @HoraLocal = ISNULL(Fecha, FORMAT(GETDATE(), 'yyyyMMdd HH:mm')) FROM @Tmp;

			IF ISNULL(@ChkMP, 0) = 1
			BEGIN
				INSERT INTO CmoDat023 (
					Programa, [Clave Maquina], [Fecha Programa], [Hora Programa], OP, Cantidad, 
					[Minutos Produccion], [Clave Proceso], [Ultimo Proceso], Usuario, [Fecha Sistema]
				)
				SELECT Programa, [Clave Maquina], [Fecha Programa], [Hora Programa], OP, Cantidad,
					[Minutos Produccion], [Clave Proceso], [Ultimo Proceso], @UsuarioERP, @HoraLocal
				FROM CmoDat020
				WHERE Programa = @Programa;

				EXEC dbo.CMOSP762 @Opcion = 1, @IdRegistro = @Programa, @Usuario = @UsuarioERP

				UPDATE CmoDat020
					SET [Clave Proceso] = @LProceso,
						Suaje = @LSuaje,
						[Minutos Preparacion] = @LMinStd,
						[Minutos Produccion] = @MinStdProd,
						[Velocidad Std] = @LVelocidad,
						Eficiencia = @LEficiencia
				WHERE Programa = @Programa;
			END

			IF ISNULL(@wFechaAnterior, '') != ''
			BEGIN
				EXEC dbo.CMOSP762 @Opcion = 2, @IdRegistro = @IdUnico, @Usuario = @UsuarioERP;

				UPDATE CmoDat021
					SET SinPreparacion = @IgnoraTiempo,
						Fecha = @FecProduccion,
						[Hora Inicio] = @HoraIni,
						[Hora Termino] = @HoraFin,
						Turno = @Turno,
						Supervisor = @LSupervisor,
						[Minutos Produccion] = @LMinutos,
						LAMINADESPEG = @txtDespCorrUtil,
						LAMINACOMBA = 0,
						LAMINAMEDIDAS = 0,
						LAMINAIMPRES = @txtDespImprUtil,
						LAMINADIMENS = 0,
						LAMINAPEGAD = 0,
						[Fecha Sistema] = @HoraLocal,
						Usuario = @UsuarioERP,
						CanTinta1 = @Cant1,
						CanTinta2 = @Cant2,
						CanTinta3 = @Cant3,
						CanTinta4 = @Cant4,
						Cantidad = @txtCantidad,
						PesoLamina = @txtPesoLamina,
						PesoCaja = @txtPesoCaja,
						Retrabajo = @txtRetrabajo,
						IdTripulacion = @IdTripulacion, --& wGuardaPA & WCajasRec
						MaqPA = CASE WHEN ISNULL(@CmbMaquinaPA, '') != '' THEN ISNULL(@CmbMaquinaPA, '') ELSE MaqPA END, 
						DespPA = CASE WHEN ISNULL(@CmbMaquinaPA, '') != '' THEN ISNULL(@TxtDesPAUtul, '') ELSE DespPA END,
						CajasRecEnProd = CASE WHEN ISNULL(@TxtCantidadCajasRec, 0) != 0 THEN ISNULL(@TxtCantidadCajasRec, 0) ELSE CajasRecEnProd END
				WHERE Programa = @Programa
					AND [Clave Maquina] = @ClaveMaquina
					AND Turno = @Turno
					AND Fecha = @FecProduccion
					AND IdUnico = @IdUnico;

				SELECT @lAfectados = @@ROWCOUNT;
				IF ISNULL(@lAfectados, 0) > 1
				BEGIN
					ROLLBACK TRANSACTION;
					-- Se identifico un error . Favor de reportarlo a mesadeservicio
					GOTO SALIR;
				END
				IF ISNULL(@lAfectados, 0) > 0
				BEGIN
					EXEC dbo.CMOSP762 @Opcion = 2, @IdRegistro = @IdUnico, @Usuario = @UsuarioERP;

					UPDATE CmoDat021
						SET Cantidad = 0,
							[Hora Inicio] = '00:00',
							[Hora Termino] = '00:00',
							[Minutos Produccion] = 0,  --& wGuardaPA & WCajasRec
							MaqPA = CASE WHEN ISNULL(@CmbMaquinaPA, '') != '' THEN ISNULL(@CmbMaquinaPA, '') ELSE MaqPA END, 
							DespPA = CASE WHEN ISNULL(@CmbMaquinaPA, '') != '' THEN ISNULL(@TxtDesPAUtul, '') ELSE DespPA END,
							CajasRecEnProd = CASE WHEN ISNULL(@TxtCantidadCajasRec, 0) != 0 THEN ISNULL(@TxtCantidadCajasRec, 0) ELSE CajasRecEnProd END
					WHERE Programa = @Programa
						AND [Clave Maquina] = @CmbMaquina
						AND Turno = @Turno
						AND Fecha IS NULL
						AND IdUnico = @IdUnico;

					SELECT @lAfectados = @@ROWCOUNT;
					IF ISNULL(@lAfectados, 0) > 1
					BEGIN
						ROLLBACK TRANSACTION;
						-- Se identifico un error . Favor de reportarlo a mesadeservicio
						GOTO SALIR;
					END
				END
			END ELSE
			BEGIN
				SELECT @lAfectados = 0;
			END

			IF ISNULL(@lAfectados, 0) = 0
			BEGIN
				EXEC dbo.CMOSP762 @Opcion = 2, @IdRegistro = @IdUnico, @Usuario = @UsuarioERP;
				UPDATE CmoDat021
					SET SinPreparacion = @IgnoraTiempo,
						Fecha = @FecProduccion,
						[Hora Inicio] = @HoraIni,
						[Hora Termino] = @HoraFin,
						Turno = @Turno,
						Supervisor = @LSupervisor,
						[Minutos Produccion] = @LMinutos,
						LAMINADESPEG = @txtDespCorrUtil,
						LAMINACOMBA = 0,
						LAMINAMEDIDAS = 0,
						LAMINAIMPRES = @txtDespImprUtil,
						LAMINADIMENS = 0,
						LAMINAPEGAD = 0,
						[Fecha Sistema] = @HoraLocal,
						Usuario = @UsuarioERP,
						CanTinta1 = @cant1,
						CanTinta2 = @cant2,
						CanTinta3 = @cant3,
						CanTinta4 = @cant4, -- & lActCantidad,
						Cantidad = CASE WHEN ISNULL(@ChkProceso, 0) = 0 THEN ISNULL(@TxtCantidad, 0) ELSE Cantidad END,
						PesoLamina = @txtPesoLamina,
						PesoCaja = @txtPesoCaja,
						Retrabajo = @txtRetrabajo,
						IdTripulacion = @IdTripulacion, --& wGuardaPA & WCajasRec
						MaqPA = CASE WHEN ISNULL(@CmbMaquinaPA, '') != '' THEN ISNULL(@CmbMaquinaPA, '') ELSE MaqPA END, 
						DespPA = CASE WHEN ISNULL(@CmbMaquinaPA, '') != '' THEN ISNULL(@TxtDesPAUtul, '') ELSE DespPA END,
						CajasRecEnProd = CASE WHEN ISNULL(@TxtCantidadCajasRec, 0) != 0 THEN ISNULL(@TxtCantidadCajasRec, 0) ELSE CajasRecEnProd END
					WHERE IdUnico = @IdUnico
						AND Programa = @Programa
						AND [Clave Maquina] = @CmbMaquina
						AND Turno = @Turno --& lActFecha
						AND Fecha = CASE WHEN ISNULL(@wFechaAnterior, '') != '' THEN @wFechaAnterior ELSE Fecha END;

				SELECT @lAfectados = @@ROWCOUNT;
				IF ISNULL(@lAfectados, 0) > 1
				BEGIN
					ROLLBACK TRANSACTION;
					-- Se identifico un error . Favor de reportarlo a mesadeservicio
					GOTO SALIR;
				END
			END

			DECLARE @lDesperdicio INT = 0, @lCantidad INT = 0, @lGolpes INT = 0, @lModulo VARCHAR(10) = 'CMOCAP032';

			;WITH tblDesp AS (
				SELECT SUM(
					ISNULL(LAMINADESPEG, 0) + ISNULL(LAMINACOMBA, 0) + ISNULL(LAMINAMEDIDAS, 0) + 
					ISNULL(LAMINAIMPRES, 0) + ISNULL(LAMINADIMENS, 0) + ISNULL(LAMINAPEGAD, 0)
				) AS Desperdicio
				FROM CmoDat021 
				WHERE Programa = @Programa
			)
			SELECT TOP 1 @lDesperdicio = ISNULL(Desperdicio, 0) FROM tblDesp;

			;WITH tblCant AS (
				SELECT SUM(ISNULL(Cantidad, 0)) AS Cantidad
				FROM CmoDat020 
				WHERE Programa = @Programa
			)
			SELECT TOP 1 @lCantidad = ISNULL(Cantidad, 0) FROM tblCant;

			SELECT @lGolpes = ISNULL(@lDesperdicio, 0) + ISNULL(@lCantidad, 0);

			IF EXISTS(SELECT 1 FROM CmoDat358 WHERE Programa = @Programa)
			BEGIN
				UPDATE CmoDat358
					SET Turno = @Turno,
						Supervisor = @LSupervisor,
						CodigoSuaje = @LSuaje,
						CodigoGrabado = @LGrabado,
						GolpesXCorrida = @lGolpes,
						ModuloUpdate = @lModulo,
						FechaUpdate = @HoraLocal,
						IdUsuarioUpdate = @UsuarioERP
				WHERE Programa = @Programa;
			END ELSE
			BEGIN
				INSERT CmoDat358 (
					Maquina, Programa, OP, Turno, Supervisor, Tripulacion, CodigoSuaje, CodigoGrabado, MinProduccion, Produccion, 
					Desperdicio, GolpesXCorrida, ModuloInsert, IdUsuarioInsert, FechaInsert, ModuloUpdate, FechaUpdate, IdUsuarioUpdate
				)
				SELECT [Clave Maquina], Programa, OP, @Turno, @LSupervisor, @IdTripulacion, @LSuaje, @LGrabado, [Minutos Produccion], Cantidad,
					@lDesperdicio, @lGolpes, @lModulo, @UsuarioERP, @HoraLocal, @lModulo, @HoraLocal, @UsuarioERP
				FROM CmoDat020
				WHERE Programa = @Programa
			END

			COMMIT TRANSACTION

		END TRY
		BEGIN CATCH
			ROLLBACK TRANSACTION;
		END CATCH
	END
SALIR:
END