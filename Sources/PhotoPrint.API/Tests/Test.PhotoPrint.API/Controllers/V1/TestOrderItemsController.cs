

using PPT.DTO;
using PPT.Utils.Convertors;
using PhotoPrint.Test.E2E.API;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net;
using Xunit;

namespace Test.E2E.PhotoPrint.API.Controllers.V1
{
    public class TestOrderItemsController : E2ETestBase, IClassFixture<WebApplicationFactory<PPT.PhotoPrint.API.Startup>>
    {
        public TestOrderItemsController(WebApplicationFactory<PPT.PhotoPrint.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void OrderItem_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var respGetAll = client.GetAsync($"/api/v1/orderitems");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<OrderItem> dtos = ExtractContentJson<List<OrderItem>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void OrderItem_Get_Success()
        {
            PPT.Interfaces.Entities.OrderItem testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                    var paramID = testEntity.ID;
                    var respGet = client.GetAsync($"/api/v1/orderitems/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respGet.Result.StatusCode);

                    OrderItem dto = ExtractContentJson<OrderItem>(respGet.Result.Content);

                    Assert.NotNull(dto);
                    Assert.NotNull(dto.Links);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void OrderItem_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/orderitems/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void OrderItem_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                    var paramID = testEntity.ID;

                    var respDel = client.DeleteAsync($"/api/v1/orderitems/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void OrderItem_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/orderitems/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void OrderItem_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.OrderItem testEntity = CreateTestEntity();
                PPT.Interfaces.Entities.OrderItem respEntity = null;
                try
                {
                    var reqDto = OrderItemConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/orderitems/", content);

                    Assert.Equal(HttpStatusCode.Created, respInsert.Result.StatusCode);

                    OrderItem respDto = ExtractContentJson<OrderItem>(respInsert.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.OrderID, respDto.OrderID);
                    Assert.Equal(reqDto.ImageID, respDto.ImageID);
                    Assert.Equal(reqDto.Width, respDto.Width);
                    Assert.Equal(reqDto.Height, respDto.Height);
                    Assert.Equal(reqDto.SizeID, respDto.SizeID);
                    Assert.Equal(reqDto.FrameTypeID, respDto.FrameTypeID);
                    Assert.Equal(reqDto.FrameSizeID, respDto.FrameSizeID);
                    Assert.Equal(reqDto.MatID, respDto.MatID);
                    Assert.Equal(reqDto.MaterialTypeID, respDto.MaterialTypeID);
                    Assert.Equal(reqDto.MountingTypeID, respDto.MountingTypeID);
                    Assert.Equal(reqDto.ItemCount, respDto.ItemCount);
                    Assert.Equal(reqDto.PriceAmountPerItem, respDto.PriceAmountPerItem);
                    Assert.Equal(reqDto.PriceCurrencyID, respDto.PriceCurrencyID);
                    Assert.Equal(reqDto.Comments, respDto.Comments);
                    Assert.Equal(reqDto.PrintingHouseID, respDto.PrintingHouseID);
                    Assert.Equal(reqDto.IsDeleted, respDto.IsDeleted);
                    Assert.Equal(reqDto.CreatedDate, respDto.CreatedDate);
                    Assert.Equal(reqDto.CreatedByID, respDto.CreatedByID);
                    Assert.Equal(reqDto.ModifiedDate, respDto.ModifiedDate);
                    Assert.Equal(reqDto.ModifiedByID, respDto.ModifiedByID);

                    respEntity = OrderItemConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void OrderItem_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.OrderItem testEntity = AddTestEntity();
                try
                {
                    testEntity.OrderID = 100012;
                    testEntity.ImageID = 100001;
                    testEntity.Width = 344;
                    testEntity.Height = 344;
                    testEntity.SizeID = 100009;
                    testEntity.FrameTypeID = 100009;
                    testEntity.FrameSizeID = 100002;
                    testEntity.MatID = 100003;
                    testEntity.MaterialTypeID = 100004;
                    testEntity.MountingTypeID = 100008;
                    testEntity.ItemCount = 911;
                    testEntity.PriceAmountPerItem = 665M;
                    testEntity.PriceCurrencyID = 108;
                    testEntity.Comments = "Comments 444dbc344c2142e188a9c5298aeab0c9";
                    testEntity.PrintingHouseID = 100002;
                    testEntity.IsDeleted = true;


                    var reqDto = OrderItemConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/orderitems/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    OrderItem respDto = ExtractContentJson<OrderItem>(respUpdate.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.OrderID, respDto.OrderID);
                    Assert.Equal(reqDto.ImageID, respDto.ImageID);
                    Assert.Equal(reqDto.Width, respDto.Width);
                    Assert.Equal(reqDto.Height, respDto.Height);
                    Assert.Equal(reqDto.SizeID, respDto.SizeID);
                    Assert.Equal(reqDto.FrameTypeID, respDto.FrameTypeID);
                    Assert.Equal(reqDto.FrameSizeID, respDto.FrameSizeID);
                    Assert.Equal(reqDto.MatID, respDto.MatID);
                    Assert.Equal(reqDto.MaterialTypeID, respDto.MaterialTypeID);
                    Assert.Equal(reqDto.MountingTypeID, respDto.MountingTypeID);
                    Assert.Equal(reqDto.ItemCount, respDto.ItemCount);
                    Assert.Equal(reqDto.PriceAmountPerItem, respDto.PriceAmountPerItem);
                    Assert.Equal(reqDto.PriceCurrencyID, respDto.PriceCurrencyID);
                    Assert.Equal(reqDto.Comments, respDto.Comments);
                    Assert.Equal(reqDto.PrintingHouseID, respDto.PrintingHouseID);
                    Assert.Equal(reqDto.IsDeleted, respDto.IsDeleted);
                    Assert.Equal(reqDto.CreatedDate, respDto.CreatedDate);
                    Assert.Equal(reqDto.CreatedByID, respDto.CreatedByID);
                    Assert.True(DateTime.UtcNow - TimeSpan.FromMinutes(1) < respDto.ModifiedDate);
                    Assert.Equal(respLogin.User.ID, respDto.ModifiedByID);

                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void OrderItem_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.OrderItem testEntity = CreateTestEntity();
                try
                {
                    testEntity.ID = Int64.MaxValue;
                    testEntity.OrderID = 100012;
                    testEntity.ImageID = 100001;
                    testEntity.Width = 344;
                    testEntity.Height = 344;
                    testEntity.SizeID = 100009;
                    testEntity.FrameTypeID = 100009;
                    testEntity.FrameSizeID = 100022;
                    testEntity.MatID = 100021;
                    testEntity.MaterialTypeID = 100014;
                    testEntity.MountingTypeID = 100005;
                    testEntity.ItemCount = 911;
                    testEntity.PriceAmountPerItem = 46538M;
                    testEntity.PriceCurrencyID = 178;
                    testEntity.Comments = "Comments 444dbc344c2142e188a9c5298aeab0c9";
                    testEntity.PrintingHouseID = 100002;
                    testEntity.IsDeleted = true;
                    testEntity.CreatedDate = DateTime.Parse("4/20/2024 10:28:52 AM");
                    testEntity.CreatedByID = 100005;
                    testEntity.ModifiedDate = DateTime.Parse("4/20/2024 10:28:52 AM");
                    testEntity.ModifiedByID = 100004;

                    var reqDto = OrderItemConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/orderitems/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(PPT.Interfaces.Entities.OrderItem entity)
        {
            if (entity != null)
            {
                var dal = CreateDal();

                return dal.Delete(
                                        entity.ID
                );
            }
            else
            {
                return false;
            }
        }

        protected PPT.Interfaces.Entities.OrderItem CreateTestEntity()
        {
            var entity = new PPT.Interfaces.Entities.OrderItem();
            entity.OrderID = 100006;
            entity.ImageID = 100017;
            entity.Width = 597;
            entity.Height = 597;
            entity.SizeID = 100010;
            entity.FrameTypeID = 100008;
            entity.FrameSizeID = 100005;
            entity.MatID = 100001;
            entity.MaterialTypeID = 100003;
            entity.MountingTypeID = 100003;
            entity.ItemCount = 165;
            entity.PriceAmountPerItem = 435345M;
            entity.PriceCurrencyID = 170;
            entity.Comments = "Comments a916b91829514ab48357a1c0408f6500";
            entity.PrintingHouseID = 100003;
            entity.IsDeleted = true;
            entity.CreatedDate = DateTime.Parse("10/31/2022 2:26:52 AM");
            entity.CreatedByID = 100002;
            entity.ModifiedDate = DateTime.Parse("3/20/2020 12:13:52 PM");
            entity.ModifiedByID = 100010;

            return entity;
        }

        protected PPT.Interfaces.Entities.OrderItem AddTestEntity()
        {
            PPT.Interfaces.Entities.OrderItem result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private PPT.Interfaces.IOrderItemDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            PPT.Interfaces.IOrderItemDal dal = new PPT.DAL.MSSQL.OrderItemDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
