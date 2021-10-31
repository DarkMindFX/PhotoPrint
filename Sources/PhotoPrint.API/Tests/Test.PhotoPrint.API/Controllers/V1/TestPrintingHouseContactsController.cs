

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
    public class TestPrintingHouseContactsController : E2ETestBase, IClassFixture<WebApplicationFactory<PPT.PhotoPrint.API.Startup>>
    {
        public TestPrintingHouseContactsController(WebApplicationFactory<PPT.PhotoPrint.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void PrintingHouseContact_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var respGetAll = client.GetAsync($"/api/v1/printinghousecontacts");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<PrintingHouseContact> dtos = ExtractContentJson<List<PrintingHouseContact>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void PrintingHouseContact_Get_Success()
        {
            PPT.Interfaces.Entities.PrintingHouseContact testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                    var paramPrintingHouseID = testEntity.PrintingHouseID;
                    var paramContactID = testEntity.ContactID;
                    var respGet = client.GetAsync($"/api/v1/printinghousecontacts/{paramPrintingHouseID}/{paramContactID}");

                    Assert.Equal(HttpStatusCode.OK, respGet.Result.StatusCode);

                    PrintingHouseContact dto = ExtractContentJson<PrintingHouseContact>(respGet.Result.Content);

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
        public void PrintingHouseContact_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramPrintingHouseID = Int64.MaxValue;
                var paramContactID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/printinghousecontacts/{paramPrintingHouseID}/{paramContactID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void PrintingHouseContact_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                    var paramPrintingHouseID = testEntity.PrintingHouseID;
                    var paramContactID = testEntity.ContactID;

                    var respDel = client.DeleteAsync($"/api/v1/printinghousecontacts/{paramPrintingHouseID}/{paramContactID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void PrintingHouseContact_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramPrintingHouseID = Int64.MaxValue;
                var paramContactID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/printinghousecontacts/{paramPrintingHouseID}/{paramContactID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void PrintingHouseContact_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.PrintingHouseContact testEntity = CreateTestEntity();
                PPT.Interfaces.Entities.PrintingHouseContact respEntity = null;
                try
                {
                    var reqDto = PrintingHouseContactConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/printinghousecontacts/", content);

                    Assert.Equal(HttpStatusCode.Created, respInsert.Result.StatusCode);

                    PrintingHouseContact respDto = ExtractContentJson<PrintingHouseContact>(respInsert.Result.Content);

                    Assert.NotNull(respDto.PrintingHouseID);
                    Assert.NotNull(respDto.ContactID);
                    Assert.Equal(reqDto.IsPrimary, respDto.IsPrimary);

                    respEntity = PrintingHouseContactConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void PrintingHouseContact_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.PrintingHouseContact testEntity = AddTestEntity();
                try
                {
                    testEntity.IsPrimary = false;

                    var reqDto = PrintingHouseContactConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/printinghousecontacts/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    PrintingHouseContact respDto = ExtractContentJson<PrintingHouseContact>(respUpdate.Result.Content);

                    Assert.NotNull(respDto.PrintingHouseID);
                    Assert.NotNull(respDto.ContactID);
                    Assert.Equal(reqDto.IsPrimary, respDto.IsPrimary);

                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void PrintingHouseContact_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.PrintingHouseContact testEntity = CreateTestEntity();
                try
                {
                    testEntity.PrintingHouseID = 100001;
                    testEntity.ContactID = 100020;
                    testEntity.IsPrimary = false;

                    var reqDto = PrintingHouseContactConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/printinghousecontacts/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(PPT.Interfaces.Entities.PrintingHouseContact entity)
        {
            if (entity != null)
            {
                var dal = CreateDal();

                return dal.Delete(
                                        entity.PrintingHouseID,
                                        entity.ContactID
                );
            }
            else
            {
                return false;
            }
        }

        protected PPT.Interfaces.Entities.PrintingHouseContact CreateTestEntity()
        {
            var entity = new PPT.Interfaces.Entities.PrintingHouseContact();
            entity.PrintingHouseID = 100001;
            entity.ContactID = 100012;
            entity.IsPrimary = true;

            return entity;
        }

        protected PPT.Interfaces.Entities.PrintingHouseContact AddTestEntity()
        {
            PPT.Interfaces.Entities.PrintingHouseContact result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private PPT.Interfaces.IPrintingHouseContactDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            PPT.Interfaces.IPrintingHouseContactDal dal = new PPT.DAL.MSSQL.PrintingHouseContactDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
