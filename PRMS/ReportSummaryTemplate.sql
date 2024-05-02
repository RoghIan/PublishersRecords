--REGULAR PIONEER REPORT
SELECT 
      [PublisherId]
      ,[FirstName]
      ,[LastName]
      ,[Group]
      ,[Hours]
      ,[BibleStudies]
      ,[HasParticipated]
      ,[ReportDate]
FROM [publishers-record-db].[dbo].[PioneerReport]
WHERE month(ReportDate) = month('04/13/2024') and year(ReportDate) = year('04/13/2024')
--TOTAL
SELECT
		SUM(CAST([publishers-record-db].[dbo].[PioneerReport].[Hours] AS INT) ) TotalHours,
		SUM(BibleStudies) TotalBibleStudies
FROM [publishers-record-db].[dbo].[PioneerReport]
WHERE month(ReportDate) = month('04/13/2024') and year(ReportDate) = year('04/13/2024')

------------------------------------------------------------------------------------------

--AUXILIARY PIONEER REPORT
SELECT 
      [PublisherId]
      ,[FirstName]
      ,[LastName]
      ,[Group]
      ,[Hours]
      ,[BibleStudies]
      ,[ReportDate]
FROM [publishers-record-db].[dbo].[AuxiReport]
WHERE month(ReportDate) = month('04/13/2024') and year(ReportDate) = year('04/13/2024')
--TOTAL
SELECT
		SUM(CAST([publishers-record-db].[dbo].[AuxiReport].[Hours] AS INT) ) TotalHours,
		SUM(BibleStudies) TotalBibleStudies
FROM [publishers-record-db].[dbo].[AuxiReport]
WHERE month(ReportDate) = month('04/13/2024') and year(ReportDate) = year('04/13/2024')

------------------------------------------------------------------------------------------

--PUBLISHER AND UNBAPTIZED REPORT
SELECT
       [PublisherId]
      ,[FirstName]
      ,[LastName]
      ,[Group]
      ,[BibleStudies]
      ,[HasParticipated]
      ,[Appointed]
      ,[ReportDate]
FROM [publishers-record-db].[dbo].[PubAndUnPubReport]
WHERE month(ReportDate) = month('04/13/2024') and year(ReportDate) = year('04/13/2024')
--TOTAL
SELECT
		SUM(BibleStudies) TotalBibleStudies
FROM [publishers-record-db].[dbo].[PubAndUnPubReport]
WHERE month(ReportDate) = month('04/13/2024') and year(ReportDate) = year('04/13/2024')

------------------------------------------------------------------------------------------

--PUBLISHER WIHTOUT REPORT
SELECT 
       [Id]
      ,[FirstName]
      ,[LastName]
      ,[GroupId]
      ,[ReportDate]
FROM [publishers-record-db].[dbo].[PublisherWithoutReport]
WHERE month(ReportDate) = month('04/13/2024') and year(ReportDate) = year('04/13/2024')

------------------------------------------------------------------------------------------

--TOTAL NUMBER OF REPORTING
SELECT COUNT(*) AS TOTAL_NUMBER_OF_REPORTING
FROM [publishers-record-db].[dbo].Reports
WHERE month(ReportDate) = month('04/13/2024') and year(ReportDate) = year('04/13/2024')
AND
Id IS NOT NULL
AND 
Reports.HasParticipated = 1

------------------------------------------------------------------------------------------

--PUBLISHERS THAT HAS NOT REPORTED YET
SELECT 
ROW_NUMBER() OVER (ORDER BY GroupId, LastName, FirstName DESC) AS 'Count',
Publishers.Id, 
FirstName, 
LastName, 
GroupId
FROM Publishers
  WHERE 1 = 1 
  AND IsActive = 1
  AND Id NOT IN (SELECT Reports.PublisherId FROM Reports WHERE month(ReportDate) = month('04/13/2024') and year(ReportDate) = year('04/13/2024'))