


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
    public class TestOrderTrackingDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IOrderTrackingDal dal = new OrderTrackingDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void OrderTracking_GetAll_Success()
        {
            var dal = PrepareOrderTrackingDal("DALInitParams");

            IList<OrderTracking> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("OrderTracking\\000.GetDetails.Success")]
        public void OrderTracking_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareOrderTrackingDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            OrderTracking entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100012, entity.OrderID);
                            Assert.AreEqual(5, entity.OrderStatusID);
                            Assert.AreEqual(DateTime.Parse("2/11/2023 1:32:39 PM"), entity.SetDate);
                            Assert.AreEqual(100011, entity.SetByID);
                            Assert.AreEqual("Comment 53770ed6b42c488cbf3a909dd2929305", entity.Comment);
                      }

        [Test]
        public void OrderTracking_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareOrderTrackingDal("DALInitParams");

            OrderTracking entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("OrderTracking\\010.Delete.Success")]
        public void OrderTracking_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareOrderTrackingDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void OrderTracking_Delete_InvalidId()
        {
            var dal = PrepareOrderTrackingDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("OrderTracking\\020.Insert.Success")]
        public void OrderTracking_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareOrderTrackingDal("DALInitParams");

            var entity = new OrderTracking();
                          entity.OrderID = 100001;
                            entity.OrderStatusID = 1;
                            entity.SetDate = DateTime.Parse("8/9/2019 12:41:39 PM");
                            entity.SetByID = 100001;
                            entity.Comment = "Comment 4a13eb1c79b944dba6d4cdc6bc0f7293";
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100001, entity.OrderID);
                            Assert.AreEqual(1, entity.OrderStatusID);
                            Assert.AreEqual(DateTime.Parse("8/9/2019 12:41:39 PM"), entity.SetDate);
                            Assert.AreEqual(100001, entity.SetByID);
                            Assert.AreEqual("Comment 4a13eb1c79b944dba6d4cdc6bc0f7293", entity.Comment);
              
        }

        [TestCase("OrderTracking\\030.Update.Success")]
        public void OrderTracking_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareOrderTrackingDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            OrderTracking entity = dal.Get(paramID);

                          entity.OrderID = 100008;
                            entity.OrderStatusID = 8;
                            entity.SetDate = DateTime.Parse("9/17/2022 8:42:39 AM");
                            entity.SetByID = 100006;
                            entity.Comment = "Comment 4ff56bd19a624d5cbb6c4eedbb0e5398";
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100008, entity.OrderID);
                            Assert.AreEqual(8, entity.OrderStatusID);
                            Assert.AreEqual(DateTime.Parse("9/17/2022 8:42:39 AM"), entity.SetDate);
                            Assert.AreEqual(100006, entity.SetByID);
                            Assert.AreEqual("Comment 4ff56bd19a624d5cbb6c4eedbb0e5398", entity.Comment);
              
        }

        [Test]
        public void OrderTracking_Update_InvalidId()
        {
            var dal = PrepareOrderTrackingDal("DALInitParams");

            var entity = new OrderTracking();
                          entity.OrderID = 100008;
                            entity.OrderStatusID = 8;
                            entity.SetDate = DateTime.Parse("9/17/2022 8:42:39 AM");
                            entity.SetByID = 100006;
                            entity.Comment = "Comment 4ff56bd19a624d5cbb6c4eedbb0e5398";
              
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


        protected IOrderTrackingDal PrepareOrderTrackingDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IOrderTrackingDal dal = new OrderTrackingDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
