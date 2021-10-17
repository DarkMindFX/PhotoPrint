

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
            
                          Assert.AreEqual("UnitName 7b95a925cfff440f877862139e93e1d6", entity.UnitName);
                            Assert.AreEqual("UnitAbbr 7b95a925cfff440f877862139e93e1d6", entity.UnitAbbr);
                            Assert.AreEqual("Description 7b95a925cfff440f877862139e93e1d6", entity.Description);
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
                          entity.UnitName = "UnitName a583638621124a0587c3009e29aa9c57";
                            entity.UnitAbbr = "UnitAbbr a583638621124a0587c3009e29aa9c57";
                            entity.Description = "Description a583638621124a0587c3009e29aa9c57";
                            entity.IsDeleted = false;              
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("UnitName a583638621124a0587c3009e29aa9c57", entity.UnitName);
                            Assert.AreEqual("UnitAbbr a583638621124a0587c3009e29aa9c57", entity.UnitAbbr);
                            Assert.AreEqual("Description a583638621124a0587c3009e29aa9c57", entity.Description);
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

                          entity.UnitName = "UnitName 7f2159ffcdb343a9b02b6e1866528d38";
                            entity.UnitAbbr = "UnitAbbr 7f2159ffcdb343a9b02b6e1866528d38";
                            entity.Description = "Description 7f2159ffcdb343a9b02b6e1866528d38";
                            entity.IsDeleted = false;              
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("UnitName 7f2159ffcdb343a9b02b6e1866528d38", entity.UnitName);
                            Assert.AreEqual("UnitAbbr 7f2159ffcdb343a9b02b6e1866528d38", entity.UnitAbbr);
                            Assert.AreEqual("Description 7f2159ffcdb343a9b02b6e1866528d38", entity.Description);
                            Assert.AreEqual(false, entity.IsDeleted);
              
        }

        [Test]
        public void Unit_Update_InvalidId()
        {
            var dal = PrepareUnitDal("DALInitParams");

            var entity = new Unit();
                          entity.UnitName = "UnitName 7f2159ffcdb343a9b02b6e1866528d38";
                            entity.UnitAbbr = "UnitAbbr 7f2159ffcdb343a9b02b6e1866528d38";
                            entity.Description = "Description 7f2159ffcdb343a9b02b6e1866528d38";
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
