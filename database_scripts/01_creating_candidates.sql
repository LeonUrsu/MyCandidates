ALTER SESSION SET CONTAINER = FREEPDB1;

ALTER SESSION SET CURRENT_SCHEMA = HR;

-- Cancellazione tabella esistente per i candidati
begin
    execute immediate 'drop table candidates cascade constraints';
exception when OTHERS then null;
end;
/

-- creazione nuova tabella per i candidati
create table candidates (
    candidate_id number(6) primary key,
    nome varchar2(50) not null,
    cognome varchar2(50) not null,
    email varchar2(100) not null,
    telefono varchar(25) not null,
    citta varchar2(50) not null,
    disponibilita varchar2(30) not null,
    note varchar(200)
);