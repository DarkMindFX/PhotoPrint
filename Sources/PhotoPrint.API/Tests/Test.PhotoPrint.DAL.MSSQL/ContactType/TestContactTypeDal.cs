


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
    public class TestContactTypeDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IContactTypeDal dal = new ContactTypeDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void ContactType_GetAll_Success()
        {
            var dal = PrepareContactTypeDal("DALInitParams");

            IList<ContactType> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("ContactType\\000.GetDetails.Success")]
        public void ContactType_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareContactTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            ContactType entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("ContactTypeName 1f24b4c1afd34d478ce9a094b03cbe60", entity.ContactTypeName);
                            Assert.AreEqual(false, entity.IsDeleted);
                      }

        [Test]
        public void ContactType_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareContactTypeDal("DALInitParams");

            ContactType entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("ContactType\\010.Delete.Success")]
        public void ContactType_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareContactTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void ContactType_Delete_InvalidId()
        {
            var dal = PrepareContactTypeDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("ContactType\\020.Insert.Success")]
        public void ContactType_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareContactTypeDal("DALInitParams");

            var entity = new ContactType();
                          entity.ContactTypeName = "ContactTypeName 5229692723e14ebf816b3426247bb4cf";
                            entity.IsDeleted = false;              
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("ContactTypeName 5229692723e14ebf816b3426247bb4cf", entity.ContactTypeName);
                            Assert.AreEqual(false, entity.IsDeleted);
              
        }

        [TestCase("ContactType\\030.Update.Success")]
        public void ContactType_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareContactTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            ContactType entity = dal.Get(paramID);

                          entity.ContactTypeName = "ContactTypeName 04a2aa31cafb46a5886f051f7b849dac";
                            entity.IsDeleted = false;              
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("ContactTypeName 04a2aa31cafb46a5886f051f7b849dac", entity.ContactTypeName);
                            Assert.AreEqual(false, entity.IsDeleted);
              
        }

        [Test]
        public void ContactType_Update_InvalidId()
        {
            var dal = PrepareContactTypeDal("DALInitParams");

            var entity = new ContactType();
                          entity.ContactTypeName = "ContactTypeName 04a2aa31cafb46a5886f051f7b849dac";
                            entity.IsDeleted = false;              
              
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

        [TestCase("ContactType\\040.Erase.Success")]
        public void ContactType_Erase_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareContactTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Erase(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void ContactType_Erase_InvalidId()
        {
            var dal = PrepareContactTypeDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Erase(paramID);
            Assert.IsFalse(removed);

        }

        protected IContactTypeDal PrepareContactTypeDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IContactTypeDal dal = new ContactTypeDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
