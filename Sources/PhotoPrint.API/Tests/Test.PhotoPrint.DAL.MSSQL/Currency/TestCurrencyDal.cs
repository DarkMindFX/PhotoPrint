

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
    public class TestCurrencyDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            ICurrencyDal dal = new CurrencyDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void Currency_GetAll_Success()
        {
            var dal = PrepareCurrencyDal("DALInitParams");

            IList<Currency> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("Currency\\000.GetDetails.Success")]
        public void Currency_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCurrencyDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Currency entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("ISO 3", entity.ISO);
                            Assert.AreEqual("CurrencyName 3b72e280df894549a943fa21eed197cb", entity.CurrencyName);
                            Assert.AreEqual(false, entity.IsDeleted);
                      }

        [Test]
        public void Currency_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareCurrencyDal("DALInitParams");

            Currency entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("Currency\\010.Delete.Success")]
        public void Currency_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCurrencyDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void Currency_Delete_InvalidId()
        {
            var dal = PrepareCurrencyDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("Currency\\020.Insert.Success")]
        public void Currency_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareCurrencyDal("DALInitParams");

            var entity = new Currency();
                          entity.ISO = "ISO 8";
                            entity.CurrencyName = "CurrencyName 8898385992e44cec9a5e37759bb0fcdb";
                            entity.IsDeleted = false;              
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("ISO 8", entity.ISO);
                            Assert.AreEqual("CurrencyName 8898385992e44cec9a5e37759bb0fcdb", entity.CurrencyName);
                            Assert.AreEqual(false, entity.IsDeleted);
              
        }

        [TestCase("Currency\\030.Update.Success")]
        public void Currency_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCurrencyDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Currency entity = dal.Get(paramID);

                          entity.ISO = "ISO b";
                            entity.CurrencyName = "CurrencyName b463bfcba47040d28988daf04e211602";
                            entity.IsDeleted = false;              
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("ISO b", entity.ISO);
                            Assert.AreEqual("CurrencyName b463bfcba47040d28988daf04e211602", entity.CurrencyName);
                            Assert.AreEqual(false, entity.IsDeleted);
              
        }

        [Test]
        public void Currency_Update_InvalidId()
        {
            var dal = PrepareCurrencyDal("DALInitParams");

            var entity = new Currency();
                          entity.ISO = "ISO b";
                            entity.CurrencyName = "CurrencyName b463bfcba47040d28988daf04e211602";
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

        protected ICurrencyDal PrepareCurrencyDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            ICurrencyDal dal = new CurrencyDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
