

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
    public class TestMatDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IMatDal dal = new MatDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void Mat_GetAll_Success()
        {
            var dal = PrepareMatDal("DALInitParams");

            IList<Mat> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("Mat\\000.GetDetails.Success")]
        public void Mat_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareMatDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Mat entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("MatName 6496f94b17194c01aec7d0c937bc9bf3", entity.MatName);
                            Assert.AreEqual("Description 6496f94b17194c01aec7d0c937bc9bf3", entity.Description);
                            Assert.AreEqual("ThumbnailUrl 6496f94b17194c01aec7d0c937bc9bf3", entity.ThumbnailUrl);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("10/11/2023 3:09:48 AM"), entity.CreatedDate);
                            Assert.AreEqual(100005, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("1/8/2020 6:45:48 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100008, entity.ModifiedByID);
                      }

        [Test]
        public void Mat_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareMatDal("DALInitParams");

            Mat entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("Mat\\010.Delete.Success")]
        public void Mat_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareMatDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void Mat_Delete_InvalidId()
        {
            var dal = PrepareMatDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("Mat\\020.Insert.Success")]
        public void Mat_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareMatDal("DALInitParams");

            var entity = new Mat();
                          entity.MatName = "MatName 7699cf9018224d61a26df5a641e67191";
                            entity.Description = "Description 7699cf9018224d61a26df5a641e67191";
                            entity.ThumbnailUrl = "ThumbnailUrl 7699cf9018224d61a26df5a641e67191";
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("9/27/2021 10:20:48 AM");
                            entity.CreatedByID = 100004;
                            entity.ModifiedDate = DateTime.Parse("2/14/2019 10:47:48 AM");
                            entity.ModifiedByID = 100007;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("MatName 7699cf9018224d61a26df5a641e67191", entity.MatName);
                            Assert.AreEqual("Description 7699cf9018224d61a26df5a641e67191", entity.Description);
                            Assert.AreEqual("ThumbnailUrl 7699cf9018224d61a26df5a641e67191", entity.ThumbnailUrl);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("9/27/2021 10:20:48 AM"), entity.CreatedDate);
                            Assert.AreEqual(100004, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("2/14/2019 10:47:48 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100007, entity.ModifiedByID);
              
        }

        [TestCase("Mat\\030.Update.Success")]
        public void Mat_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareMatDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Mat entity = dal.Get(paramID);

                          entity.MatName = "MatName d2884ed7886d48029e6648aa8b1904ad";
                            entity.Description = "Description d2884ed7886d48029e6648aa8b1904ad";
                            entity.ThumbnailUrl = "ThumbnailUrl d2884ed7886d48029e6648aa8b1904ad";
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("12/24/2021 8:34:48 PM");
                            entity.CreatedByID = 100005;
                            entity.ModifiedDate = DateTime.Parse("5/15/2019 6:21:48 AM");
                            entity.ModifiedByID = 100003;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("MatName d2884ed7886d48029e6648aa8b1904ad", entity.MatName);
                            Assert.AreEqual("Description d2884ed7886d48029e6648aa8b1904ad", entity.Description);
                            Assert.AreEqual("ThumbnailUrl d2884ed7886d48029e6648aa8b1904ad", entity.ThumbnailUrl);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("12/24/2021 8:34:48 PM"), entity.CreatedDate);
                            Assert.AreEqual(100005, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("5/15/2019 6:21:48 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100003, entity.ModifiedByID);
              
        }

        [Test]
        public void Mat_Update_InvalidId()
        {
            var dal = PrepareMatDal("DALInitParams");

            var entity = new Mat();
                          entity.MatName = "MatName d2884ed7886d48029e6648aa8b1904ad";
                            entity.Description = "Description d2884ed7886d48029e6648aa8b1904ad";
                            entity.ThumbnailUrl = "ThumbnailUrl d2884ed7886d48029e6648aa8b1904ad";
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("12/24/2021 8:34:48 PM");
                            entity.CreatedByID = 100005;
                            entity.ModifiedDate = DateTime.Parse("5/15/2019 6:21:48 AM");
                            entity.ModifiedByID = 100003;
              
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

        protected IMatDal PrepareMatDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IMatDal dal = new MatDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
