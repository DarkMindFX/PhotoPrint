

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

            Assert.AreEqual("SizeName 27a37d68711b4701b9cadc460e2e8e1f", entity.SizeName);
            Assert.AreEqual(366, entity.Width);
            Assert.AreEqual(366, entity.Height);
            Assert.AreEqual(365827, entity.IsDeleted);
            Assert.AreEqual(DateTime.Parse("1/21/2021 11:20:49 PM"), entity.CreatedDate);
            Assert.AreEqual(100004, entity.CreatedByID);
            Assert.AreEqual(DateTime.Parse("5/16/2022 12:46:49 PM"), entity.ModifiedDate);
            Assert.AreEqual(100003, entity.ModifiedByID);
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
            entity.SizeName = "SizeName 474354b31cec492d8d7638ba3ae25c40";
            entity.Width = 695;
            entity.Height = 695;
            entity.IsDeleted = true;
            entity.CreatedDate = DateTime.Parse("11/12/2022 6:33:49 PM");
            entity.CreatedByID = 100007;
            entity.ModifiedDate = DateTime.Parse("4/1/2020 4:20:49 AM");
            entity.ModifiedByID = 100009;

            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.ID);

            Assert.AreEqual("SizeName 474354b31cec492d8d7638ba3ae25c40", entity.SizeName);
            Assert.AreEqual(695, entity.Width);
            Assert.AreEqual(695, entity.Height);
            Assert.AreEqual(695563, entity.IsDeleted);
            Assert.AreEqual(DateTime.Parse("11/12/2022 6:33:49 PM"), entity.CreatedDate);
            Assert.AreEqual(100007, entity.CreatedByID);
            Assert.AreEqual(DateTime.Parse("4/1/2020 4:20:49 AM"), entity.ModifiedDate);
            Assert.AreEqual(100009, entity.ModifiedByID);

        }

        [TestCase("Size\\030.Update.Success")]
        public void Size_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareSizeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
            var paramID = (System.Int64?)objIds[0];
            Size entity = dal.Get(paramID);

            entity.SizeName = "SizeName c97f2011693747218d3a68f4c1b849f1";
            entity.Width = 740;
            entity.Height = 740;
            entity.IsDeleted = false;
            entity.CreatedDate = DateTime.Parse("2/10/2023 2:07:49 PM");
            entity.CreatedByID = 100006;
            entity.ModifiedDate = DateTime.Parse("6/29/2020 2:34:49 PM");
            entity.ModifiedByID = 100004;

            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.ID);

            Assert.AreEqual("SizeName c97f2011693747218d3a68f4c1b849f1", entity.SizeName);
            Assert.AreEqual(740, entity.Width);
            Assert.AreEqual(740, entity.Height);
            Assert.AreEqual(740413, entity.IsDeleted);
            Assert.AreEqual(DateTime.Parse("2/10/2023 2:07:49 PM"), entity.CreatedDate);
            Assert.AreEqual(100006, entity.CreatedByID);
            Assert.AreEqual(DateTime.Parse("6/29/2020 2:34:49 PM"), entity.ModifiedDate);
            Assert.AreEqual(100004, entity.ModifiedByID);

        }

        [Test]
        public void Size_Update_InvalidId()
        {
            var dal = PrepareSizeDal("DALInitParams");

            var entity = new Size();
            entity.SizeName = "SizeName c97f2011693747218d3a68f4c1b849f1";
            entity.Width = 740;
            entity.Height = 740;
            entity.IsDeleted = false;
            entity.CreatedDate = DateTime.Parse("2/10/2023 2:07:49 PM");
            entity.CreatedByID = 100006;
            entity.ModifiedDate = DateTime.Parse("6/29/2020 2:34:49 PM");
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
