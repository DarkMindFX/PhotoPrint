

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
    public class TestPrintingHouseAddressesController : E2ETestBase, IClassFixture<WebApplicationFactory<PPT.PhotoPrint.API.Startup>>
    {
        public TestPrintingHouseAddressesController(WebApplicationFactory<PPT.PhotoPrint.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void PrintingHouseAddress_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var respGetAll = client.GetAsync($"/api/v1/printinghouseaddresses");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<PrintingHouseAddress> dtos = ExtractContentJson<List<PrintingHouseAddress>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void PrintingHouseAddress_Get_Success()
        {
            PPT.Interfaces.Entities.PrintingHouseAddress testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                    var paramPrintingHouseID = testEntity.PrintingHouseID;
                    var paramAddressID = testEntity.AddressID;
                    var respGet = client.GetAsync($"/api/v1/printinghouseaddresses/{paramPrintingHouseID}/{paramAddressID}");

                    Assert.Equal(HttpStatusCode.OK, respGet.Result.StatusCode);

                    PrintingHouseAddress dto = ExtractContentJson<PrintingHouseAddress>(respGet.Result.Content);

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
        public void PrintingHouseAddress_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramPrintingHouseID = Int64.MaxValue;
                var paramAddressID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/printinghouseaddresses/{paramPrintingHouseID}/{paramAddressID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void PrintingHouseAddress_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                    var paramPrintingHouseID = testEntity.PrintingHouseID;
                    var paramAddressID = testEntity.AddressID;

                    var respDel = client.DeleteAsync($"/api/v1/printinghouseaddresses/{paramPrintingHouseID}/{paramAddressID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void PrintingHouseAddress_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramPrintingHouseID = Int64.MaxValue;
                var paramAddressID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/printinghouseaddresses/{paramPrintingHouseID}/{paramAddressID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void PrintingHouseAddress_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.PrintingHouseAddress testEntity = CreateTestEntity();
                PPT.Interfaces.Entities.PrintingHouseAddress respEntity = null;
                try
                {
                    var reqDto = PrintingHouseAddressConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/printinghouseaddresses/", content);

                    Assert.Equal(HttpStatusCode.Created, respInsert.Result.StatusCode);

                    PrintingHouseAddress respDto = ExtractContentJson<PrintingHouseAddress>(respInsert.Result.Content);

                    Assert.NotNull(respDto.PrintingHouseID);
                    Assert.NotNull(respDto.AddressID);
                    Assert.Equal(reqDto.IsPrimary, respDto.IsPrimary);

                    respEntity = PrintingHouseAddressConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void PrintingHouseAddress_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.PrintingHouseAddress testEntity = AddTestEntity();
                try
                {
                    testEntity.IsPrimary = false;

                    var reqDto = PrintingHouseAddressConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/printinghouseaddresses/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    PrintingHouseAddress respDto = ExtractContentJson<PrintingHouseAddress>(respUpdate.Result.Content);

                    Assert.NotNull(respDto.PrintingHouseID);
                    Assert.NotNull(respDto.AddressID);
                    Assert.Equal(reqDto.IsPrimary, respDto.IsPrimary);

                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void PrintingHouseAddress_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.PrintingHouseAddress testEntity = CreateTestEntity();
                try
                {
                    testEntity.PrintingHouseID = 100005;
                    testEntity.AddressID = 100010;
                    testEntity.IsPrimary = false;

                    var reqDto = PrintingHouseAddressConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/printinghouseaddresses/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(PPT.Interfaces.Entities.PrintingHouseAddress entity)
        {
            if (entity != null)
            {
                var dal = CreateDal();

                return dal.Delete(
                                        entity.PrintingHouseID,
                                        entity.AddressID
                );
            }
            else
            {
                return false;
            }
        }

        protected PPT.Interfaces.Entities.PrintingHouseAddress CreateTestEntity()
        {
            var entity = new PPT.Interfaces.Entities.PrintingHouseAddress();
            entity.PrintingHouseID = 100005;
            entity.AddressID = 100006;
            entity.IsPrimary = true;

            return entity;
        }

        protected PPT.Interfaces.Entities.PrintingHouseAddress AddTestEntity()
        {
            PPT.Interfaces.Entities.PrintingHouseAddress result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private PPT.Interfaces.IPrintingHouseAddressDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            PPT.Interfaces.IPrintingHouseAddressDal dal = new PPT.DAL.MSSQL.PrintingHouseAddressDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
