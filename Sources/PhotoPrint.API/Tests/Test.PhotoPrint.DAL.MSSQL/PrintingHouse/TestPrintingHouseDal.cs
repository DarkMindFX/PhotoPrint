


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
            
                          Assert.AreEqual("Name 40cd7e77a21c4c80b375ed0c34bc16a5", entity.Name);
                            Assert.AreEqual("Description 40cd7e77a21c4c80b375ed0c34bc16a5", entity.Description);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("2/13/2019 5:11:40 PM"), entity.CreatedDate);
                            Assert.AreEqual(100009, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("5/7/2020 3:00:40 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100011, entity.ModifiedByID);
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
                          entity.Name = "Name 66e4d2b13da7434fb6095d2aa64996ef";
                            entity.Description = "Description 66e4d2b13da7434fb6095d2aa64996ef";
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("6/15/2023 11:00:40 AM");
                            entity.CreatedByID = 100010;
                            entity.ModifiedDate = DateTime.Parse("11/2/2020 8:47:40 PM");
                            entity.ModifiedByID = 100010;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name 66e4d2b13da7434fb6095d2aa64996ef", entity.Name);
                            Assert.AreEqual("Description 66e4d2b13da7434fb6095d2aa64996ef", entity.Description);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("6/15/2023 11:00:40 AM"), entity.CreatedDate);
                            Assert.AreEqual(100010, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("11/2/2020 8:47:40 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100010, entity.ModifiedByID);
              
        }

        [TestCase("PrintingHouse\\030.Update.Success")]
        public void PrintingHouse_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PreparePrintingHouseDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            PrintingHouse entity = dal.Get(paramID);

                          entity.Name = "Name 711865c643314dae9377e8b75a3fa9fb";
                            entity.Description = "Description 711865c643314dae9377e8b75a3fa9fb";
                            entity.IsDeleted = false;              
                            entity.CreatedDate = DateTime.Parse("9/14/2023 6:34:40 AM");
                            entity.CreatedByID = 100007;
                            entity.ModifiedDate = DateTime.Parse("9/14/2023 6:34:40 AM");
                            entity.ModifiedByID = 100001;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name 711865c643314dae9377e8b75a3fa9fb", entity.Name);
                            Assert.AreEqual("Description 711865c643314dae9377e8b75a3fa9fb", entity.Description);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("9/14/2023 6:34:40 AM"), entity.CreatedDate);
                            Assert.AreEqual(100007, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("9/14/2023 6:34:40 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100001, entity.ModifiedByID);
              
        }

        [Test]
        public void PrintingHouse_Update_InvalidId()
        {
            var dal = PreparePrintingHouseDal("DALInitParams");

            var entity = new PrintingHouse();
                          entity.Name = "Name 711865c643314dae9377e8b75a3fa9fb";
                            entity.Description = "Description 711865c643314dae9377e8b75a3fa9fb";
                            entity.IsDeleted = false;              
                            entity.CreatedDate = DateTime.Parse("9/14/2023 6:34:40 AM");
                            entity.CreatedByID = 100007;
                            entity.ModifiedDate = DateTime.Parse("9/14/2023 6:34:40 AM");
                            entity.ModifiedByID = 100001;
              
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

        [TestCase("PrintingHouse\\040.Erase.Success")]
        public void PrintingHouse_Erase_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PreparePrintingHouseDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Erase(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void PrintingHouse_Erase_InvalidId()
        {
            var dal = PreparePrintingHouseDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Erase(paramID);
            Assert.IsFalse(removed);

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
