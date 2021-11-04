


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
            
                          Assert.AreEqual(100008, entity.ManagerID);
                            Assert.AreEqual(100010, entity.UserID);
                            Assert.AreEqual(100011, entity.ContactID);
                            Assert.AreEqual(100001, entity.DeliveryAddressID);
                            Assert.AreEqual(100008, entity.DeliveryServiceID);
                            Assert.AreEqual("Comments 194a337287434a068440aecb88e158e0", entity.Comments);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("4/8/2020 6:52:39 PM"), entity.CreatedDate);
                            Assert.AreEqual(100011, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("7/8/2020 2:26:39 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100011, entity.ModifiedByID);
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
                            entity.UserID = 100011;
                            entity.ContactID = 100011;
                            entity.DeliveryAddressID = 100011;
                            entity.DeliveryServiceID = 100009;
                            entity.Comments = "Comments c5f620b98172491895386bbdc4b6e977";
                            entity.IsDeleted = false;              
                            entity.CreatedDate = DateTime.Parse("5/12/2024 2:27:39 AM");
                            entity.CreatedByID = 100009;
                            entity.ModifiedDate = DateTime.Parse("9/30/2021 12:14:39 PM");
                            entity.ModifiedByID = 100007;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100011, entity.ManagerID);
                            Assert.AreEqual(100011, entity.UserID);
                            Assert.AreEqual(100011, entity.ContactID);
                            Assert.AreEqual(100011, entity.DeliveryAddressID);
                            Assert.AreEqual(100009, entity.DeliveryServiceID);
                            Assert.AreEqual("Comments c5f620b98172491895386bbdc4b6e977", entity.Comments);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("5/12/2024 2:27:39 AM"), entity.CreatedDate);
                            Assert.AreEqual(100009, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("9/30/2021 12:14:39 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100007, entity.ModifiedByID);
              
        }

        [TestCase("Order\\030.Update.Success")]
        public void Order_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareOrderDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Order entity = dal.Get(paramID);

                          entity.ManagerID = 100004;
                            entity.UserID = 100010;
                            entity.ContactID = 100019;
                            entity.DeliveryAddressID = 100008;
                            entity.DeliveryServiceID = 100003;
                            entity.Comments = "Comments b2d986c5df05439a9c7e449d440564b0";
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("5/18/2019 8:15:39 AM");
                            entity.CreatedByID = 100010;
                            entity.ModifiedDate = DateTime.Parse("3/27/2022 8:41:39 AM");
                            entity.ModifiedByID = 100007;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100004, entity.ManagerID);
                            Assert.AreEqual(100010, entity.UserID);
                            Assert.AreEqual(100019, entity.ContactID);
                            Assert.AreEqual(100008, entity.DeliveryAddressID);
                            Assert.AreEqual(100003, entity.DeliveryServiceID);
                            Assert.AreEqual("Comments b2d986c5df05439a9c7e449d440564b0", entity.Comments);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("5/18/2019 8:15:39 AM"), entity.CreatedDate);
                            Assert.AreEqual(100010, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("3/27/2022 8:41:39 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100007, entity.ModifiedByID);
              
        }

        [Test]
        public void Order_Update_InvalidId()
        {
            var dal = PrepareOrderDal("DALInitParams");

            var entity = new Order();
                          entity.ManagerID = 100004;
                            entity.UserID = 100010;
                            entity.ContactID = 100019;
                            entity.DeliveryAddressID = 100008;
                            entity.DeliveryServiceID = 100003;
                            entity.Comments = "Comments b2d986c5df05439a9c7e449d440564b0";
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("5/18/2019 8:15:39 AM");
                            entity.CreatedByID = 100010;
                            entity.ModifiedDate = DateTime.Parse("3/27/2022 8:41:39 AM");
                            entity.ModifiedByID = 100007;
              
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

        [TestCase("Order\\040.Erase.Success")]
        public void Order_Erase_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareOrderDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Erase(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void Order_Erase_InvalidId()
        {
            var dal = PrepareOrderDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Erase(paramID);
            Assert.IsFalse(removed);

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
