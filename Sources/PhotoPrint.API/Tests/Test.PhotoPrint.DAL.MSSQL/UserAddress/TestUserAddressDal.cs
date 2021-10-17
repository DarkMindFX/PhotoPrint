

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
    public class TestUserAddressDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IUserAddressDal dal = new UserAddressDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void UserAddress_GetAll_Success()
        {
            var dal = PrepareUserAddressDal("DALInitParams");

            IList<UserAddress> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("UserAddress\\000.GetDetails.Success")]
        public void UserAddress_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserAddressDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramUserID = (System.Int64)objIds[0];
                var paramAddressID = (System.Int64)objIds[1];
            UserAddress entity = dal.Get(paramUserID,paramAddressID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.UserID);
                        Assert.IsNotNull(entity.AddressID);
            
                          Assert.AreEqual(100007, entity.UserID);
                            Assert.AreEqual(100010, entity.AddressID);
                            Assert.AreEqual(true, entity.IsPrimary);
                      }

        [Test]
        public void UserAddress_GetDetails_InvalidId()
        {
                var paramUserID = Int64.MaxValue - 1;
                var paramAddressID = Int64.MaxValue - 1;
            var dal = PrepareUserAddressDal("DALInitParams");

            UserAddress entity = dal.Get(paramUserID,paramAddressID);

            Assert.IsNull(entity);
        }

        [TestCase("UserAddress\\010.Delete.Success")]
        public void UserAddress_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserAddressDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramUserID = (System.Int64)objIds[0];
                var paramAddressID = (System.Int64)objIds[1];
            bool removed = dal.Delete(paramUserID,paramAddressID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void UserAddress_Delete_InvalidId()
        {
            var dal = PrepareUserAddressDal("DALInitParams");
                var paramUserID = Int64.MaxValue - 1;
                var paramAddressID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramUserID,paramAddressID);
            Assert.IsFalse(removed);

        }

        [TestCase("UserAddress\\020.Insert.Success")]
        public void UserAddress_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareUserAddressDal("DALInitParams");

            var entity = new UserAddress();
                          entity.UserID = 100007;
                            entity.AddressID = 100011;
                            entity.IsPrimary = false;              
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.UserID);
                        Assert.IsNotNull(entity.AddressID);
            
                          Assert.AreEqual(100007, entity.UserID);
                            Assert.AreEqual(100011, entity.AddressID);
                            Assert.AreEqual(false, entity.IsPrimary);
              
        }

        [TestCase("UserAddress\\030.Update.Success")]
        public void UserAddress_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserAddressDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramUserID = (System.Int64)objIds[0];
                var paramAddressID = (System.Int64)objIds[1];
            UserAddress entity = dal.Get(paramUserID,paramAddressID);

                          entity.IsPrimary = false;              
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.UserID);
                        Assert.IsNotNull(entity.AddressID);
            
                          Assert.AreEqual(100004, entity.UserID);
                            Assert.AreEqual(100011, entity.AddressID);
                            Assert.AreEqual(false, entity.IsPrimary);
              
        }

        [Test]
        public void UserAddress_Update_InvalidId()
        {
            var dal = PrepareUserAddressDal("DALInitParams");

            var entity = new UserAddress();
                          entity.UserID = 100004;
                            entity.AddressID = 100011;
                            entity.IsPrimary = false;              
              
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

        protected IUserAddressDal PrepareUserAddressDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IUserAddressDal dal = new UserAddressDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
