##Not for prod use this command, but for test purposes
#drop database if exists bcity_assessment;

create database bcity_assessment;

use bcity_assessment;

create table client_(
    id int primary key auto_increment,
    client_code varchar(6)
);

create table contact(
    id int primary key auto_increment,
    name_ varchar(100),
    surname varchar(100),
    email varchar(100)
);

create table client_contact(
   client_id int,
   contact_id int,
   primary key(client_id, contact_id),
   constraint foreign key fk_client_contact_client (client_id) references client_(id),
   constraint foreign key fk_client_contact_contact (contact_id) references contact(id)
);

