


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
            
                          Assert.AreEqual("SizeName 7102ceab52464af0877533b065494de4", entity.SizeName);
                            Assert.AreEqual(219, entity.Width);
                            Assert.AreEqual(219, entity.Height);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("4/20/2020 10:37:40 PM"), entity.CreatedDate);
                            Assert.AreEqual(100009, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("10/4/2022 6:14:40 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100004, entity.ModifiedByID);
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
                          entity.SizeName = "SizeName 8b04bc5e957c44c8a7394e3b9198ec48";
                            entity.Width = 206;
                            entity.Height = 206;
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("3/25/2020 7:38:40 PM");
                            entity.CreatedByID = 100001;
                            entity.ModifiedDate = DateTime.Parse("3/25/2020 7:38:40 PM");
                            entity.ModifiedByID = 100003;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("SizeName 8b04bc5e957c44c8a7394e3b9198ec48", entity.SizeName);
                            Assert.AreEqual(206, entity.Width);
                            Assert.AreEqual(206, entity.Height);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("3/25/2020 7:38:40 PM"), entity.CreatedDate);
                            Assert.AreEqual(100001, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("3/25/2020 7:38:40 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100003, entity.ModifiedByID);
              
        }

        [TestCase("Size\\030.Update.Success")]
        public void Size_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareSizeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Size entity = dal.Get(paramID);

                          entity.SizeName = "SizeName 92a0c91e3d5e4ed5b097c3141a31d97c";
                            entity.Width = 728;
                            entity.Height = 728;
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("2/3/2023 5:25:40 AM");
                            entity.CreatedByID = 100003;
                            entity.ModifiedDate = DateTime.Parse("6/23/2020 3:11:40 PM");
                            entity.ModifiedByID = 100004;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("SizeName 92a0c91e3d5e4ed5b097c3141a31d97c", entity.SizeName);
                            Assert.AreEqual(728, entity.Width);
                            Assert.AreEqual(728, entity.Height);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("2/3/2023 5:25:40 AM"), entity.CreatedDate);
                            Assert.AreEqual(100003, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("6/23/2020 3:11:40 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100004, entity.ModifiedByID);
              
        }

        [Test]
        public void Size_Update_InvalidId()
        {
            var dal = PrepareSizeDal("DALInitParams");

            var entity = new Size();
                          entity.SizeName = "SizeName 92a0c91e3d5e4ed5b097c3141a31d97c";
                            entity.Width = 728;
                            entity.Height = 728;
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("2/3/2023 5:25:40 AM");
                            entity.CreatedByID = 100003;
                            entity.ModifiedDate = DateTime.Parse("6/23/2020 3:11:40 PM");
                            entity.ModifiedByID = 100004;
              
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

        [TestCase("Size\\040.Erase.Success")]
        public void Size_Erase_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareSizeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Erase(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void Size_Erase_InvalidId()
        {
            var dal = PrepareSizeDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Erase(paramID);
            Assert.IsFalse(removed);

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
