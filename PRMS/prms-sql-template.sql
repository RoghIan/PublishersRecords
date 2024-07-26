--report specific date (change the date based on the report date that you wanted) month/day/year
WHERE month(ReportDate) = month('02/13/2024') and year(ReportDate) = year('02/13/2024')

--publishers with no record of reports
--null report id means that the publisher ha NO record yet in the reports table
SELECT 
Publishers.Id, 
FirstName, 
LastName, 
GroupId
FROM Publishers
LEFT JOIN Reports
ON Publishers.Id = Reports.PublisherId
WHERE Reports.Id IS NULL
OR Reports.Hours = 0
AND
month(ReportDate) = month('02/13/2024') and year(ReportDate) = year('02/13/2024')


--Total number of reportings based on date
SELECT COUNT(*) AS TOTAL_NUMBER_OF_REPORTING
FROM Reports
WHERE 
Id IS NOT NULL
AND 
Reports.HasParticipated = 1
AND
month(ReportDate) = month('02/13/2024') and year(ReportDate) = year('02/13/2024')


--auxi reports
SELECT 
PublisherId, 
FirstName, 
LastName, 
GroupId as 'Group', 
Hours, 
Placement, 
VideoShowings, 
ReturnVisits, 
BibleStudies, 
Appointeds.Name AS Appointed, 
FORMAT (ReportDate, 'MMMM yyyy') as ReportDate  
FROM Reports 
INNER JOIN Publishers
ON Reports.PublisherId = Publishers.Id
INNER JOIN AppointedPublisher
ON AppointedPublisher.PublishersId = Publishers.Id
INNER JOIN Appointeds
ON Appointeds.Id = AppointedPublisher.AppointedsId
WHERE 
Appointeds.Name IN ('Auxiliary Pioneer')
OR 
Reports.IsAuxi = 1


--publisher and unbatized reports
SELECT 
PublisherId, 
FirstName, 
LastName, 
GroupId, 
Hours, 
Placement, 
VideoShowings, 
ReturnVisits, 
BibleStudies, 
Appointeds.Name AS Appointed, 
FORMAT (ReportDate, 'MMMM yyyy') as ReportDate  
FROM Reports 
INNER JOIN Publishers
ON Reports.PublisherId = Publishers.Id
INNER JOIN AppointedPublisher
ON AppointedPublisher.PublishersId = Publishers.Id
INNER JOIN Appointeds
ON Appointeds.Id = AppointedPublisher.AppointedsId
WHERE 
Appointeds.Name IN ('Publisher','Unbaptized Publisher')
AND
Reports.IsAuxi = 0
AND
NOT EXISTS 
(
SELECT * FROM AppointedPublisher 
WHERE Publishers.Id = AppointedPublisher.PublishersId
AND 
(AppointedPublisher.AppointedsId = 3 
OR 
AppointedPublisher.AppointedsId = 4)
)
ORDER By
GroupId, LastName, FirstName


--publisher and unbatized publisher reports total
SELECT 
SUM(Hours) TotalHours, 
SUM(Placement) TotalPlacement, 
SUM(VideoShowings) TotalVideoShowings, 
SUM(ReturnVisits) TotalReturnVisits, 
SUM(BibleStudies) TotalBibleStudies
FROM Reports 
INNER JOIN Publishers
ON Reports.PublisherId = Publishers.Id
INNER JOIN AppointedPublisher
ON AppointedPublisher.PublishersId = Publishers.Id
INNER JOIN Appointeds
ON Appointeds.Id = AppointedPublisher.AppointedsId
WHERE 
month(ReportDate) = month('02/13/2024') and year(ReportDate) = year('02/13/2024')
AND
Appointeds.Name IN ('Publisher','Unbaptized Publisher')
AND
IsAuxi = 0
AND
NOT EXISTS 
(
SELECT * FROM AppointedPublisher 
WHERE Publishers.Id = AppointedPublisher.PublishersId
AND 
AppointedPublisher.AppointedsId = 3
)


