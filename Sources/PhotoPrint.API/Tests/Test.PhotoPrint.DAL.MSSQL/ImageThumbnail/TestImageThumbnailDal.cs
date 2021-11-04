


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
            
                          Assert.AreEqual("Url 7380c1c1473343079c6f9f31091a9c99", entity.Url);
                            Assert.AreEqual(203, entity.Order);
                            Assert.AreEqual(100041, entity.ImageID);
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
                          entity.Url = "Url 90886912df2a4ea3a9c19d22d8f6dda6";
                            entity.Order = 533;
                            entity.ImageID = 100047;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Url 90886912df2a4ea3a9c19d22d8f6dda6", entity.Url);
                            Assert.AreEqual(533, entity.Order);
                            Assert.AreEqual(100047, entity.ImageID);
              
        }

        [TestCase("ImageThumbnail\\030.Update.Success")]
        public void ImageThumbnail_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareImageThumbnailDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            ImageThumbnail entity = dal.Get(paramID);

                          entity.Url = "Url ebc2bc5d491a456ca2f33aede340dcf2";
                            entity.Order = 56;
                            entity.ImageID = 100032;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Url ebc2bc5d491a456ca2f33aede340dcf2", entity.Url);
                            Assert.AreEqual(56, entity.Order);
                            Assert.AreEqual(100032, entity.ImageID);
              
        }

        [Test]
        public void ImageThumbnail_Update_InvalidId()
        {
            var dal = PrepareImageThumbnailDal("DALInitParams");

            var entity = new ImageThumbnail();
                          entity.Url = "Url ebc2bc5d491a456ca2f33aede340dcf2";
                            entity.Order = 56;
                            entity.ImageID = 100032;
              
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
