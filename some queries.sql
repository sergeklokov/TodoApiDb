/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [Id]
      ,[Name]
      ,[IsComplete]
      ,[Secret]
  FROM [LifelongLearning].[dbo].[TodoItems]


  alter table dbo.TodoItems
  alter column Name nvarchar(500) not null ;


  insert dbo.TodoItems (Name,IsComplete) values ('dog',1)