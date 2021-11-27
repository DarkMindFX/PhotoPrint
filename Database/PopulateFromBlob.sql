
/*
EXEC p_TestData_Cleanup
*/



CREATE MASTER KEY ENCRYPTION BY PASSWORD ='PhotoPrint2021!'

CREATE DATABASE SCOPED CREDENTIAL UploadPhotoPrintTestData
WITH IDENTITY = 'SHARED ACCESS SIGNATURE',
SECRET = 'sp=r&st=2021-11-27T21:36:16Z&se=2022-11-28T05:36:16Z&sv=2020-08-04&sr=c&sig=sVXC3IlTGkwaQn7z7dCJ8l4UkbuLqczCvCefHr7Qcmo%3D';

CREATE EXTERNAL DATA SOURCE PhotoPrint_Azure_TestData
WITH (
        TYPE = BLOB_STORAGE,
        LOCATION = 'https://photoprintstorage.blob.core.windows.net',
        CREDENTIAL = UploadPhotoPrintTestData
    );
GO 

/*
SELECT * FROM OPENROWSET(
   BULK 'photoprintdb-test-data/User.csv',
   DATA_SOURCE = 'PhotoPrint_Azure_TestData',
   FORMAT = 'CSV',
   FIRSTROW = 2,
   FORMATFILE='photoprintdb-test-data/User.fmt',
   FORMATFILE_DATA_SOURCE = 'PhotoPrint_Azure_TestData'
   ) AS DataFile; 
   */

EXEC p_TestData_Populate 'photoprintdb-test-data/', 'PhotoPrint_Azure_TestData'

DROP EXTERNAL DATA SOURCE PhotoPrint_Azure_TestData

DROP DATABASE SCOPED CREDENTIAL UploadPhotoPrintTestData

DROP MASTER KEY