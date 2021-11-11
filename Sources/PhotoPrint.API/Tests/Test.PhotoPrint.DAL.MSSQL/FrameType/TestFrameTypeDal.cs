


using PPT.DAL.MSSQL;
using PPT.Interfaces;
using PPT.Interfaces.Entities;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Test.PPT.Common.DAL;

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
            
                          Assert.AreEqual("FrameTypeName d767ac90e7df4e85afcb7de69bc7cc68", entity.FrameTypeName);
                            Assert.AreEqual("Description d767ac90e7df4e85afcb7de69bc7cc68", entity.Description);
                            Assert.AreEqual("ThumbnailUrl d767ac90e7df4e85afcb7de69bc7cc68", entity.ThumbnailUrl);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("1/16/2021 7:47:39 AM"), entity.CreatedDate);
                            Assert.AreEqual(100008, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("11/27/2019 10:55:39 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100002, entity.ModifiedByID);
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
                          entity.FrameTypeName = "FrameTypeName 43cbcc5573c542cb9590857915ab90e7";
                            entity.Description = "Description 43cbcc5573c542cb9590857915ab90e7";
                            entity.ThumbnailUrl = "ThumbnailUrl 43cbcc5573c542cb9590857915ab90e7";
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("2/4/2023 10:33:39 PM");
                            entity.CreatedByID = 100003;
                            entity.ModifiedDate = DateTime.Parse("2/4/2023 10:33:39 PM");
                            entity.ModifiedByID = 100006;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("FrameTypeName 43cbcc5573c542cb9590857915ab90e7", entity.FrameTypeName);
                            Assert.AreEqual("Description 43cbcc5573c542cb9590857915ab90e7", entity.Description);
                            Assert.AreEqual("ThumbnailUrl 43cbcc5573c542cb9590857915ab90e7", entity.ThumbnailUrl);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("2/4/2023 10:33:39 PM"), entity.CreatedDate);
                            Assert.AreEqual(100003, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("2/4/2023 10:33:39 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100006, entity.ModifiedByID);
              
        }

        [TestCase("FrameType\\030.Update.Success")]
        public void FrameType_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareFrameTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            FrameType entity = dal.Get(paramID);

                          entity.FrameTypeName = "FrameTypeName 4fb71f46467d402bb0a749cd3563f1e9";
                            entity.Description = "Description 4fb71f46467d402bb0a749cd3563f1e9";
                            entity.ThumbnailUrl = "ThumbnailUrl 4fb71f46467d402bb0a749cd3563f1e9";
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("6/25/2020 8:20:39 AM");
                            entity.CreatedByID = 100002;
                            entity.ModifiedDate = DateTime.Parse("6/25/2020 8:20:39 AM");
                            entity.ModifiedByID = 100008;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("FrameTypeName 4fb71f46467d402bb0a749cd3563f1e9", entity.FrameTypeName);
                            Assert.AreEqual("Description 4fb71f46467d402bb0a749cd3563f1e9", entity.Description);
                            Assert.AreEqual("ThumbnailUrl 4fb71f46467d402bb0a749cd3563f1e9", entity.ThumbnailUrl);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("6/25/2020 8:20:39 AM"), entity.CreatedDate);
                            Assert.AreEqual(100002, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("6/25/2020 8:20:39 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100008, entity.ModifiedByID);
              
        }

        [Test]
        public void FrameType_Update_InvalidId()
        {
            var dal = PrepareFrameTypeDal("DALInitParams");

            var entity = new FrameType();
                          entity.FrameTypeName = "FrameTypeName 4fb71f46467d402bb0a749cd3563f1e9";
                            entity.Description = "Description 4fb71f46467d402bb0a749cd3563f1e9";
                            entity.ThumbnailUrl = "ThumbnailUrl 4fb71f46467d402bb0a749cd3563f1e9";
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("6/25/2020 8:20:39 AM");
                            entity.CreatedByID = 100002;
                            entity.ModifiedDate = DateTime.Parse("6/25/2020 8:20:39 AM");
                            entity.ModifiedByID = 100008;
              
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

        [TestCase("FrameType\\040.Erase.Success")]
        public void FrameType_Erase_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareFrameTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Erase(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void FrameType_Erase_InvalidId()
        {
            var dal = PrepareFrameTypeDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Erase(paramID);
            Assert.IsFalse(removed);

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
