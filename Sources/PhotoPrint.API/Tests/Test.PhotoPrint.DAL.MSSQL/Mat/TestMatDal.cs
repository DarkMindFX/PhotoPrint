

using PPT.DAL.MSSQL;
using PPT.Interfaces;
using PPT.Interfaces.Entities;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace Test.PPT.DAL.MSSQL
{
    public class TestMatDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IMatDal dal = new MatDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void Mat_GetAll_Success()
        {
            var dal = PrepareMatDal("DALInitParams");

            IList<Mat> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("Mat\\000.GetDetails.Success")]
        public void Mat_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareMatDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Mat entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("MatName 309a5a43c2ee47ae8f1a14445f5bc50b", entity.MatName);
                            Assert.AreEqual("Description 309a5a43c2ee47ae8f1a14445f5bc50b", entity.Description);
                            Assert.AreEqual("ThumbnailUrl 309a5a43c2ee47ae8f1a14445f5bc50b", entity.ThumbnailUrl);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("6/22/2022 7:34:33 PM"), entity.CreatedDate);
                            Assert.AreEqual(100009, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("2/2/2021 3:09:33 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100009, entity.ModifiedByID);
                      }

        [Test]
        public void Mat_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareMatDal("DALInitParams");

            Mat entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("Mat\\010.Delete.Success")]
        public void Mat_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareMatDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void Mat_Delete_InvalidId()
        {
            var dal = PrepareMatDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("Mat\\020.Insert.Success")]
        public void Mat_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareMatDal("DALInitParams");

            var entity = new Mat();
                          entity.MatName = "MatName 06d2a79252a343eb94702fef3d5c609a";
                            entity.Description = "Description 06d2a79252a343eb94702fef3d5c609a";
                            entity.ThumbnailUrl = "ThumbnailUrl 06d2a79252a343eb94702fef3d5c609a";
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("5/1/2021 1:23:33 PM");
                            entity.CreatedByID = 100009;
                            entity.ModifiedDate = DateTime.Parse("3/11/2024 11:09:33 PM");
                            entity.ModifiedByID = 100008;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("MatName 06d2a79252a343eb94702fef3d5c609a", entity.MatName);
                            Assert.AreEqual("Description 06d2a79252a343eb94702fef3d5c609a", entity.Description);
                            Assert.AreEqual("ThumbnailUrl 06d2a79252a343eb94702fef3d5c609a", entity.ThumbnailUrl);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("5/1/2021 1:23:33 PM"), entity.CreatedDate);
                            Assert.AreEqual(100009, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("3/11/2024 11:09:33 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100008, entity.ModifiedByID);
              
        }

        [TestCase("Mat\\030.Update.Success")]
        public void Mat_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareMatDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Mat entity = dal.Get(paramID);

                          entity.MatName = "MatName 8e0c1f540543439083e681042065c689";
                            entity.Description = "Description 8e0c1f540543439083e681042065c689";
                            entity.ThumbnailUrl = "ThumbnailUrl 8e0c1f540543439083e681042065c689";
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("6/9/2024 9:23:33 AM");
                            entity.CreatedByID = 100011;
                            entity.ModifiedDate = DateTime.Parse("6/9/2024 9:23:33 AM");
                            entity.ModifiedByID = 100003;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("MatName 8e0c1f540543439083e681042065c689", entity.MatName);
                            Assert.AreEqual("Description 8e0c1f540543439083e681042065c689", entity.Description);
                            Assert.AreEqual("ThumbnailUrl 8e0c1f540543439083e681042065c689", entity.ThumbnailUrl);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("6/9/2024 9:23:33 AM"), entity.CreatedDate);
                            Assert.AreEqual(100011, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("6/9/2024 9:23:33 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100003, entity.ModifiedByID);
              
        }

        [Test]
        public void Mat_Update_InvalidId()
        {
            var dal = PrepareMatDal("DALInitParams");

            var entity = new Mat();
                          entity.MatName = "MatName 8e0c1f540543439083e681042065c689";
                            entity.Description = "Description 8e0c1f540543439083e681042065c689";
                            entity.ThumbnailUrl = "ThumbnailUrl 8e0c1f540543439083e681042065c689";
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("6/9/2024 9:23:33 AM");
                            entity.CreatedByID = 100011;
                            entity.ModifiedDate = DateTime.Parse("6/9/2024 9:23:33 AM");
                            entity.ModifiedByID = 100003;
              
            try
            {
                entity = dal.Update(entity);

                Assert.Fail("Fail - exception was expected, but wasn't thrown.");
            }
            catch (Exception ex)
            {
                Assert.Pass("Success - exception thrown as expected");
            }
        }

        protected IMatDal PrepareMatDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IMatDal dal = new MatDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
