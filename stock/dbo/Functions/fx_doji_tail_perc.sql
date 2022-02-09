CREATE FUNCTION fx_doji_tail_perc (@open float, @close float, @high float, @low float) RETURNS float
BEGIN
	IF dbo.fx_greater(@open, @close) - dbo.fx_lower(@open, @close) = 0
	BEGIN
		RETURN ((dbo.fx_lower(@open, @close) - @low) / @low) * 100
	end

	RETURN (dbo.fx_lower(@open, @close) - @low) / (dbo.fx_greater(@open, @close) - dbo.fx_lower(@open, @close))

end