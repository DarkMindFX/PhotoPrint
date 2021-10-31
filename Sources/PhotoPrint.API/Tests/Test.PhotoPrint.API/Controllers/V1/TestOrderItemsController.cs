

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
                    testEntity.OrderID = 100011;
                    testEntity.ImageID = 100018;
                    testEntity.Width = 669;
                    testEntity.Height = 669;
                    testEntity.SizeID = 100009;
                    testEntity.FrameTypeID = 100010;
                    testEntity.FrameSizeID = 100004;
                    testEntity.MatID = 100006;
                    testEntity.MaterialTypeID = 100004;
                    testEntity.MountingTypeID = 100005;
                    testEntity.ItemCount = 714;
                    testEntity.PriceAmountPerItem = 713841.3M;
                    testEntity.PriceCurrencyID = 68;
                    testEntity.Comments = "Comments 6c430effc47a4978bf8ccee771d6f5c5";
                    testEntity.PrintingHouseID = 100002;
                    testEntity.IsDeleted = false;
                    testEntity.CreatedDate = DateTime.Parse("5/21/2020 7:54:35 AM");
                    testEntity.CreatedByID = 100011;
                    testEntity.ModifiedDate = DateTime.Parse("5/21/2020 7:54:35 AM");
                    testEntity.ModifiedByID = 100004;

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
                    testEntity.OrderID = 100011;
                    testEntity.ImageID = 100018;
                    testEntity.Width = 669;
                    testEntity.Height = 669;
                    testEntity.SizeID = 100009;
                    testEntity.FrameTypeID = 100010;
                    testEntity.FrameSizeID = 100004;
                    testEntity.MatID = 100006;
                    testEntity.MaterialTypeID = 100004;
                    testEntity.MountingTypeID = 100005;
                    testEntity.ItemCount = 714;
                    testEntity.PriceAmountPerItem = 713841.3M;
                    testEntity.PriceCurrencyID = 68;
                    testEntity.Comments = "Comments 6c430effc47a4978bf8ccee771d6f5c5";
                    testEntity.PrintingHouseID = 100002;
                    testEntity.IsDeleted = false;
                    testEntity.CreatedDate = DateTime.Parse("5/21/2020 7:54:35 AM");
                    testEntity.CreatedByID = 100011;
                    testEntity.ModifiedDate = DateTime.Parse("5/21/2020 7:54:35 AM");
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
            entity.OrderID = 100008;
            entity.ImageID = 100018;
            entity.Width = 445;
            entity.Height = 445;
            entity.SizeID = 100005;
            entity.FrameTypeID = 100001;
            entity.FrameSizeID = 100004;
            entity.MatID = 100003;
            entity.MaterialTypeID = 100001;
            entity.MountingTypeID = 100003;
            entity.ItemCount = 490;
            entity.PriceAmountPerItem = 489588.16M;
            entity.PriceCurrencyID = 185;
            entity.Comments = "Comments 03eaff57684146e9ae07b7db5a2e2c1e";
            entity.PrintingHouseID = 100002;
            entity.IsDeleted = false;
            entity.CreatedDate = DateTime.Parse("2/28/2019 10:05:35 AM");
            entity.CreatedByID = 100010;
            entity.ModifiedDate = DateTime.Parse("1/7/2022 7:52:35 PM");
            entity.ModifiedByID = 100008;

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
