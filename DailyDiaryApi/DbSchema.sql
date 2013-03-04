IF EXISTS (SELECT name from master.dbo.sysdatabases WHERE name=N'DailyDiary')
	DROP DATABASE DailyDiary

CREATE DATABASE DailyDiary
GO

USE DailyDiary
GO

CREATE TABLE [dbo].[Diary](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY CLUSTERED,
	[Name] [nvarchar](50) NOT NULL UNIQUE
)

GO

CREATE TABLE [dbo].[DiaryEntry](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY CLUSTERED,
	[DiaryId] [int] NOT NULL REFERENCES [dbo].[Diary](Id),
	[Time] [datetimeoffset](7) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Content] [nvarchar](500) NOT NULL
)
GO