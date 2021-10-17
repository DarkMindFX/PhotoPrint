

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
            
                          Assert.AreEqual(6, entity.AddressTypeID);
                            Assert.AreEqual("Title 51afaf29fabb439497450d01f77efac9", entity.Title);
                            Assert.AreEqual(2, entity.CityID);
                            Assert.AreEqual("Street 51afaf29fabb439497450d01f77efac9", entity.Street);
                            Assert.AreEqual("BuildingNo 51afaf29fabb439497450d01f77efac9", entity.BuildingNo);
                            Assert.AreEqual("ApartmentNo 51afaf29fabb439497450d01f77efac9", entity.ApartmentNo);
                            Assert.AreEqual("Comment 51afaf29fabb439497450d01f77efac9", entity.Comment);
                            Assert.AreEqual(100001, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("7/9/2019 5:28:47 AM"), entity.CreatedDate);
                            Assert.AreEqual(100004, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("2/12/2023 7:16:47 AM"), entity.ModifiedDate);
                            Assert.AreEqual(false, entity.IsDeleted);
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
                          entity.AddressTypeID = 5;
                            entity.Title = "Title ce88a3df658a4965a9de08e4fefae725";
                            entity.CityID = 6;
                            entity.Street = "Street ce88a3df658a4965a9de08e4fefae725";
                            entity.BuildingNo = "BuildingNo ce88a3df658a4965a9de08e4fefae725";
                            entity.ApartmentNo = "ApartmentNo ce88a3df658a4965a9de08e4fefae725";
                            entity.Comment = "Comment ce88a3df658a4965a9de08e4fefae725";
                            entity.CreatedByID = 100002;
                            entity.CreatedDate = DateTime.Parse("4/28/2021 12:41:47 AM");
                            entity.ModifiedByID = 100010;
                            entity.ModifiedDate = DateTime.Parse("3/7/2020 6:30:47 PM");
                            entity.IsDeleted = true;              
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(5, entity.AddressTypeID);
                            Assert.AreEqual("Title ce88a3df658a4965a9de08e4fefae725", entity.Title);
                            Assert.AreEqual(6, entity.CityID);
                            Assert.AreEqual("Street ce88a3df658a4965a9de08e4fefae725", entity.Street);
                            Assert.AreEqual("BuildingNo ce88a3df658a4965a9de08e4fefae725", entity.BuildingNo);
                            Assert.AreEqual("ApartmentNo ce88a3df658a4965a9de08e4fefae725", entity.ApartmentNo);
                            Assert.AreEqual("Comment ce88a3df658a4965a9de08e4fefae725", entity.Comment);
                            Assert.AreEqual(100002, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("4/28/2021 12:41:47 AM"), entity.CreatedDate);
                            Assert.AreEqual(100010, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("3/7/2020 6:30:47 PM"), entity.ModifiedDate);
                            Assert.AreEqual(true, entity.IsDeleted);
              
        }

        [TestCase("Address\\030.Update.Success")]
        public void Address_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareAddressDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Address entity = dal.Get(paramID);

                          entity.AddressTypeID = 4;
                            entity.Title = "Title 1d460a7612384807b882c7738b4cfae3";
                            entity.CityID = 5;
                            entity.Street = "Street 1d460a7612384807b882c7738b4cfae3";
                            entity.BuildingNo = "BuildingNo 1d460a7612384807b882c7738b4cfae3";
                            entity.ApartmentNo = "ApartmentNo 1d460a7612384807b882c7738b4cfae3";
                            entity.Comment = "Comment 1d460a7612384807b882c7738b4cfae3";
                            entity.CreatedByID = 100010;
                            entity.CreatedDate = DateTime.Parse("10/24/2022 6:41:47 AM");
                            entity.ModifiedByID = 100002;
                            entity.ModifiedDate = DateTime.Parse("10/16/2023 8:56:47 AM");
                            entity.IsDeleted = false;              
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(4, entity.AddressTypeID);
                            Assert.AreEqual("Title 1d460a7612384807b882c7738b4cfae3", entity.Title);
                            Assert.AreEqual(5, entity.CityID);
                            Assert.AreEqual("Street 1d460a7612384807b882c7738b4cfae3", entity.Street);
                            Assert.AreEqual("BuildingNo 1d460a7612384807b882c7738b4cfae3", entity.BuildingNo);
                            Assert.AreEqual("ApartmentNo 1d460a7612384807b882c7738b4cfae3", entity.ApartmentNo);
                            Assert.AreEqual("Comment 1d460a7612384807b882c7738b4cfae3", entity.Comment);
                            Assert.AreEqual(100010, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("10/24/2022 6:41:47 AM"), entity.CreatedDate);
                            Assert.AreEqual(100002, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("10/16/2023 8:56:47 AM"), entity.ModifiedDate);
                            Assert.AreEqual(false, entity.IsDeleted);
              
        }

        [Test]
        public void Address_Update_InvalidId()
        {
            var dal = PrepareAddressDal("DALInitParams");

            var entity = new Address();
                          entity.AddressTypeID = 4;
                            entity.Title = "Title 1d460a7612384807b882c7738b4cfae3";
                            entity.CityID = 5;
                            entity.Street = "Street 1d460a7612384807b882c7738b4cfae3";
                            entity.BuildingNo = "BuildingNo 1d460a7612384807b882c7738b4cfae3";
                            entity.ApartmentNo = "ApartmentNo 1d460a7612384807b882c7738b4cfae3";
                            entity.Comment = "Comment 1d460a7612384807b882c7738b4cfae3";
                            entity.CreatedByID = 100010;
                            entity.CreatedDate = DateTime.Parse("10/24/2022 6:41:47 AM");
                            entity.ModifiedByID = 100002;
                            entity.ModifiedDate = DateTime.Parse("10/16/2023 8:56:47 AM");
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
