


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
    public class TestUserInteriorThumbnailDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IUserInteriorThumbnailDal dal = new UserInteriorThumbnailDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void UserInteriorThumbnail_GetAll_Success()
        {
            var dal = PrepareUserInteriorThumbnailDal("DALInitParams");

            IList<UserInteriorThumbnail> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("UserInteriorThumbnail\\000.GetDetails.Success")]
        public void UserInteriorThumbnail_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserInteriorThumbnailDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            UserInteriorThumbnail entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100005, entity.UserID);
                            Assert.AreEqual("Url 934ac3e55b294a37a28585972f71a81c", entity.Url);
                      }

        [Test]
        public void UserInteriorThumbnail_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareUserInteriorThumbnailDal("DALInitParams");

            UserInteriorThumbnail entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("UserInteriorThumbnail\\010.Delete.Success")]
        public void UserInteriorThumbnail_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserInteriorThumbnailDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void UserInteriorThumbnail_Delete_InvalidId()
        {
            var dal = PrepareUserInteriorThumbnailDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("UserInteriorThumbnail\\020.Insert.Success")]
        public void UserInteriorThumbnail_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareUserInteriorThumbnailDal("DALInitParams");

            var entity = new UserInteriorThumbnail();
                          entity.UserID = 100009;
                            entity.Url = "Url a95b4a207dff4f1f83c05652210c9e7e";
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100009, entity.UserID);
                            Assert.AreEqual("Url a95b4a207dff4f1f83c05652210c9e7e", entity.Url);
              
        }

        [TestCase("UserInteriorThumbnail\\030.Update.Success")]
        public void UserInteriorThumbnail_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserInteriorThumbnailDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            UserInteriorThumbnail entity = dal.Get(paramID);

                          entity.UserID = 100005;
                            entity.Url = "Url 53f6f60bcf13454887a368810e8655ff";
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100005, entity.UserID);
                            Assert.AreEqual("Url 53f6f60bcf13454887a368810e8655ff", entity.Url);
              
        }

        [Test]
        public void UserInteriorThumbnail_Update_InvalidId()
        {
            var dal = PrepareUserInteriorThumbnailDal("DALInitParams");

            var entity = new UserInteriorThumbnail();
                          entity.UserID = 100005;
                            entity.Url = "Url 53f6f60bcf13454887a368810e8655ff";
              
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


        protected IUserInteriorThumbnailDal PrepareUserInteriorThumbnailDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IUserInteriorThumbnailDal dal = new UserInteriorThumbnailDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
