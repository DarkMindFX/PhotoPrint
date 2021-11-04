


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
    public class TestMountingTypesController : E2ETestBase, IClassFixture<WebApplicationFactory<PPT.PhotoPrint.API.Startup>>
    {
        public TestMountingTypesController(WebApplicationFactory<PPT.PhotoPrint.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void MountingType_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var respGetAll = client.GetAsync($"/api/v1/mountingtypes");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<MountingType> dtos = ExtractContentJson<List<MountingType>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void MountingType_Get_Success()
        {
            PPT.Interfaces.Entities.MountingType testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                var paramID = testEntity.ID;
                    var respGet = client.GetAsync($"/api/v1/mountingtypes/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respGet.Result.StatusCode);

                    MountingType dto = ExtractContentJson<MountingType>(respGet.Result.Content);

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
        public void MountingType_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/mountingtypes/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void MountingType_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                var paramID = testEntity.ID;

                    var respDel = client.DeleteAsync($"/api/v1/mountingtypes/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void MountingType_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/mountingtypes/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void MountingType_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.MountingType testEntity = CreateTestEntity();
                PPT.Interfaces.Entities.MountingType respEntity = null;
                try
                {
                    var reqDto = MountingTypeConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/mountingtypes/", content);

                    Assert.Equal(HttpStatusCode.Created, respInsert.Result.StatusCode);

                    MountingType respDto = ExtractContentJson<MountingType>(respInsert.Result.Content);

                                    Assert.NotNull(respDto.ID);
                                    Assert.Equal(reqDto.MountingTypeName, respDto.MountingTypeName);
                                    Assert.Equal(reqDto.Description, respDto.Description);
                                    Assert.Equal(reqDto.ThumbnailUrl, respDto.ThumbnailUrl);
                                    Assert.Equal(reqDto.IsDeleted, respDto.IsDeleted);
                                    
                                    
                                    
                                    
                
                    respEntity = MountingTypeConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void MountingType_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.MountingType testEntity = AddTestEntity();
                try
                {
                          testEntity.MountingTypeName = "MountingTypeName c8b94fe26a004bb9821ae6ac4c73757c";
                            testEntity.Description = "Description c8b94fe26a004bb9821ae6ac4c73757c";
                            testEntity.ThumbnailUrl = "ThumbnailUrl c8b94fe26a004bb9821ae6ac4c73757c";
                            testEntity.IsDeleted = true;              
                            testEntity.CreatedDate = DateTime.Parse("5/12/2022 5:32:38 AM");
                            testEntity.CreatedByID = 594187 ;
                            testEntity.ModifiedDate = DateTime.Parse("5/12/2022 5:32:38 AM");
                            testEntity.ModifiedByID = 594187 ;
              
                    var reqDto = MountingTypeConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/mountingtypes/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    MountingType respDto = ExtractContentJson<MountingType>(respUpdate.Result.Content);

                                     Assert.NotNull(respDto.ID);
                                    Assert.Equal(reqDto.MountingTypeName, respDto.MountingTypeName);
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
        public void MountingType_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.MountingType testEntity = CreateTestEntity();
                try
                {
                             testEntity.ID = Int64.MaxValue;
                             testEntity.MountingTypeName = "MountingTypeName c8b94fe26a004bb9821ae6ac4c73757c";
                            testEntity.Description = "Description c8b94fe26a004bb9821ae6ac4c73757c";
                            testEntity.ThumbnailUrl = "ThumbnailUrl c8b94fe26a004bb9821ae6ac4c73757c";
                            testEntity.IsDeleted = true;              
                            testEntity.CreatedDate = DateTime.Parse("5/12/2022 5:32:38 AM");
                            testEntity.CreatedByID = 594187;
                            testEntity.ModifiedDate = DateTime.Parse("5/12/2022 5:32:38 AM");
                            testEntity.ModifiedByID = 594187;
              
                    var reqDto = MountingTypeConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/mountingtypes/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(PPT.Interfaces.Entities.MountingType entity)
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

        protected PPT.Interfaces.Entities.MountingType CreateTestEntity()
        {
            var entity = new PPT.Interfaces.Entities.MountingType();
                          entity.MountingTypeName = "MountingTypeName 96199efb67d14c13bc978d5eadbd9b68";
                            entity.Description = "Description 96199efb67d14c13bc978d5eadbd9b68";
                            entity.ThumbnailUrl = "ThumbnailUrl 96199efb67d14c13bc978d5eadbd9b68";
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("5/12/2022 5:32:38 AM");
                            entity.CreatedByID = 594187;
                            entity.ModifiedDate = DateTime.Parse("5/12/2022 5:32:38 AM");
                            entity.ModifiedByID = 594187;
              
            return entity;
        }

        protected PPT.Interfaces.Entities.MountingType AddTestEntity()
        {
            PPT.Interfaces.Entities.MountingType result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private PPT.Interfaces.IMountingTypeDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            PPT.Interfaces.IMountingTypeDal dal = new PPT.DAL.MSSQL.MountingTypeDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
