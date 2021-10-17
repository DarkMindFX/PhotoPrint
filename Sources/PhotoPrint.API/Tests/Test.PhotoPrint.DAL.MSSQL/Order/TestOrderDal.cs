

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
    public class TestOrderDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IOrderDal dal = new OrderDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void Order_GetAll_Success()
        {
            var dal = PrepareOrderDal("DALInitParams");

            IList<Order> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("Order\\000.GetDetails.Success")]
        public void Order_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareOrderDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Order entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100005, entity.ManagerID);
                            Assert.AreEqual(100008, entity.UserID);
                            Assert.AreEqual(100003, entity.ContactID);
                            Assert.AreEqual(100007, entity.DeliveryAddressID);
                            Assert.AreEqual(100001, entity.DeliveryServiceID);
                            Assert.AreEqual("Comments 40a57216c9eb4eb0b74621794919f186", entity.Comments);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("9/9/2019 1:51:42 PM"), entity.CreatedDate);
                            Assert.AreEqual(100002, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("10/18/2022 9:52:42 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100010, entity.ModifiedByID);
                      }

        [Test]
        public void Order_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareOrderDal("DALInitParams");

            Order entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("Order\\010.Delete.Success")]
        public void Order_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareOrderDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void Order_Delete_InvalidId()
        {
            var dal = PrepareOrderDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("Order\\020.Insert.Success")]
        public void Order_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareOrderDal("DALInitParams");

            var entity = new Order();
                          entity.ManagerID = 100004;
                            entity.UserID = 100003;
                            entity.ContactID = 100020;
                            entity.DeliveryAddressID = 100010;
                            entity.DeliveryServiceID = 100005;
                            entity.Comments = "Comments 986746cee1414e7aa8633f64e4d229b5";
                            entity.IsDeleted = false;              
                            entity.CreatedDate = DateTime.Parse("1/9/2024 7:40:42 AM");
                            entity.CreatedByID = 100006;
                            entity.ModifiedDate = DateTime.Parse("5/29/2021 5:27:42 PM");
                            entity.ModifiedByID = 100009;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100004, entity.ManagerID);
                            Assert.AreEqual(100003, entity.UserID);
                            Assert.AreEqual(100020, entity.ContactID);
                            Assert.AreEqual(100010, entity.DeliveryAddressID);
                            Assert.AreEqual(100005, entity.DeliveryServiceID);
                            Assert.AreEqual("Comments 986746cee1414e7aa8633f64e4d229b5", entity.Comments);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("1/9/2024 7:40:42 AM"), entity.CreatedDate);
                            Assert.AreEqual(100006, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("5/29/2021 5:27:42 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100009, entity.ModifiedByID);
              
        }

        [TestCase("Order\\030.Update.Success")]
        public void Order_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareOrderDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Order entity = dal.Get(paramID);

                          entity.ManagerID = 100001;
                            entity.UserID = 100002;
                            entity.ContactID = 100004;
                            entity.DeliveryAddressID = 100014;
                            entity.DeliveryServiceID = 100002;
                            entity.Comments = "Comments 7a3be9c7dcbb4ce58b77fd74cbd976de";
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("7/13/2019 7:15:42 PM");
                            entity.CreatedByID = 100008;
                            entity.ModifiedDate = DateTime.Parse("7/13/2019 7:15:42 PM");
                            entity.ModifiedByID = 100004;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100001, entity.ManagerID);
                            Assert.AreEqual(100002, entity.UserID);
                            Assert.AreEqual(100004, entity.ContactID);
                            Assert.AreEqual(100014, entity.DeliveryAddressID);
                            Assert.AreEqual(100002, entity.DeliveryServiceID);
                            Assert.AreEqual("Comments 7a3be9c7dcbb4ce58b77fd74cbd976de", entity.Comments);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("7/13/2019 7:15:42 PM"), entity.CreatedDate);
                            Assert.AreEqual(100008, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("7/13/2019 7:15:42 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100004, entity.ModifiedByID);
              
        }

        [Test]
        public void Order_Update_InvalidId()
        {
            var dal = PrepareOrderDal("DALInitParams");

            var entity = new Order();
                          entity.ManagerID = 100001;
                            entity.UserID = 100002;
                            entity.ContactID = 100004;
                            entity.DeliveryAddressID = 100014;
                            entity.DeliveryServiceID = 100002;
                            entity.Comments = "Comments 7a3be9c7dcbb4ce58b77fd74cbd976de";
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("7/13/2019 7:15:42 PM");
                            entity.CreatedByID = 100008;
                            entity.ModifiedDate = DateTime.Parse("7/13/2019 7:15:42 PM");
                            entity.ModifiedByID = 100004;
              
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

        protected IOrderDal PrepareOrderDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IOrderDal dal = new OrderDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
