


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
            
                          Assert.AreEqual("MatName 4ebb391da39245d68d534f57d837c030", entity.MatName);
                            Assert.AreEqual("Description 4ebb391da39245d68d534f57d837c030", entity.Description);
                            Assert.AreEqual("ThumbnailUrl 4ebb391da39245d68d534f57d837c030", entity.ThumbnailUrl);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("9/29/2023 5:48:39 AM"), entity.CreatedDate);
                            Assert.AreEqual(100008, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("5/10/2022 1:24:39 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100003, entity.ModifiedByID);
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
                          entity.MatName = "MatName 531f11a7832d4211ab44e8db22c06e2a";
                            entity.Description = "Description 531f11a7832d4211ab44e8db22c06e2a";
                            entity.ThumbnailUrl = "ThumbnailUrl 531f11a7832d4211ab44e8db22c06e2a";
                            entity.IsDeleted = false;              
                            entity.CreatedDate = DateTime.Parse("12/7/2022 10:48:39 AM");
                            entity.CreatedByID = 100008;
                            entity.ModifiedDate = DateTime.Parse("4/25/2020 11:15:39 AM");
                            entity.ModifiedByID = 100011;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("MatName 531f11a7832d4211ab44e8db22c06e2a", entity.MatName);
                            Assert.AreEqual("Description 531f11a7832d4211ab44e8db22c06e2a", entity.Description);
                            Assert.AreEqual("ThumbnailUrl 531f11a7832d4211ab44e8db22c06e2a", entity.ThumbnailUrl);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("12/7/2022 10:48:39 AM"), entity.CreatedDate);
                            Assert.AreEqual(100008, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("4/25/2020 11:15:39 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100011, entity.ModifiedByID);
              
        }

        [TestCase("Mat\\030.Update.Success")]
        public void Mat_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareMatDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Mat entity = dal.Get(paramID);

                          entity.MatName = "MatName 24a6430ae178498f98216ec95196aff3";
                            entity.Description = "Description 24a6430ae178498f98216ec95196aff3";
                            entity.ThumbnailUrl = "ThumbnailUrl 24a6430ae178498f98216ec95196aff3";
                            entity.IsDeleted = false;              
                            entity.CreatedDate = DateTime.Parse("3/6/2023 9:02:39 PM");
                            entity.CreatedByID = 100002;
                            entity.ModifiedDate = DateTime.Parse("7/25/2020 6:49:39 AM");
                            entity.ModifiedByID = 100005;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("MatName 24a6430ae178498f98216ec95196aff3", entity.MatName);
                            Assert.AreEqual("Description 24a6430ae178498f98216ec95196aff3", entity.Description);
                            Assert.AreEqual("ThumbnailUrl 24a6430ae178498f98216ec95196aff3", entity.ThumbnailUrl);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("3/6/2023 9:02:39 PM"), entity.CreatedDate);
                            Assert.AreEqual(100002, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("7/25/2020 6:49:39 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100005, entity.ModifiedByID);
              
        }

        [Test]
        public void Mat_Update_InvalidId()
        {
            var dal = PrepareMatDal("DALInitParams");

            var entity = new Mat();
                          entity.MatName = "MatName 24a6430ae178498f98216ec95196aff3";
                            entity.Description = "Description 24a6430ae178498f98216ec95196aff3";
                            entity.ThumbnailUrl = "ThumbnailUrl 24a6430ae178498f98216ec95196aff3";
                            entity.IsDeleted = false;              
                            entity.CreatedDate = DateTime.Parse("3/6/2023 9:02:39 PM");
                            entity.CreatedByID = 100002;
                            entity.ModifiedDate = DateTime.Parse("7/25/2020 6:49:39 AM");
                            entity.ModifiedByID = 100005;
              
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

        [TestCase("Mat\\040.Erase.Success")]
        public void Mat_Erase_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareMatDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Erase(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void Mat_Erase_InvalidId()
        {
            var dal = PrepareMatDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Erase(paramID);
            Assert.IsFalse(removed);

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
