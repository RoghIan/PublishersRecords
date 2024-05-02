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
WHERE month(ReportDate) = month('03/13/2024') and year(ReportDate) = year('03/13/2024')
--TOTAL
SELECT
		SUM(CAST([publishers-record-db].[dbo].[PioneerReport].[Hours] AS INT) ) TotalHours,
		SUM(BibleStudies) TotalBibleStudies
FROM [publishers-record-db].[dbo].[PioneerReport]
WHERE month(ReportDate) = month('03/13/2024') and year(ReportDate) = year('03/13/2024')

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
WHERE month(ReportDate) = month('03/13/2024') and year(ReportDate) = year('03/13/2024')
--TOTAL
SELECT
		SUM(CAST([publishers-record-db].[dbo].[AuxiReport].[Hours] AS INT) ) TotalHours,
		SUM(BibleStudies) TotalBibleStudies
FROM [publishers-record-db].[dbo].[AuxiReport]
WHERE month(ReportDate) = month('03/13/2024') and year(ReportDate) = year('03/13/2024')

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
WHERE month(ReportDate) = month('03/13/2024') and year(ReportDate) = year('03/13/2024')
--TOTAL
SELECT
		SUM(BibleStudies) TotalBibleStudies
FROM [publishers-record-db].[dbo].[PubAndUnPubReport]
WHERE month(ReportDate) = month('03/13/2024') and year(ReportDate) = year('03/13/2024')

------------------------------------------------------------------------------------------

--PUBLISHER WIHTOUT REPORT
SELECT 
       [Id]
      ,[FirstName]
      ,[LastName]
      ,[GroupId]
      ,[ReportDate]
FROM [publishers-record-db].[dbo].[PublisherWithoutReport]
WHERE month(ReportDate) = month('03/13/2024') and year(ReportDate) = year('03/13/2024')

------------------------------------------------------------------------------------------

--TOTAL NUMBER OF REPORTING
SELECT COUNT(*) AS TOTAL_NUMBER_OF_REPORTING
FROM [publishers-record-db].[dbo].Reports
WHERE month(ReportDate) = month('03/13/2024') and year(ReportDate) = year('03/13/2024')
AND
Id IS NOT NULL
AND 
Reports.HasParticipated = 1
