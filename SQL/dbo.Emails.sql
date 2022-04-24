CREATE TABLE [dbo].[Emails] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Sender]    NVARCHAR (MAX) NOT NULL,
    [Recipient] NVARCHAR (MAX) NOT NULL,
    [Subject]   NVARCHAR (MAX) NULL,
    [Body]      NVARCHAR (MAX) NULL,
    [Date]      DATETIME2 (7)  NOT NULL,
    [Status]    INT            NOT NULL,
    CONSTRAINT [PK_Emails] PRIMARY KEY CLUSTERED ([Id] ASC)
);

