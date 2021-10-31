

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
            
                          Assert.AreEqual("Login be029098e11649d9bcdb2a796db12cdc", entity.Login);
                            Assert.AreEqual("PwdHash be029098e11649d9bcdb2a796db12cdc", entity.PwdHash);
                            Assert.AreEqual("Salt be029098e11649d9bcdb2a796db12cdc", entity.Salt);
                            Assert.AreEqual("FirstName be029098e11649d9bcdb2a796db12cdc", entity.FirstName);
                            Assert.AreEqual("MiddleName be029098e11649d9bcdb2a796db12cdc", entity.MiddleName);
                            Assert.AreEqual("LastName be029098e11649d9bcdb2a796db12cdc", entity.LastName);
                            Assert.AreEqual("FriendlyName be029098e11649d9bcdb2a796db12cdc", entity.FriendlyName);
                            Assert.AreEqual(4, entity.UserStatusID);
                            Assert.AreEqual(3, entity.UserTypeID);
                            Assert.AreEqual(DateTime.Parse("6/2/2023 7:54:34 AM"), entity.CreatedDate);
                            Assert.AreEqual(DateTime.Parse("6/2/2023 7:54:34 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100001, entity.ModifiedByID);
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
                          entity.Login = "Login 02b20e11d11647059df5e9117224a66e";
                            entity.PwdHash = "PwdHash 02b20e11d11647059df5e9117224a66e";
                            entity.Salt = "Salt 02b20e11d11647059df5e9117224a66e";
                            entity.FirstName = "FirstName 02b20e11d11647059df5e9117224a66e";
                            entity.MiddleName = "MiddleName 02b20e11d11647059df5e9117224a66e";
                            entity.LastName = "LastName 02b20e11d11647059df5e9117224a66e";
                            entity.FriendlyName = "FriendlyName 02b20e11d11647059df5e9117224a66e";
                            entity.UserStatusID = 2;
                            entity.UserTypeID = 2;
                            entity.CreatedDate = DateTime.Parse("8/30/2023 6:08:34 PM");
                            entity.ModifiedDate = DateTime.Parse("8/30/2023 6:08:34 PM");
                            entity.ModifiedByID = 100004;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Login 02b20e11d11647059df5e9117224a66e", entity.Login);
                            Assert.AreEqual("PwdHash 02b20e11d11647059df5e9117224a66e", entity.PwdHash);
                            Assert.AreEqual("Salt 02b20e11d11647059df5e9117224a66e", entity.Salt);
                            Assert.AreEqual("FirstName 02b20e11d11647059df5e9117224a66e", entity.FirstName);
                            Assert.AreEqual("MiddleName 02b20e11d11647059df5e9117224a66e", entity.MiddleName);
                            Assert.AreEqual("LastName 02b20e11d11647059df5e9117224a66e", entity.LastName);
                            Assert.AreEqual("FriendlyName 02b20e11d11647059df5e9117224a66e", entity.FriendlyName);
                            Assert.AreEqual(2, entity.UserStatusID);
                            Assert.AreEqual(2, entity.UserTypeID);
                            Assert.AreEqual(DateTime.Parse("8/30/2023 6:08:34 PM"), entity.CreatedDate);
                            Assert.AreEqual(DateTime.Parse("8/30/2023 6:08:34 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100004, entity.ModifiedByID);
              
        }

        [TestCase("User\\030.Update.Success")]
        public void User_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            User entity = dal.Get(paramID);

                          entity.Login = "Login 31933898fd784a85bc9ac30b93269103";
                            entity.PwdHash = "PwdHash 31933898fd784a85bc9ac30b93269103";
                            entity.Salt = "Salt 31933898fd784a85bc9ac30b93269103";
                            entity.FirstName = "FirstName 31933898fd784a85bc9ac30b93269103";
                            entity.MiddleName = "MiddleName 31933898fd784a85bc9ac30b93269103";
                            entity.LastName = "LastName 31933898fd784a85bc9ac30b93269103";
                            entity.FriendlyName = "FriendlyName 31933898fd784a85bc9ac30b93269103";
                            entity.UserStatusID = 4;
                            entity.UserTypeID = 4;
                            entity.CreatedDate = DateTime.Parse("2/25/2024 11:55:34 PM");
                            entity.ModifiedDate = DateTime.Parse("2/25/2024 11:55:34 PM");
                            entity.ModifiedByID = 100005;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Login 31933898fd784a85bc9ac30b93269103", entity.Login);
                            Assert.AreEqual("PwdHash 31933898fd784a85bc9ac30b93269103", entity.PwdHash);
                            Assert.AreEqual("Salt 31933898fd784a85bc9ac30b93269103", entity.Salt);
                            Assert.AreEqual("FirstName 31933898fd784a85bc9ac30b93269103", entity.FirstName);
                            Assert.AreEqual("MiddleName 31933898fd784a85bc9ac30b93269103", entity.MiddleName);
                            Assert.AreEqual("LastName 31933898fd784a85bc9ac30b93269103", entity.LastName);
                            Assert.AreEqual("FriendlyName 31933898fd784a85bc9ac30b93269103", entity.FriendlyName);
                            Assert.AreEqual(4, entity.UserStatusID);
                            Assert.AreEqual(4, entity.UserTypeID);
                            Assert.AreEqual(DateTime.Parse("2/25/2024 11:55:34 PM"), entity.CreatedDate);
                            Assert.AreEqual(DateTime.Parse("2/25/2024 11:55:34 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100005, entity.ModifiedByID);
              
        }

        [Test]
        public void User_Update_InvalidId()
        {
            var dal = PrepareUserDal("DALInitParams");

            var entity = new User();
                          entity.Login = "Login 31933898fd784a85bc9ac30b93269103";
                            entity.PwdHash = "PwdHash 31933898fd784a85bc9ac30b93269103";
                            entity.Salt = "Salt 31933898fd784a85bc9ac30b93269103";
                            entity.FirstName = "FirstName 31933898fd784a85bc9ac30b93269103";
                            entity.MiddleName = "MiddleName 31933898fd784a85bc9ac30b93269103";
                            entity.LastName = "LastName 31933898fd784a85bc9ac30b93269103";
                            entity.FriendlyName = "FriendlyName 31933898fd784a85bc9ac30b93269103";
                            entity.UserStatusID = 4;
                            entity.UserTypeID = 4;
                            entity.CreatedDate = DateTime.Parse("2/25/2024 11:55:34 PM");
                            entity.ModifiedDate = DateTime.Parse("2/25/2024 11:55:34 PM");
                            entity.ModifiedByID = 100005;
              
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
