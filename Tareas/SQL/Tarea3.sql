use master
drop database Tarea3
truncate table Estudiantes


create database Tarea3
use Tarea3

create table Estudiantes
(
    Id int not null primary key IDENTITY(1,1),
    Nombre varchar(100) not null,
    Apellido varchar(100) not null,
    CarreraId int not null,
    Estado bit not null
)








