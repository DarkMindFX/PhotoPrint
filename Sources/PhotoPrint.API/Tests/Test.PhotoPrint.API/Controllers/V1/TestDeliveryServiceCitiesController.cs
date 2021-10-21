

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
    public class TestDeliveryServiceCitiesController : E2ETestBase, IClassFixture<WebApplicationFactory<PPT.PhotoPrint.API.Startup>>
    {
        public TestDeliveryServiceCitiesController(WebApplicationFactory<PPT.PhotoPrint.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void DeliveryServiceCity_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var respGetAll = client.GetAsync($"/api/v1/deliveryservicecities");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<DeliveryServiceCity> dtos = ExtractContentJson<List<DeliveryServiceCity>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void DeliveryServiceCity_Get_Success()
        {
            PPT.Interfaces.Entities.DeliveryServiceCity testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                var paramDeliveryServiceID = testEntity.DeliveryServiceID;
                var paramCityID = testEntity.CityID;
                    var respGet = client.GetAsync($"/api/v1/deliveryservicecities/{paramDeliveryServiceID}/{paramCityID}");

                    Assert.Equal(HttpStatusCode.OK, respGet.Result.StatusCode);

                    DeliveryServiceCity dto = ExtractContentJson<DeliveryServiceCity>(respGet.Result.Content);

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
        public void DeliveryServiceCity_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramDeliveryServiceID = Int64.MaxValue;
                var paramCityID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/deliveryservicecities/{paramDeliveryServiceID}/{paramCityID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void DeliveryServiceCity_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                var paramDeliveryServiceID = testEntity.DeliveryServiceID;
                var paramCityID = testEntity.CityID;

                    var respDel = client.DeleteAsync($"/api/v1/deliveryservicecities/{paramDeliveryServiceID}/{paramCityID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void DeliveryServiceCity_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramDeliveryServiceID = Int64.MaxValue;
                var paramCityID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/deliveryservicecities/{paramDeliveryServiceID}/{paramCityID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void DeliveryServiceCity_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.DeliveryServiceCity testEntity = CreateTestEntity();
                PPT.Interfaces.Entities.DeliveryServiceCity respEntity = null;
                try
                {
                    var reqDto = DeliveryServiceCityConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/deliveryservicecities/", content);

                    Assert.Equal(HttpStatusCode.Created, respInsert.Result.StatusCode);

                    DeliveryServiceCity respDto = ExtractContentJson<DeliveryServiceCity>(respInsert.Result.Content);

                                    Assert.NotNull(respDto.DeliveryServiceID);
                                    Assert.NotNull(respDto.CityID);
                
                    respEntity = DeliveryServiceCityConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void DeliveryServiceCity_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.DeliveryServiceCity testEntity = AddTestEntity();
                try
                {
            
                    var reqDto = DeliveryServiceCityConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/deliveryservicecities/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    DeliveryServiceCity respDto = ExtractContentJson<DeliveryServiceCity>(respUpdate.Result.Content);

                                     Assert.NotNull(respDto.DeliveryServiceID);
                                    Assert.NotNull(respDto.CityID);
                
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void DeliveryServiceCity_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.DeliveryServiceCity testEntity = CreateTestEntity();
                try
                {
                            testEntity.DeliveryServiceID = 100020;
                            testEntity.CityID = 7;
              
                    var reqDto = DeliveryServiceCityConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/deliveryservicecities/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(PPT.Interfaces.Entities.DeliveryServiceCity entity)
        {
            if (entity != null)
            {
                var dal = CreateDal();

                return dal.Delete(
                                        entity.DeliveryServiceID,
                                        entity.CityID
                );
            }
            else
            {
                return false;
            }
        }

        protected PPT.Interfaces.Entities.DeliveryServiceCity CreateTestEntity()
        {
            var entity = new PPT.Interfaces.Entities.DeliveryServiceCity();
                          entity.DeliveryServiceID = 100008;
                            entity.CityID = 6;
              
            return entity;
        }

        protected PPT.Interfaces.Entities.DeliveryServiceCity AddTestEntity()
        {
            PPT.Interfaces.Entities.DeliveryServiceCity result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private PPT.Interfaces.IDeliveryServiceCityDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            PPT.Interfaces.IDeliveryServiceCityDal dal = new PPT.DAL.MSSQL.DeliveryServiceCityDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
