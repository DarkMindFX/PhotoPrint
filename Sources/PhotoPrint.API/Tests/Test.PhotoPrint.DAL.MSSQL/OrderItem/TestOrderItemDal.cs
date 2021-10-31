

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
                            Assert.AreEqual(100049, entity.ImageID);
                            Assert.AreEqual(461, entity.Width);
                            Assert.AreEqual(461, entity.Height);
                            Assert.AreEqual(100009, entity.SizeID);
                            Assert.AreEqual(100009, entity.FrameTypeID);
                            Assert.AreEqual(100003, entity.FrameSizeID);
                            Assert.AreEqual(100001, entity.MatID);
                            Assert.AreEqual(100001, entity.MaterialTypeID);
                            Assert.AreEqual(100005, entity.MountingTypeID);
                            Assert.AreEqual(163, entity.ItemCount);
                            Assert.AreEqual(163153.51M, entity.PriceAmountPerItem);
                            Assert.AreEqual(133, entity.PriceCurrencyID);
                            Assert.AreEqual("Comments a4c589d9b7f443da81b23019d67f98c3", entity.Comments);
                            Assert.AreEqual(100002, entity.PrintingHouseID);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("3/26/2020 6:26:33 AM"), entity.CreatedDate);
                            Assert.AreEqual(100003, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("3/26/2020 6:26:33 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100008, entity.ModifiedByID);
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
                          entity.OrderID = 100010;
                            entity.ImageID = 100025;
                            entity.Width = 820;
                            entity.Height = 820;
                            entity.SizeID = 100006;
                            entity.FrameTypeID = 100004;
                            entity.FrameSizeID = 100012;
                            entity.MatID = 100005;
                            entity.MaterialTypeID = 100001;
                            entity.MountingTypeID = 100003;
                            entity.ItemCount = 343;
                            entity.PriceAmountPerItem = 342556.02M;
                            entity.PriceCurrencyID = 83;
                            entity.Comments = "Comments 453f32a09a8d4f0b848536df7936f69c";
                            entity.PrintingHouseID = 100002;
                            entity.IsDeleted = false;              
                            entity.CreatedDate = DateTime.Parse("10/30/2023 8:14:33 AM");
                            entity.CreatedByID = 100003;
                            entity.ModifiedDate = DateTime.Parse("10/30/2023 8:14:33 AM");
                            entity.ModifiedByID = 100002;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100010, entity.OrderID);
                            Assert.AreEqual(100025, entity.ImageID);
                            Assert.AreEqual(820, entity.Width);
                            Assert.AreEqual(820, entity.Height);
                            Assert.AreEqual(100006, entity.SizeID);
                            Assert.AreEqual(100004, entity.FrameTypeID);
                            Assert.AreEqual(100012, entity.FrameSizeID);
                            Assert.AreEqual(100005, entity.MatID);
                            Assert.AreEqual(100001, entity.MaterialTypeID);
                            Assert.AreEqual(100003, entity.MountingTypeID);
                            Assert.AreEqual(343, entity.ItemCount);
                            Assert.AreEqual(342556.02M, entity.PriceAmountPerItem);
                            Assert.AreEqual(83, entity.PriceCurrencyID);
                            Assert.AreEqual("Comments 453f32a09a8d4f0b848536df7936f69c", entity.Comments);
                            Assert.AreEqual(100002, entity.PrintingHouseID);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("10/30/2023 8:14:33 AM"), entity.CreatedDate);
                            Assert.AreEqual(100003, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("10/30/2023 8:14:33 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100002, entity.ModifiedByID);
              
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
                            entity.ImageID = 100008;
                            entity.Width = 999;
                            entity.Height = 999;
                            entity.SizeID = 100010;
                            entity.FrameTypeID = 100012;
                            entity.FrameSizeID = 100010;
                            entity.MatID = 100003;
                            entity.MaterialTypeID = 100005;
                            entity.MountingTypeID = 100002;
                            entity.ItemCount = 522;
                            entity.PriceAmountPerItem = 521958.53M;
                            entity.PriceCurrencyID = 226;
                            entity.Comments = "Comments f8b26fca9f1a4c858ad287f1f378efac";
                            entity.PrintingHouseID = 100004;
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("5/3/2019 7:48:33 PM");
                            entity.CreatedByID = 100008;
                            entity.ModifiedDate = DateTime.Parse("5/3/2019 7:48:33 PM");
                            entity.ModifiedByID = 100005;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100004, entity.OrderID);
                            Assert.AreEqual(100008, entity.ImageID);
                            Assert.AreEqual(999, entity.Width);
                            Assert.AreEqual(999, entity.Height);
                            Assert.AreEqual(100010, entity.SizeID);
                            Assert.AreEqual(100012, entity.FrameTypeID);
                            Assert.AreEqual(100010, entity.FrameSizeID);
                            Assert.AreEqual(100003, entity.MatID);
                            Assert.AreEqual(100005, entity.MaterialTypeID);
                            Assert.AreEqual(100002, entity.MountingTypeID);
                            Assert.AreEqual(522, entity.ItemCount);
                            Assert.AreEqual(521958.53M, entity.PriceAmountPerItem);
                            Assert.AreEqual(226, entity.PriceCurrencyID);
                            Assert.AreEqual("Comments f8b26fca9f1a4c858ad287f1f378efac", entity.Comments);
                            Assert.AreEqual(100004, entity.PrintingHouseID);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("5/3/2019 7:48:33 PM"), entity.CreatedDate);
                            Assert.AreEqual(100008, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("5/3/2019 7:48:33 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100005, entity.ModifiedByID);
              
        }

        [Test]
        public void OrderItem_Update_InvalidId()
        {
            var dal = PrepareOrderItemDal("DALInitParams");

            var entity = new OrderItem();
                          entity.OrderID = 100004;
                            entity.ImageID = 100008;
                            entity.Width = 999;
                            entity.Height = 999;
                            entity.SizeID = 100010;
                            entity.FrameTypeID = 100012;
                            entity.FrameSizeID = 100010;
                            entity.MatID = 100003;
                            entity.MaterialTypeID = 100005;
                            entity.MountingTypeID = 100002;
                            entity.ItemCount = 522;
                            entity.PriceAmountPerItem = 521958.53M;
                            entity.PriceCurrencyID = 226;
                            entity.Comments = "Comments f8b26fca9f1a4c858ad287f1f378efac";
                            entity.PrintingHouseID = 100004;
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("5/3/2019 7:48:33 PM");
                            entity.CreatedByID = 100008;
                            entity.ModifiedDate = DateTime.Parse("5/3/2019 7:48:33 PM");
                            entity.ModifiedByID = 100005;
              
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
