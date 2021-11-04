


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
    public class TestRegionDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IRegionDal dal = new RegionDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void Region_GetAll_Success()
        {
            var dal = PrepareRegionDal("DALInitParams");

            IList<Region> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("Region\\000.GetDetails.Success")]
        public void Region_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareRegionDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Region entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("RegionName ed14b7bc0ab444f5986d7c406040ddcf", entity.RegionName);
                            Assert.AreEqual(44, entity.CountryID);
                            Assert.AreEqual(false, entity.IsDeleted);
                      }

        [Test]
        public void Region_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareRegionDal("DALInitParams");

            Region entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("Region\\010.Delete.Success")]
        public void Region_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareRegionDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void Region_Delete_InvalidId()
        {
            var dal = PrepareRegionDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("Region\\020.Insert.Success")]
        public void Region_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareRegionDal("DALInitParams");

            var entity = new Region();
                          entity.RegionName = "RegionName 2d000cc56c9544bc900a7a5782807483";
                            entity.CountryID = 8;
                            entity.IsDeleted = false;              
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("RegionName 2d000cc56c9544bc900a7a5782807483", entity.RegionName);
                            Assert.AreEqual(8, entity.CountryID);
                            Assert.AreEqual(false, entity.IsDeleted);
              
        }

        [TestCase("Region\\030.Update.Success")]
        public void Region_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareRegionDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Region entity = dal.Get(paramID);

                          entity.RegionName = "RegionName 6aa1e92d35734f6cb45d3d6332f5f2ad";
                            entity.CountryID = 83;
                            entity.IsDeleted = true;              
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("RegionName 6aa1e92d35734f6cb45d3d6332f5f2ad", entity.RegionName);
                            Assert.AreEqual(83, entity.CountryID);
                            Assert.AreEqual(true, entity.IsDeleted);
              
        }

        [Test]
        public void Region_Update_InvalidId()
        {
            var dal = PrepareRegionDal("DALInitParams");

            var entity = new Region();
                          entity.RegionName = "RegionName 6aa1e92d35734f6cb45d3d6332f5f2ad";
                            entity.CountryID = 83;
                            entity.IsDeleted = true;              
              
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

        [TestCase("Region\\040.Erase.Success")]
        public void Region_Erase_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareRegionDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Erase(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void Region_Erase_InvalidId()
        {
            var dal = PrepareRegionDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Erase(paramID);
            Assert.IsFalse(removed);

        }

        protected IRegionDal PrepareRegionDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IRegionDal dal = new RegionDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
