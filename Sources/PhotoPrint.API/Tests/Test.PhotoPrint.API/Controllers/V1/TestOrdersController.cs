

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
    public class TestOrdersController : E2ETestBase, IClassFixture<WebApplicationFactory<PPT.PhotoPrint.API.Startup>>
    {
        public TestOrdersController(WebApplicationFactory<PPT.PhotoPrint.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void Order_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var respGetAll = client.GetAsync($"/api/v1/orders");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<Order> dtos = ExtractContentJson<List<Order>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void Order_Get_Success()
        {
            PPT.Interfaces.Entities.Order testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                var paramID = testEntity.ID;
                    var respGet = client.GetAsync($"/api/v1/orders/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respGet.Result.StatusCode);

                    Order dto = ExtractContentJson<Order>(respGet.Result.Content);

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
        public void Order_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/orders/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void Order_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                var paramID = testEntity.ID;

                    var respDel = client.DeleteAsync($"/api/v1/orders/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void Order_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/orders/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void Order_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.Order testEntity = CreateTestEntity();
                PPT.Interfaces.Entities.Order respEntity = null;
                try
                {
                    var reqDto = OrderConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/orders/", content);

                    Assert.Equal(HttpStatusCode.Created, respInsert.Result.StatusCode);

                    Order respDto = ExtractContentJson<Order>(respInsert.Result.Content);

                                    Assert.NotNull(respDto.ID);
                                    Assert.Equal(reqDto.ManagerID, respDto.ManagerID);
                                    Assert.Equal(reqDto.UserID, respDto.UserID);
                                    Assert.Equal(reqDto.ContactID, respDto.ContactID);
                                    Assert.Equal(reqDto.DeliveryAddressID, respDto.DeliveryAddressID);
                                    Assert.Equal(reqDto.DeliveryServiceID, respDto.DeliveryServiceID);
                                    Assert.Equal(reqDto.Comments, respDto.Comments);
                                    Assert.Equal(reqDto.IsDeleted, respDto.IsDeleted);
                                    Assert.Equal(reqDto.CreatedDate, respDto.CreatedDate);
                                    Assert.Equal(reqDto.CreatedByID, respDto.CreatedByID);
                                    Assert.Equal(reqDto.ModifiedDate, respDto.ModifiedDate);
                                    Assert.Equal(reqDto.ModifiedByID, respDto.ModifiedByID);
                
                    respEntity = OrderConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void Order_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.Order testEntity = AddTestEntity();
                try
                {
                          testEntity.ManagerID = 100064;
                            testEntity.UserID = 100003;
                            testEntity.ContactID = 100017;
                            testEntity.DeliveryAddressID = 100010;
                            testEntity.DeliveryServiceID = 100007;
                            testEntity.Comments = "Comments 3377fb3642454252915b1630272c21be";
                            testEntity.IsDeleted = false;              
                            testEntity.CreatedDate = DateTime.Parse("2/18/2024 3:14:52 AM");
                            testEntity.CreatedByID = 100006;
                            testEntity.ModifiedDate = DateTime.Parse("5/17/2024 1:28:52 PM");
                            testEntity.ModifiedByID = 100008;
              
                    var reqDto = OrderConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/orders/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    Order respDto = ExtractContentJson<Order>(respUpdate.Result.Content);

                                     Assert.NotNull(respDto.ID);
                                    Assert.Equal(reqDto.ManagerID, respDto.ManagerID);
                                    Assert.Equal(reqDto.UserID, respDto.UserID);
                                    Assert.Equal(reqDto.ContactID, respDto.ContactID);
                                    Assert.Equal(reqDto.DeliveryAddressID, respDto.DeliveryAddressID);
                                    Assert.Equal(reqDto.DeliveryServiceID, respDto.DeliveryServiceID);
                                    Assert.Equal(reqDto.Comments, respDto.Comments);
                                    Assert.Equal(reqDto.IsDeleted, respDto.IsDeleted);
                                    Assert.Equal(reqDto.CreatedDate, respDto.CreatedDate);
                                    Assert.Equal(reqDto.CreatedByID, respDto.CreatedByID);
                                    Assert.Equal(reqDto.ModifiedDate, respDto.ModifiedDate);
                                    Assert.Equal(reqDto.ModifiedByID, respDto.ModifiedByID);
                
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void Order_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.Order testEntity = CreateTestEntity();
                try
                {
                             testEntity.ID = Int64.MaxValue;
                             testEntity.ManagerID = 100064;
                            testEntity.UserID = 100003;
                            testEntity.ContactID = 100017;
                            testEntity.DeliveryAddressID = 100010;
                            testEntity.DeliveryServiceID = 100007;
                            testEntity.Comments = "Comments 3377fb3642454252915b1630272c21be";
                            testEntity.IsDeleted = false;              
                            testEntity.CreatedDate = DateTime.Parse("2/18/2024 3:14:52 AM");
                            testEntity.CreatedByID = 100006;
                            testEntity.ModifiedDate = DateTime.Parse("5/17/2024 1:28:52 PM");
                            testEntity.ModifiedByID = 100008;
              
                    var reqDto = OrderConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/orders/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(PPT.Interfaces.Entities.Order entity)
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

        protected PPT.Interfaces.Entities.Order CreateTestEntity()
        {
            var entity = new PPT.Interfaces.Entities.Order();
                          entity.ManagerID = 100006;
                            entity.UserID = 100009;
                            entity.ContactID = 100008;
                            entity.DeliveryAddressID = 100011;
                            entity.DeliveryServiceID = 100005;
                            entity.Comments = "Comments 7647f0c7f83e41f1bde30b9270a5e902";
                            entity.IsDeleted = false;              
                            entity.CreatedDate = DateTime.Parse("11/27/2022 5:26:52 AM");
                            entity.CreatedByID = 100007;
                            entity.ModifiedDate = DateTime.Parse("11/27/2022 5:26:52 AM");
                            entity.ModifiedByID = 100011;
              
            return entity;
        }

        protected PPT.Interfaces.Entities.Order AddTestEntity()
        {
            PPT.Interfaces.Entities.Order result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private PPT.Interfaces.IOrderDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            PPT.Interfaces.IOrderDal dal = new PPT.DAL.MSSQL.OrderDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
