

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
            
                          Assert.AreEqual("Title f09ecde6e1b146b7a15f0bdf1b9b6ccd", entity.Title);
                            Assert.AreEqual("Description f09ecde6e1b146b7a15f0bdf1b9b6ccd", entity.Description);
                            Assert.AreEqual("OriginUrl f09ecde6e1b146b7a15f0bdf1b9b6ccd", entity.OriginUrl);
                            Assert.AreEqual(818, entity.MaxWidth);
                            Assert.AreEqual(818, entity.MaxHeight);
                            Assert.AreEqual(818440.58M, entity.PriceAmount);
                            Assert.AreEqual(221, entity.PriceCurrencyID);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(100001, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("3/4/2023 12:21:33 PM"), entity.CreatedDate);
                            Assert.AreEqual(100003, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("6/2/2023 7:54:33 AM"), entity.ModifiedDate);
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
                          entity.Title = "Title bf617654b3d248c89e62441b901d5a6f";
                            entity.Description = "Description bf617654b3d248c89e62441b901d5a6f";
                            entity.OriginUrl = "OriginUrl bf617654b3d248c89e62441b901d5a6f";
                            entity.MaxWidth = 834;
                            entity.MaxHeight = 834;
                            entity.PriceAmount = 834222.8M;
                            entity.PriceCurrencyID = 55;
                            entity.IsDeleted = false;              
                            entity.CreatedByID = 100007;
                            entity.CreatedDate = DateTime.Parse("11/29/2023 1:42:33 PM");
                            entity.ModifiedByID = 100002;
                            entity.ModifiedDate = DateTime.Parse("11/29/2023 1:42:33 PM");
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Title bf617654b3d248c89e62441b901d5a6f", entity.Title);
                            Assert.AreEqual("Description bf617654b3d248c89e62441b901d5a6f", entity.Description);
                            Assert.AreEqual("OriginUrl bf617654b3d248c89e62441b901d5a6f", entity.OriginUrl);
                            Assert.AreEqual(834, entity.MaxWidth);
                            Assert.AreEqual(834, entity.MaxHeight);
                            Assert.AreEqual(834222.8M, entity.PriceAmount);
                            Assert.AreEqual(55, entity.PriceCurrencyID);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(100007, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("11/29/2023 1:42:33 PM"), entity.CreatedDate);
                            Assert.AreEqual(100002, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("11/29/2023 1:42:33 PM"), entity.ModifiedDate);
              
        }

        [TestCase("Image\\030.Update.Success")]
        public void Image_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareImageDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Image entity = dal.Get(paramID);

                          entity.Title = "Title bc07aeca0c44413b99ba1a3af5af3e04";
                            entity.Description = "Description bc07aeca0c44413b99ba1a3af5af3e04";
                            entity.OriginUrl = "OriginUrl bc07aeca0c44413b99ba1a3af5af3e04";
                            entity.MaxWidth = 968;
                            entity.MaxHeight = 968;
                            entity.PriceAmount = 968774.69M;
                            entity.PriceCurrencyID = 151;
                            entity.IsDeleted = true;              
                            entity.CreatedByID = 100011;
                            entity.CreatedDate = DateTime.Parse("3/4/2019 5:43:33 AM");
                            entity.ModifiedByID = 100001;
                            entity.ModifiedDate = DateTime.Parse("3/4/2019 5:43:33 AM");
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Title bc07aeca0c44413b99ba1a3af5af3e04", entity.Title);
                            Assert.AreEqual("Description bc07aeca0c44413b99ba1a3af5af3e04", entity.Description);
                            Assert.AreEqual("OriginUrl bc07aeca0c44413b99ba1a3af5af3e04", entity.OriginUrl);
                            Assert.AreEqual(968, entity.MaxWidth);
                            Assert.AreEqual(968, entity.MaxHeight);
                            Assert.AreEqual(968774.69M, entity.PriceAmount);
                            Assert.AreEqual(151, entity.PriceCurrencyID);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(100011, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("3/4/2019 5:43:33 AM"), entity.CreatedDate);
                            Assert.AreEqual(100001, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("3/4/2019 5:43:33 AM"), entity.ModifiedDate);
              
        }

        [Test]
        public void Image_Update_InvalidId()
        {
            var dal = PrepareImageDal("DALInitParams");

            var entity = new Image();
                          entity.Title = "Title bc07aeca0c44413b99ba1a3af5af3e04";
                            entity.Description = "Description bc07aeca0c44413b99ba1a3af5af3e04";
                            entity.OriginUrl = "OriginUrl bc07aeca0c44413b99ba1a3af5af3e04";
                            entity.MaxWidth = 968;
                            entity.MaxHeight = 968;
                            entity.PriceAmount = 968774.69M;
                            entity.PriceCurrencyID = 151;
                            entity.IsDeleted = true;              
                            entity.CreatedByID = 100011;
                            entity.CreatedDate = DateTime.Parse("3/4/2019 5:43:33 AM");
                            entity.ModifiedByID = 100001;
                            entity.ModifiedDate = DateTime.Parse("3/4/2019 5:43:33 AM");
              
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
