


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
    public class TestMatsController : E2ETestBase, IClassFixture<WebApplicationFactory<PPT.PhotoPrint.API.Startup>>
    {
        public TestMatsController(WebApplicationFactory<PPT.PhotoPrint.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void Mat_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var respGetAll = client.GetAsync($"/api/v1/mats");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<Mat> dtos = ExtractContentJson<List<Mat>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void Mat_Get_Success()
        {
            PPT.Interfaces.Entities.Mat testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                var paramID = testEntity.ID;
                    var respGet = client.GetAsync($"/api/v1/mats/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respGet.Result.StatusCode);

                    Mat dto = ExtractContentJson<Mat>(respGet.Result.Content);

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
        public void Mat_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/mats/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void Mat_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                var paramID = testEntity.ID;

                    var respDel = client.DeleteAsync($"/api/v1/mats/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void Mat_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/mats/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void Mat_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.Mat testEntity = CreateTestEntity();
                PPT.Interfaces.Entities.Mat respEntity = null;
                try
                {
                    var reqDto = MatConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/mats/", content);

                    Assert.Equal(HttpStatusCode.Created, respInsert.Result.StatusCode);

                    Mat respDto = ExtractContentJson<Mat>(respInsert.Result.Content);

                                    Assert.NotNull(respDto.ID);
                                    Assert.Equal(reqDto.MatName, respDto.MatName);
                                    Assert.Equal(reqDto.Description, respDto.Description);
                                    Assert.Equal(reqDto.ThumbnailUrl, respDto.ThumbnailUrl);
                                    Assert.Equal(reqDto.IsDeleted, respDto.IsDeleted);
                                    
                                    
                                    
                                    
                
                    respEntity = MatConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void Mat_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.Mat testEntity = AddTestEntity();
                try
                {
                          testEntity.MatName = "MatName 70524bd29a074103ae92c25bced9b338";
                            testEntity.Description = "Description 70524bd29a074103ae92c25bced9b338";
                            testEntity.ThumbnailUrl = "ThumbnailUrl 70524bd29a074103ae92c25bced9b338";
                            testEntity.IsDeleted = true;              
                            testEntity.CreatedDate = DateTime.Parse("6/18/2022 12:08:37 PM");
                            testEntity.CreatedByID = 100006 ;
                            testEntity.ModifiedDate = DateTime.Parse("11/6/2019 9:55:37 PM");
                            testEntity.ModifiedByID = 100010 ;
              
                    var reqDto = MatConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/mats/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    Mat respDto = ExtractContentJson<Mat>(respUpdate.Result.Content);

                                     Assert.NotNull(respDto.ID);
                                    Assert.Equal(reqDto.MatName, respDto.MatName);
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
        public void Mat_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.Mat testEntity = CreateTestEntity();
                try
                {
                             testEntity.ID = Int64.MaxValue;
                             testEntity.MatName = "MatName 70524bd29a074103ae92c25bced9b338";
                            testEntity.Description = "Description 70524bd29a074103ae92c25bced9b338";
                            testEntity.ThumbnailUrl = "ThumbnailUrl 70524bd29a074103ae92c25bced9b338";
                            testEntity.IsDeleted = true;              
                            testEntity.CreatedDate = DateTime.Parse("6/18/2022 12:08:37 PM");
                            testEntity.CreatedByID = 100006;
                            testEntity.ModifiedDate = DateTime.Parse("11/6/2019 9:55:37 PM");
                            testEntity.ModifiedByID = 100010;
              
                    var reqDto = MatConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/mats/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(PPT.Interfaces.Entities.Mat entity)
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

        protected PPT.Interfaces.Entities.Mat CreateTestEntity()
        {
            var entity = new PPT.Interfaces.Entities.Mat();
                          entity.MatName = "MatName bb387f4526b24c5e95e0839b23c5d970";
                            entity.Description = "Description bb387f4526b24c5e95e0839b23c5d970";
                            entity.ThumbnailUrl = "ThumbnailUrl bb387f4526b24c5e95e0839b23c5d970";
                            entity.IsDeleted = true;              
                            entity.CreatedDate = DateTime.Parse("2/11/2023 12:32:37 PM");
                            entity.CreatedByID = 100006;
                            entity.ModifiedDate = DateTime.Parse("5/12/2023 8:06:37 AM");
                            entity.ModifiedByID = 100003;
              
            return entity;
        }

        protected PPT.Interfaces.Entities.Mat AddTestEntity()
        {
            PPT.Interfaces.Entities.Mat result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private PPT.Interfaces.IMatDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            PPT.Interfaces.IMatDal dal = new PPT.DAL.MSSQL.MatDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
