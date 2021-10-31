

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
    public class TestImageCategoryDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IImageCategoryDal dal = new ImageCategoryDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void ImageCategory_GetAll_Success()
        {
            var dal = PrepareImageCategoryDal("DALInitParams");

            IList<ImageCategory> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("ImageCategory\\000.GetDetails.Success")]
        public void ImageCategory_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareImageCategoryDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramImageID = (System.Int64)objIds[0];
                var paramCategoryID = (System.Int64)objIds[1];
            ImageCategory entity = dal.Get(paramImageID,paramCategoryID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ImageID);
                        Assert.IsNotNull(entity.CategoryID);
            
                          Assert.AreEqual(100018, entity.ImageID);
                            Assert.AreEqual(100008, entity.CategoryID);
                      }

        [Test]
        public void ImageCategory_GetDetails_InvalidId()
        {
                var paramImageID = Int64.MaxValue - 1;
                var paramCategoryID = Int64.MaxValue - 1;
            var dal = PrepareImageCategoryDal("DALInitParams");

            ImageCategory entity = dal.Get(paramImageID,paramCategoryID);

            Assert.IsNull(entity);
        }

        [TestCase("ImageCategory\\010.Delete.Success")]
        public void ImageCategory_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareImageCategoryDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramImageID = (System.Int64)objIds[0];
                var paramCategoryID = (System.Int64)objIds[1];
            bool removed = dal.Delete(paramImageID,paramCategoryID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void ImageCategory_Delete_InvalidId()
        {
            var dal = PrepareImageCategoryDal("DALInitParams");
                var paramImageID = Int64.MaxValue - 1;
                var paramCategoryID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramImageID,paramCategoryID);
            Assert.IsFalse(removed);

        }

        [TestCase("ImageCategory\\020.Insert.Success")]
        public void ImageCategory_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareImageCategoryDal("DALInitParams");

            var entity = new ImageCategory();
                          entity.ImageID = 100031;
                            entity.CategoryID = 100005;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ImageID);
                        Assert.IsNotNull(entity.CategoryID);
            
                          Assert.AreEqual(100031, entity.ImageID);
                            Assert.AreEqual(100005, entity.CategoryID);
              
        }

        [TestCase("ImageCategory\\030.Update.Success")]
        public void ImageCategory_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareImageCategoryDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramImageID = (System.Int64)objIds[0];
                var paramCategoryID = (System.Int64)objIds[1];
            ImageCategory entity = dal.Get(paramImageID,paramCategoryID);

            
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ImageID);
                        Assert.IsNotNull(entity.CategoryID);
            
                          Assert.AreEqual(100045, entity.ImageID);
                            Assert.AreEqual(100004, entity.CategoryID);
              
        }

        [Test]
        public void ImageCategory_Update_InvalidId()
        {
            var dal = PrepareImageCategoryDal("DALInitParams");

            var entity = new ImageCategory();
                          entity.ImageID = 100045;
                            entity.CategoryID = 100004;
              
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

        protected IImageCategoryDal PrepareImageCategoryDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IImageCategoryDal dal = new ImageCategoryDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
