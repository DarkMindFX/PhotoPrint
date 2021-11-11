


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
    public class TestUnitDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IUnitDal dal = new UnitDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void Unit_GetAll_Success()
        {
            var dal = PrepareUnitDal("DALInitParams");

            IList<Unit> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("Unit\\000.GetDetails.Success")]
        public void Unit_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUnitDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Unit entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("UnitName 750a867ec61b4d31a76437c45c1b4158", entity.UnitName);
                            Assert.AreEqual("UnitAbbr 750a867ec61b4d31a76437c45c1b4158", entity.UnitAbbr);
                            Assert.AreEqual("Description 750a867ec61b4d31a76437c45c1b4158", entity.Description);
                            Assert.AreEqual(false, entity.IsDeleted);
                      }

        [Test]
        public void Unit_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareUnitDal("DALInitParams");

            Unit entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("Unit\\010.Delete.Success")]
        public void Unit_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUnitDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void Unit_Delete_InvalidId()
        {
            var dal = PrepareUnitDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("Unit\\020.Insert.Success")]
        public void Unit_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareUnitDal("DALInitParams");

            var entity = new Unit();
                          entity.UnitName = "UnitName 9e9a25d9dc3d4549bb58b22495b40a77";
                            entity.UnitAbbr = "UnitAbbr 9e9a25d9dc3d4549bb58b22495b40a77";
                            entity.Description = "Description 9e9a25d9dc3d4549bb58b22495b40a77";
                            entity.IsDeleted = false;              
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("UnitName 9e9a25d9dc3d4549bb58b22495b40a77", entity.UnitName);
                            Assert.AreEqual("UnitAbbr 9e9a25d9dc3d4549bb58b22495b40a77", entity.UnitAbbr);
                            Assert.AreEqual("Description 9e9a25d9dc3d4549bb58b22495b40a77", entity.Description);
                            Assert.AreEqual(false, entity.IsDeleted);
              
        }

        [TestCase("Unit\\030.Update.Success")]
        public void Unit_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUnitDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Unit entity = dal.Get(paramID);

                          entity.UnitName = "UnitName 78663a6dedc042c585301831c29347db";
                            entity.UnitAbbr = "UnitAbbr 78663a6dedc042c585301831c29347db";
                            entity.Description = "Description 78663a6dedc042c585301831c29347db";
                            entity.IsDeleted = false;              
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("UnitName 78663a6dedc042c585301831c29347db", entity.UnitName);
                            Assert.AreEqual("UnitAbbr 78663a6dedc042c585301831c29347db", entity.UnitAbbr);
                            Assert.AreEqual("Description 78663a6dedc042c585301831c29347db", entity.Description);
                            Assert.AreEqual(false, entity.IsDeleted);
              
        }

        [Test]
        public void Unit_Update_InvalidId()
        {
            var dal = PrepareUnitDal("DALInitParams");

            var entity = new Unit();
                          entity.UnitName = "UnitName 78663a6dedc042c585301831c29347db";
                            entity.UnitAbbr = "UnitAbbr 78663a6dedc042c585301831c29347db";
                            entity.Description = "Description 78663a6dedc042c585301831c29347db";
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

        [TestCase("Unit\\040.Erase.Success")]
        public void Unit_Erase_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUnitDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Erase(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void Unit_Erase_InvalidId()
        {
            var dal = PrepareUnitDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Erase(paramID);
            Assert.IsFalse(removed);

        }

        protected IUnitDal PrepareUnitDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IUnitDal dal = new UnitDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
