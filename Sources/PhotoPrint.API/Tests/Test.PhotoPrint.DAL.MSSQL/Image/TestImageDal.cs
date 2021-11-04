


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
    public class TestImageDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IImageDal dal = new ImageDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void Image_GetAll_Success()
        {
            var dal = PrepareImageDal("DALInitParams");

            IList<Image> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("Image\\000.GetDetails.Success")]
        public void Image_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareImageDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Image entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Title 159cf8b0946e4b02b7d89cc5be42dba4", entity.Title);
                            Assert.AreEqual("Description 159cf8b0946e4b02b7d89cc5be42dba4", entity.Description);
                            Assert.AreEqual("OriginUrl 159cf8b0946e4b02b7d89cc5be42dba4", entity.OriginUrl);
                            Assert.AreEqual(133, entity.MaxWidth);
                            Assert.AreEqual(133, entity.MaxHeight);
                            Assert.AreEqual(132394.98M, entity.PriceAmount);
                            Assert.AreEqual(81, entity.PriceCurrencyID);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(100009, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("1/3/2024 7:09:39 AM"), entity.CreatedDate);
                            Assert.AreEqual(100005, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("4/7/2019 11:10:39 PM"), entity.ModifiedDate);
                      }

        [Test]
        public void Image_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareImageDal("DALInitParams");

            Image entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("Image\\010.Delete.Success")]
        public void Image_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareImageDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void Image_Delete_InvalidId()
        {
            var dal = PrepareImageDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("Image\\020.Insert.Success")]
        public void Image_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareImageDal("DALInitParams");

            var entity = new Image();
                          entity.Title = "Title 6ab4d7fc44cd430c9406c92bb440acfc";
                            entity.Description = "Description 6ab4d7fc44cd430c9406c92bb440acfc";
                            entity.OriginUrl = "OriginUrl 6ab4d7fc44cd430c9406c92bb440acfc";
                            entity.MaxWidth = 225;
                            entity.MaxHeight = 225;
                            entity.PriceAmount = 224592.28M;
                            entity.PriceCurrencyID = 200;
                            entity.IsDeleted = false;              
                            entity.CreatedByID = 100010;
                            entity.CreatedDate = DateTime.Parse("4/26/2021 1:56:39 PM");
                            entity.ModifiedByID = 100004;
                            entity.ModifiedDate = DateTime.Parse("4/26/2021 1:56:39 PM");
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Title 6ab4d7fc44cd430c9406c92bb440acfc", entity.Title);
                            Assert.AreEqual("Description 6ab4d7fc44cd430c9406c92bb440acfc", entity.Description);
                            Assert.AreEqual("OriginUrl 6ab4d7fc44cd430c9406c92bb440acfc", entity.OriginUrl);
                            Assert.AreEqual(225, entity.MaxWidth);
                            Assert.AreEqual(225, entity.MaxHeight);
                            Assert.AreEqual(224592.28M, entity.PriceAmount);
                            Assert.AreEqual(200, entity.PriceCurrencyID);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(100010, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("4/26/2021 1:56:39 PM"), entity.CreatedDate);
                            Assert.AreEqual(100004, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("4/26/2021 1:56:39 PM"), entity.ModifiedDate);
              
        }

        [TestCase("Image\\030.Update.Success")]
        public void Image_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareImageDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Image entity = dal.Get(paramID);

                          entity.Title = "Title c802267d455d408995722f74a0f7f160";
                            entity.Description = "Description c802267d455d408995722f74a0f7f160";
                            entity.OriginUrl = "OriginUrl c802267d455d408995722f74a0f7f160";
                            entity.MaxWidth = 449;
                            entity.MaxHeight = 449;
                            entity.PriceAmount = 448845.43M;
                            entity.PriceCurrencyID = 97;
                            entity.IsDeleted = true;              
                            entity.CreatedByID = 100001;
                            entity.CreatedDate = DateTime.Parse("6/4/2024 9:57:39 AM");
                            entity.ModifiedByID = 100004;
                            entity.ModifiedDate = DateTime.Parse("6/4/2024 9:57:39 AM");
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Title c802267d455d408995722f74a0f7f160", entity.Title);
                            Assert.AreEqual("Description c802267d455d408995722f74a0f7f160", entity.Description);
                            Assert.AreEqual("OriginUrl c802267d455d408995722f74a0f7f160", entity.OriginUrl);
                            Assert.AreEqual(449, entity.MaxWidth);
                            Assert.AreEqual(449, entity.MaxHeight);
                            Assert.AreEqual(448845.43M, entity.PriceAmount);
                            Assert.AreEqual(97, entity.PriceCurrencyID);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(100001, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("6/4/2024 9:57:39 AM"), entity.CreatedDate);
                            Assert.AreEqual(100004, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("6/4/2024 9:57:39 AM"), entity.ModifiedDate);
              
        }

        [Test]
        public void Image_Update_InvalidId()
        {
            var dal = PrepareImageDal("DALInitParams");

            var entity = new Image();
                          entity.Title = "Title c802267d455d408995722f74a0f7f160";
                            entity.Description = "Description c802267d455d408995722f74a0f7f160";
                            entity.OriginUrl = "OriginUrl c802267d455d408995722f74a0f7f160";
                            entity.MaxWidth = 449;
                            entity.MaxHeight = 449;
                            entity.PriceAmount = 448845.43M;
                            entity.PriceCurrencyID = 97;
                            entity.IsDeleted = true;              
                            entity.CreatedByID = 100001;
                            entity.CreatedDate = DateTime.Parse("6/4/2024 9:57:39 AM");
                            entity.ModifiedByID = 100004;
                            entity.ModifiedDate = DateTime.Parse("6/4/2024 9:57:39 AM");
              
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

        [TestCase("Image\\040.Erase.Success")]
        public void Image_Erase_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareImageDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Erase(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void Image_Erase_InvalidId()
        {
            var dal = PrepareImageDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Erase(paramID);
            Assert.IsFalse(removed);

        }

        protected IImageDal PrepareImageDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IImageDal dal = new ImageDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
