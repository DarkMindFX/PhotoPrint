

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
            
                          Assert.AreEqual("DeliveryServiceName 3df4f9e7743541b6b00692647acd76", entity.DeliveryServiceName);
                            Assert.AreEqual("Description 3df4f9e7743541b6b00692647acd76ac", entity.Description);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("4/21/2024 8:14:32 AM"), entity.CreatedDate);
                            Assert.AreEqual(100004, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("5/29/2023 12:17:32 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100003, entity.ModifiedByID);
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
                          entity.DeliveryServiceName = "DeliveryServiceName ff781b68f4ad4d7ba85b5a53f3aeec";
                            entity.Description = "Description ff781b68f4ad4d7ba85b5a53f3aeecdb";
                            entity.IsDeleted = false;              
                            entity.CreatedDate = DateTime.Parse("2/28/2019 10:05:32 AM");
                            entity.CreatedByID = 100002;
                            entity.ModifiedDate = DateTime.Parse("4/7/2022 6:06:32 AM");
                            entity.ModifiedByID = 100007;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("DeliveryServiceName ff781b68f4ad4d7ba85b5a53f3aeec", entity.DeliveryServiceName);
                            Assert.AreEqual("Description ff781b68f4ad4d7ba85b5a53f3aeecdb", entity.Description);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("2/28/2019 10:05:32 AM"), entity.CreatedDate);
                            Assert.AreEqual(100002, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("4/7/2022 6:06:32 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100007, entity.ModifiedByID);
              
        }

        [TestCase("DeliveryService\\030.Update.Success")]
        public void DeliveryService_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareDeliveryServiceDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            DeliveryService entity = dal.Get(paramID);

                          entity.DeliveryServiceName = "DeliveryServiceName 9ed042a1118c48499f308a1ce2b944";
                            entity.Description = "Description 9ed042a1118c48499f308a1ce2b944fa";
                            entity.IsDeleted = false;              
                            entity.CreatedDate = DateTime.Parse("7/7/2022 1:39:32 AM");
                            entity.CreatedByID = 100006;
                            entity.ModifiedDate = DateTime.Parse("7/7/2022 1:39:32 AM");
                            entity.ModifiedByID = 100001;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("DeliveryServiceName 9ed042a1118c48499f308a1ce2b944", entity.DeliveryServiceName);
                            Assert.AreEqual("Description 9ed042a1118c48499f308a1ce2b944fa", entity.Description);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("7/7/2022 1:39:32 AM"), entity.CreatedDate);
                            Assert.AreEqual(100006, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("7/7/2022 1:39:32 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100001, entity.ModifiedByID);
              
        }

        [Test]
        public void DeliveryService_Update_InvalidId()
        {
            var dal = PrepareDeliveryServiceDal("DALInitParams");

            var entity = new DeliveryService();
                          entity.DeliveryServiceName = "DeliveryServiceName 9ed042a1118c48499f308a1ce2b944";
                            entity.Description = "Description 9ed042a1118c48499f308a1ce2b944fa";
                            entity.IsDeleted = false;              
                            entity.CreatedDate = DateTime.Parse("7/7/2022 1:39:32 AM");
                            entity.CreatedByID = 100006;
                            entity.ModifiedDate = DateTime.Parse("7/7/2022 1:39:32 AM");
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
