

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
    public class TestUserDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IUserDal dal = new UserDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void User_GetAll_Success()
        {
            var dal = PrepareUserDal("DALInitParams");

            IList<User> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("User\\000.GetDetails.Success")]
        public void User_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            User entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Login 809c2ba8d14044e4ab406499226bc9e8", entity.Login);
                            Assert.AreEqual("PwdHash 809c2ba8d14044e4ab406499226bc9e8", entity.PwdHash);
                            Assert.AreEqual("Salt 809c2ba8d14044e4ab406499226bc9e8", entity.Salt);
                            Assert.AreEqual("FirstName 809c2ba8d14044e4ab406499226bc9e8", entity.FirstName);
                            Assert.AreEqual("MiddleName 809c2ba8d14044e4ab406499226bc9e8", entity.MiddleName);
                            Assert.AreEqual("LastName 809c2ba8d14044e4ab406499226bc9e8", entity.LastName);
                            Assert.AreEqual("FriendlyName 809c2ba8d14044e4ab406499226bc9e8", entity.FriendlyName);
                            Assert.AreEqual(1, entity.UserStatusID);
                            Assert.AreEqual(2, entity.UserTypeID);
                            Assert.AreEqual(DateTime.Parse("5/15/2019 6:21:49 AM"), entity.CreatedDate);
                            Assert.AreEqual(DateTime.Parse("5/15/2019 6:21:49 AM"), entity.ModifiedDate);
                            Assert.AreEqual(56865, entity.ModifiedByID);
                      }

        [Test]
        public void User_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareUserDal("DALInitParams");

            User entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("User\\010.Delete.Success")]
        public void User_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void User_Delete_InvalidId()
        {
            var dal = PrepareUserDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("User\\020.Insert.Success")]
        public void User_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareUserDal("DALInitParams");

            var entity = new User();
                          entity.Login = "Login a778285d591c4e42a56bcc6c6d2d1b46";
                            entity.PwdHash = "PwdHash a778285d591c4e42a56bcc6c6d2d1b46";
                            entity.Salt = "Salt a778285d591c4e42a56bcc6c6d2d1b46";
                            entity.FirstName = "FirstName a778285d591c4e42a56bcc6c6d2d1b46";
                            entity.MiddleName = "MiddleName a778285d591c4e42a56bcc6c6d2d1b46";
                            entity.LastName = "LastName a778285d591c4e42a56bcc6c6d2d1b46";
                            entity.FriendlyName = "FriendlyName a778285d591c4e42a56bcc6c6d2d1b46";
                            entity.UserStatusID = 2;
                            entity.UserTypeID = 2;
                            entity.CreatedDate = DateTime.Parse("9/14/2023 12:10:49 AM");
                            entity.ModifiedDate = DateTime.Parse("9/14/2023 12:10:49 AM");
                            entity.ModifiedByID = 848393;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Login a778285d591c4e42a56bcc6c6d2d1b46", entity.Login);
                            Assert.AreEqual("PwdHash a778285d591c4e42a56bcc6c6d2d1b46", entity.PwdHash);
                            Assert.AreEqual("Salt a778285d591c4e42a56bcc6c6d2d1b46", entity.Salt);
                            Assert.AreEqual("FirstName a778285d591c4e42a56bcc6c6d2d1b46", entity.FirstName);
                            Assert.AreEqual("MiddleName a778285d591c4e42a56bcc6c6d2d1b46", entity.MiddleName);
                            Assert.AreEqual("LastName a778285d591c4e42a56bcc6c6d2d1b46", entity.LastName);
                            Assert.AreEqual("FriendlyName a778285d591c4e42a56bcc6c6d2d1b46", entity.FriendlyName);
                            Assert.AreEqual(2, entity.UserStatusID);
                            Assert.AreEqual(2, entity.UserTypeID);
                            Assert.AreEqual(DateTime.Parse("9/14/2023 12:10:49 AM"), entity.CreatedDate);
                            Assert.AreEqual(DateTime.Parse("9/14/2023 12:10:49 AM"), entity.ModifiedDate);
                            Assert.AreEqual(848393, entity.ModifiedByID);
              
        }

        [TestCase("User\\030.Update.Success")]
        public void User_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            User entity = dal.Get(paramID);

                          entity.Login = "Login a60f1d045db94762ba2a95296f60fb80";
                            entity.PwdHash = "PwdHash a60f1d045db94762ba2a95296f60fb80";
                            entity.Salt = "Salt a60f1d045db94762ba2a95296f60fb80";
                            entity.FirstName = "FirstName a60f1d045db94762ba2a95296f60fb80";
                            entity.MiddleName = "MiddleName a60f1d045db94762ba2a95296f60fb80";
                            entity.LastName = "LastName a60f1d045db94762ba2a95296f60fb80";
                            entity.FriendlyName = "FriendlyName a60f1d045db94762ba2a95296f60fb80";
                            entity.UserStatusID = 1;
                            entity.UserTypeID = 2;
                            entity.CreatedDate = DateTime.Parse("12/12/2023 10:23:49 AM");
                            entity.ModifiedDate = DateTime.Parse("12/12/2023 10:23:49 AM");
                            entity.ModifiedByID = 893243;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Login a60f1d045db94762ba2a95296f60fb80", entity.Login);
                            Assert.AreEqual("PwdHash a60f1d045db94762ba2a95296f60fb80", entity.PwdHash);
                            Assert.AreEqual("Salt a60f1d045db94762ba2a95296f60fb80", entity.Salt);
                            Assert.AreEqual("FirstName a60f1d045db94762ba2a95296f60fb80", entity.FirstName);
                            Assert.AreEqual("MiddleName a60f1d045db94762ba2a95296f60fb80", entity.MiddleName);
                            Assert.AreEqual("LastName a60f1d045db94762ba2a95296f60fb80", entity.LastName);
                            Assert.AreEqual("FriendlyName a60f1d045db94762ba2a95296f60fb80", entity.FriendlyName);
                            Assert.AreEqual(1, entity.UserStatusID);
                            Assert.AreEqual(2, entity.UserTypeID);
                            Assert.AreEqual(DateTime.Parse("12/12/2023 10:23:49 AM"), entity.CreatedDate);
                            Assert.AreEqual(DateTime.Parse("12/12/2023 10:23:49 AM"), entity.ModifiedDate);
                            Assert.AreEqual(893243, entity.ModifiedByID);
              
        }

        [Test]
        public void User_Update_InvalidId()
        {
            var dal = PrepareUserDal("DALInitParams");

            var entity = new User();
                          entity.Login = "Login a60f1d045db94762ba2a95296f60fb80";
                            entity.PwdHash = "PwdHash a60f1d045db94762ba2a95296f60fb80";
                            entity.Salt = "Salt a60f1d045db94762ba2a95296f60fb80";
                            entity.FirstName = "FirstName a60f1d045db94762ba2a95296f60fb80";
                            entity.MiddleName = "MiddleName a60f1d045db94762ba2a95296f60fb80";
                            entity.LastName = "LastName a60f1d045db94762ba2a95296f60fb80";
                            entity.FriendlyName = "FriendlyName a60f1d045db94762ba2a95296f60fb80";
                            entity.UserStatusID = 1;
                            entity.UserTypeID = 2;
                            entity.CreatedDate = DateTime.Parse("12/12/2023 10:23:49 AM");
                            entity.ModifiedDate = DateTime.Parse("12/12/2023 10:23:49 AM");
                            entity.ModifiedByID = 893243;
              
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

        protected IUserDal PrepareUserDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IUserDal dal = new UserDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
