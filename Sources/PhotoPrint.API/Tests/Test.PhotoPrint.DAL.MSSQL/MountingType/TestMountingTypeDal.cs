


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
    public class TestMountingTypeDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IMountingTypeDal dal = new MountingTypeDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void MountingType_GetAll_Success()
        {
            var dal = PrepareMountingTypeDal("DALInitParams");

            IList<MountingType> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("MountingType\\000.GetDetails.Success")]
        public void MountingType_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareMountingTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            MountingType entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("MountingTypeName 4a56ba4dd22d47359d2a6fed6b9f3ea0", entity.MountingTypeName);
                            Assert.AreEqual("Description 4a56ba4dd22d47359d2a6fed6b9f3ea0", entity.Description);
                            Assert.AreEqual("ThumbnailUrl 4a56ba4dd22d47359d2a6fed6b9f3ea0", entity.ThumbnailUrl);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("2/12/2023 6:41:39 AM"), entity.CreatedDate);
                            Assert.AreEqual(732925, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("2/12/2023 6:41:39 AM"), entity.ModifiedDate);
                            Assert.AreEqual(732925, entity.ModifiedByID);
                      }

        [Test]
        public void MountingType_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareMountingTypeDal("DALInitParams");

            MountingType entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("MountingType\\010.Delete.Success")]
        public void MountingType_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareMountingTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void MountingType_Delete_InvalidId()
        {
            var dal = PrepareMountingTypeDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("MountingType\\020.Insert.Success")]
        public void MountingType_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareMountingTypeDal("DALInitParams");

            var entity = new MountingType();
                          entity.MountingTypeName = "MountingTypeName f64061bf7507407bb71e464fc35ca2e6";
                            entity.Description = "Description f64061bf7507407bb71e464fc35ca2e6";
                            entity.ThumbnailUrl = "ThumbnailUrl f64061bf7507407bb71e464fc35ca2e6";
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("2/12/2023 6:41:39 AM");
                            entity.CreatedByID = 732925;
                            entity.ModifiedDate = DateTime.Parse("2/12/2023 6:41:39 AM");
                            entity.ModifiedByID = 732925;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("MountingTypeName f64061bf7507407bb71e464fc35ca2e6", entity.MountingTypeName);
                            Assert.AreEqual("Description f64061bf7507407bb71e464fc35ca2e6", entity.Description);
                            Assert.AreEqual("ThumbnailUrl f64061bf7507407bb71e464fc35ca2e6", entity.ThumbnailUrl);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("2/12/2023 6:41:39 AM"), entity.CreatedDate);
                            Assert.AreEqual(732925, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("2/12/2023 6:41:39 AM"), entity.ModifiedDate);
                            Assert.AreEqual(732925, entity.ModifiedByID);
              
        }

        [TestCase("MountingType\\030.Update.Success")]
        public void MountingType_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareMountingTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            MountingType entity = dal.Get(paramID);

                          entity.MountingTypeName = "MountingTypeName 86571a37cb084e1cbc52422a3a65e611";
                            entity.Description = "Description 86571a37cb084e1cbc52422a3a65e611";
                            entity.ThumbnailUrl = "ThumbnailUrl 86571a37cb084e1cbc52422a3a65e611";
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("2/12/2023 6:41:39 AM");
                            entity.CreatedByID = 732925;
                            entity.ModifiedDate = DateTime.Parse("2/12/2023 6:41:39 AM");
                            entity.ModifiedByID = 732925;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("MountingTypeName 86571a37cb084e1cbc52422a3a65e611", entity.MountingTypeName);
                            Assert.AreEqual("Description 86571a37cb084e1cbc52422a3a65e611", entity.Description);
                            Assert.AreEqual("ThumbnailUrl 86571a37cb084e1cbc52422a3a65e611", entity.ThumbnailUrl);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("2/12/2023 6:41:39 AM"), entity.CreatedDate);
                            Assert.AreEqual(732925, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("2/12/2023 6:41:39 AM"), entity.ModifiedDate);
                            Assert.AreEqual(732925, entity.ModifiedByID);
              
        }

        [Test]
        public void MountingType_Update_InvalidId()
        {
            var dal = PrepareMountingTypeDal("DALInitParams");

            var entity = new MountingType();
                          entity.MountingTypeName = "MountingTypeName 86571a37cb084e1cbc52422a3a65e611";
                            entity.Description = "Description 86571a37cb084e1cbc52422a3a65e611";
                            entity.ThumbnailUrl = "ThumbnailUrl 86571a37cb084e1cbc52422a3a65e611";
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("2/12/2023 6:41:39 AM");
                            entity.CreatedByID = 732925;
                            entity.ModifiedDate = DateTime.Parse("2/12/2023 6:41:39 AM");
                            entity.ModifiedByID = 732925;
              
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

        [TestCase("MountingType\\040.Erase.Success")]
        public void MountingType_Erase_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareMountingTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Erase(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void MountingType_Erase_InvalidId()
        {
            var dal = PrepareMountingTypeDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Erase(paramID);
            Assert.IsFalse(removed);

        }

        protected IMountingTypeDal PrepareMountingTypeDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IMountingTypeDal dal = new MountingTypeDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
