CREATE TABLE [dbo].[Workers]
(
  [Id] INT NOT NULL PRIMARY KEY,
  [Nickname] VARCHAR (10) NOT NULL,
  [Name] VARCHAR (10) NOT NULL,
  [Surname] VARCHAR (10) NOT NULL,
  [JobId] INT NOT NULL,
  [JobName] VARCHAR (10) NOT NULL
)