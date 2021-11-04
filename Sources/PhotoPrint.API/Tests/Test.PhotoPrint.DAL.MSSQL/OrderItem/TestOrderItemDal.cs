


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
            
                          Assert.AreEqual(100009, entity.OrderID);
                            Assert.AreEqual(100045, entity.ImageID);
                            Assert.AreEqual(81, entity.Width);
                            Assert.AreEqual(81, entity.Height);
                            Assert.AreEqual(100002, entity.SizeID);
                            Assert.AreEqual(100011, entity.FrameTypeID);
                            Assert.AreEqual(100011, entity.FrameSizeID);
                            Assert.AreEqual(100007, entity.MatID);
                            Assert.AreEqual(100002, entity.MaterialTypeID);
                            Assert.AreEqual(100006, entity.MountingTypeID);
                            Assert.AreEqual(305, entity.ItemCount);
                            Assert.AreEqual(305193.54M, entity.PriceAmountPerItem);
                            Assert.AreEqual(172, entity.PriceCurrencyID);
                            Assert.AreEqual("Comments 1d79f6d40f324fc58e12f4201ffd92d5", entity.Comments);
                            Assert.AreEqual(100003, entity.PrintingHouseID);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("8/21/2023 11:04:39 PM"), entity.CreatedDate);
                            Assert.AreEqual(100003, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("1/9/2021 8:51:39 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100001, entity.ModifiedByID);
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
                          entity.OrderID = 100005;
                            entity.ImageID = 100032;
                            entity.Width = 97;
                            entity.Height = 97;
                            entity.SizeID = 100012;
                            entity.FrameTypeID = 100016;
                            entity.FrameSizeID = 100004;
                            entity.MatID = 100003;
                            entity.MaterialTypeID = 100004;
                            entity.MountingTypeID = 100008;
                            entity.ItemCount = 619;
                            entity.PriceAmountPerItem = 619147.94M;
                            entity.PriceCurrencyID = 75;
                            entity.Comments = "Comments 3a6c13a8f74349579a653767e3a0cb82";
                            entity.PrintingHouseID = 100002;
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("11/19/2019 2:40:39 AM");
                            entity.CreatedByID = 100007;
                            entity.ModifiedDate = DateTime.Parse("11/19/2019 2:40:39 AM");
                            entity.ModifiedByID = 100006;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100005, entity.OrderID);
                            Assert.AreEqual(100032, entity.ImageID);
                            Assert.AreEqual(97, entity.Width);
                            Assert.AreEqual(97, entity.Height);
                            Assert.AreEqual(100012, entity.SizeID);
                            Assert.AreEqual(100016, entity.FrameTypeID);
                            Assert.AreEqual(100004, entity.FrameSizeID);
                            Assert.AreEqual(100003, entity.MatID);
                            Assert.AreEqual(100004, entity.MaterialTypeID);
                            Assert.AreEqual(100008, entity.MountingTypeID);
                            Assert.AreEqual(619, entity.ItemCount);
                            Assert.AreEqual(619147.94M, entity.PriceAmountPerItem);
                            Assert.AreEqual(75, entity.PriceCurrencyID);
                            Assert.AreEqual("Comments 3a6c13a8f74349579a653767e3a0cb82", entity.Comments);
                            Assert.AreEqual(100002, entity.PrintingHouseID);
                            Assert.AreEqual(true, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("11/19/2019 2:40:39 AM"), entity.CreatedDate);
                            Assert.AreEqual(100007, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("11/19/2019 2:40:39 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100006, entity.ModifiedByID);
              
        }

        [TestCase("OrderItem\\030.Update.Success")]
        public void OrderItem_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareOrderItemDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            OrderItem entity = dal.Get(paramID);

                          entity.OrderID = 100012;
                            entity.ImageID = 100038;
                            entity.Width = 232;
                            entity.Height = 232;
                            entity.SizeID = 100011;
                            entity.FrameTypeID = 100008;
                            entity.FrameSizeID = 100011;
                            entity.MatID = 100004;
                            entity.MaterialTypeID = 100002;
                            entity.MountingTypeID = 100006;
                            entity.ItemCount = 276;
                            entity.PriceAmountPerItem = 276125.14M;
                            entity.PriceCurrencyID = 63;
                            entity.Comments = "Comments 0f838a8a0585484f9939c89a58fbdc15";
                            entity.PrintingHouseID = 100005;
                            entity.IsDeleted = false;              
                            entity.CreatedDate = DateTime.Parse("6/25/2023 4:28:39 AM");
                            entity.CreatedByID = 100002;
                            entity.ModifiedDate = DateTime.Parse("11/11/2020 2:15:39 PM");
                            entity.ModifiedByID = 100003;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100012, entity.OrderID);
                            Assert.AreEqual(100038, entity.ImageID);
                            Assert.AreEqual(232, entity.Width);
                            Assert.AreEqual(232, entity.Height);
                            Assert.AreEqual(100011, entity.SizeID);
                            Assert.AreEqual(100008, entity.FrameTypeID);
                            Assert.AreEqual(100011, entity.FrameSizeID);
                            Assert.AreEqual(100004, entity.MatID);
                            Assert.AreEqual(100002, entity.MaterialTypeID);
                            Assert.AreEqual(100006, entity.MountingTypeID);
                            Assert.AreEqual(276, entity.ItemCount);
                            Assert.AreEqual(276125.14M, entity.PriceAmountPerItem);
                            Assert.AreEqual(63, entity.PriceCurrencyID);
                            Assert.AreEqual("Comments 0f838a8a0585484f9939c89a58fbdc15", entity.Comments);
                            Assert.AreEqual(100005, entity.PrintingHouseID);
                            Assert.AreEqual(false, entity.IsDeleted);
                            Assert.AreEqual(DateTime.Parse("6/25/2023 4:28:39 AM"), entity.CreatedDate);
                            Assert.AreEqual(100002, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("11/11/2020 2:15:39 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100003, entity.ModifiedByID);
              
        }

        [Test]
        public void OrderItem_Update_InvalidId()
        {
            var dal = PrepareOrderItemDal("DALInitParams");

            var entity = new OrderItem();
                          entity.OrderID = 100012;
                            entity.ImageID = 100038;
                            entity.Width = 232;
                            entity.Height = 232;
                            entity.SizeID = 100011;
                            entity.FrameTypeID = 100008;
                            entity.FrameSizeID = 100011;
                            entity.MatID = 100004;
                            entity.MaterialTypeID = 100002;
                            entity.MountingTypeID = 100006;
                            entity.ItemCount = 276;
                            entity.PriceAmountPerItem = 276125.14M;
                            entity.PriceCurrencyID = 63;
                            entity.Comments = "Comments 0f838a8a0585484f9939c89a58fbdc15";
                            entity.PrintingHouseID = 100005;
                            entity.IsDeleted = false;              
                            entity.CreatedDate = DateTime.Parse("6/25/2023 4:28:39 AM");
                            entity.CreatedByID = 100002;
                            entity.ModifiedDate = DateTime.Parse("11/11/2020 2:15:39 PM");
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

        [TestCase("OrderItem\\040.Erase.Success")]
        public void OrderItem_Erase_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareOrderItemDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Erase(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void OrderItem_Erase_InvalidId()
        {
            var dal = PrepareOrderItemDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Erase(paramID);
            Assert.IsFalse(removed);

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
