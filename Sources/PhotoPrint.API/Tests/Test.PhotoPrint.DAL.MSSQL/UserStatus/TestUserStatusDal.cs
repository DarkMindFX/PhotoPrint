

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
    public class TestUserStatusDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IUserStatusDal dal = new UserStatusDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void UserStatus_GetAll_Success()
        {
            var dal = PrepareUserStatusDal("DALInitParams");

            IList<UserStatus> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("UserStatus\\000.GetDetails.Success")]
        public void UserStatus_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserStatusDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            UserStatus entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("StatusName 33eb2b1483a44ce2914115b015687aeb", entity.StatusName);
                            Assert.AreEqual(true, entity.IsDeleted);
                      }

        [Test]
        public void UserStatus_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareUserStatusDal("DALInitParams");

            UserStatus entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("UserStatus\\010.Delete.Success")]
        public void UserStatus_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserStatusDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void UserStatus_Delete_InvalidId()
        {
            var dal = PrepareUserStatusDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("UserStatus\\020.Insert.Success")]
        public void UserStatus_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareUserStatusDal("DALInitParams");

            var entity = new UserStatus();
                          entity.StatusName = "StatusName 72d5d597be744ee6bb973abba93c5c48";
                            entity.IsDeleted = true;              
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("StatusName 72d5d597be744ee6bb973abba93c5c48", entity.StatusName);
                            Assert.AreEqual(true, entity.IsDeleted);
              
        }

        [TestCase("UserStatus\\030.Update.Success")]
        public void UserStatus_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserStatusDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            UserStatus entity = dal.Get(paramID);

                          entity.StatusName = "StatusName 7da01d86bd2d434aa11ecf83f647a6c2";
                            entity.IsDeleted = true;              
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("StatusName 7da01d86bd2d434aa11ecf83f647a6c2", entity.StatusName);
                            Assert.AreEqual(true, entity.IsDeleted);
              
        }

        [Test]
        public void UserStatus_Update_InvalidId()
        {
            var dal = PrepareUserStatusDal("DALInitParams");

            var entity = new UserStatus();
                          entity.StatusName = "StatusName 7da01d86bd2d434aa11ecf83f647a6c2";
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

        protected IUserStatusDal PrepareUserStatusDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IUserStatusDal dal = new UserStatusDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
