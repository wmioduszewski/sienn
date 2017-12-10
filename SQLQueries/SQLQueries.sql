--1
SELECT p.Code FROM Products p
WHERE p.IsAvailable=0
AND MONTH(SYSUTCDATETIME()) = MONTH(p.DeliveryDate)
AND YEAR(SYSUTCDATETIME()) = YEAR(p.DeliveryDate)

--2
SELECT p.Code FROM Products p
JOIN CategoryToProduct cp ON p.Id=cp.ProductId
GROUP BY p.Id, p.Code
HAVING COUNT(*)>1

--3
SELECT TOP 3 c.Code, AVG(p.Price) as 'Avg Price', SUM(CAST(p.IsAvailable as INT)) as 'Products In Category' FROM Categories c
JOIN CategoryToProduct cp ON c.Id=cp.CategoryId
JOIN Products p ON p.Id = cp.ProductId
GROUP BY c.Id, c.Code
ORDER BY AVG(p.Price) DESC

--Had some doubts about performance of AVG put twice here, but this answer made me calmer: https://stackoverflow.com/a/45636687/2395747
--At least two approaches to handle conditional count (i.e. shown here https://stackoverflow.com/a/1400115/2395747) but I finally decided to cast bit and sum it.
