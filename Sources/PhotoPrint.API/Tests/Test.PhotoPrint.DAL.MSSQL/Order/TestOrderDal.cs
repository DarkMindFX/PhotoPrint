

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
            
                          Assert.AreEqual(100004, entity.ManagerID);
                            Assert.AreEqual(100011, entity.UserID);
                            Assert.AreEqual(100017, entity.ContactID);
                            Assert.AreEqual(100010, entity.DeliveryAddressID);
                            Assert.AreEqual(100001, entity.DeliveryServiceID);
                            Assert.AreEqual("Comments b2566b2f2ea34644996af26d9ee31cdd", entity.Comments);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("4/29/2023 1:48:33 PM"), entity.CreatedDate);
                            Assert.AreEqual(100003, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("9/16/2020 11:35:33 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100005, entity.ModifiedByID);
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
                          entity.ManagerID = 100011;
                            entity.UserID = 100004;
                            entity.ContactID = 100014;
                            entity.DeliveryAddressID = 100003;
                            entity.DeliveryServiceID = 100007;
                            entity.Comments = "Comments 506fb48caaac4ff4a0c8db1cad58fe29";
                            entity.IsDeleted = false;              
                            entity.CreatedDate = DateTime.Parse("1/23/2024 5:49:33 AM");
                            entity.CreatedByID = 100005;
                            entity.ModifiedDate = DateTime.Parse("1/23/2024 5:49:33 AM");
                            entity.ModifiedByID = 100001;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100011, entity.ManagerID);
                            Assert.AreEqual(100004, entity.UserID);
                            Assert.AreEqual(100014, entity.ContactID);
                            Assert.AreEqual(100003, entity.DeliveryAddressID);
                            Assert.AreEqual(100007, entity.DeliveryServiceID);
                            Assert.AreEqual("Comments 506fb48caaac4ff4a0c8db1cad58fe29", entity.Comments);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("1/23/2024 5:49:33 AM"), entity.CreatedDate);
                            Assert.AreEqual(100005, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("1/23/2024 5:49:33 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100001, entity.ModifiedByID);
              
        }

        [TestCase("Order\\030.Update.Success")]
        public void Order_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareOrderDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Order entity = dal.Get(paramID);

                          entity.ManagerID = 100002;
                            entity.UserID = 100007;
                            entity.ContactID = 100008;
                            entity.DeliveryAddressID = 100010;
                            entity.DeliveryServiceID = 100008;
                            entity.Comments = "Comments bad23f4418514fd5a0087205ee4379db";
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("9/11/2021 11:10:33 AM");
                            entity.CreatedByID = 100005;
                            entity.ModifiedDate = DateTime.Parse("7/21/2024 11:37:33 AM");
                            entity.ModifiedByID = 100006;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100002, entity.ManagerID);
                            Assert.AreEqual(100007, entity.UserID);
                            Assert.AreEqual(100008, entity.ContactID);
                            Assert.AreEqual(100010, entity.DeliveryAddressID);
                            Assert.AreEqual(100008, entity.DeliveryServiceID);
                            Assert.AreEqual("Comments bad23f4418514fd5a0087205ee4379db", entity.Comments);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("9/11/2021 11:10:33 AM"), entity.CreatedDate);
                            Assert.AreEqual(100005, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("7/21/2024 11:37:33 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100006, entity.ModifiedByID);
              
        }

        [Test]
        public void Order_Update_InvalidId()
        {
            var dal = PrepareOrderDal("DALInitParams");

            var entity = new Order();
                          entity.ManagerID = 100002;
                            entity.UserID = 100007;
                            entity.ContactID = 100008;
                            entity.DeliveryAddressID = 100010;
                            entity.DeliveryServiceID = 100008;
                            entity.Comments = "Comments bad23f4418514fd5a0087205ee4379db";
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("9/11/2021 11:10:33 AM");
                            entity.CreatedByID = 100005;
                            entity.ModifiedDate = DateTime.Parse("7/21/2024 11:37:33 AM");
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
