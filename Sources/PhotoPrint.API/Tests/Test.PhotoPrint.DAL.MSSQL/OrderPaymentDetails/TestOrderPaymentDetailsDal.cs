

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
            
                          Assert.AreEqual(100003, entity.OrderID);
                            Assert.AreEqual(3, entity.PaymentMethodID);
                            Assert.AreEqual("PaymentTransUID 4383890524ff48208586c2d1b51992c0", entity.PaymentTransUID);
                            Assert.AreEqual(DateTime.Parse("7/26/2023 1:49:49 PM"), entity.PaymentDateTime);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("7/26/2023 1:49:49 PM"), entity.CreatedDate);
                            Assert.AreEqual(100001, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("10/24/2023 12:02:49 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100005, entity.ModifiedByID);
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
                          entity.OrderID = 100011;
                            entity.PaymentMethodID = 4;
                            entity.PaymentTransUID = "PaymentTransUID 9568fd562dd64467a4ea21b70680e9ab";
                            entity.PaymentDateTime = DateTime.Parse("9/7/2021 6:17:49 AM");
                            entity.IsDeleted = false;              
                            entity.CreatedDate = DateTime.Parse("9/7/2021 6:17:49 AM");
                            entity.CreatedByID = 100001;
                            entity.ModifiedDate = DateTime.Parse("9/7/2021 6:17:49 AM");
                            entity.ModifiedByID = 100002;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100011, entity.OrderID);
                            Assert.AreEqual(4, entity.PaymentMethodID);
                            Assert.AreEqual("PaymentTransUID 9568fd562dd64467a4ea21b70680e9ab", entity.PaymentTransUID);
                            Assert.AreEqual(DateTime.Parse("9/7/2021 6:17:49 AM"), entity.PaymentDateTime);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("9/7/2021 6:17:49 AM"), entity.CreatedDate);
                            Assert.AreEqual(100001, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("9/7/2021 6:17:49 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100002, entity.ModifiedByID);
              
        }

        [TestCase("OrderPaymentDetails\\030.Update.Success")]
        public void OrderPaymentDetails_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareOrderPaymentDetailsDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            OrderPaymentDetails entity = dal.Get(paramID);

                          entity.OrderID = 100001;
                            entity.PaymentMethodID = 4;
                            entity.PaymentTransUID = "PaymentTransUID 0bacd21fb916409d84f30e794666fae5";
                            entity.PaymentDateTime = DateTime.Parse("4/27/2019 11:37:49 AM");
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("4/27/2019 11:37:49 AM");
                            entity.CreatedByID = 100010;
                            entity.ModifiedDate = DateTime.Parse("4/27/2019 11:37:49 AM");
                            entity.ModifiedByID = 100001;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100001, entity.OrderID);
                            Assert.AreEqual(4, entity.PaymentMethodID);
                            Assert.AreEqual("PaymentTransUID 0bacd21fb916409d84f30e794666fae5", entity.PaymentTransUID);
                            Assert.AreEqual(DateTime.Parse("4/27/2019 11:37:49 AM"), entity.PaymentDateTime);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("4/27/2019 11:37:49 AM"), entity.CreatedDate);
                            Assert.AreEqual(100010, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("4/27/2019 11:37:49 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100001, entity.ModifiedByID);
              
        }

        [Test]
        public void OrderPaymentDetails_Update_InvalidId()
        {
            var dal = PrepareOrderPaymentDetailsDal("DALInitParams");

            var entity = new OrderPaymentDetails();
                          entity.OrderID = 100001;
                            entity.PaymentMethodID = 4;
                            entity.PaymentTransUID = "PaymentTransUID 0bacd21fb916409d84f30e794666fae5";
                            entity.PaymentDateTime = DateTime.Parse("4/27/2019 11:37:49 AM");
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("4/27/2019 11:37:49 AM");
                            entity.CreatedByID = 100010;
                            entity.ModifiedDate = DateTime.Parse("4/27/2019 11:37:49 AM");
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
