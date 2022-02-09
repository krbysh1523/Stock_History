CREATE TABLE [dbo].[prediction_history] (
    [hist_id] INT           NOT NULL,
    [symbol]  NVARCHAR (50) NOT NULL,
    [ranking] INT           NULL,
    [ratio1]  FLOAT (53)    NULL,
    [ratio2]  FLOAT (53)    NULL,
    [rate_5]  FLOAT (53)    NULL,
    [rate_10] FLOAT (53)    NULL,
    [rate_15] FLOAT (53)    NULL,
    [rate_20] FLOAT (53)    NULL,
    [rate_40] FLOAT (53)    NULL,
    [rate_60] FLOAT (53)    NULL,
    CONSTRAINT [pk_prediction_history] PRIMARY KEY NONCLUSTERED ([hist_id] ASC, [symbol] ASC)
);

