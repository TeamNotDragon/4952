CREATE TABLE [dbo].[User]
(
	[userID] INT NOT NULL IDENTITY,
    [email]    NCHAR (255) NOT NULL,
    [password] NCHAR (255) NOT NULL,
	PRIMARY KEY CLUSTERED ([userID] ASC)
)
