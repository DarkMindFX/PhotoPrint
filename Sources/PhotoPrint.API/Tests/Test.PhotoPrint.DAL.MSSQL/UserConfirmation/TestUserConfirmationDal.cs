

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
            
                          Assert.AreEqual(100005, entity.UserID);
                            Assert.AreEqual("ConfirmationCode 24d575a85e4248c399447fa00a0e4c43", entity.ConfirmationCode);
                            Assert.AreEqual(true, entity.Comfirmed);
                            Assert.AreEqual(DateTime.Parse("1/3/2024 5:57:34 PM"), entity.ExpiresDate);
                            Assert.AreEqual(DateTime.Parse("1/3/2024 5:57:34 PM"), entity.ConfirmationDate);
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
                          entity.UserID = 100009;
                            entity.ConfirmationCode = "ConfirmationCode 51ce9beb93d344deb72d23cbdc39d771";
                            entity.Comfirmed = false;              
                            entity.ExpiresDate = DateTime.Parse("8/21/2021 1:57:34 PM");
                            entity.ConfirmationDate = DateTime.Parse("8/21/2021 1:57:34 PM");
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100009, entity.UserID);
                            Assert.AreEqual("ConfirmationCode 51ce9beb93d344deb72d23cbdc39d771", entity.ConfirmationCode);
                            Assert.AreEqual(false, entity.Comfirmed);
                            Assert.AreEqual(DateTime.Parse("8/21/2021 1:57:34 PM"), entity.ExpiresDate);
                            Assert.AreEqual(DateTime.Parse("8/21/2021 1:57:34 PM"), entity.ConfirmationDate);
              
        }

        [TestCase("UserConfirmation\\030.Update.Success")]
        public void UserConfirmation_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserConfirmationDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            UserConfirmation entity = dal.Get(paramID);

                          entity.UserID = 100003;
                            entity.ConfirmationCode = "ConfirmationCode 9480c4a0802a45fea32c47ad80143fe8";
                            entity.Comfirmed = false;              
                            entity.ExpiresDate = DateTime.Parse("7/8/2019 5:32:34 AM");
                            entity.ConfirmationDate = DateTime.Parse("7/8/2019 5:32:34 AM");
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100003, entity.UserID);
                            Assert.AreEqual("ConfirmationCode 9480c4a0802a45fea32c47ad80143fe8", entity.ConfirmationCode);
                            Assert.AreEqual(false, entity.Comfirmed);
                            Assert.AreEqual(DateTime.Parse("7/8/2019 5:32:34 AM"), entity.ExpiresDate);
                            Assert.AreEqual(DateTime.Parse("7/8/2019 5:32:34 AM"), entity.ConfirmationDate);
              
        }

        [Test]
        public void UserConfirmation_Update_InvalidId()
        {
            var dal = PrepareUserConfirmationDal("DALInitParams");

            var entity = new UserConfirmation();
                          entity.UserID = 100003;
                            entity.ConfirmationCode = "ConfirmationCode 9480c4a0802a45fea32c47ad80143fe8";
                            entity.Comfirmed = false;              
                            entity.ExpiresDate = DateTime.Parse("7/8/2019 5:32:34 AM");
                            entity.ConfirmationDate = DateTime.Parse("7/8/2019 5:32:34 AM");
              
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
