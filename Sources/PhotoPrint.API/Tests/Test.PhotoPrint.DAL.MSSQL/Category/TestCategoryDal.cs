


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
            
                          Assert.AreEqual("CategoryName fca4bd9392684313b4e7b4a7f694e0ba", entity.CategoryName);
                            Assert.AreEqual("Description fca4bd9392684313b4e7b4a7f694e0ba", entity.Description);
                            Assert.AreEqual(100010, entity.ParentID);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("7/24/2021 7:01:38 AM"), entity.CreatedDate);
                            Assert.AreEqual(100002, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("6/8/2019 1:16:38 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100008, entity.ModifiedByID);
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
                          entity.CategoryName = "CategoryName 837b98f0a26646bba8c224f35c4e378f";
                            entity.Description = "Description 837b98f0a26646bba8c224f35c4e378f";
                            entity.ParentID = 100007;
                            entity.IsDeleted = false;              
                            entity.CreatedDate = DateTime.Parse("6/2/2020 12:50:38 AM");
                            entity.CreatedByID = 100007;
                            entity.ModifiedDate = DateTime.Parse("4/12/2023 10:37:38 AM");
                            entity.ModifiedByID = 100001;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("CategoryName 837b98f0a26646bba8c224f35c4e378f", entity.CategoryName);
                            Assert.AreEqual("Description 837b98f0a26646bba8c224f35c4e378f", entity.Description);
                            Assert.AreEqual(100007, entity.ParentID);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("6/2/2020 12:50:38 AM"), entity.CreatedDate);
                            Assert.AreEqual(100007, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("4/12/2023 10:37:38 AM"), entity.ModifiedDate);
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

                          entity.CategoryName = "CategoryName 2be173b81cac48dea5ee5f59bf14f99b";
                            entity.Description = "Description 2be173b81cac48dea5ee5f59bf14f99b";
                            entity.ParentID = 100009;
                            entity.IsDeleted = false;              
                            entity.CreatedDate = DateTime.Parse("7/10/2023 8:51:38 PM");
                            entity.CreatedByID = 100001;
                            entity.ModifiedDate = DateTime.Parse("7/10/2023 8:51:38 PM");
                            entity.ModifiedByID = 100004;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("CategoryName 2be173b81cac48dea5ee5f59bf14f99b", entity.CategoryName);
                            Assert.AreEqual("Description 2be173b81cac48dea5ee5f59bf14f99b", entity.Description);
                            Assert.AreEqual(100009, entity.ParentID);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("7/10/2023 8:51:38 PM"), entity.CreatedDate);
                            Assert.AreEqual(100001, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("7/10/2023 8:51:38 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100004, entity.ModifiedByID);
              
        }

        [Test]
        public void Category_Update_InvalidId()
        {
            var dal = PrepareCategoryDal("DALInitParams");

            var entity = new Category();
                          entity.CategoryName = "CategoryName 2be173b81cac48dea5ee5f59bf14f99b";
                            entity.Description = "Description 2be173b81cac48dea5ee5f59bf14f99b";
                            entity.ParentID = 100009;
                            entity.IsDeleted = false;              
                            entity.CreatedDate = DateTime.Parse("7/10/2023 8:51:38 PM");
                            entity.CreatedByID = 100001;
                            entity.ModifiedDate = DateTime.Parse("7/10/2023 8:51:38 PM");
                            entity.ModifiedByID = 100004;
              
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

        [TestCase("Category\\040.Erase.Success")]
        public void Category_Erase_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCategoryDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Erase(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void Category_Erase_InvalidId()
        {
            var dal = PrepareCategoryDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Erase(paramID);
            Assert.IsFalse(removed);

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
