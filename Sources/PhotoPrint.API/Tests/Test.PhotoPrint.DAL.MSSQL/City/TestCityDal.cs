

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
    public class TestCityDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            ICityDal dal = new CityDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void City_GetAll_Success()
        {
            var dal = PrepareCityDal("DALInitParams");

            IList<City> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("City\\000.GetDetails.Success")]
        public void City_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCityDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            City entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("CityName 9365a8a4d5a84923a66c96ee7f2e5ba6", entity.CityName);
                            Assert.AreEqual(7, entity.RegionID);
                            Assert.AreEqual(true, entity.IsDeleted);
                      }

        [Test]
        public void City_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareCityDal("DALInitParams");

            City entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("City\\010.Delete.Success")]
        public void City_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCityDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void City_Delete_InvalidId()
        {
            var dal = PrepareCityDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("City\\020.Insert.Success")]
        public void City_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareCityDal("DALInitParams");

            var entity = new City();
                          entity.CityName = "CityName 3cced70f3f1f4f289ca49a9c68d43e3d";
                            entity.RegionID = 4;
                            entity.IsDeleted = true;              
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("CityName 3cced70f3f1f4f289ca49a9c68d43e3d", entity.CityName);
                            Assert.AreEqual(4, entity.RegionID);
                            Assert.AreEqual(true, entity.IsDeleted);
              
        }

        [TestCase("City\\030.Update.Success")]
        public void City_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCityDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            City entity = dal.Get(paramID);

                          entity.CityName = "CityName fc311312ecb54a49ad8c6df36471939c";
                            entity.RegionID = 2;
                            entity.IsDeleted = false;              
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("CityName fc311312ecb54a49ad8c6df36471939c", entity.CityName);
                            Assert.AreEqual(2, entity.RegionID);
                            Assert.AreEqual(false, entity.IsDeleted);
              
        }

        [Test]
        public void City_Update_InvalidId()
        {
            var dal = PrepareCityDal("DALInitParams");

            var entity = new City();
                          entity.CityName = "CityName fc311312ecb54a49ad8c6df36471939c";
                            entity.RegionID = 2;
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

        protected ICityDal PrepareCityDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            ICityDal dal = new CityDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
