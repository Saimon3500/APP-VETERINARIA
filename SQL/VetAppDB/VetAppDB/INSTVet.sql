INSERT INTO Personas (ci, nombre, telefono, correo, direccion)
VALUES
('1234567890', 'Juan Perez', '12345678', 'juan.perez@example.com', 'Av. Principal 123'),
('2345678901', 'Maria Gomez', '87654321', 'maria.gomez@example.com', 'Calle Secundaria 456'),
('3456789012', 'Carlos Lopez', '56789012', 'carlos.lopez@example.com', 'Av. Tercera 789'),
('4567890123', 'Ana Torres', '78901234', 'ana.torres@example.com', 'Calle Cuarta 101'),
('5678901234', 'Luis Fernandez', '90123456', 'luis.fernandez@example.com', 'Av. Quinta 112');

INSERT INTO Clientes (codCliente, apellido, cuentaBanco, banco, direccion, telefono, correo)
VALUES
('C001', 'Perez', '1111222233334444', 'Banco A', 'Av. Principal 123', '12345678', 'juan.perez@example.com'),
('C002', 'Gomez', '5555666677778888', 'Banco B', 'Calle Secundaria 456', '87654321', 'maria.gomez@example.com'),
('C003', 'Lopez', '9999000011112222', 'Banco C', 'Av. Tercera 789', '56789012', 'carlos.lopez@example.com'),
('C004', 'Torres', '3333444455556666', 'Banco D', 'Calle Cuarta 101', '78901234', 'ana.torres@example.com'),
('C005', 'Fernandez', '7777888899990000', 'Banco E', 'Av. Quinta 112', '90123456', 'luis.fernandez@example.com');

INSERT INTO Vacunas (codVacuna, nombre, laboratorio, prevEnfermedad, dosis, precioUnitario)
VALUES
('V001', 'Rabia', 'Lab A', 'Rabia', 1.5, 100.00),
('V002', 'Parvovirus', 'Lab B', 'Parvovirus', 1.0, 120.00),
('V003', 'Moquillo', 'Lab C', 'Moquillo', 1.2, 110.00),
('V004', 'Hepatitis', 'Lab D', 'Hepatitis', 1.3, 130.00),
('V005', 'Leptospirosis', 'Lab E', 'Leptospirosis', 1.1, 140.00);

INSERT INTO Servicios (idServicio, nombre, descripcion, precio, unidadMedida, tipoServicio)
VALUES
('S001', 'Consulta General', 'Consulta médica general', 50.00, 'Unidad', 'Médico'),
('S002', 'Baño', 'Baño para mascotas', 30.00, 'Unidad', 'Estética'),
('S003', 'Corte de Pelo', 'Corte de pelo para mascotas', 40.00, 'Unidad', 'Estética'),
('S004', 'Vacunación', 'Aplicación de vacunas', 70.00, 'Unidad', 'Médico'),
('S005', 'Desparasitación', 'Tratamiento contra parásitos', 60.00, 'Unidad', 'Médico');

INSERT INTO Hospedajes (fechaIngreso, fechaSalida, observaciones)
VALUES
('2023-01-01', '2023-01-10', 'Ninguna'),
('2023-01-05', '2023-01-15', 'Ninguna'),
('2023-02-01', '2023-02-10', 'Ninguna'),
('2023-02-05', '2023-02-15', 'Ninguna'),
('2023-03-01', '2023-03-10', 'Ninguna');

INSERT INTO Alimentos (codAlimento, nombre, descripcion, proveedor, precioUnitario)
VALUES
('A001', 'Dog Chow', 'Alimento para perros', 'Proveedor A', 25.00),
('A002', 'Cat Chow', 'Alimento para gatos', 'Proveedor B', 30.00),
('A003', 'Bird Seed', 'Alimento para aves', 'Proveedor C', 15.00),
('A004', 'Fish Food', 'Alimento para peces', 'Proveedor D', 20.00),
('A005', 'Rabbit Food', 'Alimento para conejos', 'Proveedor E', 18.00);

INSERT INTO Comodidades (idComodidad, nombre, descripcion, precioUnitario)
VALUES
('C001', 'Cama para perros', 'Cama cómoda para perros', 50.00),
('C002', 'Rascador para gatos', 'Rascador para gatos', 60.00),
('C003', 'Jaula para aves', 'Jaula amplia para aves', 70.00),
('C004', 'Acuario', 'Acuario con accesorios', 100.00),
('C005', 'Caseta para perros', 'Caseta resistente para perros', 80.00);

