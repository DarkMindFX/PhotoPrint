


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
    public class TestMaterialTypesController : E2ETestBase, IClassFixture<WebApplicationFactory<PPT.PhotoPrint.API.Startup>>
    {
        public TestMaterialTypesController(WebApplicationFactory<PPT.PhotoPrint.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void MaterialType_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var respGetAll = client.GetAsync($"/api/v1/materialtypes");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<MaterialType> dtos = ExtractContentJson<List<MaterialType>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void MaterialType_Get_Success()
        {
            PPT.Interfaces.Entities.MaterialType testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                var paramID = testEntity.ID;
                    var respGet = client.GetAsync($"/api/v1/materialtypes/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respGet.Result.StatusCode);

                    MaterialType dto = ExtractContentJson<MaterialType>(respGet.Result.Content);

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
        public void MaterialType_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/materialtypes/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void MaterialType_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                var paramID = testEntity.ID;

                    var respDel = client.DeleteAsync($"/api/v1/materialtypes/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void MaterialType_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/materialtypes/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void MaterialType_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.MaterialType testEntity = CreateTestEntity();
                PPT.Interfaces.Entities.MaterialType respEntity = null;
                try
                {
                    var reqDto = MaterialTypeConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/materialtypes/", content);

                    Assert.Equal(HttpStatusCode.Created, respInsert.Result.StatusCode);

                    MaterialType respDto = ExtractContentJson<MaterialType>(respInsert.Result.Content);

                                    Assert.NotNull(respDto.ID);
                                    Assert.Equal(reqDto.MaterialTypeName, respDto.MaterialTypeName);
                                    Assert.Equal(reqDto.Description, respDto.Description);
                                    Assert.Equal(reqDto.ThumbnailUrl, respDto.ThumbnailUrl);
                                    Assert.Equal(reqDto.IsDeleted, respDto.IsDeleted);
                                    
                                    
                                    
                                    
                
                    respEntity = MaterialTypeConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void MaterialType_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.MaterialType testEntity = AddTestEntity();
                try
                {
                          testEntity.MaterialTypeName = "MaterialTypeName 7f7d6dbc83e74c6e80509468ef11e415";
                            testEntity.Description = "Description 7f7d6dbc83e74c6e80509468ef11e415";
                            testEntity.ThumbnailUrl = "ThumbnailUrl 7f7d6dbc83e74c6e80509468ef11e415";
                            testEntity.IsDeleted = false;              
                            testEntity.CreatedDate = DateTime.Parse("5/17/2021 5:58:38 PM");
                            testEntity.CreatedByID = 100009 ;
                            testEntity.ModifiedDate = DateTime.Parse("4/3/2019 9:32:38 AM");
                            testEntity.ModifiedByID = 100006 ;
              
                    var reqDto = MaterialTypeConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/materialtypes/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    MaterialType respDto = ExtractContentJson<MaterialType>(respUpdate.Result.Content);

                                     Assert.NotNull(respDto.ID);
                                    Assert.Equal(reqDto.MaterialTypeName, respDto.MaterialTypeName);
                                    Assert.Equal(reqDto.Description, respDto.Description);
                                    Assert.Equal(reqDto.ThumbnailUrl, respDto.ThumbnailUrl);
                                    Assert.Equal(reqDto.IsDeleted, respDto.IsDeleted);
                                    
                                    
                                    
                                    
                
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void MaterialType_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.MaterialType testEntity = CreateTestEntity();
                try
                {
                             testEntity.ID = Int64.MaxValue;
                             testEntity.MaterialTypeName = "MaterialTypeName 7f7d6dbc83e74c6e80509468ef11e415";
                            testEntity.Description = "Description 7f7d6dbc83e74c6e80509468ef11e415";
                            testEntity.ThumbnailUrl = "ThumbnailUrl 7f7d6dbc83e74c6e80509468ef11e415";
                            testEntity.IsDeleted = false;              
                            testEntity.CreatedDate = DateTime.Parse("5/17/2021 5:58:38 PM");
                            testEntity.CreatedByID = 100009;
                            testEntity.ModifiedDate = DateTime.Parse("4/3/2019 9:32:38 AM");
                            testEntity.ModifiedByID = 100006;
              
                    var reqDto = MaterialTypeConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/materialtypes/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(PPT.Interfaces.Entities.MaterialType entity)
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

        protected PPT.Interfaces.Entities.MaterialType CreateTestEntity()
        {
            var entity = new PPT.Interfaces.Entities.MaterialType();
                          entity.MaterialTypeName = "MaterialTypeName 702f7c20345d4124aa43f970a63eacc3";
                            entity.Description = "Description 702f7c20345d4124aa43f970a63eacc3";
                            entity.ThumbnailUrl = "ThumbnailUrl 702f7c20345d4124aa43f970a63eacc3";
                            entity.IsDeleted = false;              
                            entity.CreatedDate = DateTime.Parse("4/22/2020 2:46:38 PM");
                            entity.CreatedByID = 100008;
                            entity.ModifiedDate = DateTime.Parse("1/16/2021 6:47:38 AM");
                            entity.ModifiedByID = 100002;
              
            return entity;
        }

        protected PPT.Interfaces.Entities.MaterialType AddTestEntity()
        {
            PPT.Interfaces.Entities.MaterialType result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private PPT.Interfaces.IMaterialTypeDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            PPT.Interfaces.IMaterialTypeDal dal = new PPT.DAL.MSSQL.MaterialTypeDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
