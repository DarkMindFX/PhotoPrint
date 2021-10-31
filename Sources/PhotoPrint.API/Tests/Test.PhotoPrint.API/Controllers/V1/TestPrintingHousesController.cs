

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
    public class TestPrintingHousesController : E2ETestBase, IClassFixture<WebApplicationFactory<PPT.PhotoPrint.API.Startup>>
    {
        public TestPrintingHousesController(WebApplicationFactory<PPT.PhotoPrint.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void PrintingHouse_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var respGetAll = client.GetAsync($"/api/v1/printinghouses");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<PrintingHouse> dtos = ExtractContentJson<List<PrintingHouse>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void PrintingHouse_Get_Success()
        {
            PPT.Interfaces.Entities.PrintingHouse testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                    var paramID = testEntity.ID;
                    var respGet = client.GetAsync($"/api/v1/printinghouses/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respGet.Result.StatusCode);

                    PrintingHouse dto = ExtractContentJson<PrintingHouse>(respGet.Result.Content);

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
        public void PrintingHouse_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/printinghouses/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void PrintingHouse_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                    var paramID = testEntity.ID;

                    var respDel = client.DeleteAsync($"/api/v1/printinghouses/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void PrintingHouse_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/printinghouses/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void PrintingHouse_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.PrintingHouse testEntity = CreateTestEntity();
                PPT.Interfaces.Entities.PrintingHouse respEntity = null;
                try
                {
                    var reqDto = PrintingHouseConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/printinghouses/", content);

                    Assert.Equal(HttpStatusCode.Created, respInsert.Result.StatusCode);

                    PrintingHouse respDto = ExtractContentJson<PrintingHouse>(respInsert.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.Name, respDto.Name);
                    Assert.Equal(reqDto.Description, respDto.Description);
                    Assert.Equal(reqDto.IsDeleted, respDto.IsDeleted);


                    respEntity = PrintingHouseConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void PrintingHouse_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.PrintingHouse testEntity = AddTestEntity();
                try
                {
                    testEntity.Name = "Name 80c47855c6204d06935297d3b566153b";
                    testEntity.Description = "Description 80c47855c6204d06935297d3b566153b";
                    testEntity.IsDeleted = true;
                    testEntity.CreatedDate = DateTime.Parse("9/1/2023 8:29:35 PM");
                    testEntity.CreatedByID = 100004;
                    testEntity.ModifiedDate = DateTime.Parse("9/1/2023 8:29:35 PM");
                    testEntity.ModifiedByID = 100003;

                    var reqDto = PrintingHouseConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/printinghouses/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    PrintingHouse respDto = ExtractContentJson<PrintingHouse>(respUpdate.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.Name, respDto.Name);
                    Assert.Equal(reqDto.Description, respDto.Description);
                    Assert.Equal(reqDto.IsDeleted, respDto.IsDeleted);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void PrintingHouse_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.PrintingHouse testEntity = CreateTestEntity();
                try
                {
                    testEntity.ID = Int64.MaxValue;
                    testEntity.Name = "Name 80c47855c6204d06935297d3b566153b";
                    testEntity.Description = "Description 80c47855c6204d06935297d3b566153b";
                    testEntity.IsDeleted = true;
                    testEntity.CreatedDate = DateTime.Parse("9/1/2023 8:29:35 PM");
                    testEntity.CreatedByID = 100004;
                    testEntity.ModifiedDate = DateTime.Parse("9/1/2023 8:29:35 PM");
                    testEntity.ModifiedByID = 100003;

                    var reqDto = PrintingHouseConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/printinghouses/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(PPT.Interfaces.Entities.PrintingHouse entity)
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

        protected PPT.Interfaces.Entities.PrintingHouse CreateTestEntity()
        {
            var entity = new PPT.Interfaces.Entities.PrintingHouse();
            entity.Name = "Name 508d41d9ae854b52966df97dfa628265";
            entity.Description = "Description 508d41d9ae854b52966df97dfa628265";
            entity.IsDeleted = true;
            entity.CreatedDate = DateTime.Parse("6/4/2023 10:15:35 AM");
            entity.CreatedByID = 100006;
            entity.ModifiedDate = DateTime.Parse("6/4/2023 10:15:35 AM");
            entity.ModifiedByID = 100003;

            return entity;
        }

        protected PPT.Interfaces.Entities.PrintingHouse AddTestEntity()
        {
            PPT.Interfaces.Entities.PrintingHouse result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private PPT.Interfaces.IPrintingHouseDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            PPT.Interfaces.IPrintingHouseDal dal = new PPT.DAL.MSSQL.PrintingHouseDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
