


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
            
                          Assert.AreEqual(4, entity.ContactTypeID);
                            Assert.AreEqual("Title 277a751ed8374a4b9b322c6012621517", entity.Title);
                            Assert.AreEqual("Comment 277a751ed8374a4b9b322c6012621517", entity.Comment);
                            Assert.AreEqual("Value 277a751ed8374a4b9b322c6012621517", entity.Value);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(100010, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("12/15/2023 12:17:38 PM"), entity.CreatedDate);
                            Assert.AreEqual(100007, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("5/4/2021 10:04:38 PM"), entity.ModifiedDate);
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
                            entity.Title = "Title 520113c4230748ec88daf376f31d6ed1";
                            entity.Comment = "Comment 520113c4230748ec88daf376f31d6ed1";
                            entity.Value = "Value 520113c4230748ec88daf376f31d6ed1";
                            entity.IsDeleted = true;              
                            entity.CreatedByID = 100003;
                            entity.CreatedDate = DateTime.Parse("3/21/2019 1:38:38 PM");
                            entity.ModifiedByID = 100001;
                            entity.ModifiedDate = DateTime.Parse("3/21/2019 1:38:38 PM");
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(2, entity.ContactTypeID);
                            Assert.AreEqual("Title 520113c4230748ec88daf376f31d6ed1", entity.Title);
                            Assert.AreEqual("Comment 520113c4230748ec88daf376f31d6ed1", entity.Comment);
                            Assert.AreEqual("Value 520113c4230748ec88daf376f31d6ed1", entity.Value);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(100003, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("3/21/2019 1:38:38 PM"), entity.CreatedDate);
                            Assert.AreEqual(100001, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("3/21/2019 1:38:38 PM"), entity.ModifiedDate);
              
        }

        [TestCase("Contact\\030.Update.Success")]
        public void Contact_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareContactDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Contact entity = dal.Get(paramID);

                          entity.ContactTypeID = 4;
                            entity.Title = "Title 6ae62682fabc4052a06cba823df1800b";
                            entity.Comment = "Comment 6ae62682fabc4052a06cba823df1800b";
                            entity.Value = "Value 6ae62682fabc4052a06cba823df1800b";
                            entity.IsDeleted = true;              
                            entity.CreatedByID = 100002;
                            entity.CreatedDate = DateTime.Parse("6/18/2019 11:52:38 PM");
                            entity.ModifiedByID = 100007;
                            entity.ModifiedDate = DateTime.Parse("6/18/2019 11:52:38 PM");
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(4, entity.ContactTypeID);
                            Assert.AreEqual("Title 6ae62682fabc4052a06cba823df1800b", entity.Title);
                            Assert.AreEqual("Comment 6ae62682fabc4052a06cba823df1800b", entity.Comment);
                            Assert.AreEqual("Value 6ae62682fabc4052a06cba823df1800b", entity.Value);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(100002, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("6/18/2019 11:52:38 PM"), entity.CreatedDate);
                            Assert.AreEqual(100007, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("6/18/2019 11:52:38 PM"), entity.ModifiedDate);
              
        }

        [Test]
        public void Contact_Update_InvalidId()
        {
            var dal = PrepareContactDal("DALInitParams");

            var entity = new Contact();
                          entity.ContactTypeID = 4;
                            entity.Title = "Title 6ae62682fabc4052a06cba823df1800b";
                            entity.Comment = "Comment 6ae62682fabc4052a06cba823df1800b";
                            entity.Value = "Value 6ae62682fabc4052a06cba823df1800b";
                            entity.IsDeleted = true;              
                            entity.CreatedByID = 100002;
                            entity.CreatedDate = DateTime.Parse("6/18/2019 11:52:38 PM");
                            entity.ModifiedByID = 100007;
                            entity.ModifiedDate = DateTime.Parse("6/18/2019 11:52:38 PM");
              
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

        [TestCase("Contact\\040.Erase.Success")]
        public void Contact_Erase_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareContactDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Erase(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void Contact_Erase_InvalidId()
        {
            var dal = PrepareContactDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Erase(paramID);
            Assert.IsFalse(removed);

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
