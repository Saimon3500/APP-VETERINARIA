CREATE DATABASE VeterinariaExtendida;
GO
USE VeterinariaExtendida;
 
-- sin foreign keys: persona, cliente, vacuna, servicio, hospedaje, alimento, medicamento, comodidades

/*
tipos custom
*/

CREATE TYPE idFijo FROM char(20) not null
CREATE TYPE nombre FROM varchar(80) not null
CREATE TYPE numTel FROM varchar(50) not null
CREATE TYPE correo FROM varchar(50) not null
CREATE TYPE direccion FROM varchar(60) not null
CREATE TYPE fechaObligatoria FROM date not null

CREATE TABLE Personas
(

	ci				idFijo,
	nombre			nombre, 
	telefono		numTel,
	correo			correo,
	direccion		direccion

	CONSTRAINT PK_Personas PRIMARY KEY (ci)

);


CREATE TABLE Clientes
(

	codCliente		idFijo,
	apellido		nombre,
	cuentaBanco		varchar(40) not null,
	banco			nombre,
	direccion		direccion,
	telefono		numTel,
	correo			correo,
	CONSTRAINT PK_Clientes PRIMARY KEY (codCliente)

);

CREATE TABLE Vacunas
(

	codVacuna		idFijo,
	nombre			nombre,
	laboratorio		nombre,
	prevEnfermedad	varchar(50) not null,
	dosis			decimal(5,2) not null,
	precioUnitario	money not null,
	CONSTRAINT PK_Vac PRIMARY KEY (codVacuna)


);

CREATE TABLE Servicios
(

	idServicio		idFijo,
	nombre			nombre,
	descripcion		varchar(100) not null,
	precio			money not null,
	unidadMedida	char(20) not null,
	tipoServicio	char(20) not null,
	CONSTRAINT PK_Ser PRIMARY KEY (idServicio)

);


CREATE TABLE Alimentos
(

	codAlimento		INT IDENTITY(1,1) not null,
	nombre			nombre,
	descripcion		varchar(150) not null,
	proveedor		nombre,
	precioUnitario	money not null,
	CONSTRAINT PK_Alim PRIMARY KEY (codAlimento)
);

CREATE TABLE Comodidades
(

	idComodidad		INT IDENTITY(1,1) not null,
	nombre			nombre,
	descripcion		varchar(150) not null,
	precioUnitario	money not null,
	CONSTRAINT PK_Com PRIMARY KEY (idComodidad)

);

CREATE TABLE Medicamentos
(

	codMedicamento	INT IDENTITY(1,1) not null,
	laboratorio		nombre,
	presentacion	char(30) not null,
	pesoNeto		decimal(5,2) not null,
	precioUnitario	money not null,
	nombre			nombre

	CONSTRAINT PK_Medic PRIMARY KEY (codMedicamento)


);

-- con foreign keys, no asociativas: mascotas, consultas, histpeso

CREATE TABLE Mascotas
(

	codMascota			idFijo,
	codCliente			idFijo,
	nombre				nombre,
	especie				varchar(30) not null,
	raza				varchar(30) not null,
	color				varchar(20) not null,
	fechaNac			date
	CONSTRAINT PK_Mascotas PRIMARY KEY (codMascota),
	CONSTRAINT FK_CliMascotas FOREIGN KEY (codCliente) REFERENCES Clientes

);

CREATE TABLE Consultas
(

	codMascota		idFijo,
	fechaConsulta	fechaObligatoria,
	motivo			varchar(50) not null,
	diagnostico		varchar(100) not null,
	tratamiento		varchar(150) not null,
	medicacion		varchar(200) not null,
	CONSTRAINT PK_Consultas PRIMARY KEY (codMascota, fechaConsulta),
	CONSTRAINT FK_MascConsultas FOREIGN KEY (codMascota) REFERENCES Mascotas

);

CREATE TABLE HistPesos
(

	codMascota		idFijo,
	fechaPesaje		fechaObligatoria,
	peso			decimal(5,2) not null,
	CONSTRAINT PK_HistPeso PRIMARY KEY (codMascota, fechaPesaje),
	CONSTRAINT FK_MasHistPesos FOREIGN KEY (codMascota) REFERENCES Mascotas

);

