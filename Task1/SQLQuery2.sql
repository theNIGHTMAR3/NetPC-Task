--reset tables
TRUNCATE TABLE Users
TRUNCATE TABLE Contacts

--insert values
INSERT INTO Users(USERNAME,PASSWORDHASH)
		VALUES('test','9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08'),
				('admin','a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3'),
				('MichalKuprianowicz','b2690faaaa9351ecb43b60145db79e38151129eaa426b9529f1705b2dd5168bc'),
				('NetPC','5c75bc60d1ef5c2a6eabe58bcedbc2716a4a5034db50954e4e1690bba904cdd8');


INSERT INTO Contacts(Name,Surname,Email,Password,Category,Phone_number,Birth_date)
		VALUES('Michał','Kuprianowicz','mk@gmail.com','admin1','private',123456789,'2001-01-08'),
		('Alicja','Nowak','alanowak@gmail.com','somePassword','business',123654987,'1990-11-22'),
		('Jan','Kowalski','jankowalski@gmail.com','qwerty','other',898321456,'2000-5-1');
				
						
--select all
SELECT * FROM Users
SELECT * FROM Contacts