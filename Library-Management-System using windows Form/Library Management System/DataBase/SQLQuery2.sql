


create table loginTable(

id int NOT NULL IDENTITY(1,1) primary key,
username varchar(150) not null,
pass varchar(150) not null

)

insert into loginTable (username,pass) values ('btechdays','pass');

select * from loginTable