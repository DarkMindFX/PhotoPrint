

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
    public class TestOrderPaymentDetailsesController : E2ETestBase, IClassFixture<WebApplicationFactory<PPT.PhotoPrint.API.Startup>>
    {
        public TestOrderPaymentDetailsesController(WebApplicationFactory<PPT.PhotoPrint.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void OrderPaymentDetails_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var respGetAll = client.GetAsync($"/api/v1/orderpaymentdetailses");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<OrderPaymentDetails> dtos = ExtractContentJson<List<OrderPaymentDetails>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void OrderPaymentDetails_Get_Success()
        {
            PPT.Interfaces.Entities.OrderPaymentDetails testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                var paramID = testEntity.ID;
                    var respGet = client.GetAsync($"/api/v1/orderpaymentdetailses/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respGet.Result.StatusCode);

                    OrderPaymentDetails dto = ExtractContentJson<OrderPaymentDetails>(respGet.Result.Content);

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
        public void OrderPaymentDetails_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/orderpaymentdetailses/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void OrderPaymentDetails_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                var paramID = testEntity.ID;

                    var respDel = client.DeleteAsync($"/api/v1/orderpaymentdetailses/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void OrderPaymentDetails_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/orderpaymentdetailses/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void OrderPaymentDetails_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.OrderPaymentDetails testEntity = CreateTestEntity();
                PPT.Interfaces.Entities.OrderPaymentDetails respEntity = null;
                try
                {
                    var reqDto = OrderPaymentDetailsConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/orderpaymentdetailses/", content);

                    Assert.Equal(HttpStatusCode.Created, respInsert.Result.StatusCode);

                    OrderPaymentDetails respDto = ExtractContentJson<OrderPaymentDetails>(respInsert.Result.Content);

                                    Assert.NotNull(respDto.ID);
                                    Assert.Equal(reqDto.OrderID, respDto.OrderID);
                                    Assert.Equal(reqDto.PaymentMethodID, respDto.PaymentMethodID);
                                    Assert.Equal(reqDto.PaymentTransUID, respDto.PaymentTransUID);
                                    Assert.Equal(reqDto.PaymentDateTime, respDto.PaymentDateTime);
                                    Assert.Equal(reqDto.IsDeleted, respDto.IsDeleted);
                                    Assert.Equal(reqDto.CreatedDate, respDto.CreatedDate);
                                    Assert.Equal(reqDto.CreatedByID, respDto.CreatedByID);
                                    Assert.Equal(reqDto.ModifiedDate, respDto.ModifiedDate);
                                    Assert.Equal(reqDto.ModifiedByID, respDto.ModifiedByID);
                
                    respEntity = OrderPaymentDetailsConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void OrderPaymentDetails_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.OrderPaymentDetails testEntity = AddTestEntity();
                try
                {
                          testEntity.OrderID = 100002;
                            testEntity.PaymentMethodID = 6;
                            testEntity.PaymentTransUID = "PaymentTransUID 9bae7efed89b448e94b0924b9ef69f97";
                            testEntity.PaymentDateTime = DateTime.Parse("4/20/2020 3:50:52 AM");
                            testEntity.IsDeleted = true;              
                            testEntity.CreatedDate = DateTime.Parse("4/20/2020 3:50:52 AM");
                            testEntity.CreatedByID = 100064;
                            testEntity.ModifiedDate = DateTime.Parse("4/20/2020 3:50:52 AM");
                            testEntity.ModifiedByID = 100004;
              
                    var reqDto = OrderPaymentDetailsConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/orderpaymentdetailses/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    OrderPaymentDetails respDto = ExtractContentJson<OrderPaymentDetails>(respUpdate.Result.Content);

                                     Assert.NotNull(respDto.ID);
                                    Assert.Equal(reqDto.OrderID, respDto.OrderID);
                                    Assert.Equal(reqDto.PaymentMethodID, respDto.PaymentMethodID);
                                    Assert.Equal(reqDto.PaymentTransUID, respDto.PaymentTransUID);
                                    Assert.Equal(reqDto.PaymentDateTime, respDto.PaymentDateTime);
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
        public void OrderPaymentDetails_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.OrderPaymentDetails testEntity = CreateTestEntity();
                try
                {
                             testEntity.ID = Int64.MaxValue;
                             testEntity.OrderID = 100002;
                            testEntity.PaymentMethodID = 6;
                            testEntity.PaymentTransUID = "PaymentTransUID 9bae7efed89b448e94b0924b9ef69f97";
                            testEntity.PaymentDateTime = DateTime.Parse("4/20/2020 3:50:52 AM");
                            testEntity.IsDeleted = true;              
                            testEntity.CreatedDate = DateTime.Parse("4/20/2020 3:50:52 AM");
                            testEntity.CreatedByID = 100064;
                            testEntity.ModifiedDate = DateTime.Parse("4/20/2020 3:50:52 AM");
                            testEntity.ModifiedByID = 100004;
              
                    var reqDto = OrderPaymentDetailsConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/orderpaymentdetailses/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(PPT.Interfaces.Entities.OrderPaymentDetails entity)
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

        protected PPT.Interfaces.Entities.OrderPaymentDetails CreateTestEntity()
        {
            var entity = new PPT.Interfaces.Entities.OrderPaymentDetails();
                          entity.OrderID = 100007;
                            entity.PaymentMethodID = 2;
                            entity.PaymentTransUID = "PaymentTransUID d5288bc48c66468793a03c809f37c31d";
                            entity.PaymentDateTime = DateTime.Parse("6/4/2022 12:16:52 PM");
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("6/4/2022 12:16:52 PM");
                            entity.CreatedByID = 100065;
                            entity.ModifiedDate = DateTime.Parse("10/23/2019 10:03:52 PM");
                            entity.ModifiedByID = 100008;
              
            return entity;
        }

        protected PPT.Interfaces.Entities.OrderPaymentDetails AddTestEntity()
        {
            PPT.Interfaces.Entities.OrderPaymentDetails result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private PPT.Interfaces.IOrderPaymentDetailsDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            PPT.Interfaces.IOrderPaymentDetailsDal dal = new PPT.DAL.MSSQL.OrderPaymentDetailsDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
