USE [Cajas01]
GO
/****** Object:  StoredProcedure [dbo].[FCAPROG019MWSPA2]    Script Date: 15/02/2023 02:35:32 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[FCAPROG019MWSPA2]
	@Opcion INT						= NULL
	, @Fecha VARCHAR(10)			= NULL
	, @HoraIni VARCHAR(5)			= NULL
	, @HoraFin VARCHAR(5)			= NULL
	, @Turno INT					= NULL
	, @Supervisor VARCHAR(10)		= NULL
	, @Minutos INT					= NULL
	, @DespCorrugadora INT			= NULL
	, @DespImpresora INT			= NULL
	, @DespAcabados INT				= NULL
	, @FechaNow VARCHAR(15)			= NULL
	, @UsuarioERP VARCHAR(6)		= NULL
	, @Parafina VARCHAR(10)			= NULL
	, @PesoLamina INT				= NULL
	, @PesoCaja INT					= NULL
	, @Retrabajo INT				= NULL
	, @ActCantidad INT				= NULL
	, @IdTripulacion INT			= NULL

	, @Programa INT					= NULL
	, @ClaveMaquina VARCHAR(10)		= NULL
	, @wFechaAnterior VARCHAR(10)	= NULL
	, @IdUnico INT					= NULL
AS BEGIN
	-- GUARDAR INFORMACIÓN
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
END