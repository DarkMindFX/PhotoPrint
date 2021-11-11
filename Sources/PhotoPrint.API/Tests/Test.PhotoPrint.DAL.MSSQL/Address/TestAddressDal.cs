


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
                            Assert.AreEqual("Title db5a2f197cad4748bd1b94850f713ddf", entity.Title);
                            Assert.AreEqual(1, entity.CityID);
                            Assert.AreEqual("Street db5a2f197cad4748bd1b94850f713ddf", entity.Street);
                            Assert.AreEqual("BuildingNo db5a2f197cad4748bd1b94850f713ddf", entity.BuildingNo);
                            Assert.AreEqual("ApartmentNo db5a2f197cad4748bd1b94850f713ddf", entity.ApartmentNo);
                            Assert.AreEqual("Comment db5a2f197cad4748bd1b94850f713ddf", entity.Comment);
                            Assert.AreEqual(100008, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("10/28/2021 8:22:38 AM"), entity.CreatedDate);
                            Assert.AreEqual(100008, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("6/15/2019 4:22:38 AM"), entity.ModifiedDate);
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
                          entity.AddressTypeID = 4;
                            entity.Title = "Title ee918aa1e601486f9d45655af1390a70";
                            entity.CityID = 4;
                            entity.Street = "Street ee918aa1e601486f9d45655af1390a70";
                            entity.BuildingNo = "BuildingNo ee918aa1e601486f9d45655af1390a70";
                            entity.ApartmentNo = "ApartmentNo ee918aa1e601486f9d45655af1390a70";
                            entity.Comment = "Comment ee918aa1e601486f9d45655af1390a70";
                            entity.CreatedByID = 100006;
                            entity.CreatedDate = DateTime.Parse("4/18/2023 4:24:38 PM");
                            entity.ModifiedByID = 100007;
                            entity.ModifiedDate = DateTime.Parse("4/18/2023 4:24:38 PM");
                            entity.IsDeleted = false;              
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(4, entity.AddressTypeID);
                            Assert.AreEqual("Title ee918aa1e601486f9d45655af1390a70", entity.Title);
                            Assert.AreEqual(4, entity.CityID);
                            Assert.AreEqual("Street ee918aa1e601486f9d45655af1390a70", entity.Street);
                            Assert.AreEqual("BuildingNo ee918aa1e601486f9d45655af1390a70", entity.BuildingNo);
                            Assert.AreEqual("ApartmentNo ee918aa1e601486f9d45655af1390a70", entity.ApartmentNo);
                            Assert.AreEqual("Comment ee918aa1e601486f9d45655af1390a70", entity.Comment);
                            Assert.AreEqual(100006, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("4/18/2023 4:24:38 PM"), entity.CreatedDate);
                            Assert.AreEqual(100007, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("4/18/2023 4:24:38 PM"), entity.ModifiedDate);
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
                            entity.Title = "Title a83d33ef6a2142b7aa70cee52c4794c3";
                            entity.CityID = 7;
                            entity.Street = "Street a83d33ef6a2142b7aa70cee52c4794c3";
                            entity.BuildingNo = "BuildingNo a83d33ef6a2142b7aa70cee52c4794c3";
                            entity.ApartmentNo = "ApartmentNo a83d33ef6a2142b7aa70cee52c4794c3";
                            entity.Comment = "Comment a83d33ef6a2142b7aa70cee52c4794c3";
                            entity.CreatedByID = 100006;
                            entity.CreatedDate = DateTime.Parse("7/18/2023 11:58:38 AM");
                            entity.ModifiedByID = 100007;
                            entity.ModifiedDate = DateTime.Parse("12/4/2020 12:24:38 PM");
                            entity.IsDeleted = true;              
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(5, entity.AddressTypeID);
                            Assert.AreEqual("Title a83d33ef6a2142b7aa70cee52c4794c3", entity.Title);
                            Assert.AreEqual(7, entity.CityID);
                            Assert.AreEqual("Street a83d33ef6a2142b7aa70cee52c4794c3", entity.Street);
                            Assert.AreEqual("BuildingNo a83d33ef6a2142b7aa70cee52c4794c3", entity.BuildingNo);
                            Assert.AreEqual("ApartmentNo a83d33ef6a2142b7aa70cee52c4794c3", entity.ApartmentNo);
                            Assert.AreEqual("Comment a83d33ef6a2142b7aa70cee52c4794c3", entity.Comment);
                            Assert.AreEqual(100006, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("7/18/2023 11:58:38 AM"), entity.CreatedDate);
                            Assert.AreEqual(100007, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("12/4/2020 12:24:38 PM"), entity.ModifiedDate);
                            Assert.AreEqual(true, entity.IsDeleted);
              
        }

        [Test]
        public void Address_Update_InvalidId()
        {
            var dal = PrepareAddressDal("DALInitParams");

            var entity = new Address();
                          entity.AddressTypeID = 5;
                            entity.Title = "Title a83d33ef6a2142b7aa70cee52c4794c3";
                            entity.CityID = 7;
                            entity.Street = "Street a83d33ef6a2142b7aa70cee52c4794c3";
                            entity.BuildingNo = "BuildingNo a83d33ef6a2142b7aa70cee52c4794c3";
                            entity.ApartmentNo = "ApartmentNo a83d33ef6a2142b7aa70cee52c4794c3";
                            entity.Comment = "Comment a83d33ef6a2142b7aa70cee52c4794c3";
                            entity.CreatedByID = 100006;
                            entity.CreatedDate = DateTime.Parse("7/18/2023 11:58:38 AM");
                            entity.ModifiedByID = 100007;
                            entity.ModifiedDate = DateTime.Parse("12/4/2020 12:24:38 PM");
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

        [TestCase("Address\\040.Erase.Success")]
        public void Address_Erase_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareAddressDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Erase(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void Address_Erase_InvalidId()
        {
            var dal = PrepareAddressDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Erase(paramID);
            Assert.IsFalse(removed);

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
