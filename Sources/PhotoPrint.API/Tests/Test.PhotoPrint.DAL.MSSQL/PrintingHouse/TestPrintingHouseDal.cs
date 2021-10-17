

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
            
                          Assert.AreEqual("Name deec7c019c98423c890738aa3e85b7d8", entity.Name);
                            Assert.AreEqual("Description deec7c019c98423c890738aa3e85b7d8", entity.Description);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("4/15/2019 7:52:49 AM"), entity.CreatedDate);
                            Assert.AreEqual(100003, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("7/19/2023 10:42:49 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100001, entity.ModifiedByID);
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
                          entity.Name = "Name 36d576885ae14a67bedd7c780c177008";
                            entity.Description = "Description 36d576885ae14a67bedd7c780c177008";
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("5/14/2024 6:20:49 AM");
                            entity.CreatedByID = 100007;
                            entity.ModifiedDate = DateTime.Parse("10/2/2021 4:07:49 PM");
                            entity.ModifiedByID = 100004;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name 36d576885ae14a67bedd7c780c177008", entity.Name);
                            Assert.AreEqual("Description 36d576885ae14a67bedd7c780c177008", entity.Description);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("5/14/2024 6:20:49 AM"), entity.CreatedDate);
                            Assert.AreEqual(100007, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("10/2/2021 4:07:49 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100004, entity.ModifiedByID);
              
        }

        [TestCase("PrintingHouse\\030.Update.Success")]
        public void PrintingHouse_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PreparePrintingHouseDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            PrintingHouse entity = dal.Get(paramID);

                          entity.Name = "Name b39413a9abe040e5ab829242f699404a";
                            entity.Description = "Description b39413a9abe040e5ab829242f699404a";
                            entity.IsDeleted = false;              
                            entity.CreatedDate = DateTime.Parse("1/1/2022 11:41:49 AM");
                            entity.CreatedByID = 100009;
                            entity.ModifiedDate = DateTime.Parse("5/21/2019 12:08:49 PM");
                            entity.ModifiedByID = 100002;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name b39413a9abe040e5ab829242f699404a", entity.Name);
                            Assert.AreEqual("Description b39413a9abe040e5ab829242f699404a", entity.Description);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("1/1/2022 11:41:49 AM"), entity.CreatedDate);
                            Assert.AreEqual(100009, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("5/21/2019 12:08:49 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100002, entity.ModifiedByID);
              
        }

        [Test]
        public void PrintingHouse_Update_InvalidId()
        {
            var dal = PreparePrintingHouseDal("DALInitParams");

            var entity = new PrintingHouse();
                          entity.Name = "Name b39413a9abe040e5ab829242f699404a";
                            entity.Description = "Description b39413a9abe040e5ab829242f699404a";
                            entity.IsDeleted = false;              
                            entity.CreatedDate = DateTime.Parse("1/1/2022 11:41:49 AM");
                            entity.CreatedByID = 100009;
                            entity.ModifiedDate = DateTime.Parse("5/21/2019 12:08:49 PM");
                            entity.ModifiedByID = 100002;
              
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
