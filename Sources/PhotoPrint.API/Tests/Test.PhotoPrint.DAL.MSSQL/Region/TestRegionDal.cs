

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
            
                          Assert.AreEqual("RegionName bf864ed74cea46ee82b220877c8860eb", entity.RegionName);
                            Assert.AreEqual(125, entity.CountryID);
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
                          entity.RegionName = "RegionName 04a90cf3eaa74a629c3bddf002edebda";
                            entity.CountryID = 199;
                            entity.IsDeleted = false;              
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("RegionName 04a90cf3eaa74a629c3bddf002edebda", entity.RegionName);
                            Assert.AreEqual(199, entity.CountryID);
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

                          entity.RegionName = "RegionName 51a471a4f7584010a9c65e3b7160e0ed";
                            entity.CountryID = 123;
                            entity.IsDeleted = false;              
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("RegionName 51a471a4f7584010a9c65e3b7160e0ed", entity.RegionName);
                            Assert.AreEqual(123, entity.CountryID);
                            Assert.AreEqual(false, entity.IsDeleted);
              
        }

        [Test]
        public void Region_Update_InvalidId()
        {
            var dal = PrepareRegionDal("DALInitParams");

            var entity = new Region();
                          entity.RegionName = "RegionName 51a471a4f7584010a9c65e3b7160e0ed";
                            entity.CountryID = 123;
                            entity.IsDeleted = false;              
              
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
