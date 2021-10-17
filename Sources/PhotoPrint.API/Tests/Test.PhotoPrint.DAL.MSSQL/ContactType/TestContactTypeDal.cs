

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
            
                          Assert.AreEqual("ContactTypeName 02b03e2221f34effa78f3158fbc7c517", entity.ContactTypeName);
                            Assert.AreEqual(true, entity.IsDeleted);
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
                          entity.ContactTypeName = "ContactTypeName 1baf378ba82b4961962766f04870c771";
                            entity.IsDeleted = true;              
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("ContactTypeName 1baf378ba82b4961962766f04870c771", entity.ContactTypeName);
                            Assert.AreEqual(true, entity.IsDeleted);
              
        }

        [TestCase("ContactType\\030.Update.Success")]
        public void ContactType_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareContactTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            ContactType entity = dal.Get(paramID);

                          entity.ContactTypeName = "ContactTypeName 7c68532bd17e4567b259161ff24f7b17";
                            entity.IsDeleted = true;              
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("ContactTypeName 7c68532bd17e4567b259161ff24f7b17", entity.ContactTypeName);
                            Assert.AreEqual(true, entity.IsDeleted);
              
        }

        [Test]
        public void ContactType_Update_InvalidId()
        {
            var dal = PrepareContactTypeDal("DALInitParams");

            var entity = new ContactType();
                          entity.ContactTypeName = "ContactTypeName 7c68532bd17e4567b259161ff24f7b17";
                            entity.IsDeleted = true;              
              
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
