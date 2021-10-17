

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
    public class TestOrderItemDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IOrderItemDal dal = new OrderItemDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void OrderItem_GetAll_Success()
        {
            var dal = PrepareOrderItemDal("DALInitParams");

            IList<OrderItem> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("OrderItem\\000.GetDetails.Success")]
        public void OrderItem_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareOrderItemDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            OrderItem entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100007, entity.OrderID);
                            Assert.AreEqual(100019, entity.ImageID);
                            Assert.AreEqual(953, entity.Width);
                            Assert.AreEqual(953, entity.Height);
                            Assert.AreEqual(100009, entity.SizeID);
                            Assert.AreEqual(100005, entity.FrameTypeID);
                            Assert.AreEqual(100011, entity.FrameSizeID);
                            Assert.AreEqual(100005, entity.MatID);
                            Assert.AreEqual(100001, entity.MaterialTypeID);
                            Assert.AreEqual(100006, entity.MountingTypeID);
                            Assert.AreEqual(373, entity.ItemCount);
                            Assert.AreEqual(555, entity.PriceAmountPerItem);
                            Assert.AreEqual(35, entity.PriceCurrencyID);
                            Assert.AreEqual("Comments 8534c1f87d8e4cd09c5c3a398e883d64", entity.Comments);
                            Assert.AreEqual(100003, entity.PrintingHouseID);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("5/4/2021 6:28:49 AM"), entity.CreatedDate);
                            Assert.AreEqual(100010, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("3/14/2024 4:14:49 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100006, entity.ModifiedByID);
                      }

        [Test]
        public void OrderItem_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareOrderItemDal("DALInitParams");

            OrderItem entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("OrderItem\\010.Delete.Success")]
        public void OrderItem_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareOrderItemDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void OrderItem_Delete_InvalidId()
        {
            var dal = PrepareOrderItemDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("OrderItem\\020.Insert.Success")]
        public void OrderItem_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareOrderItemDal("DALInitParams");

            var entity = new OrderItem();
                          entity.OrderID = 100012;
                            entity.ImageID = 100020;
                            entity.Width = 388;
                            entity.Height = 388;
                            entity.SizeID = 100008;
                            entity.FrameTypeID = 100006;
                            entity.FrameSizeID = 100011;
                            entity.MatID = 100002;
                            entity.MaterialTypeID = 100001;
                            entity.MountingTypeID = 100004;
                            entity.ItemCount = 359;
                            entity.PriceAmountPerItem = 555M;
                            entity.PriceCurrencyID = 208;
                            entity.Comments = "Comments 0ea424b8503641928e07fe541a1de985";
                            entity.PrintingHouseID = 100004;
                            entity.IsDeleted = false;              
                            entity.CreatedDate = DateTime.Parse("11/20/2023 3:02:49 AM");
                            entity.CreatedByID = 100009;
                            entity.ModifiedDate = DateTime.Parse("4/8/2021 12:48:49 PM");
                            entity.ModifiedByID = 100003;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100012, entity.OrderID);
                            Assert.AreEqual(100020, entity.ImageID);
                            Assert.AreEqual(388, entity.Width);
                            Assert.AreEqual(388, entity.Height);
                            Assert.AreEqual(100008, entity.SizeID);
                            Assert.AreEqual(100006, entity.FrameTypeID);
                            Assert.AreEqual(100011, entity.FrameSizeID);
                            Assert.AreEqual(100002, entity.MatID);
                            Assert.AreEqual(100001, entity.MaterialTypeID);
                            Assert.AreEqual(100004, entity.MountingTypeID);
                            Assert.AreEqual(359, entity.ItemCount);
                            Assert.AreEqual(555M, entity.PriceAmountPerItem);
                            Assert.AreEqual(208, entity.PriceCurrencyID);
                            Assert.AreEqual("Comments 0ea424b8503641928e07fe541a1de985", entity.Comments);
                            Assert.AreEqual(100004, entity.PrintingHouseID);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("11/20/2023 3:02:49 AM"), entity.CreatedDate);
                            Assert.AreEqual(100009, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("4/8/2021 12:48:49 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100003, entity.ModifiedByID);
              
        }

        [TestCase("OrderItem\\030.Update.Success")]
        public void OrderItem_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareOrderItemDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            OrderItem entity = dal.Get(paramID);

                          entity.OrderID = 100004;
                            entity.ImageID = 100003;
                            entity.Width = 151;
                            entity.Height = 151;
                            entity.SizeID = 100010;
                            entity.FrameTypeID = 100018;
                            entity.FrameSizeID = 100008;
                            entity.MatID = 100004;
                            entity.MaterialTypeID = 100002;
                            entity.MountingTypeID = 100003;
                            entity.ItemCount = 718;
                            entity.PriceAmountPerItem = 777M;
                            entity.PriceCurrencyID = 144;
                            entity.Comments = "Comments 2d882d9b320742bcb2d2ff7295612622";
                            entity.PrintingHouseID = 100004;
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("8/14/2020 12:25:49 PM");
                            entity.CreatedByID = 100002;
                            entity.ModifiedDate = DateTime.Parse("8/14/2020 12:25:49 PM");
                            entity.ModifiedByID = 100003;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100004, entity.OrderID);
                            Assert.AreEqual(100003, entity.ImageID);
                            Assert.AreEqual(151, entity.Width);
                            Assert.AreEqual(151, entity.Height);
                            Assert.AreEqual(100010, entity.SizeID);
                            Assert.AreEqual(100018, entity.FrameTypeID);
                            Assert.AreEqual(100008, entity.FrameSizeID);
                            Assert.AreEqual(100004, entity.MatID);
                            Assert.AreEqual(100002, entity.MaterialTypeID);
                            Assert.AreEqual(100003, entity.MountingTypeID);
                            Assert.AreEqual(718, entity.ItemCount);
                            Assert.AreEqual(777M, entity.PriceAmountPerItem);
                            Assert.AreEqual(144, entity.PriceCurrencyID);
                            Assert.AreEqual("Comments 2d882d9b320742bcb2d2ff7295612622", entity.Comments);
                            Assert.AreEqual(100004, entity.PrintingHouseID);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("8/14/2020 12:25:49 PM"), entity.CreatedDate);
                            Assert.AreEqual(100002, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("8/14/2020 12:25:49 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100003, entity.ModifiedByID);
              
        }

        [Test]
        public void OrderItem_Update_InvalidId()
        {
            var dal = PrepareOrderItemDal("DALInitParams");

            var entity = new OrderItem();
                          entity.OrderID = 100004;
                            entity.ImageID = 100003;
                            entity.Width = 151;
                            entity.Height = 151;
                            entity.SizeID = 100010;
                            entity.FrameTypeID = 100018;
                            entity.FrameSizeID = 100008;
                            entity.MatID = 100004;
                            entity.MaterialTypeID = 100002;
                            entity.MountingTypeID = 100003;
                            entity.ItemCount = 718;
                            entity.PriceAmountPerItem = 128.00M;
                            entity.PriceCurrencyID = 144;
                            entity.Comments = "Comments 2d882d9b320742bcb2d2ff7295612622";
                            entity.PrintingHouseID = 100004;
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("8/14/2020 12:25:49 PM");
                            entity.CreatedByID = 100002;
                            entity.ModifiedDate = DateTime.Parse("8/14/2020 12:25:49 PM");
                            entity.ModifiedByID = 100003;
              
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

        protected IOrderItemDal PrepareOrderItemDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IOrderItemDal dal = new OrderItemDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
