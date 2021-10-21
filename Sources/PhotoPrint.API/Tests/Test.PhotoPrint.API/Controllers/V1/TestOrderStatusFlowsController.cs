

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
    public class TestOrderStatusFlowsController : E2ETestBase, IClassFixture<WebApplicationFactory<PPT.PhotoPrint.API.Startup>>
    {
        public TestOrderStatusFlowsController(WebApplicationFactory<PPT.PhotoPrint.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void OrderStatusFlow_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var respGetAll = client.GetAsync($"/api/v1/orderstatusflows");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<OrderStatusFlow> dtos = ExtractContentJson<List<OrderStatusFlow>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void OrderStatusFlow_Get_Success()
        {
            PPT.Interfaces.Entities.OrderStatusFlow testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                var paramFromStatusID = testEntity.FromStatusID;
                var paramToStatusID = testEntity.ToStatusID;
                    var respGet = client.GetAsync($"/api/v1/orderstatusflows/{paramFromStatusID}/{paramToStatusID}");

                    Assert.Equal(HttpStatusCode.OK, respGet.Result.StatusCode);

                    OrderStatusFlow dto = ExtractContentJson<OrderStatusFlow>(respGet.Result.Content);

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
        public void OrderStatusFlow_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramFromStatusID = Int64.MaxValue;
                var paramToStatusID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/orderstatusflows/{paramFromStatusID}/{paramToStatusID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void OrderStatusFlow_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                var paramFromStatusID = testEntity.FromStatusID;
                var paramToStatusID = testEntity.ToStatusID;

                    var respDel = client.DeleteAsync($"/api/v1/orderstatusflows/{paramFromStatusID}/{paramToStatusID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void OrderStatusFlow_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramFromStatusID = Int64.MaxValue;
                var paramToStatusID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/orderstatusflows/{paramFromStatusID}/{paramToStatusID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void OrderStatusFlow_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.OrderStatusFlow testEntity = CreateTestEntity();
                PPT.Interfaces.Entities.OrderStatusFlow respEntity = null;
                try
                {
                    var reqDto = OrderStatusFlowConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/orderstatusflows/", content);

                    Assert.Equal(HttpStatusCode.Created, respInsert.Result.StatusCode);

                    OrderStatusFlow respDto = ExtractContentJson<OrderStatusFlow>(respInsert.Result.Content);

                                    Assert.NotNull(respDto.FromStatusID);
                                    Assert.NotNull(respDto.ToStatusID);
                
                    respEntity = OrderStatusFlowConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void OrderStatusFlow_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.OrderStatusFlow testEntity = AddTestEntity();
                try
                {
            
                    var reqDto = OrderStatusFlowConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/orderstatusflows/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    OrderStatusFlow respDto = ExtractContentJson<OrderStatusFlow>(respUpdate.Result.Content);

                                     Assert.NotNull(respDto.FromStatusID);
                                    Assert.NotNull(respDto.ToStatusID);
                
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void OrderStatusFlow_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.OrderStatusFlow testEntity = CreateTestEntity();
                try
                {
                            testEntity.FromStatusID = 5;
                            testEntity.ToStatusID = 1;
              
                    var reqDto = OrderStatusFlowConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/orderstatusflows/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(PPT.Interfaces.Entities.OrderStatusFlow entity)
        {
            if (entity != null)
            {
                var dal = CreateDal();

                return dal.Delete(
                                        entity.FromStatusID,
                                        entity.ToStatusID
                );
            }
            else
            {
                return false;
            }
        }

        protected PPT.Interfaces.Entities.OrderStatusFlow CreateTestEntity()
        {
            var entity = new PPT.Interfaces.Entities.OrderStatusFlow();
                          entity.FromStatusID = 6;
                            entity.ToStatusID = 3;
              
            return entity;
        }

        protected PPT.Interfaces.Entities.OrderStatusFlow AddTestEntity()
        {
            PPT.Interfaces.Entities.OrderStatusFlow result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private PPT.Interfaces.IOrderStatusFlowDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            PPT.Interfaces.IOrderStatusFlowDal dal = new PPT.DAL.MSSQL.OrderStatusFlowDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
