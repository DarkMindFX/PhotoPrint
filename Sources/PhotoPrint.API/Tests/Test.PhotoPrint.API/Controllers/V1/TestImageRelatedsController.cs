

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
    public class TestImageRelatedsController : E2ETestBase, IClassFixture<WebApplicationFactory<PPT.PhotoPrint.API.Startup>>
    {
        public TestImageRelatedsController(WebApplicationFactory<PPT.PhotoPrint.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void ImageRelated_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var respGetAll = client.GetAsync($"/api/v1/imagerelateds");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<ImageRelated> dtos = ExtractContentJson<List<ImageRelated>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void ImageRelated_Get_Success()
        {
            PPT.Interfaces.Entities.ImageRelated testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                var paramImageID = testEntity.ImageID;
                var paramRelatedImageID = testEntity.RelatedImageID;
                    var respGet = client.GetAsync($"/api/v1/imagerelateds/{paramImageID}/{paramRelatedImageID}");

                    Assert.Equal(HttpStatusCode.OK, respGet.Result.StatusCode);

                    ImageRelated dto = ExtractContentJson<ImageRelated>(respGet.Result.Content);

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
        public void ImageRelated_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramImageID = Int64.MaxValue;
                var paramRelatedImageID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/imagerelateds/{paramImageID}/{paramRelatedImageID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void ImageRelated_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                var paramImageID = testEntity.ImageID;
                var paramRelatedImageID = testEntity.RelatedImageID;

                    var respDel = client.DeleteAsync($"/api/v1/imagerelateds/{paramImageID}/{paramRelatedImageID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void ImageRelated_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramImageID = Int64.MaxValue;
                var paramRelatedImageID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/imagerelateds/{paramImageID}/{paramRelatedImageID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void ImageRelated_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.ImageRelated testEntity = CreateTestEntity();
                PPT.Interfaces.Entities.ImageRelated respEntity = null;
                try
                {
                    var reqDto = ImageRelatedConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/imagerelateds/", content);

                    Assert.Equal(HttpStatusCode.Created, respInsert.Result.StatusCode);

                    ImageRelated respDto = ExtractContentJson<ImageRelated>(respInsert.Result.Content);

                                    Assert.NotNull(respDto.ImageID);
                                    Assert.NotNull(respDto.RelatedImageID);
                
                    respEntity = ImageRelatedConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void ImageRelated_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.ImageRelated testEntity = AddTestEntity();
                try
                {
            
                    var reqDto = ImageRelatedConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/imagerelateds/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    ImageRelated respDto = ExtractContentJson<ImageRelated>(respUpdate.Result.Content);

                                     Assert.NotNull(respDto.ImageID);
                                    Assert.NotNull(respDto.RelatedImageID);
                
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void ImageRelated_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.ImageRelated testEntity = CreateTestEntity();
                try
                {
                            testEntity.ImageID = 100013;
                            testEntity.RelatedImageID = 100043;
              
                    var reqDto = ImageRelatedConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/imagerelateds/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(PPT.Interfaces.Entities.ImageRelated entity)
        {
            if (entity != null)
            {
                var dal = CreateDal();

                return dal.Delete(
                                        entity.ImageID,
                                        entity.RelatedImageID
                );
            }
            else
            {
                return false;
            }
        }

        protected PPT.Interfaces.Entities.ImageRelated CreateTestEntity()
        {
            var entity = new PPT.Interfaces.Entities.ImageRelated();
                          entity.ImageID = 100032;
                            entity.RelatedImageID = 100031;
              
            return entity;
        }

        protected PPT.Interfaces.Entities.ImageRelated AddTestEntity()
        {
            PPT.Interfaces.Entities.ImageRelated result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private PPT.Interfaces.IImageRelatedDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            PPT.Interfaces.IImageRelatedDal dal = new PPT.DAL.MSSQL.ImageRelatedDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
