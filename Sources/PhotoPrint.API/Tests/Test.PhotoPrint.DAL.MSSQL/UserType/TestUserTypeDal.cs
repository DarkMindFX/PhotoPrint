

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
    public class TestUserTypeDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IUserTypeDal dal = new UserTypeDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void UserType_GetAll_Success()
        {
            var dal = PrepareUserTypeDal("DALInitParams");

            IList<UserType> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("UserType\\000.GetDetails.Success")]
        public void UserType_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            UserType entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("UserTypeName 114c412a2cea47f29c00f2b98d847af2", entity.UserTypeName);
                            Assert.AreEqual(false, entity.IsDeleted);
                      }

        [Test]
        public void UserType_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareUserTypeDal("DALInitParams");

            UserType entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("UserType\\010.Delete.Success")]
        public void UserType_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void UserType_Delete_InvalidId()
        {
            var dal = PrepareUserTypeDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("UserType\\020.Insert.Success")]
        public void UserType_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareUserTypeDal("DALInitParams");

            var entity = new UserType();
                          entity.UserTypeName = "UserTypeName 7c4a299c5a7c4b3f9d910aed2f122549";
                            entity.IsDeleted = false;              
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("UserTypeName 7c4a299c5a7c4b3f9d910aed2f122549", entity.UserTypeName);
                            Assert.AreEqual(false, entity.IsDeleted);
              
        }

        [TestCase("UserType\\030.Update.Success")]
        public void UserType_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            UserType entity = dal.Get(paramID);

                          entity.UserTypeName = "UserTypeName 045872aad309441d8f596e48aac58ee3";
                            entity.IsDeleted = false;              
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("UserTypeName 045872aad309441d8f596e48aac58ee3", entity.UserTypeName);
                            Assert.AreEqual(false, entity.IsDeleted);
              
        }

        [Test]
        public void UserType_Update_InvalidId()
        {
            var dal = PrepareUserTypeDal("DALInitParams");

            var entity = new UserType();
                          entity.UserTypeName = "UserTypeName 045872aad309441d8f596e48aac58ee3";
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

        protected IUserTypeDal PrepareUserTypeDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IUserTypeDal dal = new UserTypeDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
