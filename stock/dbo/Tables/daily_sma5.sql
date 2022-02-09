CREATE TABLE [dbo].[daily_sma5] (
    [symbol]         NVARCHAR (50) NOT NULL,
    [date_hist]      DATE          NOT NULL,
    [price]          FLOAT (53)    NULL,
    [price_pre]      FLOAT (53)    NULL,
    [price_chg_perc] FLOAT (53)    NULL,
    [sma5]           FLOAT (53)    NULL,
    [sma5_pre]       FLOAT (53)    NULL,
    [sma5_chg_perc]  FLOAT (53)    NULL,
    CONSTRAINT [PK_daily_sma5] PRIMARY KEY CLUSTERED ([symbol] ASC, [date_hist] ASC)
);

