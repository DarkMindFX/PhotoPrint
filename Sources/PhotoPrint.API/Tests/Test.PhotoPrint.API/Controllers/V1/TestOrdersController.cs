


using PPT.DTO;
using PPT.Utils.Convertors;
using PPT.Test.E2E.API;
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
                          testEntity.ManagerID = 100001 ;
                            testEntity.UserID = 100007 ;
                            testEntity.ContactID = 100012 ;
                            testEntity.DeliveryAddressID = 100009 ;
                            testEntity.DeliveryServiceID = 100008 ;
                            testEntity.Comments = "Comments 2843fe671d2b4544b345ea5dc5f37592";
                            testEntity.IsDeleted = true;              
                            testEntity.CreatedDate = DateTime.Parse("11/27/2021 10:59:38 PM");
                            testEntity.CreatedByID = 100009 ;
                            testEntity.ModifiedDate = DateTime.Parse("2/25/2022 9:13:38 AM");
                            testEntity.ModifiedByID = 100004 ;
              
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
                             testEntity.ManagerID = 100001;
                            testEntity.UserID = 100007;
                            testEntity.ContactID = 100012;
                            testEntity.DeliveryAddressID = 100009;
                            testEntity.DeliveryServiceID = 100008;
                            testEntity.Comments = "Comments 2843fe671d2b4544b345ea5dc5f37592";
                            testEntity.IsDeleted = true;              
                            testEntity.CreatedDate = DateTime.Parse("11/27/2021 10:59:38 PM");
                            testEntity.CreatedByID = 100009;
                            testEntity.ModifiedDate = DateTime.Parse("2/25/2022 9:13:38 AM");
                            testEntity.ModifiedByID = 100004;
              
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


        return dal.Erase(                        entity.ID
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
                          entity.ManagerID = 100008;
                            entity.UserID = 100007;
                            entity.ContactID = 100011;
                            entity.DeliveryAddressID = 100005;
                            entity.DeliveryServiceID = 100001;
                            entity.Comments = "Comments 0137314e9ba34cbfbb1878356f622253";
                            entity.IsDeleted = false;              
                            entity.CreatedDate = DateTime.Parse("11/4/2019 7:34:38 PM");
                            entity.CreatedByID = 100009;
                            entity.ModifiedDate = DateTime.Parse("2/2/2020 5:48:38 AM");
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
