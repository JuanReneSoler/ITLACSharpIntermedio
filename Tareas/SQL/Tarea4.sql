
use master
drop database Tarea4

create database Tarea4
use Tarea4

create table Region
(
    Id int not null primary key identity(1,1),
    Nombre varchar(100) not null,
    RGBColor varchar(8) not null
)

create table Tipo
(
    Id int not null primary key IDENTITY(1,1),
    Nombre varchar(100) not null
)

create table Pokemon
(
    Id int not null primary key identity(1,1),
    Nombre varchar(50) not null,
    RegionId int not null FOREIGN key REFERENCES Region(Id)
)

create table TipoPokemon
(
    Id int not null primary key identity(1,1),
    PokemonId int not null foreign key references Pokemon(Id),
    TipoId int not null foreign key references Tipo(Id)
)

create table Ataques
(
    Id int not null primary key identity(1,1),
    PokemonId int not null foreign key references Pokemon(Id),
    Nombre varchar(100)
)

