

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
    public class TestOrderTrackingsController : E2ETestBase, IClassFixture<WebApplicationFactory<PPT.PhotoPrint.API.Startup>>
    {
        public TestOrderTrackingsController(WebApplicationFactory<PPT.PhotoPrint.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void OrderTracking_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var respGetAll = client.GetAsync($"/api/v1/ordertrackings");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<OrderTracking> dtos = ExtractContentJson<List<OrderTracking>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void OrderTracking_Get_Success()
        {
            PPT.Interfaces.Entities.OrderTracking testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                    var paramID = testEntity.ID;
                    var respGet = client.GetAsync($"/api/v1/ordertrackings/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respGet.Result.StatusCode);

                    OrderTracking dto = ExtractContentJson<OrderTracking>(respGet.Result.Content);

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
        public void OrderTracking_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/ordertrackings/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void OrderTracking_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                    var paramID = testEntity.ID;

                    var respDel = client.DeleteAsync($"/api/v1/ordertrackings/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void OrderTracking_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/ordertrackings/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void OrderTracking_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.OrderTracking testEntity = CreateTestEntity();
                PPT.Interfaces.Entities.OrderTracking respEntity = null;
                try
                {
                    var reqDto = OrderTrackingConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/ordertrackings/", content);

                    Assert.Equal(HttpStatusCode.Created, respInsert.Result.StatusCode);

                    OrderTracking respDto = ExtractContentJson<OrderTracking>(respInsert.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.OrderID, respDto.OrderID);
                    Assert.Equal(reqDto.OrderStatusID, respDto.OrderStatusID);
                    Assert.Equal(reqDto.SetDate, respDto.SetDate);
                    Assert.Equal(reqDto.SetByID, respDto.SetByID);
                    Assert.Equal(reqDto.Comment, respDto.Comment);

                    respEntity = OrderTrackingConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void OrderTracking_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.OrderTracking testEntity = AddTestEntity();
                try
                {
                    testEntity.OrderID = 100003;
                    testEntity.OrderStatusID = 1;
                    testEntity.SetDate = DateTime.Parse("11/26/2021 2:52:53 AM");
                    testEntity.SetByID = 100004;
                    testEntity.Comment = "Comment f2627bb0034a408dbc1fe19d7fdbe9b1";

                    var reqDto = OrderTrackingConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/ordertrackings/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    OrderTracking respDto = ExtractContentJson<OrderTracking>(respUpdate.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.OrderID, respDto.OrderID);
                    Assert.Equal(reqDto.OrderStatusID, respDto.OrderStatusID);
                    Assert.Equal(reqDto.SetDate, respDto.SetDate);
                    Assert.Equal(reqDto.SetByID, respDto.SetByID);
                    Assert.Equal(reqDto.Comment, respDto.Comment);

                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void OrderTracking_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.OrderTracking testEntity = CreateTestEntity();
                try
                {
                    testEntity.ID = Int64.MaxValue;
                    testEntity.OrderID = 100003;
                    testEntity.OrderStatusID = 1;
                    testEntity.SetDate = DateTime.Parse("11/26/2021 2:52:53 AM");
                    testEntity.SetByID = 100011;
                    testEntity.Comment = "Comment f2627bb0034a408dbc1fe19d7fdbe9b1";

                    var reqDto = OrderTrackingConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/ordertrackings/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(PPT.Interfaces.Entities.OrderTracking entity)
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

        protected PPT.Interfaces.Entities.OrderTracking CreateTestEntity()
        {
            var entity = new PPT.Interfaces.Entities.OrderTracking();
            entity.OrderID = 100005;
            entity.OrderStatusID = 4;
            entity.SetDate = DateTime.Parse("3/2/2021 10:51:53 AM");
            entity.SetByID = 100010;
            entity.Comment = "Comment aa44e1542918490492fe692514dc5cca";

            return entity;
        }

        protected PPT.Interfaces.Entities.OrderTracking AddTestEntity()
        {
            PPT.Interfaces.Entities.OrderTracking result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private PPT.Interfaces.IOrderTrackingDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            PPT.Interfaces.IOrderTrackingDal dal = new PPT.DAL.MSSQL.OrderTrackingDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
