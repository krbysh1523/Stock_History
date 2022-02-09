CREATE TABLE [dbo].[watch_list] (
    [symbol]     NVARCHAR (50)  NOT NULL,
    [watch_type] NCHAR (10)     NULL,
    [Sector]     NVARCHAR (250) NULL,
    [Industry]   NVARCHAR (250) NULL,
    [company]    NVARCHAR (250) NULL,
    [market_cap] FLOAT (53)     NULL,
    CONSTRAINT [PK_watch_list] PRIMARY KEY CLUSTERED ([symbol] ASC)
);

