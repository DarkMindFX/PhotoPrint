


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
    public class TestMaterialTypeDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IMaterialTypeDal dal = new MaterialTypeDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void MaterialType_GetAll_Success()
        {
            var dal = PrepareMaterialTypeDal("DALInitParams");

            IList<MaterialType> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("MaterialType\\000.GetDetails.Success")]
        public void MaterialType_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareMaterialTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            MaterialType entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("MaterialTypeName b45e9f05fcb840bf83b48e87b8206552", entity.MaterialTypeName);
                            Assert.AreEqual("Description b45e9f05fcb840bf83b48e87b8206552", entity.Description);
                            Assert.AreEqual("ThumbnailUrl b45e9f05fcb840bf83b48e87b8206552", entity.ThumbnailUrl);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("11/30/2019 6:25:39 AM"), entity.CreatedDate);
                            Assert.AreEqual(100002, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("4/7/2019 6:01:39 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100005, entity.ModifiedByID);
                      }

        [Test]
        public void MaterialType_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareMaterialTypeDal("DALInitParams");

            MaterialType entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("MaterialType\\010.Delete.Success")]
        public void MaterialType_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareMaterialTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void MaterialType_Delete_InvalidId()
        {
            var dal = PrepareMaterialTypeDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("MaterialType\\020.Insert.Success")]
        public void MaterialType_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareMaterialTypeDal("DALInitParams");

            var entity = new MaterialType();
                          entity.MaterialTypeName = "MaterialTypeName 5cebfca2560d407d9ab49cfa701e2755";
                            entity.Description = "Description 5cebfca2560d407d9ab49cfa701e2755";
                            entity.ThumbnailUrl = "ThumbnailUrl 5cebfca2560d407d9ab49cfa701e2755";
                            entity.IsDeleted = false;              
                            entity.CreatedDate = DateTime.Parse("12/17/2021 11:52:39 AM");
                            entity.CreatedByID = 100009;
                            entity.ModifiedDate = DateTime.Parse("5/7/2019 9:38:39 PM");
                            entity.ModifiedByID = 100001;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("MaterialTypeName 5cebfca2560d407d9ab49cfa701e2755", entity.MaterialTypeName);
                            Assert.AreEqual("Description 5cebfca2560d407d9ab49cfa701e2755", entity.Description);
                            Assert.AreEqual("ThumbnailUrl 5cebfca2560d407d9ab49cfa701e2755", entity.ThumbnailUrl);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("12/17/2021 11:52:39 AM"), entity.CreatedDate);
                            Assert.AreEqual(100009, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("5/7/2019 9:38:39 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100001, entity.ModifiedByID);
              
        }

        [TestCase("MaterialType\\030.Update.Success")]
        public void MaterialType_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareMaterialTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            MaterialType entity = dal.Get(paramID);

                          entity.MaterialTypeName = "MaterialTypeName ec14c354d5834b36aa92a129b97f7332";
                            entity.Description = "Description ec14c354d5834b36aa92a129b97f7332";
                            entity.ThumbnailUrl = "ThumbnailUrl ec14c354d5834b36aa92a129b97f7332";
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("3/18/2022 7:25:39 AM");
                            entity.CreatedByID = 100009;
                            entity.ModifiedDate = DateTime.Parse("8/5/2019 7:52:39 AM");
                            entity.ModifiedByID = 100010;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("MaterialTypeName ec14c354d5834b36aa92a129b97f7332", entity.MaterialTypeName);
                            Assert.AreEqual("Description ec14c354d5834b36aa92a129b97f7332", entity.Description);
                            Assert.AreEqual("ThumbnailUrl ec14c354d5834b36aa92a129b97f7332", entity.ThumbnailUrl);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("3/18/2022 7:25:39 AM"), entity.CreatedDate);
                            Assert.AreEqual(100009, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("8/5/2019 7:52:39 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100010, entity.ModifiedByID);
              
        }

        [Test]
        public void MaterialType_Update_InvalidId()
        {
            var dal = PrepareMaterialTypeDal("DALInitParams");

            var entity = new MaterialType();
                          entity.MaterialTypeName = "MaterialTypeName ec14c354d5834b36aa92a129b97f7332";
                            entity.Description = "Description ec14c354d5834b36aa92a129b97f7332";
                            entity.ThumbnailUrl = "ThumbnailUrl ec14c354d5834b36aa92a129b97f7332";
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("3/18/2022 7:25:39 AM");
                            entity.CreatedByID = 100009;
                            entity.ModifiedDate = DateTime.Parse("8/5/2019 7:52:39 AM");
                            entity.ModifiedByID = 100010;
              
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

        [TestCase("MaterialType\\040.Erase.Success")]
        public void MaterialType_Erase_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareMaterialTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Erase(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void MaterialType_Erase_InvalidId()
        {
            var dal = PrepareMaterialTypeDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Erase(paramID);
            Assert.IsFalse(removed);

        }

        protected IMaterialTypeDal PrepareMaterialTypeDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IMaterialTypeDal dal = new MaterialTypeDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
