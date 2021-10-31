

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
    public class TestUserContactDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IUserContactDal dal = new UserContactDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void UserContact_GetAll_Success()
        {
            var dal = PrepareUserContactDal("DALInitParams");

            IList<UserContact> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("UserContact\\000.GetDetails.Success")]
        public void UserContact_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserContactDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramUserID = (System.Int64)objIds[0];
                var paramContactID = (System.Int64)objIds[1];
            UserContact entity = dal.Get(paramUserID,paramContactID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.UserID);
                        Assert.IsNotNull(entity.ContactID);
            
                          Assert.AreEqual(100011, entity.UserID);
                            Assert.AreEqual(100021, entity.ContactID);
                            Assert.AreEqual(true, entity.IsPrimary);
                      }

        [Test]
        public void UserContact_GetDetails_InvalidId()
        {
                var paramUserID = Int64.MaxValue - 1;
                var paramContactID = Int64.MaxValue - 1;
            var dal = PrepareUserContactDal("DALInitParams");

            UserContact entity = dal.Get(paramUserID,paramContactID);

            Assert.IsNull(entity);
        }

        [TestCase("UserContact\\010.Delete.Success")]
        public void UserContact_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserContactDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramUserID = (System.Int64)objIds[0];
                var paramContactID = (System.Int64)objIds[1];
            bool removed = dal.Delete(paramUserID,paramContactID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void UserContact_Delete_InvalidId()
        {
            var dal = PrepareUserContactDal("DALInitParams");
                var paramUserID = Int64.MaxValue - 1;
                var paramContactID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramUserID,paramContactID);
            Assert.IsFalse(removed);

        }

        [TestCase("UserContact\\020.Insert.Success")]
        public void UserContact_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareUserContactDal("DALInitParams");

            var entity = new UserContact();
                          entity.UserID = 100004;
                            entity.ContactID = 100020;
                            entity.IsPrimary = false;              
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.UserID);
                        Assert.IsNotNull(entity.ContactID);
            
                          Assert.AreEqual(100004, entity.UserID);
                            Assert.AreEqual(100020, entity.ContactID);
                            Assert.AreEqual(false, entity.IsPrimary);
              
        }

        [TestCase("UserContact\\030.Update.Success")]
        public void UserContact_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserContactDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramUserID = (System.Int64)objIds[0];
                var paramContactID = (System.Int64)objIds[1];
            UserContact entity = dal.Get(paramUserID,paramContactID);

                          entity.IsPrimary = false;              
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.UserID);
                        Assert.IsNotNull(entity.ContactID);
            
                          Assert.AreEqual(100010, entity.UserID);
                            Assert.AreEqual(100011, entity.ContactID);
                            Assert.AreEqual(false, entity.IsPrimary);
              
        }

        [Test]
        public void UserContact_Update_InvalidId()
        {
            var dal = PrepareUserContactDal("DALInitParams");

            var entity = new UserContact();
                          entity.UserID = 100010;
                            entity.ContactID = 100011;
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

        protected IUserContactDal PrepareUserContactDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IUserContactDal dal = new UserContactDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
