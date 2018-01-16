/*
Desarrollador: Bryan Silverio Nieves
Fecha: 15/01/2018
*/

create database MovieStore;
use MovieStore;

/*
Crear tabla Movie
*/
create table Movie(
Id int not null primary key identity,
Title varchar(50) not null,
ReleaseDate datetime not null,
Director varchar(30) not null
);

insert into Movie(Title,ReleaseDate,Director)values('El Padrino','1972-01-01','Francis Ford Coppola')
insert into Movie(Title,ReleaseDate,Director)values('El Origen','2010-02-10','Christopher Nolan')
insert into Movie(Title,ReleaseDate,Director)values('Interestelar','2014-03-15','Christopher Nolan')
insert into Movie(Title,ReleaseDate,Director)values('El lobo de Wall Street ','2013-04-20','Martin Scorsese')
insert into Movie(Title,ReleaseDate,Director)values('Logan: Wolverine ','2017-03-12','James Mangold')


/*
Nombre: SP_GetMovieList
Accion: Consulta todos lo campos de la tabla Movie
*/
create proc SP_GetMovieList
as
begin
	select * from Movie
end

/*
Nombre: SP_GetMovieByTitle
Accion: Consulta pelicula en base el @title como parametro
*/
create proc SP_GetMovieByTitle
@title varchar(50)
as
begin
	select * from Movie where Title=@title
end

/*
Nombre: SP_DeleteMovie
Accion: Elimina un registro en base al id
*/
create proc SP_DeleteMovie
@id int 
as 
begin
	delete from Movie where Id = @id
end

/*
Nombre: SP_PutMovie
Accion: Actualiza un registro en base al id y los datos de los demas campos a actualizar
*/
create proc SP_PutMovie
@id int ,
@title varchar(50),
@releaseDate datetime,
@director varchar(30)
as 
begin
	UPDATE Movie
	SET Title =  @title
		,ReleaseDate = @releaseDate
		,Director = @director
	WHERE Id = @id
end

/*
Nombre: SP_PostMovie
Accion: Inserta un nuevo registro
*/
create proc SP_PostMovie
@title varchar(50),
@releaseDate datetime,
@director varchar(30)
as 
begin
	insert into Movie (Title,ReleaseDate,Director) values(@title,@releaseDate,@director)
end

/*
Nombre: SP_ValidateMovie
Accion: Consulta el numero de registros que coincidan con los parametros de title y director
*/
create proc SP_ValidateMovie
@title varchar(50),
@director varchar(30)
as
begin
	select COUNT(*) existe from Movie  where Title=@title and Director=@director
end
