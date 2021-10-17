

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
            
                          Assert.AreEqual("CityName 9670732595fa47e7b04df9d21b50f5c5", entity.CityName);
                            Assert.AreEqual(6, entity.RegionID);
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
                          entity.CityName = "CityName 35fa63fdf12f4bc0b789f47d14405226";
                            entity.RegionID = 7;
                            entity.IsDeleted = false;              
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("CityName 35fa63fdf12f4bc0b789f47d14405226", entity.CityName);
                            Assert.AreEqual(7, entity.RegionID);
                            Assert.AreEqual(false, entity.IsDeleted);
              
        }

        [TestCase("City\\030.Update.Success")]
        public void City_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCityDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            City entity = dal.Get(paramID);

                          entity.CityName = "CityName 4758bbfd63fa424ab4f7d91487546a67";
                            entity.RegionID = 7;
                            entity.IsDeleted = false;              
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("CityName 4758bbfd63fa424ab4f7d91487546a67", entity.CityName);
                            Assert.AreEqual(7, entity.RegionID);
                            Assert.AreEqual(false, entity.IsDeleted);
              
        }

        [Test]
        public void City_Update_InvalidId()
        {
            var dal = PrepareCityDal("DALInitParams");

            var entity = new City();
                          entity.CityName = "CityName 4758bbfd63fa424ab4f7d91487546a67";
                            entity.RegionID = 7;
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
