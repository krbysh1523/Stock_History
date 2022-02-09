CREATE TABLE [dbo].[symbol_att] (
    [symbol]      NVARCHAR (50)  NOT NULL,
    [name]        NVARCHAR (250) NULL,
    [Sector]      NVARCHAR (250) NULL,
    [Industry]    NVARCHAR (250) NULL,
    [market_cap]  FLOAT (53)     NULL,
    [market_rank] INT            NULL,
    [symbol_type] NVARCHAR (50)  NULL,
    CONSTRAINT [PK_symbol_att] PRIMARY KEY CLUSTERED ([symbol] ASC)
);

