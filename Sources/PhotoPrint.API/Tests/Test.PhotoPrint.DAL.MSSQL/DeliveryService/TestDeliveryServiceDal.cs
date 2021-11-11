


using PPT.DAL.MSSQL;
using PPT.Interfaces;
using PPT.Interfaces.Entities;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Test.PPT.Common.DAL;

namespace Test.PPT.DAL.MSSQL
{
    public class TestDeliveryServiceDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IDeliveryServiceDal dal = new DeliveryServiceDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void DeliveryService_GetAll_Success()
        {
            var dal = PrepareDeliveryServiceDal("DALInitParams");

            IList<DeliveryService> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("DeliveryService\\000.GetDetails.Success")]
        public void DeliveryService_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareDeliveryServiceDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            DeliveryService entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("DeliveryServiceName ca9920a03b15444d9f5855297cb362", entity.DeliveryServiceName);
                            Assert.AreEqual("Description ca9920a03b15444d9f5855297cb362c6", entity.Description);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("8/5/2023 6:41:38 AM"), entity.CreatedDate);
                            Assert.AreEqual(100008, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("5/7/2019 4:30:38 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100005, entity.ModifiedByID);
                      }

        [Test]
        public void DeliveryService_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareDeliveryServiceDal("DALInitParams");

            DeliveryService entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("DeliveryService\\010.Delete.Success")]
        public void DeliveryService_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareDeliveryServiceDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void DeliveryService_Delete_InvalidId()
        {
            var dal = PrepareDeliveryServiceDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("DeliveryService\\020.Insert.Success")]
        public void DeliveryService_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareDeliveryServiceDal("DALInitParams");

            var entity = new DeliveryService();
                          entity.DeliveryServiceName = "DeliveryServiceName 14ff3145396046a19148cbc0aa43ef";
                            entity.Description = "Description 14ff3145396046a19148cbc0aa43efc3";
                            entity.IsDeleted = false;              
                            entity.CreatedDate = DateTime.Parse("4/3/2024 7:43:38 PM");
                            entity.CreatedByID = 100009;
                            entity.ModifiedDate = DateTime.Parse("8/23/2021 5:30:38 AM");
                            entity.ModifiedByID = 100001;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("DeliveryServiceName 14ff3145396046a19148cbc0aa43ef", entity.DeliveryServiceName);
                            Assert.AreEqual("Description 14ff3145396046a19148cbc0aa43efc3", entity.Description);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("4/3/2024 7:43:38 PM"), entity.CreatedDate);
                            Assert.AreEqual(100009, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("8/23/2021 5:30:38 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100001, entity.ModifiedByID);
              
        }

        [TestCase("DeliveryService\\030.Update.Success")]
        public void DeliveryService_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareDeliveryServiceDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            DeliveryService entity = dal.Get(paramID);

                          entity.DeliveryServiceName = "DeliveryServiceName 6b8cebeb868f4c9584af4fabb4ce5d";
                            entity.Description = "Description 6b8cebeb868f4c9584af4fabb4ce5ddd";
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("2/18/2022 11:17:38 AM");
                            entity.CreatedByID = 100003;
                            entity.ModifiedDate = DateTime.Parse("2/18/2022 11:17:38 AM");
                            entity.ModifiedByID = 100011;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("DeliveryServiceName 6b8cebeb868f4c9584af4fabb4ce5d", entity.DeliveryServiceName);
                            Assert.AreEqual("Description 6b8cebeb868f4c9584af4fabb4ce5ddd", entity.Description);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("2/18/2022 11:17:38 AM"), entity.CreatedDate);
                            Assert.AreEqual(100003, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("2/18/2022 11:17:38 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100011, entity.ModifiedByID);
              
        }

        [Test]
        public void DeliveryService_Update_InvalidId()
        {
            var dal = PrepareDeliveryServiceDal("DALInitParams");

            var entity = new DeliveryService();
                          entity.DeliveryServiceName = "DeliveryServiceName 6b8cebeb868f4c9584af4fabb4ce5d";
                            entity.Description = "Description 6b8cebeb868f4c9584af4fabb4ce5ddd";
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("2/18/2022 11:17:38 AM");
                            entity.CreatedByID = 100003;
                            entity.ModifiedDate = DateTime.Parse("2/18/2022 11:17:38 AM");
                            entity.ModifiedByID = 100011;
              
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

        [TestCase("DeliveryService\\040.Erase.Success")]
        public void DeliveryService_Erase_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareDeliveryServiceDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Erase(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void DeliveryService_Erase_InvalidId()
        {
            var dal = PrepareDeliveryServiceDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Erase(paramID);
            Assert.IsFalse(removed);

        }

        protected IDeliveryServiceDal PrepareDeliveryServiceDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IDeliveryServiceDal dal = new DeliveryServiceDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
