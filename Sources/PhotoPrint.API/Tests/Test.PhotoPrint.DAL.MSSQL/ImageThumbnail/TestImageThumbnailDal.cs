

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
    public class TestImageThumbnailDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IImageThumbnailDal dal = new ImageThumbnailDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void ImageThumbnail_GetAll_Success()
        {
            var dal = PrepareImageThumbnailDal("DALInitParams");

            IList<ImageThumbnail> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("ImageThumbnail\\000.GetDetails.Success")]
        public void ImageThumbnail_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareImageThumbnailDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            ImageThumbnail entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Url 7869df0429d9437cbb127a2308f13378", entity.Url);
                            Assert.AreEqual(64, entity.Order);
                            Assert.AreEqual(100012, entity.ImageID);
                      }

        [Test]
        public void ImageThumbnail_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareImageThumbnailDal("DALInitParams");

            ImageThumbnail entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("ImageThumbnail\\010.Delete.Success")]
        public void ImageThumbnail_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareImageThumbnailDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void ImageThumbnail_Delete_InvalidId()
        {
            var dal = PrepareImageThumbnailDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("ImageThumbnail\\020.Insert.Success")]
        public void ImageThumbnail_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareImageThumbnailDal("DALInitParams");

            var entity = new ImageThumbnail();
                          entity.Url = "Url dcc6e4b0b49d4c308b14fa13b01f3454";
                            entity.Order = 586;
                            entity.ImageID = 100025;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Url dcc6e4b0b49d4c308b14fa13b01f3454", entity.Url);
                            Assert.AreEqual(586, entity.Order);
                            Assert.AreEqual(100025, entity.ImageID);
              
        }

        [TestCase("ImageThumbnail\\030.Update.Success")]
        public void ImageThumbnail_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareImageThumbnailDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            ImageThumbnail entity = dal.Get(paramID);

                          entity.Url = "Url e7617b77ffc3473cb64c6b3f2cec497d";
                            entity.Order = 720;
                            entity.ImageID = 100044;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Url e7617b77ffc3473cb64c6b3f2cec497d", entity.Url);
                            Assert.AreEqual(720, entity.Order);
                            Assert.AreEqual(100044, entity.ImageID);
              
        }

        [Test]
        public void ImageThumbnail_Update_InvalidId()
        {
            var dal = PrepareImageThumbnailDal("DALInitParams");

            var entity = new ImageThumbnail();
                          entity.Url = "Url e7617b77ffc3473cb64c6b3f2cec497d";
                            entity.Order = 720;
                            entity.ImageID = 100044;
              
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

        protected IImageThumbnailDal PrepareImageThumbnailDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IImageThumbnailDal dal = new ImageThumbnailDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
