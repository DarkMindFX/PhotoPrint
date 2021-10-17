

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
    public class TestMountingTypeDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IMountingTypeDal dal = new MountingTypeDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void MountingType_GetAll_Success()
        {
            var dal = PrepareMountingTypeDal("DALInitParams");

            IList<MountingType> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("MountingType\\000.GetDetails.Success")]
        public void MountingType_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareMountingTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            MountingType entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("MountingTypeName b1f6393c5a3249048f1b7f7570e6b13b", entity.MountingTypeName);
                            Assert.AreEqual("Description b1f6393c5a3249048f1b7f7570e6b13b", entity.Description);
                            Assert.AreEqual("ThumbnailUrl b1f6393c5a3249048f1b7f7570e6b13b", entity.ThumbnailUrl);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("5/6/2021 8:48:48 AM"), entity.CreatedDate);
                            Assert.AreEqual(418165, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("5/6/2021 8:48:48 AM"), entity.ModifiedDate);
                            Assert.AreEqual(418165, entity.ModifiedByID);
                      }

        [Test]
        public void MountingType_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareMountingTypeDal("DALInitParams");

            MountingType entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("MountingType\\010.Delete.Success")]
        public void MountingType_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareMountingTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void MountingType_Delete_InvalidId()
        {
            var dal = PrepareMountingTypeDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("MountingType\\020.Insert.Success")]
        public void MountingType_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareMountingTypeDal("DALInitParams");

            var entity = new MountingType();
                          entity.MountingTypeName = "MountingTypeName 695d93859a574f369a6c371d31e97c65";
                            entity.Description = "Description 695d93859a574f369a6c371d31e97c65";
                            entity.ThumbnailUrl = "ThumbnailUrl 695d93859a574f369a6c371d31e97c65";
                            entity.IsDeleted = false;              
                            entity.CreatedDate = DateTime.Parse("5/6/2021 8:48:48 AM");
                            entity.CreatedByID = 418165;
                            entity.ModifiedDate = DateTime.Parse("5/6/2021 8:48:48 AM");
                            entity.ModifiedByID = 418165;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("MountingTypeName 695d93859a574f369a6c371d31e97c65", entity.MountingTypeName);
                            Assert.AreEqual("Description 695d93859a574f369a6c371d31e97c65", entity.Description);
                            Assert.AreEqual("ThumbnailUrl 695d93859a574f369a6c371d31e97c65", entity.ThumbnailUrl);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("5/6/2021 8:48:48 AM"), entity.CreatedDate);
                            Assert.AreEqual(418165, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("5/6/2021 8:48:48 AM"), entity.ModifiedDate);
                            Assert.AreEqual(418165, entity.ModifiedByID);
              
        }

        [TestCase("MountingType\\030.Update.Success")]
        public void MountingType_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareMountingTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            MountingType entity = dal.Get(paramID);

                          entity.MountingTypeName = "MountingTypeName 51101588f38b498e844dfa54fcf8dccd";
                            entity.Description = "Description 51101588f38b498e844dfa54fcf8dccd";
                            entity.ThumbnailUrl = "ThumbnailUrl 51101588f38b498e844dfa54fcf8dccd";
                            entity.IsDeleted = false;              
                            entity.CreatedDate = DateTime.Parse("5/6/2021 8:48:48 AM");
                            entity.CreatedByID = 418165;
                            entity.ModifiedDate = DateTime.Parse("5/6/2021 8:48:48 AM");
                            entity.ModifiedByID = 418165;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("MountingTypeName 51101588f38b498e844dfa54fcf8dccd", entity.MountingTypeName);
                            Assert.AreEqual("Description 51101588f38b498e844dfa54fcf8dccd", entity.Description);
                            Assert.AreEqual("ThumbnailUrl 51101588f38b498e844dfa54fcf8dccd", entity.ThumbnailUrl);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("5/6/2021 8:48:48 AM"), entity.CreatedDate);
                            Assert.AreEqual(418165, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("5/6/2021 8:48:48 AM"), entity.ModifiedDate);
                            Assert.AreEqual(418165, entity.ModifiedByID);
              
        }

        [Test]
        public void MountingType_Update_InvalidId()
        {
            var dal = PrepareMountingTypeDal("DALInitParams");

            var entity = new MountingType();
                          entity.MountingTypeName = "MountingTypeName 51101588f38b498e844dfa54fcf8dccd";
                            entity.Description = "Description 51101588f38b498e844dfa54fcf8dccd";
                            entity.ThumbnailUrl = "ThumbnailUrl 51101588f38b498e844dfa54fcf8dccd";
                            entity.IsDeleted = false;              
                            entity.CreatedDate = DateTime.Parse("5/6/2021 8:48:48 AM");
                            entity.CreatedByID = 418165;
                            entity.ModifiedDate = DateTime.Parse("5/6/2021 8:48:48 AM");
                            entity.ModifiedByID = 418165;
              
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

        protected IMountingTypeDal PrepareMountingTypeDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IMountingTypeDal dal = new MountingTypeDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
