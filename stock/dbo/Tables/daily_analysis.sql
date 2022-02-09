CREATE TABLE [dbo].[daily_analysis] (
    [symbol]          NVARCHAR (50) NOT NULL,
    [date]            DATE          NOT NULL,
    [open_close_perc] FLOAT (53)    NULL,
    [high_low_perc]   FLOAT (53)    NULL,
    [dir]             CHAR (1)      NULL,
    [oc_by_hl_perc]   FLOAT (53)    NULL,
    [dir_on]          INT           NULL,
    [point1]          INT           NULL,
    [point2]          INT           NULL,
    [point3]          INT           NULL,
    CONSTRAINT [PK_daily_analysis] PRIMARY KEY CLUSTERED ([symbol] ASC, [date] ASC)
);

