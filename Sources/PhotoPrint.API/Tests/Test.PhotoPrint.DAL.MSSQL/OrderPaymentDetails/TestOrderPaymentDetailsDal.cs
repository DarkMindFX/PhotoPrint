


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
    public class TestOrderPaymentDetailsDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IOrderPaymentDetailsDal dal = new OrderPaymentDetailsDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void OrderPaymentDetails_GetAll_Success()
        {
            var dal = PrepareOrderPaymentDetailsDal("DALInitParams");

            IList<OrderPaymentDetails> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("OrderPaymentDetails\\000.GetDetails.Success")]
        public void OrderPaymentDetails_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareOrderPaymentDetailsDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            OrderPaymentDetails entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100004, entity.OrderID);
                            Assert.AreEqual(3, entity.PaymentMethodID);
                            Assert.AreEqual("PaymentTransUID dccd59ae73b04b7887bf7984872a81cc", entity.PaymentTransUID);
                            Assert.AreEqual(DateTime.Parse("2/27/2019 11:17:39 PM"), entity.PaymentDateTime);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("2/27/2019 11:17:39 PM"), entity.CreatedDate);
                            Assert.AreEqual(100002, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("10/3/2022 1:05:39 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100010, entity.ModifiedByID);
                      }

        [Test]
        public void OrderPaymentDetails_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareOrderPaymentDetailsDal("DALInitParams");

            OrderPaymentDetails entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("OrderPaymentDetails\\010.Delete.Success")]
        public void OrderPaymentDetails_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareOrderPaymentDetailsDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void OrderPaymentDetails_Delete_InvalidId()
        {
            var dal = PrepareOrderPaymentDetailsDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("OrderPaymentDetails\\020.Insert.Success")]
        public void OrderPaymentDetails_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareOrderPaymentDetailsDal("DALInitParams");

            var entity = new OrderPaymentDetails();
                          entity.OrderID = 100006;
                            entity.PaymentMethodID = 10025;
                            entity.PaymentTransUID = "PaymentTransUID 214392caaf304e25b214b93c98074ad9";
                            entity.PaymentDateTime = DateTime.Parse("9/27/2023 12:40:39 PM");
                            entity.IsDeleted = false;              
                            entity.CreatedDate = DateTime.Parse("9/27/2023 12:40:39 PM");
                            entity.CreatedByID = 100009;
                            entity.ModifiedDate = DateTime.Parse("9/27/2023 12:40:39 PM");
                            entity.ModifiedByID = 100005;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100006, entity.OrderID);
                            Assert.AreEqual(10025, entity.PaymentMethodID);
                            Assert.AreEqual("PaymentTransUID 214392caaf304e25b214b93c98074ad9", entity.PaymentTransUID);
                            Assert.AreEqual(DateTime.Parse("9/27/2023 12:40:39 PM"), entity.PaymentDateTime);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("9/27/2023 12:40:39 PM"), entity.CreatedDate);
                            Assert.AreEqual(100009, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("9/27/2023 12:40:39 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100005, entity.ModifiedByID);
              
        }

        [TestCase("OrderPaymentDetails\\030.Update.Success")]
        public void OrderPaymentDetails_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareOrderPaymentDetailsDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            OrderPaymentDetails entity = dal.Get(paramID);

                          entity.OrderID = 100010;
                            entity.PaymentMethodID = 5;
                            entity.PaymentTransUID = "PaymentTransUID 67d5c7bcd549491c940ec50e1535f187";
                            entity.PaymentDateTime = DateTime.Parse("12/25/2023 10:53:39 PM");
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("12/25/2023 10:53:39 PM");
                            entity.CreatedByID = 100002;
                            entity.ModifiedDate = DateTime.Parse("5/15/2021 8:40:39 AM");
                            entity.ModifiedByID = 100011;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100010, entity.OrderID);
                            Assert.AreEqual(5, entity.PaymentMethodID);
                            Assert.AreEqual("PaymentTransUID 67d5c7bcd549491c940ec50e1535f187", entity.PaymentTransUID);
                            Assert.AreEqual(DateTime.Parse("12/25/2023 10:53:39 PM"), entity.PaymentDateTime);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("12/25/2023 10:53:39 PM"), entity.CreatedDate);
                            Assert.AreEqual(100002, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("5/15/2021 8:40:39 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100011, entity.ModifiedByID);
              
        }

        [Test]
        public void OrderPaymentDetails_Update_InvalidId()
        {
            var dal = PrepareOrderPaymentDetailsDal("DALInitParams");

            var entity = new OrderPaymentDetails();
                          entity.OrderID = 100010;
                            entity.PaymentMethodID = 5;
                            entity.PaymentTransUID = "PaymentTransUID 67d5c7bcd549491c940ec50e1535f187";
                            entity.PaymentDateTime = DateTime.Parse("12/25/2023 10:53:39 PM");
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("12/25/2023 10:53:39 PM");
                            entity.CreatedByID = 100002;
                            entity.ModifiedDate = DateTime.Parse("5/15/2021 8:40:39 AM");
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

        [TestCase("OrderPaymentDetails\\040.Erase.Success")]
        public void OrderPaymentDetails_Erase_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareOrderPaymentDetailsDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Erase(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void OrderPaymentDetails_Erase_InvalidId()
        {
            var dal = PrepareOrderPaymentDetailsDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Erase(paramID);
            Assert.IsFalse(removed);

        }

        protected IOrderPaymentDetailsDal PrepareOrderPaymentDetailsDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IOrderPaymentDetailsDal dal = new OrderPaymentDetailsDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
