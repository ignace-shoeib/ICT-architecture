create database IF NOT EXISTS Project;
use Project;
create table if not exists Files(
	ID int primary key auto_increment,
    FileName varchar(255),
    CreationDate datetime,
    UUID char(36)
);