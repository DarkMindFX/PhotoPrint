

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
            
                          Assert.AreEqual("MaterialTypeName 71a2e09e35964b4f8f301b1ab9b56e61", entity.MaterialTypeName);
                            Assert.AreEqual("Description 71a2e09e35964b4f8f301b1ab9b56e61", entity.Description);
                            Assert.AreEqual("ThumbnailUrl 71a2e09e35964b4f8f301b1ab9b56e61", entity.ThumbnailUrl);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("2/1/2021 9:57:48 AM"), entity.CreatedDate);
                            Assert.AreEqual(100002, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("1/19/2023 11:46:48 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100001, entity.ModifiedByID);
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
                          entity.MaterialTypeName = "MaterialTypeName 4d5a2e53fdda4f30b98e50a1f0597014";
                            entity.Description = "Description 4d5a2e53fdda4f30b98e50a1f0597014";
                            entity.ThumbnailUrl = "ThumbnailUrl 4d5a2e53fdda4f30b98e50a1f0597014";
                            entity.IsDeleted = false;              
                            entity.CreatedDate = DateTime.Parse("12/29/2021 9:12:48 AM");
                            entity.CreatedByID = 100010;
                            entity.ModifiedDate = DateTime.Parse("5/19/2019 6:59:48 PM");
                            entity.ModifiedByID = 100002;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("MaterialTypeName 4d5a2e53fdda4f30b98e50a1f0597014", entity.MaterialTypeName);
                            Assert.AreEqual("Description 4d5a2e53fdda4f30b98e50a1f0597014", entity.Description);
                            Assert.AreEqual("ThumbnailUrl 4d5a2e53fdda4f30b98e50a1f0597014", entity.ThumbnailUrl);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("12/29/2021 9:12:48 AM"), entity.CreatedDate);
                            Assert.AreEqual(100010, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("5/19/2019 6:59:48 PM"), entity.ModifiedDate);
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

                          entity.MaterialTypeName = "MaterialTypeName a0a7eb608cfc4a729313a62a0ba671a4";
                            entity.Description = "Description a0a7eb608cfc4a729313a62a0ba671a4";
                            entity.ThumbnailUrl = "ThumbnailUrl a0a7eb608cfc4a729313a62a0ba671a4";
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("8/18/2019 2:33:48 PM");
                            entity.CreatedByID = 100007;
                            entity.ModifiedDate = DateTime.Parse("8/18/2019 2:33:48 PM");
                            entity.ModifiedByID = 100001;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("MaterialTypeName a0a7eb608cfc4a729313a62a0ba671a4", entity.MaterialTypeName);
                            Assert.AreEqual("Description a0a7eb608cfc4a729313a62a0ba671a4", entity.Description);
                            Assert.AreEqual("ThumbnailUrl a0a7eb608cfc4a729313a62a0ba671a4", entity.ThumbnailUrl);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("8/18/2019 2:33:48 PM"), entity.CreatedDate);
                            Assert.AreEqual(100007, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("8/18/2019 2:33:48 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100001, entity.ModifiedByID);
              
        }

        [Test]
        public void MaterialType_Update_InvalidId()
        {
            var dal = PrepareMaterialTypeDal("DALInitParams");

            var entity = new MaterialType();
                          entity.MaterialTypeName = "MaterialTypeName a0a7eb608cfc4a729313a62a0ba671a4";
                            entity.Description = "Description a0a7eb608cfc4a729313a62a0ba671a4";
                            entity.ThumbnailUrl = "ThumbnailUrl a0a7eb608cfc4a729313a62a0ba671a4";
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("8/18/2019 2:33:48 PM");
                            entity.CreatedByID = 100007;
                            entity.ModifiedDate = DateTime.Parse("8/18/2019 2:33:48 PM");
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
