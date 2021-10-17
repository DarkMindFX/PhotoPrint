

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
            
                          Assert.AreEqual("DeliveryServiceName ba22fa1b5e474ea99bb9e4c6d9ab97", entity.DeliveryServiceName);
                            Assert.AreEqual("Description ba22fa1b5e474ea99bb9e4c6d9ab9742", entity.Description);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("7/3/2019 7:30:48 AM"), entity.CreatedDate);
                            Assert.AreEqual(100005, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("3/16/2022 10:40:48 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100007, entity.ModifiedByID);
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
                          entity.DeliveryServiceName = "DeliveryServiceName 00bf33d3800343b78bcd3de2073864";
                            entity.Description = "Description 00bf33d3800343b78bcd3de207386442";
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("10/6/2019 3:42:48 PM");
                            entity.CreatedByID = 100006;
                            entity.ModifiedDate = DateTime.Parse("12/27/2020 1:30:48 PM");
                            entity.ModifiedByID = 100008;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("DeliveryServiceName 00bf33d3800343b78bcd3de2073864", entity.DeliveryServiceName);
                            Assert.AreEqual("Description 00bf33d3800343b78bcd3de207386442", entity.Description);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("10/6/2019 3:42:48 PM"), entity.CreatedDate);
                            Assert.AreEqual(100006, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("12/27/2020 1:30:48 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100008, entity.ModifiedByID);
              
        }

        [TestCase("DeliveryService\\030.Update.Success")]
        public void DeliveryService_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareDeliveryServiceDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            DeliveryService entity = dal.Get(paramID);

                          entity.DeliveryServiceName = "DeliveryServiceName 311de72dfc024740bcd6aafeffa6df";
                            entity.Description = "Description 311de72dfc024740bcd6aafeffa6df2d";
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("4/15/2023 2:30:48 PM");
                            entity.CreatedByID = 100003;
                            entity.ModifiedDate = DateTime.Parse("7/15/2023 10:04:48 AM");
                            entity.ModifiedByID = 100006;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("DeliveryServiceName 311de72dfc024740bcd6aafeffa6df", entity.DeliveryServiceName);
                            Assert.AreEqual("Description 311de72dfc024740bcd6aafeffa6df2d", entity.Description);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("4/15/2023 2:30:48 PM"), entity.CreatedDate);
                            Assert.AreEqual(100003, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("7/15/2023 10:04:48 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100006, entity.ModifiedByID);
              
        }

        [Test]
        public void DeliveryService_Update_InvalidId()
        {
            var dal = PrepareDeliveryServiceDal("DALInitParams");

            var entity = new DeliveryService();
                          entity.DeliveryServiceName = "DeliveryServiceName 311de72dfc024740bcd6aafeffa6df";
                            entity.Description = "Description 311de72dfc024740bcd6aafeffa6df2d";
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("4/15/2023 2:30:48 PM");
                            entity.CreatedByID = 100003;
                            entity.ModifiedDate = DateTime.Parse("7/15/2023 10:04:48 AM");
                            entity.ModifiedByID = 100006;
              
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
