

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
    public class TestFrameTypesController : E2ETestBase, IClassFixture<WebApplicationFactory<PPT.PhotoPrint.API.Startup>>
    {
        public TestFrameTypesController(WebApplicationFactory<PPT.PhotoPrint.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void FrameType_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var respGetAll = client.GetAsync($"/api/v1/frametypes");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<FrameType> dtos = ExtractContentJson<List<FrameType>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void FrameType_Get_Success()
        {
            PPT.Interfaces.Entities.FrameType testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                    var paramID = testEntity.ID;
                    var respGet = client.GetAsync($"/api/v1/frametypes/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respGet.Result.StatusCode);

                    FrameType dto = ExtractContentJson<FrameType>(respGet.Result.Content);

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
        public void FrameType_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/frametypes/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void FrameType_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                    var paramID = testEntity.ID;

                    var respDel = client.DeleteAsync($"/api/v1/frametypes/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void FrameType_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/frametypes/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void FrameType_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.FrameType testEntity = CreateTestEntity();
                PPT.Interfaces.Entities.FrameType respEntity = null;
                try
                {
                    var reqDto = FrameTypeConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/frametypes/", content);

                    Assert.Equal(HttpStatusCode.Created, respInsert.Result.StatusCode);

                    FrameType respDto = ExtractContentJson<FrameType>(respInsert.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.FrameTypeName, respDto.FrameTypeName);
                    Assert.Equal(reqDto.Description, respDto.Description);
                    Assert.Equal(reqDto.ThumbnailUrl, respDto.ThumbnailUrl);
                    Assert.Equal(reqDto.IsDeleted, respDto.IsDeleted);

                    respEntity = FrameTypeConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void FrameType_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.FrameType testEntity = AddTestEntity();
                try
                {
                    testEntity.FrameTypeName = "FrameTypeName 082dfd8ae6b34fe8ac132b64605183d9";
                    testEntity.Description = "Description 082dfd8ae6b34fe8ac132b64605183d9";
                    testEntity.ThumbnailUrl = "ThumbnailUrl 082dfd8ae6b34fe8ac132b64605183d9";
                    testEntity.IsDeleted = false;
                    testEntity.CreatedDate = DateTime.Parse("12/15/2019 4:27:35 PM");
                    testEntity.CreatedByID = 100007;
                    testEntity.ModifiedDate = DateTime.Parse("3/15/2020 12:01:35 PM");
                    testEntity.ModifiedByID = 100008;

                    var reqDto = FrameTypeConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/frametypes/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    FrameType respDto = ExtractContentJson<FrameType>(respUpdate.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.FrameTypeName, respDto.FrameTypeName);
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
        public void FrameType_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.FrameType testEntity = CreateTestEntity();
                try
                {
                    testEntity.ID = Int64.MaxValue;
                    testEntity.FrameTypeName = "FrameTypeName 082dfd8ae6b34fe8ac132b64605183d9";
                    testEntity.Description = "Description 082dfd8ae6b34fe8ac132b64605183d9";
                    testEntity.ThumbnailUrl = "ThumbnailUrl 082dfd8ae6b34fe8ac132b64605183d9";
                    testEntity.IsDeleted = false;
                    testEntity.CreatedDate = DateTime.Parse("12/15/2019 4:27:35 PM");
                    testEntity.CreatedByID = 100007;
                    testEntity.ModifiedDate = DateTime.Parse("3/15/2020 12:01:35 PM");
                    testEntity.ModifiedByID = 100008;

                    var reqDto = FrameTypeConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/frametypes/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(PPT.Interfaces.Entities.FrameType entity)
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

        protected PPT.Interfaces.Entities.FrameType CreateTestEntity()
        {
            var entity = new PPT.Interfaces.Entities.FrameType();
            entity.FrameTypeName = "FrameTypeName 3a82267f0a454e718a878bee690675bb";
            entity.Description = "Description 3a82267f0a454e718a878bee690675bb";
            entity.ThumbnailUrl = "ThumbnailUrl 3a82267f0a454e718a878bee690675bb";
            entity.IsDeleted = true;
            entity.CreatedDate = DateTime.Parse("4/29/2022 8:27:35 PM");
            entity.CreatedByID = 100007;
            entity.ModifiedDate = DateTime.Parse("9/18/2019 6:14:35 AM");
            entity.ModifiedByID = 100009;

            return entity;
        }

        protected PPT.Interfaces.Entities.FrameType AddTestEntity()
        {
            PPT.Interfaces.Entities.FrameType result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private PPT.Interfaces.IFrameTypeDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            PPT.Interfaces.IFrameTypeDal dal = new PPT.DAL.MSSQL.FrameTypeDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
