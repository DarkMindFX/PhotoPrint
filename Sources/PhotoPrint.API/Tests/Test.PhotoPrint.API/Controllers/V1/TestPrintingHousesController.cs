

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
                                    Assert.Equal(reqDto.CreatedDate, respDto.CreatedDate);
                                    Assert.Equal(reqDto.CreatedByID, respDto.CreatedByID);
                                    Assert.Equal(reqDto.ModifiedDate, respDto.ModifiedDate);
                                    Assert.Equal(reqDto.ModifiedByID, respDto.ModifiedByID);
                
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
                          testEntity.Name = "Name ee4e255f51df402ca5ac0ac1afd09897";
                            testEntity.Description = "Description ee4e255f51df402ca5ac0ac1afd09897";
                            testEntity.IsDeleted = false;              
                            testEntity.CreatedDate = DateTime.Parse("2/18/2023 12:40:53 AM");
                            testEntity.CreatedByID = 100064;
                            testEntity.ModifiedDate = DateTime.Parse("2/18/2023 12:40:53 AM");
                            testEntity.ModifiedByID = 100008;
              
                    var reqDto = PrintingHouseConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/printinghouses/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    PrintingHouse respDto = ExtractContentJson<PrintingHouse>(respUpdate.Result.Content);

                                     Assert.NotNull(respDto.ID);
                                    Assert.Equal(reqDto.Name, respDto.Name);
                                    Assert.Equal(reqDto.Description, respDto.Description);
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
                             testEntity.Name = "Name ee4e255f51df402ca5ac0ac1afd09897";
                            testEntity.Description = "Description ee4e255f51df402ca5ac0ac1afd09897";
                            testEntity.IsDeleted = false;              
                            testEntity.CreatedDate = DateTime.Parse("2/18/2023 12:40:53 AM");
                            testEntity.CreatedByID = 100064;
                            testEntity.ModifiedDate = DateTime.Parse("2/18/2023 12:40:53 AM");
                            testEntity.ModifiedByID = 100008;
              
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
                          entity.Name = "Name 792f08b3cc5e42528e2fb93e0bf22025";
                            entity.Description = "Description 792f08b3cc5e42528e2fb93e0bf22025";
                            entity.IsDeleted = false;              
                            entity.CreatedDate = DateTime.Parse("11/20/2022 2:27:53 PM");
                            entity.CreatedByID = 100009;
                            entity.ModifiedDate = DateTime.Parse("4/8/2020 2:54:53 PM");
                            entity.ModifiedByID = 100010;
              
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
