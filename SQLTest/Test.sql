IF OBJECT_ID ('GetCategory', 'TF') IS NOT NULL
BEGIN
    DROP FUNCTION GetCategory;
END
GO

CREATE FUNCTION GetCategory (@name VARCHAR(MAX), @stringToSplit VARCHAR(MAX) )
RETURNS
 @returnList TABLE ([ItemName] [nvarchar] (MAX), [Category] [nvarchar] (MAX))
AS
BEGIN

 DECLARE @category NVARCHAR(MAX);
 DECLARE @pos INT;

 WHILE CHARINDEX(' ', @stringToSplit) > 0
 BEGIN
  SELECT @pos  = CHARINDEX(' ', @stringToSplit);
  SELECT @category = SUBSTRING(@stringToSplit, 1, @pos-1);

  INSERT INTO @returnList 
  SELECT @name, @category;

  SELECT @stringToSplit = SUBSTRING(@stringToSplit, @pos+1, LEN(@stringToSplit)-@pos);
 END

 INSERT INTO @returnList
 SELECT @name, @stringToSplit;

 RETURN;
END

SELECT ItemName, Category FROM demo CROSS APPLY GetCategory(name,hint);
