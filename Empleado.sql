CREATE DATABASE JSanchezEmpleado
USE JSanchezEmpleado;

CREATE TABLE CatEntidadFederativa(
	IdCatEntidadFederativa INT IDENTITY(1,1) PRIMARY KEY,
	Estado VARCHAR(100)
);
GO

CREATE TABLE Empleado(
	IdEmpleado INT IDENTITY(1,1) PRIMARY KEY,
	NumeroNomina VARCHAR(10),
	Nombre VARCHAR(100),
	ApellidoPaterno VARCHAR(100),
	ApellidoMaterno VARCHAR(100),
	IdEstado INT,
	CONSTRAINT fk_EmpleadoEstado FOREIGN KEY (IdEstado) REFERENCES CatEntidadFederativa (IdCatEntidadFederativa)
);
GO

CREATE PROCEDURE EstadoAdd --'Puebla'
@Estado VARCHAR(100)
AS
INSERT INTO CatEntidadFederativa(Estado)VALUES(@Estado)
GO

ALTER PROCEDURE EmpleadoAdd --'Ulices','Esteban','Vargas',2
--@NumeroNomina VARCHAR(10),
@Nombre VARCHAR(100),
@ApellidoPaterno VARCHAR(100),
@ApellidoMaterno VARCHAR(100),
@IdEstado INT
AS
INSERT INTO Empleado(
					 --NumeroNomina,
					 Nombre,
					 ApellidoPaterno,
					 ApellidoMaterno,
					 IdEstado) VALUES(
									  --@NumeroNomina,
									  @Nombre,
									  @ApellidoPaterno,
									  @ApellidoMaterno,
									  @IdEstado)
GO

CREATE PROCEDURE EmpleadoUpdate --5,'Ulices','Esteban','Vargas',3
@IdEmpleado INT,
--@NumeroNomina VARCHAR(10),
@Nombre VARCHAR(100),
@ApellidoPaterno VARCHAR(100),
@ApellidoMaterno VARCHAR(100),
@IdEstado INT
AS
UPDATE Empleado 
SET
	NumeroNomina = CONVERT(VARCHAR,@IdEstado) + CONVERT(VARCHAR,@IdEmpleado),
	Nombre = @Nombre,
	ApellidoPaterno = @ApellidoPaterno,
	ApellidoMaterno = @ApellidoMaterno,
	IdEstado = @IdEstado
	WHERE IdEmpleado = @IdEmpleado
GO

CREATE PROCEDURE EmpleadoDelete
@IdEmpleado INT
AS
DELETE FROM Empleado WHERE IdEmpleado = @IdEmpleado
GO

ALTER PROCEDURE EmpleadoGetAll
AS
SELECT 
	  IdEmpleado,
	  NumeroNomina,
	  Nombre,
	  ApellidoPaterno,
	  ApellidoMaterno,
	  Empleado.IdEstado,
	  CatEntidadFederativa.Estado AS Estado
	  FROM Empleado
	  INNER JOIN CatEntidadFederativa ON Empleado.IdEstado = CatEntidadFederativa.IdCatEntidadFederativa
GO

ALTER PROCEDURE EmpleadoGetById --1
@IdEmpleado INT
AS
SELECT 
	  IdEmpleado,
	  NumeroNomina,
	  Nombre,
	  ApellidoPaterno,
	  ApellidoMaterno,
	  Empleado.IdEstado,
	  CatEntidadFederativa.Estado AS Estado
	  FROM Empleado
	  INNER JOIN CatEntidadFederativa ON Empleado.IdEstado = CatEntidadFederativa.IdCatEntidadFederativa
	  WHERE IdEmpleado = @IdEmpleado
GO

CREATE PROCEDURE CatEntidadFederativaGetAll
AS
SELECT IdCatEntidadFederativa,Estado FROM CatEntidadFederativa
GO

--Actualizar automaticamente el NumeroNomina con IdEstado y IdEmpleado 
CREATE TRIGGER NumeroNominaEmpleado
ON Empleado
AFTER INSERT
AS
BEGIN
	DECLARE @NumeroNominaEmpleado VARCHAR(10)
	DECLARE @IdEmpleado INT

	SET @NumeroNominaEmpleado =(SELECT CONVERT(VARCHAR,IdEstado) + CONVERT(VARCHAR,IdEmpleado) FROM inserted )
	SET @IdEmpleado = (SELECT IdEmpleado FROM inserted)
	UPDATE Empleado
	SET NumeroNomina = @NumeroNominaEmpleado
	WHERE IdEmpleado = @IdEmpleado
END
GO