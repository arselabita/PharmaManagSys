CREATE TABLE [dbo].[ManufacturerTbl]
(
	[ManId] INT NOT NULL PRIMARY KEY IDENTITY(500, 1), 
    [ManName] VARCHAR(50) NOT NULL, 
    [ManAdd] VARCHAR(100) NOT NULL, 
    [ManPhone] VARCHAR(50) NOT NULL, 
    [ManJDate] DATE NOT NULL
)
