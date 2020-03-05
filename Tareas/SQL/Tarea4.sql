



create database Tarea4
use Tarea4

create table Region
(
    Id int not null primary key identity(1,1),
    
)

create table Pokemon
(
    Id int not null primary key identity(1,1),
    Nombre varchar(50) not null,
    TipoId int not null FOREIGN KEY REFERENCES Tipo(Id),
    RegionId int not null FOREIGN key REFERENCES Region(Id)
)








