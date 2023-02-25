USE [Cajas01]
GO

CREATE TYPE CMODAT382_259TD_001 AS TABLE (
	IdConcepto INT,
	Concepto VARCHAR(200),
	Cantidad INT,
	EsUtilizado BIT,
	OP VARCHAR(10),
	ClaveMaquinaDesp VARCHAR(10),
	Programa INT,
	Turno INT,
	ClaveMaquinaCap VARCHAR(10),
	MaquinaConcepto VARCHAR(100)
)