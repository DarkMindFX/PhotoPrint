


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
    public class TestSizesController : E2ETestBase, IClassFixture<WebApplicationFactory<PPT.PhotoPrint.API.Startup>>
    {
        public TestSizesController(WebApplicationFactory<PPT.PhotoPrint.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void Size_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var respGetAll = client.GetAsync($"/api/v1/sizes");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<Size> dtos = ExtractContentJson<List<Size>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void Size_Get_Success()
        {
            PPT.Interfaces.Entities.Size testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                var paramID = testEntity.ID;
                    var respGet = client.GetAsync($"/api/v1/sizes/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respGet.Result.StatusCode);

                    Size dto = ExtractContentJson<Size>(respGet.Result.Content);

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
        public void Size_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/sizes/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void Size_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                var paramID = testEntity.ID;

                    var respDel = client.DeleteAsync($"/api/v1/sizes/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void Size_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/sizes/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void Size_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.Size testEntity = CreateTestEntity();
                PPT.Interfaces.Entities.Size respEntity = null;
                try
                {
                    var reqDto = SizeConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/sizes/", content);

                    Assert.Equal(HttpStatusCode.Created, respInsert.Result.StatusCode);

                    Size respDto = ExtractContentJson<Size>(respInsert.Result.Content);

                                    Assert.NotNull(respDto.ID);
                                    Assert.Equal(reqDto.SizeName, respDto.SizeName);
                                    Assert.Equal(reqDto.Width, respDto.Width);
                                    Assert.Equal(reqDto.Height, respDto.Height);
                                    Assert.Equal(reqDto.IsDeleted, respDto.IsDeleted);
                                    
                                    
                                    
                                    
                
                    respEntity = SizeConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void Size_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.Size testEntity = AddTestEntity();
                try
                {
                          testEntity.SizeName = "SizeName fbb7e50be2984d5f9cb5790baad03724";
                            testEntity.Width = 540 ;
                            testEntity.Height = 540 ;
                            testEntity.IsDeleted = false;              
                            testEntity.CreatedDate = DateTime.Parse("1/24/2022 12:27:38 AM");
                            testEntity.CreatedByID = 100005 ;
                            testEntity.ModifiedDate = DateTime.Parse("4/16/2023 10:15:38 PM");
                            testEntity.ModifiedByID = 100011 ;
              
                    var reqDto = SizeConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/sizes/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    Size respDto = ExtractContentJson<Size>(respUpdate.Result.Content);

                                     Assert.NotNull(respDto.ID);
                                    Assert.Equal(reqDto.SizeName, respDto.SizeName);
                                    Assert.Equal(reqDto.Width, respDto.Width);
                                    Assert.Equal(reqDto.Height, respDto.Height);
                                    Assert.Equal(reqDto.IsDeleted, respDto.IsDeleted);
                                    
                                    
                                    
                                    
                
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void Size_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.Size testEntity = CreateTestEntity();
                try
                {
                             testEntity.ID = Int64.MaxValue;
                             testEntity.SizeName = "SizeName fbb7e50be2984d5f9cb5790baad03724";
                            testEntity.Width = 540;
                            testEntity.Height = 540;
                            testEntity.IsDeleted = false;              
                            testEntity.CreatedDate = DateTime.Parse("1/24/2022 12:27:38 AM");
                            testEntity.CreatedByID = 100005;
                            testEntity.ModifiedDate = DateTime.Parse("4/16/2023 10:15:38 PM");
                            testEntity.ModifiedByID = 100011;
              
                    var reqDto = SizeConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/sizes/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(PPT.Interfaces.Entities.Size entity)
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

        protected PPT.Interfaces.Entities.Size CreateTestEntity()
        {
            var entity = new PPT.Interfaces.Entities.Size();
                          entity.SizeName = "SizeName 9e0581979b824da788e718b0aa96acae";
                            entity.Width = 451;
                            entity.Height = 451;
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("7/28/2021 6:40:38 PM");
                            entity.CreatedByID = 100007;
                            entity.ModifiedDate = DateTime.Parse("6/7/2024 4:26:38 AM");
                            entity.ModifiedByID = 100001;
              
            return entity;
        }

        protected PPT.Interfaces.Entities.Size AddTestEntity()
        {
            PPT.Interfaces.Entities.Size result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private PPT.Interfaces.ISizeDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            PPT.Interfaces.ISizeDal dal = new PPT.DAL.MSSQL.SizeDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
