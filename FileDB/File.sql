CREATE TABLE [dbo].[File]
(
	[FileID] INT NOT NULL IDENTITY,
	[userID]          INT             NOT NULL,
    [data]            VARBINARY (MAX) NOT NULL,
    [fileName]        NCHAR (255)     NOT NULL,
    [fileSize]        INT      NOT NULL,
    [fileDateCreated] DATETIME      NOT NULL,
	PRIMARY KEY CLUSTERED ([fileID] ASC),
	CONSTRAINT [userID] FOREIGN KEY ([userID]) REFERENCES [dbo].[User] ([userID])
)
