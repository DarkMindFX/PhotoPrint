


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
    public class TestCountryDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            ICountryDal dal = new CountryDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void Country_GetAll_Success()
        {
            var dal = PrepareCountryDal("DALInitParams");

            IList<Country> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("Country\\000.GetDetails.Success")]
        public void Country_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCountryDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Country entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("CountryName f0cbb5276b2d426b8f8839f55ddce362", entity.CountryName);
                            Assert.AreEqual("ISO f", entity.ISO);
                            Assert.AreEqual(true, entity.IsDeleted);
                      }

        [Test]
        public void Country_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareCountryDal("DALInitParams");

            Country entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("Country\\010.Delete.Success")]
        public void Country_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCountryDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void Country_Delete_InvalidId()
        {
            var dal = PrepareCountryDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("Country\\020.Insert.Success")]
        public void Country_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareCountryDal("DALInitParams");

            var entity = new Country();
                          entity.CountryName = "CountryName 5fb93d4ba8b14427975c73a92af8ee7e";
                            entity.ISO = "ISO 5";
                            entity.IsDeleted = true;              
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("CountryName 5fb93d4ba8b14427975c73a92af8ee7e", entity.CountryName);
                            Assert.AreEqual("ISO 5", entity.ISO);
                            Assert.AreEqual(true, entity.IsDeleted);
              
        }

        [TestCase("Country\\030.Update.Success")]
        public void Country_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCountryDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Country entity = dal.Get(paramID);

                          entity.CountryName = "CountryName 5e3ddfd88afb423591f99429c2c59e6d";
                            entity.ISO = "ISO 5";
                            entity.IsDeleted = true;              
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("CountryName 5e3ddfd88afb423591f99429c2c59e6d", entity.CountryName);
                            Assert.AreEqual("ISO 5", entity.ISO);
                            Assert.AreEqual(true, entity.IsDeleted);
              
        }

        [Test]
        public void Country_Update_InvalidId()
        {
            var dal = PrepareCountryDal("DALInitParams");

            var entity = new Country();
                          entity.CountryName = "CountryName 5e3ddfd88afb423591f99429c2c59e6d";
                            entity.ISO = "ISO 5";
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

        [TestCase("Country\\040.Erase.Success")]
        public void Country_Erase_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCountryDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Erase(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void Country_Erase_InvalidId()
        {
            var dal = PrepareCountryDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Erase(paramID);
            Assert.IsFalse(removed);

        }

        protected ICountryDal PrepareCountryDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            ICountryDal dal = new CountryDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
