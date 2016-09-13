

DECLARE @d TABLE (Supplier VARCHAR(32), Quantity INT);

INSERT @d SELECT 'ABC',3
UNION ALL SELECT 'BCD',1  
UNION ALL SELECT 'CDE',2  
UNION ALL SELECT 'DEF',1;

;WITH SupplyToInsert (Supplier, Qty) AS (
    SELECT Supplier, Quantity
    FROM @d
    --WHERE Quantity > 1

    UNION ALL

    SELECT S.Supplier, I.Qty - 1
    FROM @d S
        INNER JOIN SupplyToInsert I ON I.Supplier = S.Supplier
    WHERE I.Qty > 1
)

--SELECT Supplier, Quantity  FROM @d 
--union all
select * from SupplyToInsert order by Supplier, Qty

/*WITH x AS 
(
  SELECT TOP (10) rn = ROW_NUMBER() --since OP stated max = 10
  OVER (ORDER BY [object_id]) 
  FROM sys.all_columns 
  ORDER BY [object_id]
)

SELECT d.Supplier, d.Quantity
FROM x
CROSS JOIN @d AS d
WHERE x.rn <= d.Quantity
ORDER BY d.Supplier;*/


;WITH SupplyToInsert (Supplier, Qty) AS (
    SELECT Supplier, Quantity - 1
    FROM @d
    WHERE Quantity > 1

    UNION ALL

    SELECT S.Supplier, I.Qty - 1
    FROM @d S
        INNER JOIN SupplyToInsert I ON I.Supplier = S.Supplier
    WHERE I.Qty > 1
)

SELECT Supplier, Quantity  FROM @d 
union all
select * from SupplyToInsert order by Supplier, Quantity

/*INSERT INTO @d (Supplier, Quantity)
SELECT I.Supplier, S.Quantity
FROM SupplyToInsert I
    INNER JOIN @d S ON S.Supplier = I.Supplier

select * from @d order by Supplier*/

