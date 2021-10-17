

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
            
                          Assert.AreEqual(100008, entity.OrderID);
                            Assert.AreEqual(9, entity.OrderStatusID);
                            Assert.AreEqual(DateTime.Parse("5/13/2022 5:17:49 PM"), entity.SetDate);
                            Assert.AreEqual(100010, entity.SetByID);
                            Assert.AreEqual("Comment 4b84cfd0b6204275b1582bab15bf20a6", entity.Comment);
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
                          entity.OrderID = 100007;
                            entity.OrderStatusID = 11;
                            entity.SetDate = DateTime.Parse("12/9/2023 2:54:49 PM");
                            entity.SetByID = 100004;
                            entity.Comment = "Comment 37733796239c42b4ac9dc76fd05e983f";
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100007, entity.OrderID);
                            Assert.AreEqual(11, entity.OrderStatusID);
                            Assert.AreEqual(DateTime.Parse("12/9/2023 2:54:49 PM"), entity.SetDate);
                            Assert.AreEqual(100004, entity.SetByID);
                            Assert.AreEqual("Comment 37733796239c42b4ac9dc76fd05e983f", entity.Comment);
              
        }

        [TestCase("OrderTracking\\030.Update.Success")]
        public void OrderTracking_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareOrderTrackingDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            OrderTracking entity = dal.Get(paramID);

                          entity.OrderID = 100010;
                            entity.OrderStatusID = 3;
                            entity.SetDate = DateTime.Parse("10/25/2021 6:28:49 AM");
                            entity.SetByID = 100008;
                            entity.Comment = "Comment ad04dfe0b03e4cae8a591c15dce85917";
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100010, entity.OrderID);
                            Assert.AreEqual(3, entity.OrderStatusID);
                            Assert.AreEqual(DateTime.Parse("10/25/2021 6:28:49 AM"), entity.SetDate);
                            Assert.AreEqual(100008, entity.SetByID);
                            Assert.AreEqual("Comment ad04dfe0b03e4cae8a591c15dce85917", entity.Comment);
              
        }

        [Test]
        public void OrderTracking_Update_InvalidId()
        {
            var dal = PrepareOrderTrackingDal("DALInitParams");

            var entity = new OrderTracking();
                          entity.OrderID = 100010;
                            entity.OrderStatusID = 3;
                            entity.SetDate = DateTime.Parse("10/25/2021 6:28:49 AM");
                            entity.SetByID = 100008;
                            entity.Comment = "Comment ad04dfe0b03e4cae8a591c15dce85917";
              
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
