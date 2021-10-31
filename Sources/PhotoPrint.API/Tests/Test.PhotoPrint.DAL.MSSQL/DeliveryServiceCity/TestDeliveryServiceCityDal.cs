

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
    public class TestDeliveryServiceCityDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IDeliveryServiceCityDal dal = new DeliveryServiceCityDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void DeliveryServiceCity_GetAll_Success()
        {
            var dal = PrepareDeliveryServiceCityDal("DALInitParams");

            IList<DeliveryServiceCity> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("DeliveryServiceCity\\000.GetDetails.Success")]
        public void DeliveryServiceCity_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareDeliveryServiceCityDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramDeliveryServiceID = (System.Int64)objIds[0];
                var paramCityID = (System.Int64)objIds[1];
            DeliveryServiceCity entity = dal.Get(paramDeliveryServiceID,paramCityID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.DeliveryServiceID);
                        Assert.IsNotNull(entity.CityID);
            
                          Assert.AreEqual(100005, entity.DeliveryServiceID);
                            Assert.AreEqual(6, entity.CityID);
                      }

        [Test]
        public void DeliveryServiceCity_GetDetails_InvalidId()
        {
                var paramDeliveryServiceID = Int64.MaxValue - 1;
                var paramCityID = Int64.MaxValue - 1;
            var dal = PrepareDeliveryServiceCityDal("DALInitParams");

            DeliveryServiceCity entity = dal.Get(paramDeliveryServiceID,paramCityID);

            Assert.IsNull(entity);
        }

        [TestCase("DeliveryServiceCity\\010.Delete.Success")]
        public void DeliveryServiceCity_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareDeliveryServiceCityDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramDeliveryServiceID = (System.Int64)objIds[0];
                var paramCityID = (System.Int64)objIds[1];
            bool removed = dal.Delete(paramDeliveryServiceID,paramCityID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void DeliveryServiceCity_Delete_InvalidId()
        {
            var dal = PrepareDeliveryServiceCityDal("DALInitParams");
                var paramDeliveryServiceID = Int64.MaxValue - 1;
                var paramCityID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramDeliveryServiceID,paramCityID);
            Assert.IsFalse(removed);

        }

        [TestCase("DeliveryServiceCity\\020.Insert.Success")]
        public void DeliveryServiceCity_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareDeliveryServiceCityDal("DALInitParams");

            var entity = new DeliveryServiceCity();
                          entity.DeliveryServiceID = 100009;
                            entity.CityID = 1;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.DeliveryServiceID);
                        Assert.IsNotNull(entity.CityID);
            
                          Assert.AreEqual(100009, entity.DeliveryServiceID);
                            Assert.AreEqual(1, entity.CityID);
              
        }

        [TestCase("DeliveryServiceCity\\030.Update.Success")]
        public void DeliveryServiceCity_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareDeliveryServiceCityDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramDeliveryServiceID = (System.Int64)objIds[0];
                var paramCityID = (System.Int64)objIds[1];
            DeliveryServiceCity entity = dal.Get(paramDeliveryServiceID,paramCityID);

            
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.DeliveryServiceID);
                        Assert.IsNotNull(entity.CityID);
            
                          Assert.AreEqual(100009, entity.DeliveryServiceID);
                            Assert.AreEqual(1, entity.CityID);
              
        }

        [Test]
        public void DeliveryServiceCity_Update_InvalidId()
        {
            var dal = PrepareDeliveryServiceCityDal("DALInitParams");

            var entity = new DeliveryServiceCity();
                          entity.DeliveryServiceID = 100009;
                            entity.CityID = 1;
              
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

        protected IDeliveryServiceCityDal PrepareDeliveryServiceCityDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IDeliveryServiceCityDal dal = new DeliveryServiceCityDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