-- entidades asociativas: consumo vet, persona cliente, aplica vacuna, consumo hotel

CREATE TABLE ConsumosVet
(

	codMascota		idFijo,
	codVacuna		char(20),
	idServicio		idFijo,
	idConsumoVet	INT IDENTITY(1,1) not null,
	observaciones	varchar(200) not null,
	cantVacunas		int not null DEFAULT 0,
	nit				idFijo,
	CONSTRAINT PK_CV PRIMARY KEY (codMascota, idServicio, idConsumoVet),
	CONSTRAINT FK_MasCV FOREIGN KEY (codMascota) REFERENCES Mascotas,
	CONSTRAINT FK_VacCV FOREIGN KEY (codVacuna) REFERENCES Vacunas,
	CONSTRAINT FK_SerCV FOREIGN KEY (idServicio) REFERENCES Servicios

);

CREATE TABLE PersonaCliente
(
	codCliente			idFijo,
	ci					idFijo,
	fechaAsociacion		fechaObligatoria

	CONSTRAINT PK_PC PRIMARY KEY (codCliente, ci, fechaAsociacion),
	CONSTRAINT FK_ClientesPC FOREIGN KEY (codCliente) REFERENCES Clientes(codCliente),
	CONSTRAINT FK_PersonasPC FOREIGN KEY (ci) REFERENCES Personas(ci)
);

CREATE TABLE AplicaVacuna
(

	codMascota		idFijo,
	codVacuna		idFijo,
	fechaPrevista	fechaObligatoria,
	fechaAplicacion	date DEFAULT GETDATE(),
	dosisAplicada	int not null DEFAULT 0,
	CONSTRAINT PK_AplicaVac PRIMARY KEY (codMascota, codVacuna, fechaPrevista),
	CONSTRAINT FK_MasAV FOREIGN KEY (codMascota) REFERENCES Mascotas,
	CONSTRAINT FK_VacAV FOREIGN KEY (codVacuna) REFERENCES Vacunas,


);

CREATE TABLE Hospedajes
(

	idHospedaje		INT IDENTITY (1,1) not null,
	codMascota		idFijo,
	fechaIngreso	fechaObligatoria DEFAULT GETDATE(),
	fechaSalida		date DEFAULT GETDATE(),
	observaciones	varchar(150) not null,
	CONSTRAINT PK_idHospedaje PRIMARY KEY (idHospedaje, codMascota),
	CONSTRAINT FK_MasHosp FOREIGN KEY (codMascota) REFERENCES Mascotas

);

CREATE TABLE ConsumoHotel
(

	idHospedaje		int not null,
	idServicio		idFijo,
	codMascota		idFijo,
	codAlimento		int,
	codMedicamento	int,
	idComodidad		int,
	NIT				varchar(20) NOT NULL,
	observaciones	varchar(150) NOT NULL,
	nochesHosp		int not null DEFAULT 1,
	cantidadAlim	int NOT NULL DEFAULT 0,
	cantidadMedic	int NOT NULL DEFAULT 0,
	cantidadCom		int NOT NULL DEFAULT 0,
	cantidadBanos	int not null default 0,
	CONSTRAINT PK_ConsumoHotel PRIMARY KEY (idHospedaje, idServicio, codMascota),
	CONSTRAINT FK_HospedajeCH FOREIGN KEY (idHospedaje, codMascota) REFERENCES Hospedajes(idHospedaje, codMascota),
	CONSTRAINT FK_ServiciosCH FOREIGN KEY (idServicio) REFERENCES Servicios,
	CONSTRAINT FK_MascCH FOREIGN KEY (codMascota) REFERENCES Mascotas,
	CONSTRAINT FK_AlimCH FOREIGN KEY (codAlimento) REFERENCES Alimentos,
	CONSTRAINT FK_MedicCH FOREIGN KEY (codMedicamento) REFERENCES Medicamentos,
	CONSTRAINT FK_ComodCH FOREIGN KEY (idComodidad) REFERENCES Comodidades,
	
);



