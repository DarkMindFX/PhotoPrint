

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
    public class TestPrintingHouseContactDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IPrintingHouseContactDal dal = new PrintingHouseContactDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void PrintingHouseContact_GetAll_Success()
        {
            var dal = PreparePrintingHouseContactDal("DALInitParams");

            IList<PrintingHouseContact> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("PrintingHouseContact\\000.GetDetails.Success")]
        public void PrintingHouseContact_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PreparePrintingHouseContactDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramPrintingHouseID = (System.Int64)objIds[0];
                var paramContactID = (System.Int64)objIds[1];
            PrintingHouseContact entity = dal.Get(paramPrintingHouseID,paramContactID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.PrintingHouseID);
                        Assert.IsNotNull(entity.ContactID);
            
                          Assert.AreEqual(100001, entity.PrintingHouseID);
                            Assert.AreEqual(100020, entity.ContactID);
                            Assert.AreEqual(false, entity.IsPrimary);
                      }

        [Test]
        public void PrintingHouseContact_GetDetails_InvalidId()
        {
                var paramPrintingHouseID = Int64.MaxValue - 1;
                var paramContactID = Int64.MaxValue - 1;
            var dal = PreparePrintingHouseContactDal("DALInitParams");

            PrintingHouseContact entity = dal.Get(paramPrintingHouseID,paramContactID);

            Assert.IsNull(entity);
        }

        [TestCase("PrintingHouseContact\\010.Delete.Success")]
        public void PrintingHouseContact_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PreparePrintingHouseContactDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramPrintingHouseID = (System.Int64)objIds[0];
                var paramContactID = (System.Int64)objIds[1];
            bool removed = dal.Delete(paramPrintingHouseID,paramContactID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void PrintingHouseContact_Delete_InvalidId()
        {
            var dal = PreparePrintingHouseContactDal("DALInitParams");
                var paramPrintingHouseID = Int64.MaxValue - 1;
                var paramContactID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramPrintingHouseID,paramContactID);
            Assert.IsFalse(removed);

        }

        [TestCase("PrintingHouseContact\\020.Insert.Success")]
        public void PrintingHouseContact_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PreparePrintingHouseContactDal("DALInitParams");

            var entity = new PrintingHouseContact();
                          entity.PrintingHouseID = 100005;
                            entity.ContactID = 100011;
                            entity.IsPrimary = false;              
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.PrintingHouseID);
                        Assert.IsNotNull(entity.ContactID);
            
                          Assert.AreEqual(100005, entity.PrintingHouseID);
                            Assert.AreEqual(100011, entity.ContactID);
                            Assert.AreEqual(false, entity.IsPrimary);
              
        }

        [TestCase("PrintingHouseContact\\030.Update.Success")]
        public void PrintingHouseContact_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PreparePrintingHouseContactDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramPrintingHouseID = (System.Int64)objIds[0];
                var paramContactID = (System.Int64)objIds[1];
            PrintingHouseContact entity = dal.Get(paramPrintingHouseID,paramContactID);

                          entity.IsPrimary = true;              
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.PrintingHouseID);
                        Assert.IsNotNull(entity.ContactID);
            
                          Assert.AreEqual(100005, entity.PrintingHouseID);
                            Assert.AreEqual(100002, entity.ContactID);
                            Assert.AreEqual(true, entity.IsPrimary);
              
        }

        [Test]
        public void PrintingHouseContact_Update_InvalidId()
        {
            var dal = PreparePrintingHouseContactDal("DALInitParams");

            var entity = new PrintingHouseContact();
                          entity.PrintingHouseID = 100005;
                            entity.ContactID = 100002;
                            entity.IsPrimary = true;              
              
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

        protected IPrintingHouseContactDal PreparePrintingHouseContactDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IPrintingHouseContactDal dal = new PrintingHouseContactDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
