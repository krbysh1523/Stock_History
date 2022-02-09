CREATE TABLE [dbo].[earning] (
    [symbol]             NVARCHAR (50) NOT NULL,
    [fiscalDateEnding]   DATE          NOT NULL,
    [reportedDate]       DATE          NULL,
    [reportedEPS]        FLOAT (53)    NULL,
    [estimatedEPS]       FLOAT (53)    NULL,
    [surprise]           FLOAT (53)    NULL,
    [surprisePercentage] FLOAT (53)    NULL,
    CONSTRAINT [PK_earning] PRIMARY KEY CLUSTERED ([symbol] ASC, [fiscalDateEnding] ASC)
);

