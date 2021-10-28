

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
    public class TestUserConfirmationDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IUserConfirmationDal dal = new UserConfirmationDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void UserConfirmation_GetAll_Success()
        {
            var dal = PrepareUserConfirmationDal("DALInitParams");

            IList<UserConfirmation> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("UserConfirmation\\000.GetDetails.Success")]
        public void UserConfirmation_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserConfirmationDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            UserConfirmation entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100003, entity.UserID);
                            Assert.AreEqual("ConfirmationCode 4613055313c54e6e8bc76c5fa201c97b", entity.ConfirmationCode);
                            Assert.AreEqual(true, entity.Comfirmed);
                            Assert.AreEqual(DateTime.Parse("10/18/2019 7:33:26 AM"), entity.ExpiresDate);
                            Assert.AreEqual(DateTime.Parse("10/18/2019 7:33:26 AM"), entity.ConfirmationDate);
                      }

        [Test]
        public void UserConfirmation_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareUserConfirmationDal("DALInitParams");

            UserConfirmation entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("UserConfirmation\\010.Delete.Success")]
        public void UserConfirmation_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserConfirmationDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void UserConfirmation_Delete_InvalidId()
        {
            var dal = PrepareUserConfirmationDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("UserConfirmation\\020.Insert.Success")]
        public void UserConfirmation_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareUserConfirmationDal("DALInitParams");

            var entity = new UserConfirmation();
                          entity.UserID = 100001;
                            entity.ConfirmationCode = "ConfirmationCode e15e5835df8e40f0bdfe6df436e2be0d";
                            entity.Comfirmed = true;              
                            entity.ExpiresDate = DateTime.Parse("8/23/2023 4:55:26 AM");
                            entity.ConfirmationDate = DateTime.Parse("8/23/2023 4:55:26 AM");
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100001, entity.UserID);
                            Assert.AreEqual("ConfirmationCode e15e5835df8e40f0bdfe6df436e2be0d", entity.ConfirmationCode);
                            Assert.AreEqual(true, entity.Comfirmed);
                            Assert.AreEqual(DateTime.Parse("8/23/2023 4:55:26 AM"), entity.ExpiresDate);
                            Assert.AreEqual(DateTime.Parse("8/23/2023 4:55:26 AM"), entity.ConfirmationDate);
              
        }

        [TestCase("UserConfirmation\\030.Update.Success")]
        public void UserConfirmation_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserConfirmationDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            UserConfirmation entity = dal.Get(paramID);

                          entity.UserID = 100009;
                            entity.ConfirmationCode = "ConfirmationCode 8d89783eb4ed413ba87d23cb37bb8132";
                            entity.Comfirmed = false;              
                            entity.ExpiresDate = DateTime.Parse("1/9/2021 5:22:26 AM");
                            entity.ConfirmationDate = DateTime.Parse("1/9/2021 5:22:26 AM");
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100009, entity.UserID);
                            Assert.AreEqual("ConfirmationCode 8d89783eb4ed413ba87d23cb37bb8132", entity.ConfirmationCode);
                            Assert.AreEqual(false, entity.Comfirmed);
                            Assert.AreEqual(DateTime.Parse("1/9/2021 5:22:26 AM"), entity.ExpiresDate);
                            Assert.AreEqual(DateTime.Parse("1/9/2021 5:22:26 AM"), entity.ConfirmationDate);
              
        }

        [Test]
        public void UserConfirmation_Update_InvalidId()
        {
            var dal = PrepareUserConfirmationDal("DALInitParams");

            var entity = new UserConfirmation();
                          entity.UserID = 100009;
                            entity.ConfirmationCode = "ConfirmationCode 8d89783eb4ed413ba87d23cb37bb8132";
                            entity.Comfirmed = false;              
                            entity.ExpiresDate = DateTime.Parse("1/9/2021 5:22:26 AM");
                            entity.ConfirmationDate = DateTime.Parse("1/9/2021 5:22:26 AM");
              
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

        protected IUserConfirmationDal PrepareUserConfirmationDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IUserConfirmationDal dal = new UserConfirmationDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
