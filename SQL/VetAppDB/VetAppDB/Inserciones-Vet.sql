USE VeterinariaExtendida;
GO

-- Insertar en la tabla Servicios
INSERT INTO Servicios (idServicio, nombre, descripcion, precio, unidadMedida, tipoServicio) VALUES
('H001', 'BásicoPeque', 'Servicio básico del hotel para mascotas pequeñas, incluye baño inicial y alimentación general', 50, 'Noche', 'Hospedaje'),
('H002', 'BásicoMed', 'Servicio básico del hotel para mascotas medianas, incluye baño inicial y alimentación general', 55, 'Noche', 'Hospedaje'),
('H003', 'BásicoGrande', 'Servicio básico del hotel para mascotas grandes, incluye baño inicial y alimentación general', 60, 'Noche', 'Hospedaje'),
('NE000', 'NecesidadEspecial', 'Servicio de la veterinaria que atiende a mascotas con necesidades especiales', 30, 'Servicio', 'Extra'),
('BE001', 'BañoExtraPeque', 'Servicio de higiene extra para mascotas pequeñas', 5, 'Servicio', 'Extra'),
('BE002', 'BañoExtraMediano', 'Servicio de higiene extra para mascotas medianas', 10, 'Servicio', 'Extra'),
('BE003', 'BañoExtraGrande', 'Servicio de higiene extra para mascotas grandes', 15, 'Servicio', 'Extra'),
('CG000', 'ConsultaGeneral', 'Consulta general de la veterinaria, para todo tipo de mascotas', 150, 'Servicio', 'Veterinaria'),
('AV000', 'AplicaciónVacuna', 'Servicio de vacunación para la mascota', 60, 'Servicio', 'Veterinaria');



-- Insertar en la tabla Personas
INSERT INTO Personas (ci, nombre, telefono, correo, direccion) VALUES
('1234567890', 'Carlos Ramirez', '123456789', 'carlos@example.com', 'Calle 1, Ciudad'),
('0987654321', 'Ana Perez', '987654321', 'ana@example.com', 'Calle 2, Ciudad'),
('1122334455', 'Luis Fernandez', '1122334455', 'luis@example.com', 'Calle 3, Ciudad'),
('5566778899', 'Maria Gomez', '5566778899', 'maria@example.com', 'Calle 4, Ciudad'),
('4433221100', 'Pedro Martinez', '4433221100', 'pedro@example.com', 'Calle 5, Ciudad');

-- Insertar en la tabla Clientes
INSERT INTO Clientes (codCliente, apellido, cuentaBanco, banco, direccion, telefono, correo) VALUES
('C001', 'Ramirez', '1234567890123456', 'Banco 1', 'Calle 1, Ciudad', '123456789', 'carlos@example.com'),
('C002', 'Perez', '2345678901234567', 'Banco 2', 'Calle 2, Ciudad', '987654321', 'ana@example.com'),
('C003', 'Fernandez', '3456789012345678', 'Banco 3', 'Calle 3, Ciudad', '1122334455', 'luis@example.com'),
('C004', 'Gomez', '4567890123456789', 'Banco 4', 'Calle 4, Ciudad', '5566778899', 'maria@example.com'),
('C005', 'Martinez', '5678901234567890', 'Banco 5', 'Calle 5, Ciudad', '4433221100', 'pedro@example.com');

-- Insertar en la tabla PersonaCliente
INSERT INTO PersonaCliente (codCliente, ci, fechaAsociacion) VALUES
('C001', '1234567890', '2024-01-01'),
('C002', '0987654321', '2024-01-02'),
('C003', '1122334455', '2024-01-03'),
('C004', '5566778899', '2024-01-04'),
('C005', '4433221100', '2024-01-05');

-- Insertar en la tabla Mascotas
INSERT INTO Mascotas (codMascota, codCliente, nombre, especie, raza, color, fechaNac) VALUES
('M001', 'C001', 'Rex', 'Canino', 'Labrador', 'Negro', '2022-01-01'),
('M002', 'C002', 'Michi', 'Felino', 'Siames', 'Blanco', '2021-02-02'),
('M003', 'C003', 'Piolin', 'Ave', 'Canario', 'Amarillo', '2020-03-03'),
('M004', 'C004', 'Nemo', 'Pez', 'Pez payaso', 'Naranja', '2019-04-04'),
('M005', 'C005', 'Bunny', 'Conejo', 'Holandés', 'Gris', '2018-05-05');

-- Insertar en la tabla Vacunas
INSERT INTO Vacunas (codVacuna, nombre, laboratorio, prevEnfermedad, dosis, precioUnitario) VALUES
('V001', 'Rabia', 'Lab 1', 'Rabia', 1, 100.00),
('V002', 'Moquillo', 'Lab 2', 'Moquillo', 1, 150.00),
('V003', 'Parvovirus', 'Lab 3', 'Parvovirus', 1, 120.00),
('V004', 'Hepatitis', 'Lab 4', 'Hepatitis', 1, 130.00),
('V005', 'Leptospirosis', 'Lab 5', 'Leptospirosis', 1, 140.00);

-- Insertar en la tabla AplicaVacuna
INSERT INTO AplicaVacuna (codMascota, codVacuna, fechaPrevista, fechaAplicacion, dosisAplicada) VALUES
('M001', 'V001', '2024-05-01', '2024-05-01', 1),
('M002', 'V002', '2024-05-02', '2024-05-02', 1),
('M003', 'V003', '2024-05-03', '2024-05-03', 1),
('M004', 'V004', '2024-05-04', '2024-05-04', 1),
('M005', 'V005', '2024-05-05', '2024-05-05', 1);


-- Insertar en la tabla Consultas
INSERT INTO Consultas (codMascota, fechaConsulta, motivo, diagnostico, tratamiento, medicacion) VALUES
('M001', '2024-05-01', 'Revisión general', 'Saludable', 'Ninguno', 'Ninguna'),
('M002', '2024-05-02', 'Dolor de estómago', 'Gastritis', 'Medicamento A', 'Medicamento A, 5mg'),
('M003', '2024-05-03', 'Herida en la pata', 'Corte leve', 'Vendaje', 'Ninguna'),
('M004', '2024-05-04', 'Problemas respiratorios', 'Asma', 'Inhalador', 'Inhalador B, 2 veces al día'),
('M005', '2024-05-05', 'Vacunación', 'Vacunación de rutina', 'Vacuna aplicada', 'Vacuna C, 1 dosis');



/* SELECT * FROM Servicios
SELECT * FROM Personas
SELECT * FROM Clientes
SELECT * FROM Mascotas
SELECT * FROM PersonaCliente
SELECT * FROM Servicios
SELECT * FROM AplicaVacuna 
SELECT * FROM Consultas
SELECT * FROM ConsumosVet
SELECT * FROM Hospedajes;
SELECT * FROM Comodidades;
SELECT * FROM Alimentos;
SELECT * FROM Medicamentos;
SELECT * FROM ConsumoHotel;
*/
