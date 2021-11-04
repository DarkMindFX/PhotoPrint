


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
            
                          Assert.AreEqual("Login 1256e39b49b545bf9f6077896a843518", entity.Login);
                            Assert.AreEqual("PwdHash 1256e39b49b545bf9f6077896a843518", entity.PwdHash);
                            Assert.AreEqual("Salt 1256e39b49b545bf9f6077896a843518", entity.Salt);
                            Assert.AreEqual("FirstName 1256e39b49b545bf9f6077896a843518", entity.FirstName);
                            Assert.AreEqual("MiddleName 1256e39b49b545bf9f6077896a843518", entity.MiddleName);
                            Assert.AreEqual("LastName 1256e39b49b545bf9f6077896a843518", entity.LastName);
                            Assert.AreEqual("FriendlyName 1256e39b49b545bf9f6077896a843518", entity.FriendlyName);
                            Assert.AreEqual(1, entity.UserStatusID);
                            Assert.AreEqual(10036, entity.UserTypeID);
                            Assert.AreEqual(DateTime.Parse("12/25/2020 9:37:40 AM"), entity.CreatedDate);
                            Assert.AreEqual(DateTime.Parse("12/25/2020 9:37:40 AM"), entity.ModifiedDate);
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
                          entity.Login = "Login 0c772dda60bb4c4f890a8736199e02d6";
                            entity.PwdHash = "PwdHash 0c772dda60bb4c4f890a8736199e02d6";
                            entity.Salt = "Salt 0c772dda60bb4c4f890a8736199e02d6";
                            entity.FirstName = "FirstName 0c772dda60bb4c4f890a8736199e02d6";
                            entity.MiddleName = "MiddleName 0c772dda60bb4c4f890a8736199e02d6";
                            entity.LastName = "LastName 0c772dda60bb4c4f890a8736199e02d6";
                            entity.FriendlyName = "FriendlyName 0c772dda60bb4c4f890a8736199e02d6";
                            entity.UserStatusID = 1;
                            entity.UserTypeID = 4;
                            entity.CreatedDate = DateTime.Parse("3/4/2020 2:37:40 PM");
                            entity.ModifiedDate = DateTime.Parse("3/4/2020 2:37:40 PM");
                            entity.ModifiedByID = 100010;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Login 0c772dda60bb4c4f890a8736199e02d6", entity.Login);
                            Assert.AreEqual("PwdHash 0c772dda60bb4c4f890a8736199e02d6", entity.PwdHash);
                            Assert.AreEqual("Salt 0c772dda60bb4c4f890a8736199e02d6", entity.Salt);
                            Assert.AreEqual("FirstName 0c772dda60bb4c4f890a8736199e02d6", entity.FirstName);
                            Assert.AreEqual("MiddleName 0c772dda60bb4c4f890a8736199e02d6", entity.MiddleName);
                            Assert.AreEqual("LastName 0c772dda60bb4c4f890a8736199e02d6", entity.LastName);
                            Assert.AreEqual("FriendlyName 0c772dda60bb4c4f890a8736199e02d6", entity.FriendlyName);
                            Assert.AreEqual(1, entity.UserStatusID);
                            Assert.AreEqual(4, entity.UserTypeID);
                            Assert.AreEqual(DateTime.Parse("3/4/2020 2:37:40 PM"), entity.CreatedDate);
                            Assert.AreEqual(DateTime.Parse("3/4/2020 2:37:40 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100010, entity.ModifiedByID);
              
        }

        [TestCase("User\\030.Update.Success")]
        public void User_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            User entity = dal.Get(paramID);

                          entity.Login = "Login 41ccc074dd3843bea7aa3387a488f724";
                            entity.PwdHash = "PwdHash 41ccc074dd3843bea7aa3387a488f724";
                            entity.Salt = "Salt 41ccc074dd3843bea7aa3387a488f724";
                            entity.FirstName = "FirstName 41ccc074dd3843bea7aa3387a488f724";
                            entity.MiddleName = "MiddleName 41ccc074dd3843bea7aa3387a488f724";
                            entity.LastName = "LastName 41ccc074dd3843bea7aa3387a488f724";
                            entity.FriendlyName = "FriendlyName 41ccc074dd3843bea7aa3387a488f724";
                            entity.UserStatusID = 2;
                            entity.UserTypeID = 5;
                            entity.CreatedDate = DateTime.Parse("7/10/2023 8:51:40 PM");
                            entity.ModifiedDate = DateTime.Parse("7/10/2023 8:51:40 PM");
                            entity.ModifiedByID = 100010;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Login 41ccc074dd3843bea7aa3387a488f724", entity.Login);
                            Assert.AreEqual("PwdHash 41ccc074dd3843bea7aa3387a488f724", entity.PwdHash);
                            Assert.AreEqual("Salt 41ccc074dd3843bea7aa3387a488f724", entity.Salt);
                            Assert.AreEqual("FirstName 41ccc074dd3843bea7aa3387a488f724", entity.FirstName);
                            Assert.AreEqual("MiddleName 41ccc074dd3843bea7aa3387a488f724", entity.MiddleName);
                            Assert.AreEqual("LastName 41ccc074dd3843bea7aa3387a488f724", entity.LastName);
                            Assert.AreEqual("FriendlyName 41ccc074dd3843bea7aa3387a488f724", entity.FriendlyName);
                            Assert.AreEqual(2, entity.UserStatusID);
                            Assert.AreEqual(5, entity.UserTypeID);
                            Assert.AreEqual(DateTime.Parse("7/10/2023 8:51:40 PM"), entity.CreatedDate);
                            Assert.AreEqual(DateTime.Parse("7/10/2023 8:51:40 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100010, entity.ModifiedByID);
              
        }

        [Test]
        public void User_Update_InvalidId()
        {
            var dal = PrepareUserDal("DALInitParams");

            var entity = new User();
                          entity.Login = "Login 41ccc074dd3843bea7aa3387a488f724";
                            entity.PwdHash = "PwdHash 41ccc074dd3843bea7aa3387a488f724";
                            entity.Salt = "Salt 41ccc074dd3843bea7aa3387a488f724";
                            entity.FirstName = "FirstName 41ccc074dd3843bea7aa3387a488f724";
                            entity.MiddleName = "MiddleName 41ccc074dd3843bea7aa3387a488f724";
                            entity.LastName = "LastName 41ccc074dd3843bea7aa3387a488f724";
                            entity.FriendlyName = "FriendlyName 41ccc074dd3843bea7aa3387a488f724";
                            entity.UserStatusID = 2;
                            entity.UserTypeID = 5;
                            entity.CreatedDate = DateTime.Parse("7/10/2023 8:51:40 PM");
                            entity.ModifiedDate = DateTime.Parse("7/10/2023 8:51:40 PM");
                            entity.ModifiedByID = 100010;
              
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
