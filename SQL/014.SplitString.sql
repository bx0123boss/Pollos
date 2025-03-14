CREATE FUNCTION dbo.SplitString
(
    @inputString VARCHAR(MAX),
    @separator CHAR(1)
)
RETURNS @outputTable TABLE (Value VARCHAR(MAX))
AS
BEGIN
    DECLARE @start INT = 1, @end INT;
    SET @end = CHARINDEX(@separator, @inputString);

    WHILE @start < LEN(@inputString) + 1
    BEGIN
        IF @end = 0 
            SET @end = LEN(@inputString) + 1;

        INSERT INTO @outputTable (Value)
        VALUES (SUBSTRING(@inputString, @start, @end - @start));

        SET @start = @end + 1;
        SET @end = CHARINDEX(@separator, @inputString, @start);
    END

    RETURN;
END;