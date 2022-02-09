CREATE TABLE [dbo].[daily_volume] (
    [symbol]    NVARCHAR (50) NOT NULL,
    [date_hist] DATE          NOT NULL,
    [volume]    FLOAT (53)    NULL,
    [SMA_005]   FLOAT (53)    NULL,
    [SMA_010]   FLOAT (53)    NULL,
    [SMA_020]   FLOAT (53)    NULL,
    [SMA_030]   FLOAT (53)    NULL,
    [SMA_060]   FLOAT (53)    NULL,
    [SMA_120]   FLOAT (53)    NULL,
    [SMA_240]   FLOAT (53)    NULL,
    CONSTRAINT [PK_daily_volume] PRIMARY KEY CLUSTERED ([symbol] ASC, [date_hist] ASC)
);

