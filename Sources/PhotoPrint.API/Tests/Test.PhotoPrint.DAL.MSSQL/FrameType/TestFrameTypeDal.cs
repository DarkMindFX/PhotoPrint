

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
            
                          Assert.AreEqual("FrameTypeName a7d018b621694117bced296decee40e4", entity.FrameTypeName);
                            Assert.AreEqual("Description a7d018b621694117bced296decee40e4", entity.Description);
                            Assert.AreEqual("ThumbnailUrl a7d018b621694117bced296decee40e4", entity.ThumbnailUrl);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("11/29/2021 10:44:48 AM"), entity.CreatedDate);
                            Assert.AreEqual(100006, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("11/16/2019 5:55:48 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100004, entity.ModifiedByID);
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
                          entity.FrameTypeName = "FrameTypeName 67d627ea74fd43009fd7cf847007a207";
                            entity.Description = "Description 67d627ea74fd43009fd7cf847007a207";
                            entity.ThumbnailUrl = "ThumbnailUrl 67d627ea74fd43009fd7cf847007a207";
                            entity.IsDeleted = false;              
                            entity.CreatedDate = DateTime.Parse("5/14/2020 11:42:48 PM");
                            entity.CreatedByID = 100007;
                            entity.ModifiedDate = DateTime.Parse("5/14/2020 11:42:48 PM");
                            entity.ModifiedByID = 100002;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("FrameTypeName 67d627ea74fd43009fd7cf847007a207", entity.FrameTypeName);
                            Assert.AreEqual("Description 67d627ea74fd43009fd7cf847007a207", entity.Description);
                            Assert.AreEqual("ThumbnailUrl 67d627ea74fd43009fd7cf847007a207", entity.ThumbnailUrl);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("5/14/2020 11:42:48 PM"), entity.CreatedDate);
                            Assert.AreEqual(100007, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("5/14/2020 11:42:48 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100002, entity.ModifiedByID);
              
        }

        [TestCase("FrameType\\030.Update.Success")]
        public void FrameType_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareFrameTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            FrameType entity = dal.Get(paramID);

                          entity.FrameTypeName = "FrameTypeName 44bda55972ac4d5cb814db2670544b49";
                            entity.Description = "Description 44bda55972ac4d5cb814db2670544b49";
                            entity.ThumbnailUrl = "ThumbnailUrl 44bda55972ac4d5cb814db2670544b49";
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("8/11/2020 9:56:48 AM");
                            entity.CreatedByID = 100003;
                            entity.ModifiedDate = DateTime.Parse("8/11/2020 9:56:48 AM");
                            entity.ModifiedByID = 100007;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("FrameTypeName 44bda55972ac4d5cb814db2670544b49", entity.FrameTypeName);
                            Assert.AreEqual("Description 44bda55972ac4d5cb814db2670544b49", entity.Description);
                            Assert.AreEqual("ThumbnailUrl 44bda55972ac4d5cb814db2670544b49", entity.ThumbnailUrl);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("8/11/2020 9:56:48 AM"), entity.CreatedDate);
                            Assert.AreEqual(100003, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("8/11/2020 9:56:48 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100007, entity.ModifiedByID);
              
        }

        [Test]
        public void FrameType_Update_InvalidId()
        {
            var dal = PrepareFrameTypeDal("DALInitParams");

            var entity = new FrameType();
                          entity.FrameTypeName = "FrameTypeName 44bda55972ac4d5cb814db2670544b49";
                            entity.Description = "Description 44bda55972ac4d5cb814db2670544b49";
                            entity.ThumbnailUrl = "ThumbnailUrl 44bda55972ac4d5cb814db2670544b49";
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("8/11/2020 9:56:48 AM");
                            entity.CreatedByID = 100003;
                            entity.ModifiedDate = DateTime.Parse("8/11/2020 9:56:48 AM");
                            entity.ModifiedByID = 100007;
              
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