--auxi reports total
SELECT 
SUM(Hours) TotalHours, 
SUM(Placement) TotalPlacement, 
SUM(VideoShowings) TotalVideoShowings, 
SUM(ReturnVisits) TotalReturnVisits, 
SUM(BibleStudies) TotalBibleStudies
FROM Reports 
INNER JOIN Publishers
ON Reports.PublisherId = Publishers.Id
WHERE 
Reports.IsAuxi = 1
AND
month(ReportDate) = month('02/13/2024') and year(ReportDate) = year('02/13/2024')


  --pioneer reports total
  SELECT 
  SUM(Hours) TotalHours, 
  SUM(Placement) TotalPlacement, 
  SUM(VideoShowings) TotalVideoShowings, 
  SUM(ReturnVisits) TotalReturnVisits, 
  SUM(BibleStudies) TotalBibleStudies
  FROM Reports 
  INNER JOIN Publishers
  ON Reports.PublisherId = Publishers.Id
  INNER JOIN AppointedPublisher
  ON AppointedPublisher.PublishersId = Publishers.Id
  INNER JOIN Appointeds
  ON Appointeds.Id = AppointedPublisher.AppointedsId
  WHERE 
  month(ReportDate) = month('02/13/2024') and year(ReportDate) = year('02/13/2024')
  AND
  Appointeds.Name IN ('Pioneer')


--publisher and unbatized publisher reports total with data
SELECT 
FirstName, 
LastName, 
GroupId as 'Group', 
Appointeds.Name AS Appointed, 
FORMAT (ReportDate, 'MMMM yyyy') as ReportDate,
Hours = SUM(Hours), 
Placement = SUM(Placement), 
VideoShowings = SUM(VideoShowings), 
ReturnVisits = SUM(ReturnVisits), 
BibleStudies = SUM(BibleStudies)
FROM Reports 
INNER JOIN Publishers
ON Reports.PublisherId = Publishers.Id
INNER JOIN AppointedPublisher
ON AppointedPublisher.PublishersId = Publishers.Id
INNER JOIN Appointeds
ON Appointeds.Id = AppointedPublisher.AppointedsId
WHERE 
month(ReportDate) = month('02/13/2024') and year(ReportDate) = year('02/13/2024')
AND
Appointeds.Name IN ('Publisher','Unbaptized Publisher')
AND
NOT EXISTS 
(
SELECT * FROM AppointedPublisher 
WHERE Publishers.Id = AppointedPublisher.PublishersId
AND 
(AppointedPublisher.AppointedsId = 3 
OR 
AppointedPublisher.AppointedsId = 4))
GROUP BY GROUPING SETS ( (FirstName, LastName, GroupId, Appointeds.Name, ReportDate), () )
ORDER By
GroupId, LastName, FirstName


--auxi reports total with data
SELECT 
FirstName, 
LastName, 
GroupId as 'Group', 
Appointeds.Name AS Appointed, 
FORMAT (ReportDate, 'MMMM yyyy') as ReportDate,
Hours = SUM(Hours), 
Placement = SUM(Placement), 
VideoShowings = SUM(VideoShowings), 
ReturnVisits = SUM(ReturnVisits), 
BibleStudies = SUM(BibleStudies)
FROM Reports 
INNER JOIN Publishers
ON Reports.PublisherId = Publishers.Id
INNER JOIN AppointedPublisher
ON AppointedPublisher.PublishersId = Publishers.Id
INNER JOIN Appointeds
ON Appointeds.Id = AppointedPublisher.AppointedsId
WHERE 
month(ReportDate) = month('02/13/2024') and year(ReportDate) = year('02/13/2024')
AND
Appointeds.Name IN ('Auxiliary Pioneer')
GROUP BY GROUPING SETS ( (FirstName, LastName, GroupId, Appointeds.Name, ReportDate), () )
ORDER By
GroupId, LastName, FirstName


--pioneer reports total with data
SELECT 
FirstName, 
LastName, 
GroupId as 'Group', 
Appointeds.Name AS Appointed, 
FORMAT (ReportDate, 'MMMM yyyy') as ReportDate,
Hours = SUM(Hours), 
Placement = SUM(Placement), 
VideoShowings = SUM(VideoShowings), 
ReturnVisits = SUM(ReturnVisits), 
BibleStudies = SUM(BibleStudies)
FROM Reports 
INNER JOIN Publishers
ON Reports.PublisherId = Publishers.Id
INNER JOIN AppointedPublisher
ON AppointedPublisher.PublishersId = Publishers.Id
INNER JOIN Appointeds
ON Appointeds.Id = AppointedPublisher.AppointedsId
WHERE 
month(ReportDate) = month('02/13/2024') and year(ReportDate) = year('02/13/2024')
AND
Appointeds.Name IN ('Pioneer')
GROUP BY GROUPING SETS ( (FirstName, LastName, GroupId, Appointeds.Name, ReportDate), () )
ORDER By
GroupId, LastName, FirstName


