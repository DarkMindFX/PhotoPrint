

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
                            Assert.AreEqual(1, entity.PaymentMethodID);
                            Assert.AreEqual("PaymentTransUID 8e2d84fdcbf646abb9b717a1a41fa0db", entity.PaymentTransUID);
                            Assert.AreEqual(DateTime.Parse("8/14/2022 8:23:33 AM"), entity.PaymentDateTime);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("8/14/2022 8:23:33 AM"), entity.CreatedDate);
                            Assert.AreEqual(100002, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("8/14/2022 8:23:33 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100006, entity.ModifiedByID);
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
                          entity.OrderID = 100012;
                            entity.PaymentMethodID = 4;
                            entity.PaymentTransUID = "PaymentTransUID fd901386f2994b01ad93fb9105340006";
                            entity.PaymentDateTime = DateTime.Parse("4/1/2020 4:24:33 AM");
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("4/1/2020 4:24:33 AM");
                            entity.CreatedByID = 100007;
                            entity.ModifiedDate = DateTime.Parse("4/1/2020 4:24:33 AM");
                            entity.ModifiedByID = 100005;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100012, entity.OrderID);
                            Assert.AreEqual(4, entity.PaymentMethodID);
                            Assert.AreEqual("PaymentTransUID fd901386f2994b01ad93fb9105340006", entity.PaymentTransUID);
                            Assert.AreEqual(DateTime.Parse("4/1/2020 4:24:33 AM"), entity.PaymentDateTime);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("4/1/2020 4:24:33 AM"), entity.CreatedDate);
                            Assert.AreEqual(100007, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("4/1/2020 4:24:33 AM"), entity.ModifiedDate);
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

                          entity.OrderID = 100008;
                            entity.PaymentMethodID = 1;
                            entity.PaymentTransUID = "PaymentTransUID 3de9c5f71bcb42a8bfff42ce8e67143d";
                            entity.PaymentDateTime = DateTime.Parse("6/28/2020 2:38:33 PM");
                            entity.IsDeleted = false;              
                            entity.CreatedDate = DateTime.Parse("6/28/2020 2:38:33 PM");
                            entity.CreatedByID = 100007;
                            entity.ModifiedDate = DateTime.Parse("5/10/2023 12:24:33 AM");
                            entity.ModifiedByID = 100002;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100008, entity.OrderID);
                            Assert.AreEqual(1, entity.PaymentMethodID);
                            Assert.AreEqual("PaymentTransUID 3de9c5f71bcb42a8bfff42ce8e67143d", entity.PaymentTransUID);
                            Assert.AreEqual(DateTime.Parse("6/28/2020 2:38:33 PM"), entity.PaymentDateTime);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("6/28/2020 2:38:33 PM"), entity.CreatedDate);
                            Assert.AreEqual(100007, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("5/10/2023 12:24:33 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100002, entity.ModifiedByID);
              
        }

        [Test]
        public void OrderPaymentDetails_Update_InvalidId()
        {
            var dal = PrepareOrderPaymentDetailsDal("DALInitParams");

            var entity = new OrderPaymentDetails();
                          entity.OrderID = 100008;
                            entity.PaymentMethodID = 1;
                            entity.PaymentTransUID = "PaymentTransUID 3de9c5f71bcb42a8bfff42ce8e67143d";
                            entity.PaymentDateTime = DateTime.Parse("6/28/2020 2:38:33 PM");
                            entity.IsDeleted = false;              
                            entity.CreatedDate = DateTime.Parse("6/28/2020 2:38:33 PM");
                            entity.CreatedByID = 100007;
                            entity.ModifiedDate = DateTime.Parse("5/10/2023 12:24:33 AM");
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
