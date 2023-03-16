--report specific date (change the date based on the report date that you wanted)
 WHERE month(ReportDate) = month('2/2/2023') and year(ReportDate) = year('2/2/2023')


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
month(ReportDate) = month('2/2/2023') and year(ReportDate) = year('2/2/2023')
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
INNER JOIN AppointedPublisher
ON AppointedPublisher.PublishersId = Publishers.Id
INNER JOIN Appointeds
ON Appointeds.Id = AppointedPublisher.AppointedsId
WHERE 
month(ReportDate) = month('2/2/2023') and year(ReportDate) = year('2/2/2023')
AND
Appointeds.Name IN ('Auxiliary Pioneer')


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
month(ReportDate) = month('2/2/2023') and year(ReportDate) = year('2/2/2023')
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
month(ReportDate) = month('2/2/2023') and year(ReportDate) = year('2/2/2023')
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
month(ReportDate) = month('2/2/2023') and year(ReportDate) = year('2/2/2023')
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
month(ReportDate) = month('2/2/2023') and year(ReportDate) = year('2/2/2023')
AND
Appointeds.Name IN ('Pioneer')
GROUP BY GROUPING SETS ( (FirstName, LastName, GroupId, Appointeds.Name, ReportDate), () )
ORDER By
GroupId, LastName, FirstName