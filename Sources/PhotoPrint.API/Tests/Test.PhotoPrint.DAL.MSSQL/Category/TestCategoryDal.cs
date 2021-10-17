

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
    public class TestCategoryDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            ICategoryDal dal = new CategoryDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void Category_GetAll_Success()
        {
            var dal = PrepareCategoryDal("DALInitParams");

            IList<Category> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("Category\\000.GetDetails.Success")]
        public void Category_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCategoryDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Category entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("CategoryName 28315d5513974453916b727ec3d51e57", entity.CategoryName);
                            Assert.AreEqual("Description 28315d5513974453916b727ec3d51e57", entity.Description);
                            Assert.AreEqual(100010, entity.ParentID);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("11/18/2021 6:59:47 AM"), entity.CreatedDate);
                            Assert.AreEqual(100009, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("3/25/2021 6:35:47 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100005, entity.ModifiedByID);
                      }

        [Test]
        public void Category_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareCategoryDal("DALInitParams");

            Category entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("Category\\010.Delete.Success")]
        public void Category_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCategoryDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void Category_Delete_InvalidId()
        {
            var dal = PrepareCategoryDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("Category\\020.Insert.Success")]
        public void Category_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareCategoryDal("DALInitParams");

            var entity = new Category();
                          entity.CategoryName = "CategoryName 338e7e574abb4b3e83c4aff645c3ec24";
                            entity.Description = "Description 338e7e574abb4b3e83c4aff645c3ec24";
                            entity.ParentID = 100005;
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("8/12/2019 4:35:47 PM");
                            entity.CreatedByID = 100005;
                            entity.ModifiedDate = DateTime.Parse("6/15/2019 9:58:47 PM");
                            entity.ModifiedByID = 100001;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("CategoryName 338e7e574abb4b3e83c4aff645c3ec24", entity.CategoryName);
                            Assert.AreEqual("Description 338e7e574abb4b3e83c4aff645c3ec24", entity.Description);
                            Assert.AreEqual(100005, entity.ParentID);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("8/12/2019 4:35:47 PM"), entity.CreatedDate);
                            Assert.AreEqual(100005, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("6/15/2019 9:58:47 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100001, entity.ModifiedByID);
              
        }

        [TestCase("Category\\030.Update.Success")]
        public void Category_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCategoryDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Category entity = dal.Get(paramID);

                          entity.CategoryName = "CategoryName 09e9d45105fa402baa5fc3b701337ff0";
                            entity.Description = "Description 09e9d45105fa402baa5fc3b701337ff0";
                            entity.ParentID = 100004;
                            entity.IsDeleted = false;              
                            entity.CreatedDate = DateTime.Parse("8/30/2022 7:34:47 AM");
                            entity.CreatedByID = 100003;
                            entity.ModifiedDate = DateTime.Parse("7/3/2022 12:58:47 PM");
                            entity.ModifiedByID = 100008;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("CategoryName 09e9d45105fa402baa5fc3b701337ff0", entity.CategoryName);
                            Assert.AreEqual("Description 09e9d45105fa402baa5fc3b701337ff0", entity.Description);
                            Assert.AreEqual(100004, entity.ParentID);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("8/30/2022 7:34:47 AM"), entity.CreatedDate);
                            Assert.AreEqual(100003, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("7/3/2022 12:58:47 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100008, entity.ModifiedByID);
              
        }

        [Test]
        public void Category_Update_InvalidId()
        {
            var dal = PrepareCategoryDal("DALInitParams");

            var entity = new Category();
                          entity.CategoryName = "CategoryName 09e9d45105fa402baa5fc3b701337ff0";
                            entity.Description = "Description 09e9d45105fa402baa5fc3b701337ff0";
                            entity.ParentID = 100004;
                            entity.IsDeleted = false;              
                            entity.CreatedDate = DateTime.Parse("8/30/2022 7:34:47 AM");
                            entity.CreatedByID = 100003;
                            entity.ModifiedDate = DateTime.Parse("7/3/2022 12:58:47 PM");
                            entity.ModifiedByID = 100008;
              
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

        protected ICategoryDal PrepareCategoryDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            ICategoryDal dal = new CategoryDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
