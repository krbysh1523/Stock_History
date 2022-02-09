CREATE TABLE [dbo].[daily_industry] (
    [industry]    NVARCHAR (250) NULL,
    [date_hist]   DATE           NOT NULL,
    [price_chg]   FLOAT (53)     NULL,
    [idx_chg]     FLOAT (53)     NULL,
    [strong]      FLOAT (53)     NULL,
    [strong_sma5] FLOAT (53)     NULL
);

