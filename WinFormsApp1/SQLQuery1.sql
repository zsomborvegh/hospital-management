create table AddPatient
(
Names varchar(150),
Full_Address varchar(300),
Contact bigint,
Age int,
Gender varchar(10),
Blood_Group varchar(20),
Major_Disease varchar(400),
pid bigint unique,
)

select * from AddPatient;