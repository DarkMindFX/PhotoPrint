

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
    public class TestImageCategoriesController : E2ETestBase, IClassFixture<WebApplicationFactory<PPT.PhotoPrint.API.Startup>>
    {
        public TestImageCategoriesController(WebApplicationFactory<PPT.PhotoPrint.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void ImageCategory_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var respGetAll = client.GetAsync($"/api/v1/imagecategories");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<ImageCategory> dtos = ExtractContentJson<List<ImageCategory>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void ImageCategory_Get_Success()
        {
            PPT.Interfaces.Entities.ImageCategory testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                var paramImageID = testEntity.ImageID;
                var paramCategoryID = testEntity.CategoryID;
                    var respGet = client.GetAsync($"/api/v1/imagecategories/{paramImageID}/{paramCategoryID}");

                    Assert.Equal(HttpStatusCode.OK, respGet.Result.StatusCode);

                    ImageCategory dto = ExtractContentJson<ImageCategory>(respGet.Result.Content);

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
        public void ImageCategory_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramImageID = Int64.MaxValue;
                var paramCategoryID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/imagecategories/{paramImageID}/{paramCategoryID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void ImageCategory_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                var paramImageID = testEntity.ImageID;
                var paramCategoryID = testEntity.CategoryID;

                    var respDel = client.DeleteAsync($"/api/v1/imagecategories/{paramImageID}/{paramCategoryID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void ImageCategory_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramImageID = Int64.MaxValue;
                var paramCategoryID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/imagecategories/{paramImageID}/{paramCategoryID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void ImageCategory_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.ImageCategory testEntity = CreateTestEntity();
                PPT.Interfaces.Entities.ImageCategory respEntity = null;
                try
                {
                    var reqDto = ImageCategoryConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/imagecategories/", content);

                    Assert.Equal(HttpStatusCode.Created, respInsert.Result.StatusCode);

                    ImageCategory respDto = ExtractContentJson<ImageCategory>(respInsert.Result.Content);

                                    Assert.NotNull(respDto.ImageID);
                                    Assert.NotNull(respDto.CategoryID);
                
                    respEntity = ImageCategoryConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void ImageCategory_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.ImageCategory testEntity = AddTestEntity();
                try
                {
            
                    var reqDto = ImageCategoryConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/imagecategories/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    ImageCategory respDto = ExtractContentJson<ImageCategory>(respUpdate.Result.Content);

                                     Assert.NotNull(respDto.ImageID);
                                    Assert.NotNull(respDto.CategoryID);
                
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void ImageCategory_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.ImageCategory testEntity = CreateTestEntity();
                try
                {
                            testEntity.ImageID = 100031;
                            testEntity.CategoryID = 100006;
              
                    var reqDto = ImageCategoryConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/imagecategories/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(PPT.Interfaces.Entities.ImageCategory entity)
        {
            if (entity != null)
            {
                var dal = CreateDal();

                return dal.Delete(
                                        entity.ImageID,
                                        entity.CategoryID
                );
            }
            else
            {
                return false;
            }
        }

        protected PPT.Interfaces.Entities.ImageCategory CreateTestEntity()
        {
            var entity = new PPT.Interfaces.Entities.ImageCategory();
                          entity.ImageID = 100050;
                            entity.CategoryID = 100003;
              
            return entity;
        }

        protected PPT.Interfaces.Entities.ImageCategory AddTestEntity()
        {
            PPT.Interfaces.Entities.ImageCategory result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private PPT.Interfaces.IImageCategoryDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            PPT.Interfaces.IImageCategoryDal dal = new PPT.DAL.MSSQL.ImageCategoryDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
