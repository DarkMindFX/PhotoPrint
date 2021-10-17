

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
            
                          Assert.AreEqual("Title f819626aaeec4d3d88ce7808978c9b65", entity.Title);
                            Assert.AreEqual("Description f819626aaeec4d3d88ce7808978c9b65", entity.Description);
                            Assert.AreEqual("OriginUrl f819626aaeec4d3d88ce7808978c9b65", entity.OriginUrl);
                            Assert.AreEqual(76, entity.MaxWidth);
                            Assert.AreEqual(76, entity.MaxHeight);
                            Assert.AreEqual(128, entity.PriceAmount);
                            Assert.AreEqual(259, entity.PriceCurrencyID);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(100006, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("4/12/2021 8:18:48 AM"), entity.CreatedDate);
                            Assert.AreEqual(100009, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("2/20/2024 8:45:48 AM"), entity.ModifiedDate);
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
                          entity.Title = "Title 2f9cd2a50f6b4478baf33810f700ed73";
                            entity.Description = "Description 2f9cd2a50f6b4478baf33810f700ed73";
                            entity.OriginUrl = "OriginUrl 2f9cd2a50f6b4478baf33810f700ed73";
                            entity.MaxWidth = 630;
                            entity.MaxHeight = 630;
                            entity.PriceAmount = 128.00M;
                            entity.PriceCurrencyID = 209;
                            entity.IsDeleted = false;              
                            entity.CreatedByID = 100010;
                            entity.CreatedDate = DateTime.Parse("11/21/2019 6:33:48 AM");
                            entity.ModifiedByID = 100010;
                            entity.ModifiedDate = DateTime.Parse("10/1/2022 4:20:48 PM");
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Title 2f9cd2a50f6b4478baf33810f700ed73", entity.Title);
                            Assert.AreEqual("Description 2f9cd2a50f6b4478baf33810f700ed73", entity.Description);
                            Assert.AreEqual("OriginUrl 2f9cd2a50f6b4478baf33810f700ed73", entity.OriginUrl);
                            Assert.AreEqual(630, entity.MaxWidth);
                            Assert.AreEqual(630, entity.MaxHeight);
                            Assert.AreEqual(128.00M, entity.PriceAmount);
                            Assert.AreEqual(209, entity.PriceCurrencyID);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(100010, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("11/21/2019 6:33:48 AM"), entity.CreatedDate);
                            Assert.AreEqual(100010, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("10/1/2022 4:20:48 PM"), entity.ModifiedDate);
              
        }

        [TestCase("Image\\030.Update.Success")]
        public void Image_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareImageDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Image entity = dal.Get(paramID);

                          entity.Title = "Title 155bb1a73abf471198abdfa4781e190f";
                            entity.Description = "Description 155bb1a73abf471198abdfa4781e190f";
                            entity.OriginUrl = "OriginUrl 155bb1a73abf471198abdfa4781e190f";
                            entity.MaxWidth = 719;
                            entity.MaxHeight = 719;
                            entity.PriceAmount = 129.00M;
                            entity.PriceCurrencyID = 203;
                            entity.IsDeleted = false;              
                            entity.CreatedByID = 100007;
                            entity.CreatedDate = DateTime.Parse("5/19/2020 12:20:48 PM");
                            entity.ModifiedByID = 100003;
                            entity.ModifiedDate = DateTime.Parse("5/19/2020 12:20:48 PM");
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Title 155bb1a73abf471198abdfa4781e190f", entity.Title);
                            Assert.AreEqual("Description 155bb1a73abf471198abdfa4781e190f", entity.Description);
                            Assert.AreEqual("OriginUrl 155bb1a73abf471198abdfa4781e190f", entity.OriginUrl);
                            Assert.AreEqual(719, entity.MaxWidth);
                            Assert.AreEqual(719, entity.MaxHeight);
                            Assert.AreEqual(129, entity.PriceAmount);
                            Assert.AreEqual(203, entity.PriceCurrencyID);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(100007, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("5/19/2020 12:20:48 PM"), entity.CreatedDate);
                            Assert.AreEqual(100003, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("5/19/2020 12:20:48 PM"), entity.ModifiedDate);
              
        }

        [Test]
        public void Image_Update_InvalidId()
        {
            var dal = PrepareImageDal("DALInitParams");

            var entity = new Image();
                          entity.Title = "Title 155bb1a73abf471198abdfa4781e190f";
                            entity.Description = "Description 155bb1a73abf471198abdfa4781e190f";
                            entity.OriginUrl = "OriginUrl 155bb1a73abf471198abdfa4781e190f";
                            entity.MaxWidth = 719;
                            entity.MaxHeight = 719;
                            entity.PriceAmount = 128.00M;
                            entity.PriceCurrencyID = 203;
                            entity.IsDeleted = false;              
                            entity.CreatedByID = 100007;
                            entity.CreatedDate = DateTime.Parse("5/19/2020 12:20:48 PM");
                            entity.ModifiedByID = 100003;
                            entity.ModifiedDate = DateTime.Parse("5/19/2020 12:20:48 PM");
              
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
