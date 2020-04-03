CREATE TABLE [dbo].[Table]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [CustomerId] NVARCHAR(50) NULL, 
    [IsNDPRAccepted] BIT NOT NULL, 
    [DateCreated] DATETIME NOT NULL, 
    [CIF] NCHAR(10) NOT NULL, 
    [CustomerName] NVARCHAR(MAX) NOT NULL, 
    [FilePath] NVARCHAR(MAX) NOT NULL
)