INSERT INTO Medicamentos (codMedicamento, laboratorio, presentacion, pesoNeto, precioUnitario, nombre)
VALUES
('M001', 'Lab A', 'Tabletas', 0.5, 20.00, 'Antibiótico A'),
('M002', 'Lab B', 'Jarabe', 1.0, 25.00, 'Antibiótico B'),
('M003', 'Lab C', 'Inyección', 0.3, 30.00, 'Antibiótico C'),
('M004', 'Lab D', 'Tabletas', 0.4, 22.00, 'Antiinflamatorio A'),
('M005', 'Lab E', 'Jarabe', 1.2, 28.00, 'Antiinflamatorio B');

INSERT INTO Mascotas (codMascota, codCliente, nombre, especie, raza, color, fechaNac)
VALUES
('M001', 'C001', 'Fido', 'Perro', 'Labrador', 'Negro', '2019-01-01'),
('M002', 'C002', 'Mia', 'Gato', 'Siames', 'Blanco', '2020-05-05'),
('M003', 'C003', 'Luna', 'Perro', 'Bulldog', 'Marrón', '2018-07-07'),
('M004', 'C004', 'Max', 'Perro', 'Pastor Alemán', 'Negro y marrón', '2017-09-09'),
('M005', 'C005', 'Bella', 'Conejo', 'Enano', 'Blanco', '2021-02-02');

INSERT INTO Consultas (codMascota, fechaConsulta, motivo, diagnostico, tratamiento, medicacion)
VALUES
('M001', '2023-01-10', 'Chequeo general', 'Saludable', 'Ninguno', 'Ninguno'),
('M002', '2023-01-15', 'Vacunación', 'Saludable', 'Vacuna A', 'Ninguno'),
('M003', '2023-02-10', 'Desparasitación', 'Saludable', 'Desparasitante B', 'Ninguno'),
('M004', '2023-02-15', 'Herida en pata', 'Herida leve', 'Antibiótico C', 'Antibiótico C'),
('M005', '2023-03-10', 'Chequeo general', 'Saludable', 'Ninguno', 'Ninguno');

INSERT INTO HistPesos (codMascota, fechaPesaje, peso)
VALUES
('M001', '2023-01-10', 20.5),
('M002', '2023-01-15', 4.2),
('M003', '2023-02-10', 15.0),
('M004', '2023-02-15', 25.3),
('M005', '2023-03-10', 2.8);

INSERT INTO AplicaVacuna (codMascota, codVacuna, fechaPrevista, fechaAplicacion, dosisAplicada)
VALUES
('M001', 'V001', '2023-01-01', '2023-01-10', 1),
('M002', 'V002', '2023-01-10', '2023-01-15', 1),
('M003', 'V003', '2023-02-01', '2023-02-10', 1),
('M004', 'V004', '2023-02-10', '2023-02-15', 1),
('M005', 'V005', '2023-03-01', '2023-03-10', 1);

INSERT INTO ConsumoHotel (idHospedaje, idServicio, codMascota, codAlimento, codMedicamento, idComodidad, NIT, observaciones, cantidadAlim, cantidadMedic, cantidadCom)
VALUES
(1, 'S001', 'M001', 'A001', 'M001', 'C001', '1234567890', 'Ninguna', 1, 1, 1),
(2, 'S002', 'M002', 'A002', 'M002', 'C002', '2345678901', 'Ninguna', 1, 1, 1),
(3, 'S003', 'M003', 'A003', 'M003', 'C003', '3456789012', 'Ninguna', 1, 1, 1),
(4, 'S004', 'M004', 'A004', 'M004', 'C004', '4567890123', 'Ninguna', 1, 1, 1),
(5, 'S005', 'M005', 'A005', 'M005', 'C005', '5678901234', 'Ninguna', 1, 1, 1);

DELETE FROM Personas;
DELETE FROM Clientes;
DELETE FROM Vacunas;
DELETE FROM Servicios;
DELETE FROM Hospedajes;
DELETE FROM Alimentos;
DELETE FROM Comodidades;
DELETE FROM Medicamentos;
DELETE FROM Mascotas;
DELETE FROM Consultas;
DELETE FROM HistPesos;
DELETE FROM ConsumosVet;
DELETE FROM PersonaCliente;
DELETE FROM AplicaVacuna;
DELETE FROM ConsumoHotel;
