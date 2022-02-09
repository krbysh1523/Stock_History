CREATE TABLE [dbo].[daily_source] (
    [symbol]            NVARCHAR (50) NOT NULL,
    [date]              DATE          NOT NULL,
    [open]              FLOAT (53)    NULL,
    [close]             FLOAT (53)    NULL,
    [adjusted_close]    FLOAT (53)    NULL,
    [high]              FLOAT (53)    NULL,
    [low]               FLOAT (53)    NULL,
    [volume]            FLOAT (53)    NULL,
    [dividend_amount]   FLOAT (53)    NULL,
    [split_coefficient] FLOAT (53)    NULL,
    CONSTRAINT [PK_daily_source] PRIMARY KEY CLUSTERED ([symbol] ASC, [date] ASC)
);

