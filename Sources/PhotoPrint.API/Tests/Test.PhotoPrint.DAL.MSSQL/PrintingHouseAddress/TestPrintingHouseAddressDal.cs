


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
    public class TestPrintingHouseAddressDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IPrintingHouseAddressDal dal = new PrintingHouseAddressDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void PrintingHouseAddress_GetAll_Success()
        {
            var dal = PreparePrintingHouseAddressDal("DALInitParams");

            IList<PrintingHouseAddress> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("PrintingHouseAddress\\000.GetDetails.Success")]
        public void PrintingHouseAddress_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PreparePrintingHouseAddressDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramPrintingHouseID = (System.Int64)objIds[0];
                var paramAddressID = (System.Int64)objIds[1];
            PrintingHouseAddress entity = dal.Get(paramPrintingHouseID,paramAddressID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.PrintingHouseID);
                        Assert.IsNotNull(entity.AddressID);
            
                          Assert.AreEqual(100002, entity.PrintingHouseID);
                            Assert.AreEqual(100001, entity.AddressID);
                            Assert.AreEqual(true, entity.IsPrimary);
                      }

        [Test]
        public void PrintingHouseAddress_GetDetails_InvalidId()
        {
                var paramPrintingHouseID = Int64.MaxValue - 1;
                var paramAddressID = Int64.MaxValue - 1;
            var dal = PreparePrintingHouseAddressDal("DALInitParams");

            PrintingHouseAddress entity = dal.Get(paramPrintingHouseID,paramAddressID);

            Assert.IsNull(entity);
        }

        [TestCase("PrintingHouseAddress\\010.Delete.Success")]
        public void PrintingHouseAddress_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PreparePrintingHouseAddressDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramPrintingHouseID = (System.Int64)objIds[0];
                var paramAddressID = (System.Int64)objIds[1];
            bool removed = dal.Delete(paramPrintingHouseID,paramAddressID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void PrintingHouseAddress_Delete_InvalidId()
        {
            var dal = PreparePrintingHouseAddressDal("DALInitParams");
                var paramPrintingHouseID = Int64.MaxValue - 1;
                var paramAddressID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramPrintingHouseID,paramAddressID);
            Assert.IsFalse(removed);

        }

        [TestCase("PrintingHouseAddress\\020.Insert.Success")]
        public void PrintingHouseAddress_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PreparePrintingHouseAddressDal("DALInitParams");

            var entity = new PrintingHouseAddress();
                          entity.PrintingHouseID = 100001;
                            entity.AddressID = 100002;
                            entity.IsPrimary = true;              
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.PrintingHouseID);
                        Assert.IsNotNull(entity.AddressID);
            
                          Assert.AreEqual(100001, entity.PrintingHouseID);
                            Assert.AreEqual(100002, entity.AddressID);
                            Assert.AreEqual(true, entity.IsPrimary);
              
        }

        [TestCase("PrintingHouseAddress\\030.Update.Success")]
        public void PrintingHouseAddress_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PreparePrintingHouseAddressDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramPrintingHouseID = (System.Int64)objIds[0];
                var paramAddressID = (System.Int64)objIds[1];
            PrintingHouseAddress entity = dal.Get(paramPrintingHouseID,paramAddressID);

                          entity.IsPrimary = true;              
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.PrintingHouseID);
                        Assert.IsNotNull(entity.AddressID);
            
                          Assert.AreEqual(100001, entity.PrintingHouseID);
                            Assert.AreEqual(100015, entity.AddressID);
                            Assert.AreEqual(true, entity.IsPrimary);
              
        }

        [Test]
        public void PrintingHouseAddress_Update_InvalidId()
        {
            var dal = PreparePrintingHouseAddressDal("DALInitParams");

            var entity = new PrintingHouseAddress();
                          entity.PrintingHouseID = 100001;
                            entity.AddressID = 100015;
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


        protected IPrintingHouseAddressDal PreparePrintingHouseAddressDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IPrintingHouseAddressDal dal = new PrintingHouseAddressDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
