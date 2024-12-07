USE CatHotel;

GO
-- COMIENZO RESET DATOS

DELETE FROM Reserva_Servicio;
GO

DELETE FROM Reserva;
GO

DELETE FROM Servicio;
GO

DELETE FROM Habitacion;
GO

DELETE FROM Gato;
GO

DELETE FROM Propietario;
GO

-- FIN RESET DATOS


-- COMIENZO PROPIETARIO

INSERT INTO Propietario VALUES ('12345678','JuanCho','099123456', 'juancho@gmail.com'),
							   ('87654321', 'Pedro Gomez','099654321','pedrogomez@gmail.com'),
							   ('91979735', 'Mateo C', '092831273','mateoc@gmail.com'),
							   ('42445789', 'Andres F', '094567667', 'andres_546@gmail.com'),
							   ('45768751', 'Robert I', '092887699', 'robert22@gmail.com'),
							   ('37659347', 'Carlos I', '095787990', 'Carlos3445@gmail.com'),
							   ('24536475', 'Jose G', '095768776', 'Jose_34@gmail.com'),
							   ('42758599', 'Clara V', '094776432', 'ClaraV_43@gmail.com'),
							   ('34568873', 'Micaela V', '096554263', 'micaela234@gmail.com'),
							   ('45462253', 'Ana B', '093546567', 'Ana_B_24@gmail.com');
GO
-- FIN PROPIETARIO


-- COMIENZO GATO

-- https://learn.microsoft.com/en-us/sql/t-sql/database-console-commands/dbcc-checkident-transact-sql
-- reset IDs if re-running script
IF IDENT_CURRENT ('Gato') > 1 BEGIN DBCC CHECKIDENT ('Gato', RESEED, 0) WITH NO_INFOMSGS END
GO

INSERT INTO Gato VALUES ('Pepe', 'Siames', 5, 6.2, '12345678'),
						('Pipo JR', 'Persa', 13, 3.2, '87654321'),
						('Pity', 'Persa', 13, 3.2, '87654321'),
						('Ramon', 'Persa', 14, 4.1, '87654321'),
						('Marco Aurelio', 'Birman', 11, 4.4, '91979735'),
						('Luna', 'Bengali', 3, 4.5, '91979735'),
						('Milo', 'Sphynx', 4, 5.0, '45768751'),
						('Nina', 'Maine Coon', 2, 6.8, '45768751'),
						('Simba', 'Sphynx ', 5, 4.2, '42758599'),
						('Olivia', 'Siames', 1, 3.9, '45462253'),
						('Leo', 'Ragdoll' , 4, 5.3, '34568873'),
						('Nina', 'Sphynx', 2, 3.8, '24536475'),
						('Charlie', 'Ragdoll', 5, 7.2, '34568873'),
						('Loki', 'Abyssinian', 3, 4.1, '24536475'),
						('Malvabizco', 'Siames', 6, 5.5, '42758599'),
						('Oscar', 'Persa', 8, 6.0, '34568873'),
						('Alfonso', 'Ragdoll' , 2, 8.5, '24536475'),
						('Toby', 'Birman', 4, 4.9, '45462253'),
						('Lily', 'Bengali', 3, 4.6, '42758599'),
						('Nano', 'Sphynx', 1, 3.2, '45768751');
GO
-- FIN GATO


-- COMIENZO HABITACION
INSERT INTO Habitacion VALUES   ('Habitacion1', 5, 20.00, 'DISPONIBLE'),
								('Habitacion2', 10, 45.00, 'DISPONIBLE'),
								('Habitacion3', 7, 35.00, 'LIMPIANDO'),
								('Habitacion4', 15, 60.00, 'DISPONIBLE'),
								('Habitacion5', 20, 120.00, 'LIMPIANDO'),
								('Habitacion6', 12, 75.00, 'DISPONIBLE'),
								('Habitacion7', 6, 40.00, 'DISPONIBLE'),
								('Habitacion8', 8, 55.00, 'LIMPIANDO'),
								('Habitacion9', 18, 90.00, 'DISPONIBLE'),
								('Habitacion10', 11, 50.00, 'LIMPIANDO'),
								('Habitacion11', 1, 50.00, 'DISPONIBLE'),
								('Habitacion12', 5, 50.00, 'DISPONIBLE');
GO
-- FIN HABITACION


-- COMIENZO RESERVA

-- reset IDs if re-running script
-- https://learn.microsoft.com/en-us/sql/t-sql/database-console-commands/dbcc-checkident-transact-sql
IF IDENT_CURRENT ('Reserva') > 1 BEGIN DBCC CHECKIDENT ('Reserva', RESEED, 0) WITH NO_INFOMSGS END
GO


