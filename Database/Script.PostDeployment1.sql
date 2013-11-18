/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
INSERT INTO [dbo].[UserStatistic]( [EmailAddress], [Location], [AgresivityRate], [DateTime])
SELECT  N'232@yahoo.com', N'Sibiu', 56, '20131116 18:41:37.190' UNION ALL
SELECT  N'232@yahoo.com', N'Sibiu', 78, '20131116 18:42:30.370' UNION ALL
SELECT  N'232@yahoo.com', N'Sibiu', 12, '20131116 18:42:30.370' UNION ALL
SELECT  N'232@yahoo.com', N'Sibiu', 56, '20131116 18:42:30.370' UNION ALL
SELECT  N'232@yahoo.com', N'Brasov', 23, '20131116 18:42:40.920' UNION ALL
SELECT  N'232@yahoo.com', N'Brasov', 88, '20131116 18:42:40.920' UNION ALL
SELECT  N'232@yahoo.com', N'Brasov', 1, '20131116 18:42:40.920' UNION ALL
SELECT  N'232@yahoo.com', N'Timisoara', 89, '20131116 18:42:48.030' UNION ALL
SELECT  N'232@yahoo.com', N'Timisoara', 78, '20131116 18:42:48.030' UNION ALL
SELECT  N'232@yahoo.com', N'Timisoara', 80, '20131116 18:42:48.030' UNION ALL
SELECT  N'232@yahoo.com', N'Constanta', 34, '20131116 18:42:54.320' UNION ALL
SELECT  N'232@yahoo.com', N'Constanta', 36, '20131116 18:42:54.320' UNION ALL
SELECT  N'232@yahoo.com', N'Constanta', 35, '20131116 18:42:54.320' UNION ALL
SELECT  N'232@yahoo.com', N'Constanta', 31, '20131116 18:42:54.320' UNION ALL
SELECT  N'232@yahoo.com', N'Cluj - Napoca', 99, '20131116 18:43:28.080' UNION ALL
SELECT  N'232@yahoo.com', N'Cluj - Napoca', 99, '20131116 18:43:28.080' UNION ALL
SELECT  N'232@yahoo.com', N'Cluj - Napoca', 1, '20131116 18:43:28.080' UNION ALL
SELECT  N'232@yahoo.com', N'Cluj - Napoca', 4, '20131116 18:43:28.080' UNION ALL
SELECT  N'232@yahoo.com', N'Alba - Iulia', 1, '20131116 18:43:28.080' UNION ALL
SELECT  N'232@yahoo.com', N'Bucuresti', 99, '20131116 18:43:37.317' UNION ALL
SELECT  N'Cluj - Napoca', N'Bucuresti', 99, '20131116 18:43:37.317' UNION ALL
SELECT  N'Cluj - Napoca', N'Bucuresti', 99, '20131116 18:43:37.317' UNION ALL
SELECT  N'Cluj - Napoca', N'Bucuresti', 99, '20131116 18:43:37.317' UNION ALL
SELECT  N'Cluj - Napoca', N'Bucuresti', 98, '20131116 18:43:37.317'