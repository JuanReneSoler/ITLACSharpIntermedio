

use master
drop database Tarea5

create database Tarea5
use Tarea5

create table Publicacion
(
    Id int not null primary key identity(1,1),
    Titulo varchar(100) not null,
    Contenido varchar(max) not null,
    IdUser nvarchar(450) not null,
    FechaPublicacion DATETIME2 default GETDATE()
)

