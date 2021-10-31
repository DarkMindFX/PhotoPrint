

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
                    testEntity.OrderID = 100012;
                    testEntity.PaymentMethodID = 1;
                    testEntity.PaymentTransUID = "PaymentTransUID 836a8270fe39449f9e599de2ee71b265";
                    testEntity.PaymentDateTime = DateTime.Parse("3/26/2024 5:15:35 AM");
                    testEntity.IsDeleted = true;
                    testEntity.CreatedDate = DateTime.Parse("3/26/2024 5:15:35 AM");
                    testEntity.CreatedByID = 100010;
                    testEntity.ModifiedDate = DateTime.Parse("3/26/2024 5:15:35 AM");
                    testEntity.ModifiedByID = 100006;

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
                    testEntity.OrderID = 100012;
                    testEntity.PaymentMethodID = 1;
                    testEntity.PaymentTransUID = "PaymentTransUID 836a8270fe39449f9e599de2ee71b265";
                    testEntity.PaymentDateTime = DateTime.Parse("3/26/2024 5:15:35 AM");
                    testEntity.IsDeleted = true;
                    testEntity.CreatedDate = DateTime.Parse("3/26/2024 5:15:35 AM");
                    testEntity.CreatedByID = 100010;
                    testEntity.ModifiedDate = DateTime.Parse("3/26/2024 5:15:35 AM");
                    testEntity.ModifiedByID = 100006;

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
            entity.OrderID = 100003;
            entity.PaymentMethodID = 3;
            entity.PaymentTransUID = "PaymentTransUID 676b55e5f70849bfa40337ff2a464806";
            entity.PaymentDateTime = DateTime.Parse("2/15/2021 9:15:35 AM");
            entity.IsDeleted = true;
            entity.CreatedDate = DateTime.Parse("2/15/2021 9:15:35 AM");
            entity.CreatedByID = 100008;
            entity.ModifiedDate = DateTime.Parse("12/26/2023 9:41:35 AM");
            entity.ModifiedByID = 100003;

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
