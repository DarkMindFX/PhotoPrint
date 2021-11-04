


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
    public class TestImageRelatedDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IImageRelatedDal dal = new ImageRelatedDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void ImageRelated_GetAll_Success()
        {
            var dal = PrepareImageRelatedDal("DALInitParams");

            IList<ImageRelated> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("ImageRelated\\000.GetDetails.Success")]
        public void ImageRelated_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareImageRelatedDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramImageID = (System.Int64)objIds[0];
                var paramRelatedImageID = (System.Int64)objIds[1];
            ImageRelated entity = dal.Get(paramImageID,paramRelatedImageID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ImageID);
                        Assert.IsNotNull(entity.RelatedImageID);
            
                          Assert.AreEqual(100012, entity.ImageID);
                            Assert.AreEqual(100023, entity.RelatedImageID);
                      }

        [Test]
        public void ImageRelated_GetDetails_InvalidId()
        {
                var paramImageID = Int64.MaxValue - 1;
                var paramRelatedImageID = Int64.MaxValue - 1;
            var dal = PrepareImageRelatedDal("DALInitParams");

            ImageRelated entity = dal.Get(paramImageID,paramRelatedImageID);

            Assert.IsNull(entity);
        }

        [TestCase("ImageRelated\\010.Delete.Success")]
        public void ImageRelated_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareImageRelatedDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramImageID = (System.Int64)objIds[0];
                var paramRelatedImageID = (System.Int64)objIds[1];
            bool removed = dal.Delete(paramImageID,paramRelatedImageID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void ImageRelated_Delete_InvalidId()
        {
            var dal = PrepareImageRelatedDal("DALInitParams");
                var paramImageID = Int64.MaxValue - 1;
                var paramRelatedImageID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramImageID,paramRelatedImageID);
            Assert.IsFalse(removed);

        }

        [TestCase("ImageRelated\\020.Insert.Success")]
        public void ImageRelated_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareImageRelatedDal("DALInitParams");

            var entity = new ImageRelated();
                          entity.ImageID = 100036;
                            entity.RelatedImageID = 100003;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ImageID);
                        Assert.IsNotNull(entity.RelatedImageID);
            
                          Assert.AreEqual(100036, entity.ImageID);
                            Assert.AreEqual(100003, entity.RelatedImageID);
              
        }

        [TestCase("ImageRelated\\030.Update.Success")]
        public void ImageRelated_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareImageRelatedDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramImageID = (System.Int64)objIds[0];
                var paramRelatedImageID = (System.Int64)objIds[1];
            ImageRelated entity = dal.Get(paramImageID,paramRelatedImageID);

            
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ImageID);
                        Assert.IsNotNull(entity.RelatedImageID);
            
                          Assert.AreEqual(100009, entity.ImageID);
                            Assert.AreEqual(100030, entity.RelatedImageID);
              
        }

        [Test]
        public void ImageRelated_Update_InvalidId()
        {
            var dal = PrepareImageRelatedDal("DALInitParams");

            var entity = new ImageRelated();
                          entity.ImageID = 100009;
                            entity.RelatedImageID = 100030;
              
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


        protected IImageRelatedDal PrepareImageRelatedDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IImageRelatedDal dal = new ImageRelatedDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
