

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
    public class TestDeliveryServicesController : E2ETestBase, IClassFixture<WebApplicationFactory<PPT.PhotoPrint.API.Startup>>
    {
        public TestDeliveryServicesController(WebApplicationFactory<PPT.PhotoPrint.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void DeliveryService_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var respGetAll = client.GetAsync($"/api/v1/deliveryservices");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<DeliveryService> dtos = ExtractContentJson<List<DeliveryService>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void DeliveryService_Get_Success()
        {
            PPT.Interfaces.Entities.DeliveryService testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                    var paramID = testEntity.ID;
                    var respGet = client.GetAsync($"/api/v1/deliveryservices/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respGet.Result.StatusCode);

                    DeliveryService dto = ExtractContentJson<DeliveryService>(respGet.Result.Content);

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
        public void DeliveryService_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/deliveryservices/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void DeliveryService_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                    var paramID = testEntity.ID;

                    var respDel = client.DeleteAsync($"/api/v1/deliveryservices/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void DeliveryService_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/deliveryservices/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void DeliveryService_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.DeliveryService testEntity = CreateTestEntity();
                PPT.Interfaces.Entities.DeliveryService respEntity = null;
                try
                {
                    var reqDto = DeliveryServiceConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/deliveryservices/", content);

                    Assert.Equal(HttpStatusCode.Created, respInsert.Result.StatusCode);

                    DeliveryService respDto = ExtractContentJson<DeliveryService>(respInsert.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.DeliveryServiceName, respDto.DeliveryServiceName);
                    Assert.Equal(reqDto.Description, respDto.Description);
                    Assert.Equal(reqDto.IsDeleted, respDto.IsDeleted);
                    Assert.Equal(reqDto.CreatedDate, respDto.CreatedDate);
                    Assert.Equal(reqDto.CreatedByID, respDto.CreatedByID);
                    Assert.Equal(reqDto.ModifiedDate, respDto.ModifiedDate);
                    Assert.Equal(reqDto.ModifiedByID, respDto.ModifiedByID);

                    respEntity = DeliveryServiceConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void DeliveryService_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.DeliveryService testEntity = AddTestEntity();
                try
                {
                    testEntity.DeliveryServiceName = "DeliveryServiceName 174156a6eeab4243a5d97763e36e29";
                    testEntity.Description = "Description 174156a6eeab4243a5d97763e36e29ff";
                    testEntity.IsDeleted = true;

                    var reqDto = DeliveryServiceConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/deliveryservices/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    DeliveryService respDto = ExtractContentJson<DeliveryService>(respUpdate.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.DeliveryServiceName, respDto.DeliveryServiceName);
                    Assert.Equal(reqDto.Description, respDto.Description);
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
        public void DeliveryService_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.DeliveryService testEntity = CreateTestEntity();
                try
                {
                    testEntity.ID = Int64.MaxValue;
                    testEntity.DeliveryServiceName = "DeliveryServiceName 174156a6eeab4243a5d97763e36e29";
                    testEntity.Description = "Description 174156a6eeab4243a5d97763e36e29ff";
                    testEntity.IsDeleted = true;
                    testEntity.CreatedDate = DateTime.Parse("3/10/2024 8:15:52 AM");
                    testEntity.CreatedByID = 100064;
                    testEntity.ModifiedDate = DateTime.Parse("7/29/2021 6:02:52 PM");
                    testEntity.ModifiedByID = 100004;

                    var reqDto = DeliveryServiceConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/deliveryservices/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(PPT.Interfaces.Entities.DeliveryService entity)
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

        protected PPT.Interfaces.Entities.DeliveryService CreateTestEntity()
        {
            var entity = new PPT.Interfaces.Entities.DeliveryService();
            entity.DeliveryServiceName = "DeliveryServiceName 6fb37774fb034571b8c8a4fcddfaf9";
            entity.Description = "Description 6fb37774fb034571b8c8a4fcddfaf90e";
            entity.IsDeleted = true;
            entity.CreatedDate = DateTime.Parse("12/11/2023 10:01:52 PM");
            entity.CreatedByID = 100007;
            entity.ModifiedDate = DateTime.Parse("5/1/2021 7:48:52 AM");
            entity.ModifiedByID = 100007;

            return entity;
        }

        protected PPT.Interfaces.Entities.DeliveryService AddTestEntity()
        {
            PPT.Interfaces.Entities.DeliveryService result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private PPT.Interfaces.IDeliveryServiceDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            PPT.Interfaces.IDeliveryServiceDal dal = new PPT.DAL.MSSQL.DeliveryServiceDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
