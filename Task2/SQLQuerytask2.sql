
DROP TABLE Zatrudnienie
DROP TABLE Przedsi�biorstwa
DROP TABLE Osoby


CREATE TABLE [dbo].[Osoby](
	[Id_osoby] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Imi�] [nchar](20) NOT NULL,
	[Nazwisko] [nchar](40) NOT NULL,
	[P�e�] [nchar](10) CHECK([P�e�] in ('M�czyzna', 'Kobieta')) NOT NULL,
	[Data_urodzenia] [date] NOT NULL,
	[Zarobki] [float] NOT NULL,
);

CREATE TABLE [dbo].[Przedsi�biorstwa](
	[Id_przedsi�biorstwa] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Id_prezesa] [int] FOREIGN KEY REFERENCES Osoby(Id_osoby) NOT NULL,
	[Nazwa] [nchar](30) UNIQUE NOT NULL,
);

CREATE TABLE [dbo].[Zatrudnienie](
	[Id_zatrudnienia] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Id_pracownika] [int] FOREIGN KEY REFERENCES Osoby(Id_osoby) NOT NULL,
	[Id_przedsi�biorstwa] [int] FOREIGN KEY REFERENCES Przedsi�biorstwa(Id_przedsi�biorstwa)  NOT NULL,
	[Rodzaj_umowy] [nchar](10) CHECK([Rodzaj_umowy] in ('Zlecenie', 'Praca'))NOT NULL,
);
	
INSERT INTO Osoby (Imi�,Nazwisko,P�e�,Data_urodzenia,Zarobki)
	VALUES('Jan','Nowak','M�czyzna','1994-11-04','3700'),
			('Ela','Nowak','Kobieta','2000-01-15','2700'),
			('Micha�','Kuprianowicz','M�czyzna','2001-01-08','3500'),
			('Emil','Kowalski','M�czyzna','1999-05-01','2500'),
			('Ela','Nowak','Kobieta','2000-01-15','2700'),
			('Ania','S�owik','Kobieta','2001-07-09','3500'),
			('Micha�','Nowak','M�czyzna','1990-03-22','4000');


INSERT INTO Przedsi�biorstwa(Id_prezesa,Nazwa)
	VALUES(1,'Pol-Bud'),
			(2,'Dudex');

INSERT INTO Zatrudnienie(Id_pracownika,Id_przedsi�biorstwa,Rodzaj_umowy)
	VALUES(3,1,'Praca'),
		(4,1,'Zlecenie'),
			(5,2,'Praca'),
			(6,2,'Zlecenie'),
			(7,2,'Praca');

SELECT * FROM Osoby
SELECT * FROM Przedsi�biorstwa
SELECT * FROM Zatrudnienie

SELECT Rodzaj_umowy AS 'Rodzaj Umowy', Przedsi�biorstwa.Nazwa, COUNT(Id_zatrudnienia) AS ' ilo�� pracownik�w', AVG(Zarobki) AS '�rednie wynagrodzenia'
FROM Zatrudnienie
JOIN Osoby ON Zatrudnienie.Id_pracownika = Osoby.Id_osoby
JOIN Przedsi�biorstwa ON Zatrudnienie.Id_przedsi�biorstwa = Przedsi�biorstwa.Id_przedsi�biorstwa
GROUP BY Rodzaj_umowy,Przedsi�biorstwa.Nazwa;


