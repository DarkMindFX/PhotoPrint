

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
            
                          Assert.AreEqual(100010, entity.OrderID);
                            Assert.AreEqual(4, entity.OrderStatusID);
                            Assert.AreEqual(DateTime.Parse("1/11/2020 7:26:33 PM"), entity.SetDate);
                            Assert.AreEqual(100003, entity.SetByID);
                            Assert.AreEqual("Comment 3522cc3ace3641eaa4759f568ae9ef77", entity.Comment);
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
                          entity.OrderID = 100005;
                            entity.OrderStatusID = 7;
                            entity.SetDate = DateTime.Parse("5/20/2023 11:01:33 AM");
                            entity.SetByID = 100011;
                            entity.Comment = "Comment ef0d29efbe584a5491ab16ae2315ce58";
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100005, entity.OrderID);
                            Assert.AreEqual(7, entity.OrderStatusID);
                            Assert.AreEqual(DateTime.Parse("5/20/2023 11:01:33 AM"), entity.SetDate);
                            Assert.AreEqual(100011, entity.SetByID);
                            Assert.AreEqual("Comment ef0d29efbe584a5491ab16ae2315ce58", entity.Comment);
              
        }

        [TestCase("OrderTracking\\030.Update.Success")]
        public void OrderTracking_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareOrderTrackingDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            OrderTracking entity = dal.Get(paramID);

                          entity.OrderID = 100006;
                            entity.OrderStatusID = 3;
                            entity.SetDate = DateTime.Parse("8/17/2023 9:14:33 PM");
                            entity.SetByID = 100007;
                            entity.Comment = "Comment e696468f762546adbb0ab781d0a5ba0c";
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100006, entity.OrderID);
                            Assert.AreEqual(3, entity.OrderStatusID);
                            Assert.AreEqual(DateTime.Parse("8/17/2023 9:14:33 PM"), entity.SetDate);
                            Assert.AreEqual(100007, entity.SetByID);
                            Assert.AreEqual("Comment e696468f762546adbb0ab781d0a5ba0c", entity.Comment);
              
        }

        [Test]
        public void OrderTracking_Update_InvalidId()
        {
            var dal = PrepareOrderTrackingDal("DALInitParams");

            var entity = new OrderTracking();
                          entity.OrderID = 100006;
                            entity.OrderStatusID = 3;
                            entity.SetDate = DateTime.Parse("8/17/2023 9:14:33 PM");
                            entity.SetByID = 100007;
                            entity.Comment = "Comment e696468f762546adbb0ab781d0a5ba0c";
              
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
