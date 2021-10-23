

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
    public class TestAddressTypeDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IAddressTypeDal dal = new AddressTypeDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void AddressType_GetAll_Success()
        {
            var dal = PrepareAddressTypeDal("DALInitParams");

            IList<AddressType> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("AddressType\\000.GetDetails.Success")]
        public void AddressType_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareAddressTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            AddressType entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("AddressTypeName 43f079185d234707ba0db64329ebbbf0", entity.AddressTypeName);
                            Assert.AreEqual(false, entity.IsDeleted);
                      }

        [Test]
        public void AddressType_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareAddressTypeDal("DALInitParams");

            AddressType entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("AddressType\\010.Delete.Success")]
        public void AddressType_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareAddressTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void AddressType_Delete_InvalidId()
        {
            var dal = PrepareAddressTypeDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("AddressType\\020.Insert.Success")]
        public void AddressType_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareAddressTypeDal("DALInitParams");

            var entity = new AddressType();
                          entity.AddressTypeName = "AddressTypeName 334da7e2cc544101a8df95875d584809";
                            entity.IsDeleted = false;              
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("AddressTypeName 334da7e2cc544101a8df95875d584809", entity.AddressTypeName);
                            Assert.AreEqual(false, entity.IsDeleted);
              
        }

        [TestCase("AddressType\\030.Update.Success")]
        public void AddressType_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareAddressTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            AddressType entity = dal.Get(paramID);

                          entity.AddressTypeName = "AddressTypeName 593dcfb6484640089cae34d61f69f880";
                            entity.IsDeleted = false;              
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("AddressTypeName 593dcfb6484640089cae34d61f69f880", entity.AddressTypeName);
                            Assert.AreEqual(false, entity.IsDeleted);
              
        }

        [Test]
        public void AddressType_Update_InvalidId()
        {
            var dal = PrepareAddressTypeDal("DALInitParams");

            var entity = new AddressType();
                          entity.AddressTypeName = "AddressTypeName 593dcfb6484640089cae34d61f69f880";
                            entity.IsDeleted = false;              
              
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

        protected IAddressTypeDal PrepareAddressTypeDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IAddressTypeDal dal = new AddressTypeDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}