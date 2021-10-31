

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
    public class TestSizeDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            ISizeDal dal = new SizeDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void Size_GetAll_Success()
        {
            var dal = PrepareSizeDal("DALInitParams");

            IList<Size> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("Size\\000.GetDetails.Success")]
        public void Size_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareSizeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Size entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("SizeName b5c1ceaf194f4c51bbdb08ffc9020da2", entity.SizeName);
                            Assert.AreEqual(255, entity.Width);
                            Assert.AreEqual(255, entity.Height);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("6/27/2020 9:29:33 PM"), entity.CreatedDate);
                            Assert.AreEqual(100007, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("5/7/2019 3:18:33 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100006, entity.ModifiedByID);
                      }

        [Test]
        public void Size_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareSizeDal("DALInitParams");

            Size entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("Size\\010.Delete.Success")]
        public void Size_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareSizeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void Size_Delete_InvalidId()
        {
            var dal = PrepareSizeDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("Size\\020.Insert.Success")]
        public void Size_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareSizeDal("DALInitParams");

            var entity = new Size();
                          entity.SizeName = "SizeName b717ab6e141c44de9047816daf4825cc";
                            entity.Width = 748;
                            entity.Height = 748;
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("3/11/2023 12:39:33 PM");
                            entity.CreatedByID = 100001;
                            entity.ModifiedDate = DateTime.Parse("7/28/2020 1:06:33 PM");
                            entity.ModifiedByID = 100005;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("SizeName b717ab6e141c44de9047816daf4825cc", entity.SizeName);
                            Assert.AreEqual(748, entity.Width);
                            Assert.AreEqual(748, entity.Height);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("3/11/2023 12:39:33 PM"), entity.CreatedDate);
                            Assert.AreEqual(100001, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("7/28/2020 1:06:33 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100005, entity.ModifiedByID);
              
        }

        [TestCase("Size\\030.Update.Success")]
        public void Size_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareSizeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Size entity = dal.Get(paramID);

                          entity.SizeName = "SizeName f12f38841db54f4aa537c7b702b8edbd";
                            entity.Width = 837;
                            entity.Height = 837;
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("9/6/2023 9:07:33 AM");
                            entity.CreatedByID = 100010;
                            entity.ModifiedDate = DateTime.Parse("1/24/2021 6:54:33 PM");
                            entity.ModifiedByID = 100011;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("SizeName f12f38841db54f4aa537c7b702b8edbd", entity.SizeName);
                            Assert.AreEqual(837, entity.Width);
                            Assert.AreEqual(837, entity.Height);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("9/6/2023 9:07:33 AM"), entity.CreatedDate);
                            Assert.AreEqual(100010, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("1/24/2021 6:54:33 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100011, entity.ModifiedByID);
              
        }

        [Test]
        public void Size_Update_InvalidId()
        {
            var dal = PrepareSizeDal("DALInitParams");

            var entity = new Size();
                          entity.SizeName = "SizeName f12f38841db54f4aa537c7b702b8edbd";
                            entity.Width = 837;
                            entity.Height = 837;
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("9/6/2023 9:07:33 AM");
                            entity.CreatedByID = 100010;
                            entity.ModifiedDate = DateTime.Parse("1/24/2021 6:54:33 PM");
                            entity.ModifiedByID = 100011;
              
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

        protected ISizeDal PrepareSizeDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            ISizeDal dal = new SizeDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
