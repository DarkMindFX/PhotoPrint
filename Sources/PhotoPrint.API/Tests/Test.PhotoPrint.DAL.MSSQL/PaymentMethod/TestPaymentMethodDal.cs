

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
    public class TestPaymentMethodDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IPaymentMethodDal dal = new PaymentMethodDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void PaymentMethod_GetAll_Success()
        {
            var dal = PreparePaymentMethodDal("DALInitParams");

            IList<PaymentMethod> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("PaymentMethod\\000.GetDetails.Success")]
        public void PaymentMethod_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PreparePaymentMethodDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            PaymentMethod entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name d36a69e84895410da926c98c5ff23361", entity.Name);
                            Assert.AreEqual("Description d36a69e84895410da926c98c5ff23361", entity.Description);
                            Assert.AreEqual(true, entity.IsDeleted);
                      }

        [Test]
        public void PaymentMethod_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PreparePaymentMethodDal("DALInitParams");

            PaymentMethod entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("PaymentMethod\\010.Delete.Success")]
        public void PaymentMethod_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PreparePaymentMethodDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void PaymentMethod_Delete_InvalidId()
        {
            var dal = PreparePaymentMethodDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("PaymentMethod\\020.Insert.Success")]
        public void PaymentMethod_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PreparePaymentMethodDal("DALInitParams");

            var entity = new PaymentMethod();
                          entity.Name = "Name 85a460cf6b7044a49aac728e299d8f4a";
                            entity.Description = "Description 85a460cf6b7044a49aac728e299d8f4a";
                            entity.IsDeleted = true;              
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name 85a460cf6b7044a49aac728e299d8f4a", entity.Name);
                            Assert.AreEqual("Description 85a460cf6b7044a49aac728e299d8f4a", entity.Description);
                            Assert.AreEqual(true, entity.IsDeleted);
              
        }

        [TestCase("PaymentMethod\\030.Update.Success")]
        public void PaymentMethod_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PreparePaymentMethodDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            PaymentMethod entity = dal.Get(paramID);

                          entity.Name = "Name ba2e6ac523ac4902a9defa9c2a8e93be";
                            entity.Description = "Description ba2e6ac523ac4902a9defa9c2a8e93be";
                            entity.IsDeleted = true;              
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name ba2e6ac523ac4902a9defa9c2a8e93be", entity.Name);
                            Assert.AreEqual("Description ba2e6ac523ac4902a9defa9c2a8e93be", entity.Description);
                            Assert.AreEqual(true, entity.IsDeleted);
              
        }

        [Test]
        public void PaymentMethod_Update_InvalidId()
        {
            var dal = PreparePaymentMethodDal("DALInitParams");

            var entity = new PaymentMethod();
                          entity.Name = "Name ba2e6ac523ac4902a9defa9c2a8e93be";
                            entity.Description = "Description ba2e6ac523ac4902a9defa9c2a8e93be";
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

        protected IPaymentMethodDal PreparePaymentMethodDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IPaymentMethodDal dal = new PaymentMethodDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
