

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
            
                          Assert.AreEqual(6, entity.ContactTypeID);
                            Assert.AreEqual("Title 05698bbe54ef41769b7fa9bf81adb445", entity.Title);
                            Assert.AreEqual("Comment 05698bbe54ef41769b7fa9bf81adb445", entity.Comment);
                            Assert.AreEqual("Value 05698bbe54ef41769b7fa9bf81adb445", entity.Value);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(100008, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("12/26/2022 7:04:48 AM"), entity.CreatedDate);
                            Assert.AreEqual(100009, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("11/5/2021 10:13:48 AM"), entity.ModifiedDate);
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
                          entity.ContactTypeID = 3;
                            entity.Title = "Title 2f7b0a1b98fa49768b79088086c58d00";
                            entity.Comment = "Comment 2f7b0a1b98fa49768b79088086c58d00";
                            entity.Value = "Value 2f7b0a1b98fa49768b79088086c58d00";
                            entity.IsDeleted = true;              
                            entity.CreatedByID = 100003;
                            entity.CreatedDate = DateTime.Parse("3/31/2023 3:16:48 PM");
                            entity.ModifiedByID = 100008;
                            entity.ModifiedDate = DateTime.Parse("8/19/2020 1:03:48 AM");
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(3, entity.ContactTypeID);
                            Assert.AreEqual("Title 2f7b0a1b98fa49768b79088086c58d00", entity.Title);
                            Assert.AreEqual("Comment 2f7b0a1b98fa49768b79088086c58d00", entity.Comment);
                            Assert.AreEqual("Value 2f7b0a1b98fa49768b79088086c58d00", entity.Value);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(100003, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("3/31/2023 3:16:48 PM"), entity.CreatedDate);
                            Assert.AreEqual(100008, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("8/19/2020 1:03:48 AM"), entity.ModifiedDate);
              
        }

        [TestCase("Contact\\030.Update.Success")]
        public void Contact_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareContactDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Contact entity = dal.Get(paramID);

                          entity.ContactTypeID = 5;
                            entity.Title = "Title 9f81915be9064f03a027e72b8b51dc29";
                            entity.Comment = "Comment 9f81915be9064f03a027e72b8b51dc29";
                            entity.Value = "Value 9f81915be9064f03a027e72b8b51dc29";
                            entity.IsDeleted = true;              
                            entity.CreatedByID = 100009;
                            entity.CreatedDate = DateTime.Parse("9/26/2023 9:03:48 PM");
                            entity.ModifiedByID = 100004;
                            entity.ModifiedDate = DateTime.Parse("9/26/2023 9:03:48 PM");
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(5, entity.ContactTypeID);
                            Assert.AreEqual("Title 9f81915be9064f03a027e72b8b51dc29", entity.Title);
                            Assert.AreEqual("Comment 9f81915be9064f03a027e72b8b51dc29", entity.Comment);
                            Assert.AreEqual("Value 9f81915be9064f03a027e72b8b51dc29", entity.Value);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(100009, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("9/26/2023 9:03:48 PM"), entity.CreatedDate);
                            Assert.AreEqual(100004, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("9/26/2023 9:03:48 PM"), entity.ModifiedDate);
              
        }

        [Test]
        public void Contact_Update_InvalidId()
        {
            var dal = PrepareContactDal("DALInitParams");

            var entity = new Contact();
                          entity.ContactTypeID = 5;
                            entity.Title = "Title 9f81915be9064f03a027e72b8b51dc29";
                            entity.Comment = "Comment 9f81915be9064f03a027e72b8b51dc29";
                            entity.Value = "Value 9f81915be9064f03a027e72b8b51dc29";
                            entity.IsDeleted = true;              
                            entity.CreatedByID = 100009;
                            entity.CreatedDate = DateTime.Parse("9/26/2023 9:03:48 PM");
                            entity.ModifiedByID = 100004;
                            entity.ModifiedDate = DateTime.Parse("9/26/2023 9:03:48 PM");
              
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
