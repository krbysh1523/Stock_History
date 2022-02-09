CREATE TABLE [dbo].[api] (
    [api_id]   INT           IDENTITY (1, 1) NOT NULL,
    [api_key]  NVARCHAR (50) NULL,
    [last_ran] DATETIME      NULL
);