INSERT INTO Reserva VALUES	(1,'Habitacion1', '2023-11-01', '2023-11-08', 1000.00),
							(2,'Habitacion1', '2023-11-01', '2024-01-08', 550.00),
							(3,'Habitacion1', '2023-11-05', '2023-11-12', 700.00),
							(4,'Habitacion3', '2023-11-01', '2024-05-08', 550.00),
							(5,'Habitacion3', '2023-12-08', '2023-12-12', 600.00),
							(6,'Habitacion3', '2023-12-08', '2023-12-12', 600.00);
GO

-- RESERVAS DEL ANIO ACTUAL
INSERT INTO Reserva VALUES	(7,'Habitacion4', '2024-11-01','2024-11-12', 750.00),
							(8,'Habitacion4', '2024-10-08', '2024-12-20', 680.00),
							(9,'Habitacion4', '2024-09-08', '2024-12-10', 710.00),
							(10,'Habitacion5', '2024-10-05', '2024-11-07', 800.00),
							(11,'Habitacion5', '2024-11-03', '2024-11-10', 800.00),
							(12,'Habitacion5', '2024-10-05', '2024-12-10', 900.00);
GO

-- FIN RESERVA


-- COMIENZO SERVICIO

INSERT INTO Servicio VALUES	('PASEO', 200.00),
							('PELUQUERIA', 500.00),
							('CONTROL_PARASITOS', 300.00),
							('REVISION_VETERINARIA', 500.00),
							('JUEGO', 100.00),
							('MASAJE', 150.00),
							('ENTRENAMIENTO', 180.00);
GO

-- FIN SERVICIO


-- COMIENZO RESERVA_SERVICIO

INSERT INTO Reserva_Servicio VALUES (1, 'PASEO', 2),
									(1, 'PELUQUERIA', 1),
									(1, 'CONTROL_PARASITOS', 2),
									(1, 'ENTRENAMIENTO', 3);
GO
INSERT INTO Reserva_Servicio VALUES (2, 'PASEO', 2),
									(2, 'CONTROL_PARASITOS', 3),
									(2, 'PELUQUERIA', 1),
									(2, 'ENTRENAMIENTO', 3),
									(2, 'JUEGO', 3);
GO
INSERT INTO Reserva_Servicio VALUES (3, 'PASEO', 1),
									(3, 'JUEGO', 3),
									(3, 'ENTRENAMIENTO', 2),
									(3, 'REVISION_VETERINARIA', 1),
									(3, 'CONTROL_PARASITOS', 1);
GO
INSERT INTO Reserva_Servicio VALUES	(4, 'PASEO', 1),
									(4, 'JUEGO', 1),
									(4, 'REVISION_VETERINARIA', 1),
									(4, 'CONTROL_PARASITOS', 1);
GO
INSERT INTO Reserva_Servicio VALUES (5, 'PASEO', 1),
									(5, 'JUEGO', 1),
									(5, 'CONTROL_PARASITOS', 1);
GO

INSERT INTO Reserva_Servicio VALUES	(6,'PASEO', 2),
									(6,'PELUQUERIA', 1),
									(6,'CONTROL_PARASITOS', 3),
									(6,'REVISION_VETERINARIA', 2),
									(6,'JUEGO', 3),
									(6,'MASAJE', 1),
									(6,'ENTRENAMIENTO', 2);
GO

-- RESERVAS DEL ANIO EN CURSO

INSERT INTO Reserva_Servicio VALUES (7, 'PASEO', 1),
									(7, 'JUEGO', 1),
									(7, 'REVISION_VETERINARIA', 1),
									(7, 'CONTROL_PARASITOS', 1);

INSERT INTO Reserva_Servicio VALUES (8, 'PASEO', 1),
									(8, 'JUEGO', 1),
									(8, 'CONTROL_PARASITOS', 1);

INSERT INTO Reserva_Servicio VALUES (9, 'JUEGO', 2),
									(9, 'ENTRENAMIENTO', 2),
									(9, 'REVISION_VETERINARIA', 1);

INSERT INTO Reserva_Servicio VALUES (10, 'PASEO', 1),
									(10, 'JUEGO', 1),
									(10, 'CONTROL_PARASITOS', 1);

INSERT INTO Reserva_Servicio VALUES (11, 'PASEO', 1),
									(11, 'JUEGO', 1),
									(11, 'CONTROL_PARASITOS', 1);

INSERT INTO Reserva_Servicio VALUES	 (8, 'PELUQUERIA', 1),
									 (9, 'PELUQUERIA', 1),
									 (10, 'PELUQUERIA', 1),
									 (11, 'PELUQUERIA', 1),
									 (12, 'PELUQUERIA', 1);
GO
-- FIN RESERVA_SERVICIO
