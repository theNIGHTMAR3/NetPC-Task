--reset table
TRUNCATE TABLE Users

--insert values
INSERT INTO Users(USERNAME,PASSWORDHASH)
		VALUES('test','test'),
				('admin','123'),
				('MichalKuprianowicz','MK'),
				('NetPC','Staz');

--select all
SELECT * FROM Users