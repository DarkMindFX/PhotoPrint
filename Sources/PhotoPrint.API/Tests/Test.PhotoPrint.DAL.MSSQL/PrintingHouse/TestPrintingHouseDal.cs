

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
    public class TestPrintingHouseDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IPrintingHouseDal dal = new PrintingHouseDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void PrintingHouse_GetAll_Success()
        {
            var dal = PreparePrintingHouseDal("DALInitParams");

            IList<PrintingHouse> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("PrintingHouse\\000.GetDetails.Success")]
        public void PrintingHouse_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PreparePrintingHouseDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            PrintingHouse entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name 882a68430de140eb8090e54066d39f31", entity.Name);
                            Assert.AreEqual("Description 882a68430de140eb8090e54066d39f31", entity.Description);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("11/19/2019 1:28:33 PM"), entity.CreatedDate);
                            Assert.AreEqual(100006, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("2/17/2020 9:02:33 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100009, entity.ModifiedByID);
                      }

        [Test]
        public void PrintingHouse_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PreparePrintingHouseDal("DALInitParams");

            PrintingHouse entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("PrintingHouse\\010.Delete.Success")]
        public void PrintingHouse_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PreparePrintingHouseDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void PrintingHouse_Delete_InvalidId()
        {
            var dal = PreparePrintingHouseDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("PrintingHouse\\020.Insert.Success")]
        public void PrintingHouse_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PreparePrintingHouseDal("DALInitParams");

            var entity = new PrintingHouse();
                          entity.Name = "Name d2878edad80746a4bf5929f0fd47dd19";
                            entity.Description = "Description d2878edad80746a4bf5929f0fd47dd19";
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("3/28/2023 5:02:33 AM");
                            entity.CreatedByID = 100004;
                            entity.ModifiedDate = DateTime.Parse("3/28/2023 5:02:33 AM");
                            entity.ModifiedByID = 100009;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name d2878edad80746a4bf5929f0fd47dd19", entity.Name);
                            Assert.AreEqual("Description d2878edad80746a4bf5929f0fd47dd19", entity.Description);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("3/28/2023 5:02:33 AM"), entity.CreatedDate);
                            Assert.AreEqual(100004, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("3/28/2023 5:02:33 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100009, entity.ModifiedByID);
              
        }

        [TestCase("PrintingHouse\\030.Update.Success")]
        public void PrintingHouse_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PreparePrintingHouseDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            PrintingHouse entity = dal.Get(paramID);

                          entity.Name = "Name f3b901d4779a4ea4b59d2d0d2bdcba67";
                            entity.Description = "Description f3b901d4779a4ea4b59d2d0d2bdcba67";
                            entity.IsDeleted = false;              
                            entity.CreatedDate = DateTime.Parse("6/25/2023 3:16:33 PM");
                            entity.CreatedByID = 100010;
                            entity.ModifiedDate = DateTime.Parse("11/12/2020 1:03:33 AM");
                            entity.ModifiedByID = 100003;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name f3b901d4779a4ea4b59d2d0d2bdcba67", entity.Name);
                            Assert.AreEqual("Description f3b901d4779a4ea4b59d2d0d2bdcba67", entity.Description);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("6/25/2023 3:16:33 PM"), entity.CreatedDate);
                            Assert.AreEqual(100010, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("11/12/2020 1:03:33 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100003, entity.ModifiedByID);
              
        }

        [Test]
        public void PrintingHouse_Update_InvalidId()
        {
            var dal = PreparePrintingHouseDal("DALInitParams");

            var entity = new PrintingHouse();
                          entity.Name = "Name f3b901d4779a4ea4b59d2d0d2bdcba67";
                            entity.Description = "Description f3b901d4779a4ea4b59d2d0d2bdcba67";
                            entity.IsDeleted = false;              
                            entity.CreatedDate = DateTime.Parse("6/25/2023 3:16:33 PM");
                            entity.CreatedByID = 100010;
                            entity.ModifiedDate = DateTime.Parse("11/12/2020 1:03:33 AM");
                            entity.ModifiedByID = 100003;
              
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

        protected IPrintingHouseDal PreparePrintingHouseDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IPrintingHouseDal dal = new PrintingHouseDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
