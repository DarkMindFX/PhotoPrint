

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
            
                          Assert.AreEqual("MaterialTypeName 9ee2fdeae8544ae3a4508890f95b78ca", entity.MaterialTypeName);
                            Assert.AreEqual("Description 9ee2fdeae8544ae3a4508890f95b78ca", entity.Description);
                            Assert.AreEqual("ThumbnailUrl 9ee2fdeae8544ae3a4508890f95b78ca", entity.ThumbnailUrl);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("10/1/2021 4:11:33 PM"), entity.CreatedDate);
                            Assert.AreEqual(100010, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("10/1/2021 4:11:33 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100003, entity.ModifiedByID);
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
                          entity.MaterialTypeName = "MaterialTypeName c3f3f23d4a4046dcb460f55b86efc636";
                            entity.Description = "Description c3f3f23d4a4046dcb460f55b86efc636";
                            entity.ThumbnailUrl = "ThumbnailUrl c3f3f23d4a4046dcb460f55b86efc636";
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("12/31/2021 11:45:33 AM");
                            entity.CreatedByID = 100004;
                            entity.ModifiedDate = DateTime.Parse("5/20/2019 12:11:33 PM");
                            entity.ModifiedByID = 100002;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("MaterialTypeName c3f3f23d4a4046dcb460f55b86efc636", entity.MaterialTypeName);
                            Assert.AreEqual("Description c3f3f23d4a4046dcb460f55b86efc636", entity.Description);
                            Assert.AreEqual("ThumbnailUrl c3f3f23d4a4046dcb460f55b86efc636", entity.ThumbnailUrl);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("12/31/2021 11:45:33 AM"), entity.CreatedDate);
                            Assert.AreEqual(100004, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("5/20/2019 12:11:33 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100002, entity.ModifiedByID);
              
        }

        [TestCase("MaterialType\\030.Update.Success")]
        public void MaterialType_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareMaterialTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            MaterialType entity = dal.Get(paramID);

                          entity.MaterialTypeName = "MaterialTypeName ff2d9853a4d040fdae18acb35bfd1865";
                            entity.Description = "Description ff2d9853a4d040fdae18acb35bfd1865";
                            entity.ThumbnailUrl = "ThumbnailUrl ff2d9853a4d040fdae18acb35bfd1865";
                            entity.IsDeleted = false;              
                            entity.CreatedDate = DateTime.Parse("6/27/2022 8:12:33 AM");
                            entity.CreatedByID = 100007;
                            entity.ModifiedDate = DateTime.Parse("6/27/2022 8:12:33 AM");
                            entity.ModifiedByID = 100008;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("MaterialTypeName ff2d9853a4d040fdae18acb35bfd1865", entity.MaterialTypeName);
                            Assert.AreEqual("Description ff2d9853a4d040fdae18acb35bfd1865", entity.Description);
                            Assert.AreEqual("ThumbnailUrl ff2d9853a4d040fdae18acb35bfd1865", entity.ThumbnailUrl);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("6/27/2022 8:12:33 AM"), entity.CreatedDate);
                            Assert.AreEqual(100007, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("6/27/2022 8:12:33 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100008, entity.ModifiedByID);
              
        }

        [Test]
        public void MaterialType_Update_InvalidId()
        {
            var dal = PrepareMaterialTypeDal("DALInitParams");

            var entity = new MaterialType();
                          entity.MaterialTypeName = "MaterialTypeName ff2d9853a4d040fdae18acb35bfd1865";
                            entity.Description = "Description ff2d9853a4d040fdae18acb35bfd1865";
                            entity.ThumbnailUrl = "ThumbnailUrl ff2d9853a4d040fdae18acb35bfd1865";
                            entity.IsDeleted = false;              
                            entity.CreatedDate = DateTime.Parse("6/27/2022 8:12:33 AM");
                            entity.CreatedByID = 100007;
                            entity.ModifiedDate = DateTime.Parse("6/27/2022 8:12:33 AM");
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
