

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
    public class TestAddressDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IAddressDal dal = new AddressDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void Address_GetAll_Success()
        {
            var dal = PrepareAddressDal("DALInitParams");

            IList<Address> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("Address\\000.GetDetails.Success")]
        public void Address_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareAddressDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Address entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(2, entity.AddressTypeID);
                            Assert.AreEqual("Title dc3480a1231d484491e1afb6406bbf69", entity.Title);
                            Assert.AreEqual(10, entity.CityID);
                            Assert.AreEqual("Street dc3480a1231d484491e1afb6406bbf69", entity.Street);
                            Assert.AreEqual("BuildingNo dc3480a1231d484491e1afb6406bbf69", entity.BuildingNo);
                            Assert.AreEqual("ApartmentNo dc3480a1231d484491e1afb6406bbf69", entity.ApartmentNo);
                            Assert.AreEqual("Comment dc3480a1231d484491e1afb6406bbf69", entity.Comment);
                            Assert.AreEqual(100011, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("11/22/2022 10:22:32 PM"), entity.CreatedDate);
                            Assert.AreEqual(100002, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("11/22/2022 10:22:32 PM"), entity.ModifiedDate);
                            Assert.AreEqual(true, entity.IsDeleted);
                      }

        [Test]
        public void Address_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareAddressDal("DALInitParams");

            Address entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("Address\\010.Delete.Success")]
        public void Address_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareAddressDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void Address_Delete_InvalidId()
        {
            var dal = PrepareAddressDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("Address\\020.Insert.Success")]
        public void Address_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareAddressDal("DALInitParams");

            var entity = new Address();
                          entity.AddressTypeID = 2;
                            entity.Title = "Title 5ed1a83072274ffbbbf89b566f79da3d";
                            entity.CityID = 9;
                            entity.Street = "Street 5ed1a83072274ffbbbf89b566f79da3d";
                            entity.BuildingNo = "BuildingNo 5ed1a83072274ffbbbf89b566f79da3d";
                            entity.ApartmentNo = "ApartmentNo 5ed1a83072274ffbbbf89b566f79da3d";
                            entity.Comment = "Comment 5ed1a83072274ffbbbf89b566f79da3d";
                            entity.CreatedByID = 100003;
                            entity.CreatedDate = DateTime.Parse("7/10/2020 6:22:32 PM");
                            entity.ModifiedByID = 100006;
                            entity.ModifiedDate = DateTime.Parse("5/22/2023 4:09:32 AM");
                            entity.IsDeleted = false;              
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(2, entity.AddressTypeID);
                            Assert.AreEqual("Title 5ed1a83072274ffbbbf89b566f79da3d", entity.Title);
                            Assert.AreEqual(9, entity.CityID);
                            Assert.AreEqual("Street 5ed1a83072274ffbbbf89b566f79da3d", entity.Street);
                            Assert.AreEqual("BuildingNo 5ed1a83072274ffbbbf89b566f79da3d", entity.BuildingNo);
                            Assert.AreEqual("ApartmentNo 5ed1a83072274ffbbbf89b566f79da3d", entity.ApartmentNo);
                            Assert.AreEqual("Comment 5ed1a83072274ffbbbf89b566f79da3d", entity.Comment);
                            Assert.AreEqual(100003, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("7/10/2020 6:22:32 PM"), entity.CreatedDate);
                            Assert.AreEqual(100006, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("5/22/2023 4:09:32 AM"), entity.ModifiedDate);
                            Assert.AreEqual(false, entity.IsDeleted);
              
        }

        [TestCase("Address\\030.Update.Success")]
        public void Address_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareAddressDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Address entity = dal.Get(paramID);

                          entity.AddressTypeID = 5;
                            entity.Title = "Title 234dadaa56194b089b0c097361f548a0";
                            entity.CityID = 1;
                            entity.Street = "Street 234dadaa56194b089b0c097361f548a0";
                            entity.BuildingNo = "BuildingNo 234dadaa56194b089b0c097361f548a0";
                            entity.ApartmentNo = "ApartmentNo 234dadaa56194b089b0c097361f548a0";
                            entity.Comment = "Comment 234dadaa56194b089b0c097361f548a0";
                            entity.CreatedByID = 100005;
                            entity.CreatedDate = DateTime.Parse("8/18/2023 2:23:32 PM");
                            entity.ModifiedByID = 100010;
                            entity.ModifiedDate = DateTime.Parse("8/18/2023 2:23:32 PM");
                            entity.IsDeleted = true;              
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(5, entity.AddressTypeID);
                            Assert.AreEqual("Title 234dadaa56194b089b0c097361f548a0", entity.Title);
                            Assert.AreEqual(1, entity.CityID);
                            Assert.AreEqual("Street 234dadaa56194b089b0c097361f548a0", entity.Street);
                            Assert.AreEqual("BuildingNo 234dadaa56194b089b0c097361f548a0", entity.BuildingNo);
                            Assert.AreEqual("ApartmentNo 234dadaa56194b089b0c097361f548a0", entity.ApartmentNo);
                            Assert.AreEqual("Comment 234dadaa56194b089b0c097361f548a0", entity.Comment);
                            Assert.AreEqual(100005, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("8/18/2023 2:23:32 PM"), entity.CreatedDate);
                            Assert.AreEqual(100010, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("8/18/2023 2:23:32 PM"), entity.ModifiedDate);
                            Assert.AreEqual(true, entity.IsDeleted);
              
        }

        [Test]
        public void Address_Update_InvalidId()
        {
            var dal = PrepareAddressDal("DALInitParams");

            var entity = new Address();
                          entity.AddressTypeID = 5;
                            entity.Title = "Title 234dadaa56194b089b0c097361f548a0";
                            entity.CityID = 1;
                            entity.Street = "Street 234dadaa56194b089b0c097361f548a0";
                            entity.BuildingNo = "BuildingNo 234dadaa56194b089b0c097361f548a0";
                            entity.ApartmentNo = "ApartmentNo 234dadaa56194b089b0c097361f548a0";
                            entity.Comment = "Comment 234dadaa56194b089b0c097361f548a0";
                            entity.CreatedByID = 100005;
                            entity.CreatedDate = DateTime.Parse("8/18/2023 2:23:32 PM");
                            entity.ModifiedByID = 100010;
                            entity.ModifiedDate = DateTime.Parse("8/18/2023 2:23:32 PM");
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

        protected IAddressDal PrepareAddressDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IAddressDal dal = new AddressDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
