
DROP TABLE Zatrudnienie
DROP TABLE Przedsiêbiorstwa
DROP TABLE Osoby


CREATE TABLE [dbo].[Osoby](
	[Id_osoby] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Imiê] [nchar](20) NOT NULL,
	[Nazwisko] [nchar](40) NOT NULL,
	[P³eæ] [nchar](10) CHECK([P³eæ] in ('Mê¿czyzna', 'Kobieta')) NOT NULL,
	[Data_urodzenia] [date] NOT NULL,
	[Zarobki] [float] NOT NULL,
);

CREATE TABLE [dbo].[Przedsiêbiorstwa](
	[Id_przedsiêbiorstwa] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Id_prezesa] [int] FOREIGN KEY REFERENCES Osoby(Id_osoby) NOT NULL,
	[Nazwa] [nchar](30) UNIQUE NOT NULL,
);

CREATE TABLE [dbo].[Zatrudnienie](
	[Id_zatrudnienia] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Id_pracownika] [int] FOREIGN KEY REFERENCES Osoby(Id_osoby) NOT NULL,
	[Id_przedsiêbiorstwa] [int] FOREIGN KEY REFERENCES Przedsiêbiorstwa(Id_przedsiêbiorstwa)  NOT NULL,
	[Rodzaj_umowy] [nchar](10) CHECK([Rodzaj_umowy] in ('Zlecenie', 'Praca'))NOT NULL,
);
	
INSERT INTO Osoby (Imiê,Nazwisko,P³eæ,Data_urodzenia,Zarobki)
	VALUES('Jan','Nowak','Mê¿czyzna','1994-11-04','3700'),
			('Ela','Nowak','Kobieta','2000-01-15','2700'),
			('Micha³','Kuprianowicz','Mê¿czyzna','2001-01-08','3500'),
			('Emil','Kowalski','Mê¿czyzna','1999-05-01','2500'),
			('Ela','Nowak','Kobieta','2000-01-15','2700'),
			('Ania','S³owik','Kobieta','2001-07-09','3500'),
			('Micha³','Nowak','Mê¿czyzna','1990-03-22','4000');


INSERT INTO Przedsiêbiorstwa(Id_prezesa,Nazwa)
	VALUES(1,'Pol-Bud'),
			(2,'Dudex');

INSERT INTO Zatrudnienie(Id_pracownika,Id_przedsiêbiorstwa,Rodzaj_umowy)
	VALUES(3,1,'Praca'),
		(4,1,'Zlecenie'),
			(5,2,'Praca'),
			(6,2,'Zlecenie'),
			(7,2,'Praca');

SELECT * FROM Osoby
SELECT * FROM Przedsiêbiorstwa
SELECT * FROM Zatrudnienie

SELECT Rodzaj_umowy AS 'Rodzaj Umowy', Przedsiêbiorstwa.Nazwa, COUNT(Id_zatrudnienia) AS ' iloœæ pracowników', AVG(Zarobki) AS 'Œrednie wynagrodzenia'
FROM Zatrudnienie
JOIN Osoby ON Zatrudnienie.Id_pracownika = Osoby.Id_osoby
JOIN Przedsiêbiorstwa ON Zatrudnienie.Id_przedsiêbiorstwa = Przedsiêbiorstwa.Id_przedsiêbiorstwa
GROUP BY Rodzaj_umowy,Przedsiêbiorstwa.Nazwa;


