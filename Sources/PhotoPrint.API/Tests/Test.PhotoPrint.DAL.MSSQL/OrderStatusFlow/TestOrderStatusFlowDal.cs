

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
    public class TestOrderStatusFlowDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IOrderStatusFlowDal dal = new OrderStatusFlowDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void OrderStatusFlow_GetAll_Success()
        {
            var dal = PrepareOrderStatusFlowDal("DALInitParams");

            IList<OrderStatusFlow> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("OrderStatusFlow\\000.GetDetails.Success")]
        public void OrderStatusFlow_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareOrderStatusFlowDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramFromStatusID = (System.Int64)objIds[0];
                var paramToStatusID = (System.Int64)objIds[1];
            OrderStatusFlow entity = dal.Get(paramFromStatusID,paramToStatusID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.FromStatusID);
                        Assert.IsNotNull(entity.ToStatusID);
            
                          Assert.AreEqual(7, entity.FromStatusID);
                            Assert.AreEqual(7, entity.ToStatusID);
                      }

        [Test]
        public void OrderStatusFlow_GetDetails_InvalidId()
        {
                var paramFromStatusID = Int64.MaxValue - 1;
                var paramToStatusID = Int64.MaxValue - 1;
            var dal = PrepareOrderStatusFlowDal("DALInitParams");

            OrderStatusFlow entity = dal.Get(paramFromStatusID,paramToStatusID);

            Assert.IsNull(entity);
        }

        [TestCase("OrderStatusFlow\\010.Delete.Success")]
        public void OrderStatusFlow_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareOrderStatusFlowDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramFromStatusID = (System.Int64)objIds[0];
                var paramToStatusID = (System.Int64)objIds[1];
            bool removed = dal.Delete(paramFromStatusID,paramToStatusID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void OrderStatusFlow_Delete_InvalidId()
        {
            var dal = PrepareOrderStatusFlowDal("DALInitParams");
                var paramFromStatusID = Int64.MaxValue - 1;
                var paramToStatusID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramFromStatusID,paramToStatusID);
            Assert.IsFalse(removed);

        }

        [TestCase("OrderStatusFlow\\020.Insert.Success")]
        public void OrderStatusFlow_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareOrderStatusFlowDal("DALInitParams");

            var entity = new OrderStatusFlow();
                          entity.FromStatusID = 11;
                            entity.ToStatusID = 8;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.FromStatusID);
                        Assert.IsNotNull(entity.ToStatusID);
            
                          Assert.AreEqual(11, entity.FromStatusID);
                            Assert.AreEqual(8, entity.ToStatusID);
              
        }

        [TestCase("OrderStatusFlow\\030.Update.Success")]
        public void OrderStatusFlow_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareOrderStatusFlowDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramFromStatusID = (System.Int64)objIds[0];
                var paramToStatusID = (System.Int64)objIds[1];
            OrderStatusFlow entity = dal.Get(paramFromStatusID,paramToStatusID);

            
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.FromStatusID);
                        Assert.IsNotNull(entity.ToStatusID);
            
                          Assert.AreEqual(2, entity.FromStatusID);
                            Assert.AreEqual(2, entity.ToStatusID);
              
        }

        [Test]
        public void OrderStatusFlow_Update_InvalidId()
        {
            var dal = PrepareOrderStatusFlowDal("DALInitParams");

            var entity = new OrderStatusFlow();
                          entity.FromStatusID = 2;
                            entity.ToStatusID = 2;
              
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

        protected IOrderStatusFlowDal PrepareOrderStatusFlowDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IOrderStatusFlowDal dal = new OrderStatusFlowDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
