


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
            
                          Assert.AreEqual("AddressTypeName c5092e878ee34f1592cb2935104a01c5", entity.AddressTypeName);
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
                          entity.AddressTypeName = "AddressTypeName 822c55762f2c4301a9ecb5f4b2a5e3c0";
                            entity.IsDeleted = false;              
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("AddressTypeName 822c55762f2c4301a9ecb5f4b2a5e3c0", entity.AddressTypeName);
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

                          entity.AddressTypeName = "AddressTypeName cfb49d68fcd344c6a5ca135ea0929f9d";
                            entity.IsDeleted = false;              
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("AddressTypeName cfb49d68fcd344c6a5ca135ea0929f9d", entity.AddressTypeName);
                            Assert.AreEqual(false, entity.IsDeleted);
              
        }

        [Test]
        public void AddressType_Update_InvalidId()
        {
            var dal = PrepareAddressTypeDal("DALInitParams");

            var entity = new AddressType();
                          entity.AddressTypeName = "AddressTypeName cfb49d68fcd344c6a5ca135ea0929f9d";
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

        [TestCase("AddressType\\040.Erase.Success")]
        public void AddressType_Erase_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareAddressTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Erase(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void AddressType_Erase_InvalidId()
        {
            var dal = PrepareAddressTypeDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Erase(paramID);
            Assert.IsFalse(removed);

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
