CREATE TABLE [Person](
 	[ID] Int PRIMARY KEY IDENTITY(1,1) NOT NULL,
  	[Name]  NCHAR (20)   NOT NULL,
    [Surname]  NCHAR (40)   NOT NULL,
  	[Gender]  NCHAR (40)   NOT NULL,
  [Salary]  FLOAT (20)   NOT NULL,
  CHECK([Gender] in ('Male', 'Female'))
  );
  
  CREATE TABLE [Corporation](
 	[ID] Int PRIMARY KEY IDENTITY(1,1) NOT NULL,
  	[Name]  NCHAR (20)   NOT NULL,

  );
  
  CREATE TABLE [Employment](
 	[ID] Int PRIMARY KEY IDENTITY(1,1) NOT NULL,
  	[Name]  NCHAR (20)   NOT NULL,contract 
	[Contract_type]  NCHAR (20)   NOT NULL,
  )
  