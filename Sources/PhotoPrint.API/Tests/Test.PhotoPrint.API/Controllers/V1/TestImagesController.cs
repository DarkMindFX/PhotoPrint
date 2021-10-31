

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
    public class TestImagesController : E2ETestBase, IClassFixture<WebApplicationFactory<PPT.PhotoPrint.API.Startup>>
    {
        public TestImagesController(WebApplicationFactory<PPT.PhotoPrint.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void Image_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var respGetAll = client.GetAsync($"/api/v1/images");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<Image> dtos = ExtractContentJson<List<Image>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void Image_Get_Success()
        {
            PPT.Interfaces.Entities.Image testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                    var paramID = testEntity.ID;
                    var respGet = client.GetAsync($"/api/v1/images/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respGet.Result.StatusCode);

                    Image dto = ExtractContentJson<Image>(respGet.Result.Content);

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
        public void Image_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/images/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void Image_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                    var paramID = testEntity.ID;

                    var respDel = client.DeleteAsync($"/api/v1/images/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void Image_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/images/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void Image_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.Image testEntity = CreateTestEntity();
                PPT.Interfaces.Entities.Image respEntity = null;
                try
                {
                    var reqDto = ImageConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/images/", content);

                    Assert.Equal(HttpStatusCode.Created, respInsert.Result.StatusCode);

                    Image respDto = ExtractContentJson<Image>(respInsert.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.Title, respDto.Title);
                    Assert.Equal(reqDto.Description, respDto.Description);
                    Assert.Equal(reqDto.OriginUrl, respDto.OriginUrl);
                    Assert.Equal(reqDto.MaxWidth, respDto.MaxWidth);
                    Assert.Equal(reqDto.MaxHeight, respDto.MaxHeight);
                    Assert.Equal(reqDto.PriceAmount, respDto.PriceAmount);
                    Assert.Equal(reqDto.PriceCurrencyID, respDto.PriceCurrencyID);
                    Assert.Equal(reqDto.IsDeleted, respDto.IsDeleted);


                    respEntity = ImageConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void Image_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.Image testEntity = AddTestEntity();
                try
                {
                    testEntity.Title = "Title b0500654749842489440e915857ba600";
                    testEntity.Description = "Description b0500654749842489440e915857ba600";
                    testEntity.OriginUrl = "OriginUrl b0500654749842489440e915857ba600";
                    testEntity.MaxWidth = 948;
                    testEntity.MaxHeight = 948;
                    testEntity.PriceAmount = 948884.57M;
                    testEntity.PriceCurrencyID = 229;
                    testEntity.IsDeleted = false;
                    testEntity.CreatedByID = 100010;
                    testEntity.CreatedDate = DateTime.Parse("9/3/2021 8:03:35 PM");
                    testEntity.ModifiedByID = 100002;
                    testEntity.ModifiedDate = DateTime.Parse("9/3/2021 8:03:35 PM");

                    var reqDto = ImageConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/images/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    Image respDto = ExtractContentJson<Image>(respUpdate.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.Title, respDto.Title);
                    Assert.Equal(reqDto.Description, respDto.Description);
                    Assert.Equal(reqDto.OriginUrl, respDto.OriginUrl);
                    Assert.Equal(reqDto.MaxWidth, respDto.MaxWidth);
                    Assert.Equal(reqDto.MaxHeight, respDto.MaxHeight);
                    Assert.Equal(reqDto.PriceAmount, respDto.PriceAmount);
                    Assert.Equal(reqDto.PriceCurrencyID, respDto.PriceCurrencyID);
                    Assert.Equal(reqDto.IsDeleted, respDto.IsDeleted);

                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void Image_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.Image testEntity = CreateTestEntity();
                try
                {
                    testEntity.ID = Int64.MaxValue;
                    testEntity.Title = "Title b0500654749842489440e915857ba600";
                    testEntity.Description = "Description b0500654749842489440e915857ba600";
                    testEntity.OriginUrl = "OriginUrl b0500654749842489440e915857ba600";
                    testEntity.MaxWidth = 948;
                    testEntity.MaxHeight = 948;
                    testEntity.PriceAmount = 948884.57M;
                    testEntity.PriceCurrencyID = 229;
                    testEntity.IsDeleted = false;
                    testEntity.CreatedByID = 100010;
                    testEntity.CreatedDate = DateTime.Parse("9/3/2021 8:03:35 PM");
                    testEntity.ModifiedByID = 100002;
                    testEntity.ModifiedDate = DateTime.Parse("9/3/2021 8:03:35 PM");

                    var reqDto = ImageConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/images/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(PPT.Interfaces.Entities.Image entity)
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

        protected PPT.Interfaces.Entities.Image CreateTestEntity()
        {
            var entity = new PPT.Interfaces.Entities.Image();
            entity.Title = "Title 8f19715485bb4c3f844bdc7bfe33614d";
            entity.Description = "Description 8f19715485bb4c3f844bdc7bfe33614d";
            entity.OriginUrl = "OriginUrl 8f19715485bb4c3f844bdc7bfe33614d";
            entity.MaxWidth = 382;
            entity.MaxHeight = 382;
            entity.PriceAmount = 381608.62M;
            entity.PriceCurrencyID = 39;
            entity.IsDeleted = false;
            entity.CreatedByID = 100005;
            entity.CreatedDate = DateTime.Parse("1/18/2024 12:02:35 AM");
            entity.ModifiedByID = 100005;
            entity.ModifiedDate = DateTime.Parse("1/18/2024 12:02:35 AM");

            return entity;
        }

        protected PPT.Interfaces.Entities.Image AddTestEntity()
        {
            PPT.Interfaces.Entities.Image result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private PPT.Interfaces.IImageDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            PPT.Interfaces.IImageDal dal = new PPT.DAL.MSSQL.ImageDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
