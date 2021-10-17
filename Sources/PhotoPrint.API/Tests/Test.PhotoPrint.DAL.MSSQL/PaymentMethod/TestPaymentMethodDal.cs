

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
            
                          Assert.AreEqual("Name 834a976410274e98973c69b60b685f4c", entity.Name);
                            Assert.AreEqual("Description 834a976410274e98973c69b60b685f4c", entity.Description);
                            Assert.AreEqual(false, entity.IsDeleted);
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
                          entity.Name = "Name 9bb3a174b4674cb0bd7e37be5911da7c";
                            entity.Description = "Description 9bb3a174b4674cb0bd7e37be5911da7c";
                            entity.IsDeleted = false;              
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name 9bb3a174b4674cb0bd7e37be5911da7c", entity.Name);
                            Assert.AreEqual("Description 9bb3a174b4674cb0bd7e37be5911da7c", entity.Description);
                            Assert.AreEqual(false, entity.IsDeleted);
              
        }

        [TestCase("PaymentMethod\\030.Update.Success")]
        public void PaymentMethod_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PreparePaymentMethodDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            PaymentMethod entity = dal.Get(paramID);

                          entity.Name = "Name 5028d108bdf34393a12bb51e03aa262d";
                            entity.Description = "Description 5028d108bdf34393a12bb51e03aa262d";
                            entity.IsDeleted = false;              
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name 5028d108bdf34393a12bb51e03aa262d", entity.Name);
                            Assert.AreEqual("Description 5028d108bdf34393a12bb51e03aa262d", entity.Description);
                            Assert.AreEqual(false, entity.IsDeleted);
              
        }

        [Test]
        public void PaymentMethod_Update_InvalidId()
        {
            var dal = PreparePaymentMethodDal("DALInitParams");

            var entity = new PaymentMethod();
                          entity.Name = "Name 5028d108bdf34393a12bb51e03aa262d";
                            entity.Description = "Description 5028d108bdf34393a12bb51e03aa262d";
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
