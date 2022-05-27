CREATE DATABASE moviedb;

go

use moviedb

go

create table [Movies]
(
    Id int not null,
    Title nvarchar(MAX) not null,
    Description nvarchar(MAX) not null
)
go