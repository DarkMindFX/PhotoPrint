

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
            
                          Assert.AreEqual("MountingTypeName ef97cba037724eeebda16ad2d1466dc9", entity.MountingTypeName);
                            Assert.AreEqual("Description ef97cba037724eeebda16ad2d1466dc9", entity.Description);
                            Assert.AreEqual("ThumbnailUrl ef97cba037724eeebda16ad2d1466dc9", entity.ThumbnailUrl);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("7/23/2019 4:46:33 AM"), entity.CreatedDate);
                            Assert.AreEqual(84243, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("7/23/2019 4:46:33 AM"), entity.ModifiedDate);
                            Assert.AreEqual(84243, entity.ModifiedByID);
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
                          entity.MountingTypeName = "MountingTypeName e12545da5fc84946a2e2e9ec920748bc";
                            entity.Description = "Description e12545da5fc84946a2e2e9ec920748bc";
                            entity.ThumbnailUrl = "ThumbnailUrl e12545da5fc84946a2e2e9ec920748bc";
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("7/23/2019 4:46:33 AM");
                            entity.CreatedByID = 84243;
                            entity.ModifiedDate = DateTime.Parse("7/23/2019 4:46:33 AM");
                            entity.ModifiedByID = 84243;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("MountingTypeName e12545da5fc84946a2e2e9ec920748bc", entity.MountingTypeName);
                            Assert.AreEqual("Description e12545da5fc84946a2e2e9ec920748bc", entity.Description);
                            Assert.AreEqual("ThumbnailUrl e12545da5fc84946a2e2e9ec920748bc", entity.ThumbnailUrl);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("7/23/2019 4:46:33 AM"), entity.CreatedDate);
                            Assert.AreEqual(84243, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("7/23/2019 4:46:33 AM"), entity.ModifiedDate);
                            Assert.AreEqual(84243, entity.ModifiedByID);
              
        }

        [TestCase("MountingType\\030.Update.Success")]
        public void MountingType_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareMountingTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            MountingType entity = dal.Get(paramID);

                          entity.MountingTypeName = "MountingTypeName f73ff7899f2242a3a7e05792545f4ef1";
                            entity.Description = "Description f73ff7899f2242a3a7e05792545f4ef1";
                            entity.ThumbnailUrl = "ThumbnailUrl f73ff7899f2242a3a7e05792545f4ef1";
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("7/23/2019 4:46:33 AM");
                            entity.CreatedByID = 84243;
                            entity.ModifiedDate = DateTime.Parse("7/23/2019 4:46:33 AM");
                            entity.ModifiedByID = 84243;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("MountingTypeName f73ff7899f2242a3a7e05792545f4ef1", entity.MountingTypeName);
                            Assert.AreEqual("Description f73ff7899f2242a3a7e05792545f4ef1", entity.Description);
                            Assert.AreEqual("ThumbnailUrl f73ff7899f2242a3a7e05792545f4ef1", entity.ThumbnailUrl);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("7/23/2019 4:46:33 AM"), entity.CreatedDate);
                            Assert.AreEqual(84243, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("7/23/2019 4:46:33 AM"), entity.ModifiedDate);
                            Assert.AreEqual(84243, entity.ModifiedByID);
              
        }

        [Test]
        public void MountingType_Update_InvalidId()
        {
            var dal = PrepareMountingTypeDal("DALInitParams");

            var entity = new MountingType();
                          entity.MountingTypeName = "MountingTypeName f73ff7899f2242a3a7e05792545f4ef1";
                            entity.Description = "Description f73ff7899f2242a3a7e05792545f4ef1";
                            entity.ThumbnailUrl = "ThumbnailUrl f73ff7899f2242a3a7e05792545f4ef1";
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("7/23/2019 4:46:33 AM");
                            entity.CreatedByID = 84243;
                            entity.ModifiedDate = DateTime.Parse("7/23/2019 4:46:33 AM");
                            entity.ModifiedByID = 84243;
              
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