--Average Hours
  SELECT 
  PublisherId,
 MAX([FirstName]) AS FirstName,
 MAX([LastName]) AS LastName,
 MAX([Group]) AS FirstName,
  ROUND(AVG([Hours]),2) AS AverageHours 
  FROM [publishers-record-db].[dbo].[PioneerReport]
  WHERE 
  month(ReportDate) = month('02/13/2024') and year(ReportDate) = year('02/13/2024') --months to average at
  AND [GROUP] = 3
  GROUP BY PublisherId;



  --Selecting publisher, pioneers
  SELECT TOP (1000) [Id] as PublisherId
  ,[LastName]
      ,[FirstName]
  FROM [publishers-record-db].[dbo].[Publishers]
  WHERE 1=1
  AND IsActive = 1
  AND GroupId = 7
  --AND Id NOT IN(SELECT PublishersId FROM AppointedPublisher WHERE AppointedsId = 3) --Publisher Report
  AND Id IN(SELECT PublishersId FROM AppointedPublisher WHERE AppointedsId = 3) -- Pioneer Report
  ORDER BY LastName, FirstName



--AVERAGE HOURS PER Publisher By DATE
  SELECT 
PublisherId, 
LastName, 
FirstName, 
GroupId as 'Group', 
Hours, 
BibleStudies,
FORMAT (ReportDate, 'MMMM yyyy') as ReportDate,
ReportDate as ReportDateSort
FROM Reports 
INNER JOIN Publishers
ON Reports.PublisherId = Publishers.Id
INNER JOIN AppointedPublisher
ON AppointedPublisher.PublishersId = Publishers.Id
INNER JOIN Appointeds
ON Appointeds.Id = AppointedPublisher.AppointedsId
WHERE 
(Reports.ReportingAs = 'RegularPioneer'
OR
Appointeds.Name IN ('Pioneer'))
AND Appointeds.Name NOT IN ('Publisher','Elder', 'Ministerial Servant', 'Unbaptized Publisher')
--AND ReportDate BETWEEN '08/01/2023' AND '02/01/2024'
AND month(ReportDate) = month('01/13/2023') and year(ReportDate) = year('08/13/2024')
ORDER BY LastName, FirstName
--AND month(ReportDate) IN (month('08/13/2023'),month('09/13/2023'),month('10/13/2023'),month('11/13/2023'),month('12/13/2023'),month('01/13/2024'))
--AND YEAR(ReportDate) IN ('2023','2024')
--AND ReportDate IN ('August 2023','September 2023','October 2023','November 2023','December 2023','January 2024')


---PUBLISHERS HISTORICAL REPORTS AND AVERAGE
SELECT 
Reports.Id,
[Hours] ,
HasParticipated,
[BibleStudies] ,
FORMAT (ReportDate, 'MMMM yyyy') AS ReportDate
FROM [publishers-record-db].[dbo].[Reports]
INNER JOIN Publishers ON Reports.PublisherId = Publishers.Id
WHERE PublisherId = 3124
ORDER BY [Reports].ReportDate


SELECT PublisherId,
       MAX([FirstName]) AS FirstName,
       MAX([LastName]) AS LastName
       --ROUND(AVG([Hours]), 2) AS AverageHours
FROM [publishers-record-db].[dbo].[PubAndUnPubReport]
WHERE PublisherId = 3124
GROUP BY PublisherId



--GET ALL ACTIVE PUBLISHERS
SELECT 
Id,
FirstName,
MiddleName,
LastName
FROM Publishers
WHERE 1=1
AND IsActive = 1
ORDER BY LastName, FirstName





  Id	Name	Description
1	Elder	
2	Ministerial Servant	
3	Pioneer	
5	Publisher	
6	Unbaptized Publisher	