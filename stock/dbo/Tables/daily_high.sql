CREATE TABLE [dbo].[daily_high] (
    [symbol]  NVARCHAR (50) NOT NULL,
    [date]    DATE          NOT NULL,
    [SMA_005] FLOAT (53)    NULL,
    [SMA_010] FLOAT (53)    NULL,
    [SMA_020] FLOAT (53)    NULL,
    [SMA_030] FLOAT (53)    NULL,
    [SMA_060] FLOAT (53)    NULL,
    [SMA_120] FLOAT (53)    NULL,
    [SMA_200] FLOAT (53)    NULL
);

