

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
    public class TestOrderStatusDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IOrderStatusDal dal = new OrderStatusDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void OrderStatus_GetAll_Success()
        {
            var dal = PrepareOrderStatusDal("DALInitParams");

            IList<OrderStatus> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("OrderStatus\\000.GetDetails.Success")]
        public void OrderStatus_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareOrderStatusDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            OrderStatus entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("OrderStatusName 13260a8d84d942c48686b8b701d3ebb3", entity.OrderStatusName);
                            Assert.AreEqual(true, entity.IsDeleted);
                      }

        [Test]
        public void OrderStatus_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareOrderStatusDal("DALInitParams");

            OrderStatus entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("OrderStatus\\010.Delete.Success")]
        public void OrderStatus_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareOrderStatusDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void OrderStatus_Delete_InvalidId()
        {
            var dal = PrepareOrderStatusDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("OrderStatus\\020.Insert.Success")]
        public void OrderStatus_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareOrderStatusDal("DALInitParams");

            var entity = new OrderStatus();
                          entity.OrderStatusName = "OrderStatusName 0db821e8effa4aef91b5f4c7037be14e";
                            entity.IsDeleted = true;              
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("OrderStatusName 0db821e8effa4aef91b5f4c7037be14e", entity.OrderStatusName);
                            Assert.AreEqual(true, entity.IsDeleted);
              
        }

        [TestCase("OrderStatus\\030.Update.Success")]
        public void OrderStatus_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareOrderStatusDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            OrderStatus entity = dal.Get(paramID);

                          entity.OrderStatusName = "OrderStatusName 753473acdd8241f5b3884d7b88f2b07a";
                            entity.IsDeleted = true;              
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("OrderStatusName 753473acdd8241f5b3884d7b88f2b07a", entity.OrderStatusName);
                            Assert.AreEqual(true, entity.IsDeleted);
              
        }

        [Test]
        public void OrderStatus_Update_InvalidId()
        {
            var dal = PrepareOrderStatusDal("DALInitParams");

            var entity = new OrderStatus();
                          entity.OrderStatusName = "OrderStatusName 753473acdd8241f5b3884d7b88f2b07a";
                            entity.IsDeleted = true;              
              
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

        protected IOrderStatusDal PrepareOrderStatusDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IOrderStatusDal dal = new OrderStatusDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
