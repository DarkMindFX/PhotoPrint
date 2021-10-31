

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
    public class TestContactDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IContactDal dal = new ContactDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void Contact_GetAll_Success()
        {
            var dal = PrepareContactDal("DALInitParams");

            IList<Contact> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("Contact\\000.GetDetails.Success")]
        public void Contact_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareContactDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Contact entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(2, entity.ContactTypeID);
                            Assert.AreEqual("Title fe50735305194b718823da3fc5639716", entity.Title);
                            Assert.AreEqual("Comment fe50735305194b718823da3fc5639716", entity.Comment);
                            Assert.AreEqual("Value fe50735305194b718823da3fc5639716", entity.Value);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(100001, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("1/7/2020 6:48:32 AM"), entity.CreatedDate);
                            Assert.AreEqual(100001, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("11/16/2022 7:15:32 AM"), entity.ModifiedDate);
                      }

        [Test]
        public void Contact_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareContactDal("DALInitParams");

            Contact entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("Contact\\010.Delete.Success")]
        public void Contact_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareContactDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void Contact_Delete_InvalidId()
        {
            var dal = PrepareContactDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("Contact\\020.Insert.Success")]
        public void Contact_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareContactDal("DALInitParams");

            var entity = new Contact();
                          entity.ContactTypeID = 2;
                            entity.Title = "Title 3ae9b0eefaad41dcaad3d3529a7aa6e5";
                            entity.Comment = "Comment 3ae9b0eefaad41dcaad3d3529a7aa6e5";
                            entity.Value = "Value 3ae9b0eefaad41dcaad3d3529a7aa6e5";
                            entity.IsDeleted = true;              
                            entity.CreatedByID = 100001;
                            entity.CreatedDate = DateTime.Parse("2/14/2023 2:49:32 AM");
                            entity.ModifiedByID = 100004;
                            entity.ModifiedDate = DateTime.Parse("2/14/2023 2:49:32 AM");
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(2, entity.ContactTypeID);
                            Assert.AreEqual("Title 3ae9b0eefaad41dcaad3d3529a7aa6e5", entity.Title);
                            Assert.AreEqual("Comment 3ae9b0eefaad41dcaad3d3529a7aa6e5", entity.Comment);
                            Assert.AreEqual("Value 3ae9b0eefaad41dcaad3d3529a7aa6e5", entity.Value);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(100001, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("2/14/2023 2:49:32 AM"), entity.CreatedDate);
                            Assert.AreEqual(100004, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("2/14/2023 2:49:32 AM"), entity.ModifiedDate);
              
        }

        [TestCase("Contact\\030.Update.Success")]
        public void Contact_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareContactDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Contact entity = dal.Get(paramID);

                          entity.ContactTypeID = 1;
                            entity.Title = "Title 4e8fb92ad4334e25bf09d781d8af5023";
                            entity.Comment = "Comment 4e8fb92ad4334e25bf09d781d8af5023";
                            entity.Value = "Value 4e8fb92ad4334e25bf09d781d8af5023";
                            entity.IsDeleted = false;              
                            entity.CreatedByID = 100002;
                            entity.CreatedDate = DateTime.Parse("5/14/2023 1:03:32 PM");
                            entity.ModifiedByID = 100003;
                            entity.ModifiedDate = DateTime.Parse("10/1/2020 10:49:32 PM");
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(1, entity.ContactTypeID);
                            Assert.AreEqual("Title 4e8fb92ad4334e25bf09d781d8af5023", entity.Title);
                            Assert.AreEqual("Comment 4e8fb92ad4334e25bf09d781d8af5023", entity.Comment);
                            Assert.AreEqual("Value 4e8fb92ad4334e25bf09d781d8af5023", entity.Value);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(100002, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("5/14/2023 1:03:32 PM"), entity.CreatedDate);
                            Assert.AreEqual(100003, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("10/1/2020 10:49:32 PM"), entity.ModifiedDate);
              
        }

        [Test]
        public void Contact_Update_InvalidId()
        {
            var dal = PrepareContactDal("DALInitParams");

            var entity = new Contact();
                          entity.ContactTypeID = 1;
                            entity.Title = "Title 4e8fb92ad4334e25bf09d781d8af5023";
                            entity.Comment = "Comment 4e8fb92ad4334e25bf09d781d8af5023";
                            entity.Value = "Value 4e8fb92ad4334e25bf09d781d8af5023";
                            entity.IsDeleted = false;              
                            entity.CreatedByID = 100002;
                            entity.CreatedDate = DateTime.Parse("5/14/2023 1:03:32 PM");
                            entity.ModifiedByID = 100003;
                            entity.ModifiedDate = DateTime.Parse("10/1/2020 10:49:32 PM");
              
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

        protected IContactDal PrepareContactDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IContactDal dal = new ContactDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
