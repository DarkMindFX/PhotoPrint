

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
    public class TestFrameTypeDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IFrameTypeDal dal = new FrameTypeDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void FrameType_GetAll_Success()
        {
            var dal = PrepareFrameTypeDal("DALInitParams");

            IList<FrameType> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("FrameType\\000.GetDetails.Success")]
        public void FrameType_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareFrameTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            FrameType entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("FrameTypeName 09dcd3d190ba4ffd9d0402b93410f21c", entity.FrameTypeName);
                            Assert.AreEqual("Description 09dcd3d190ba4ffd9d0402b93410f21c", entity.Description);
                            Assert.AreEqual("ThumbnailUrl 09dcd3d190ba4ffd9d0402b93410f21c", entity.ThumbnailUrl);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("4/13/2021 1:30:33 AM"), entity.CreatedDate);
                            Assert.AreEqual(100011, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("12/31/2022 5:06:33 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100006, entity.ModifiedByID);
                      }

        [Test]
        public void FrameType_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareFrameTypeDal("DALInitParams");

            FrameType entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("FrameType\\010.Delete.Success")]
        public void FrameType_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareFrameTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void FrameType_Delete_InvalidId()
        {
            var dal = PrepareFrameTypeDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("FrameType\\020.Insert.Success")]
        public void FrameType_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareFrameTypeDal("DALInitParams");

            var entity = new FrameType();
                          entity.FrameTypeName = "FrameTypeName f42c56bf99c742079f444d7825822602";
                            entity.Description = "Description f42c56bf99c742079f444d7825822602";
                            entity.ThumbnailUrl = "ThumbnailUrl f42c56bf99c742079f444d7825822602";
                            entity.IsDeleted = false;              
                            entity.CreatedDate = DateTime.Parse("8/18/2020 1:07:33 AM");
                            entity.CreatedByID = 100010;
                            entity.ModifiedDate = DateTime.Parse("6/29/2023 10:53:33 AM");
                            entity.ModifiedByID = 100009;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("FrameTypeName f42c56bf99c742079f444d7825822602", entity.FrameTypeName);
                            Assert.AreEqual("Description f42c56bf99c742079f444d7825822602", entity.Description);
                            Assert.AreEqual("ThumbnailUrl f42c56bf99c742079f444d7825822602", entity.ThumbnailUrl);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("8/18/2020 1:07:33 AM"), entity.CreatedDate);
                            Assert.AreEqual(100010, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("6/29/2023 10:53:33 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100009, entity.ModifiedByID);
              
        }

        [TestCase("FrameType\\030.Update.Success")]
        public void FrameType_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareFrameTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            FrameType entity = dal.Get(paramID);

                          entity.FrameTypeName = "FrameTypeName 217f233dd23d4b2b935c69add0f84f64";
                            entity.Description = "Description 217f233dd23d4b2b935c69add0f84f64";
                            entity.ThumbnailUrl = "ThumbnailUrl 217f233dd23d4b2b935c69add0f84f64";
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("11/15/2020 11:20:33 AM");
                            entity.CreatedByID = 100001;
                            entity.ModifiedDate = DateTime.Parse("2/13/2021 6:54:33 AM");
                            entity.ModifiedByID = 100001;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("FrameTypeName 217f233dd23d4b2b935c69add0f84f64", entity.FrameTypeName);
                            Assert.AreEqual("Description 217f233dd23d4b2b935c69add0f84f64", entity.Description);
                            Assert.AreEqual("ThumbnailUrl 217f233dd23d4b2b935c69add0f84f64", entity.ThumbnailUrl);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("11/15/2020 11:20:33 AM"), entity.CreatedDate);
                            Assert.AreEqual(100001, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("2/13/2021 6:54:33 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100001, entity.ModifiedByID);
              
        }

        [Test]
        public void FrameType_Update_InvalidId()
        {
            var dal = PrepareFrameTypeDal("DALInitParams");

            var entity = new FrameType();
                          entity.FrameTypeName = "FrameTypeName 217f233dd23d4b2b935c69add0f84f64";
                            entity.Description = "Description 217f233dd23d4b2b935c69add0f84f64";
                            entity.ThumbnailUrl = "ThumbnailUrl 217f233dd23d4b2b935c69add0f84f64";
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("11/15/2020 11:20:33 AM");
                            entity.CreatedByID = 100001;
                            entity.ModifiedDate = DateTime.Parse("2/13/2021 6:54:33 AM");
                            entity.ModifiedByID = 100001;
              
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

        protected IFrameTypeDal PrepareFrameTypeDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IFrameTypeDal dal = new FrameTypeDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
