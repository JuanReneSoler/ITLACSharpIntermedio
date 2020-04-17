create database Security

use master
drop DATABASE SistemaVotaciones

create database SistemaVotaciones

use SistemaVotaciones

create table Partidos
(
    Id int not null IDENTITY(1,1) primary KEY,
    Nombre VARCHAR(100) not null,
    Descripcion varchar(max) not null,
    LogoPartido NVARCHAR(max),
    Estado bit not null default 1
)

create table Ciudadanos
(
    Id int not null primary key IDENTITY(1,1),
    DocIdentidad varchar(11) not null UNIQUE,
    Nombre varchar(100) not null,
    Apellido varchar(100) not null,
    Email VARCHAR(max) not null,
    Estado bit not null default 1
)

create table PuestosElectivos
(
    Id int not null primary key IDENTITY(1,1),
    Nombre varchar(100) not null,
    Descripcion varchar(max) not null,
    Estado bit not null default 1
)

create table EstadosElecciones
(
    Id int not null primary key identity(1,1),
    DescripcionEstado varchar(25) not null
)

create table Elecciones
(
    Id int not null primary key IDENTITY(1,1),
    Nombre VARCHAR(100) not null,
    FechaRealizadion DATETIME2 not null,
    EstadosId int not null foreign key references EstadosElecciones(Id)
)

create table Candidatos
(
    Id int not null IDENTITY(1,1) primary key,
    CiudadanoId int not null FOREIGN key REFERENCES Ciudadanos(Id),
    --NombreCompleto varchar(200) not null,
    PartidoId int FOREIGN KEY REFERENCES Partidos(Id),
    PuestoId int FOREIGN key REFERENCES PuestosElectivos(Id),
    EleccionId int not null FOREIGN key references Elecciones(Id),
    FotoPerfil varchar(max),
    Estado bit not null default 1
)

create table Votantes
(
    Id int not null primary key identity(1,1),
    CiudadanoId int not null foreign key references Ciudadanos(Id),
    EleccionId int not null foreign key REFERENCES Elecciones(Id),
    Fecha DATETIME2 not null,
)

create table Votos
(
    Id int not null primary key identity(1,1),
    EleccionId int not null foreign key REFERENCES Elecciones(Id),
    CandidatoId int not null FOREIGN key REFERENCES Candidatos(Id),
    Fecha datetime2 not null
)

insert into EstadosElecciones values('Creada')
insert into EstadosElecciones values('En proceso de votacion')
insert into EstadosElecciones values('Cerrada')


