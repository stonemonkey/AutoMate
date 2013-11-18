CREATE TABLE [dbo].[UserStatistic]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [EmailAddress] NVARCHAR(500) NOT NULL, 
    [Location] NVARCHAR(500) NOT NULL, 
    [AgresivityRate] INT NOT NULL, 
    [DateTime] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    CONSTRAINT [CK_UserStatistic_AgresivityRate] CHECK (AgresivityRate<=100 AND AgresivityRate >= 1)
)
