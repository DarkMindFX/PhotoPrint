

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
    public class TestUsersController : E2ETestBase, IClassFixture<WebApplicationFactory<PPT.PhotoPrint.API.Startup>>
    {
        public TestUsersController(WebApplicationFactory<PPT.PhotoPrint.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void User_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var respGetAll = client.GetAsync($"/api/v1/users");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<User> dtos = ExtractContentJson<List<User>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void User_Get_Success()
        {
            PPT.Interfaces.Entities.User testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                    var paramID = testEntity.ID;
                    var respGet = client.GetAsync($"/api/v1/users/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respGet.Result.StatusCode);

                    User dto = ExtractContentJson<User>(respGet.Result.Content);

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
        public void User_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/users/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void User_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                    var paramID = testEntity.ID;

                    var respDel = client.DeleteAsync($"/api/v1/users/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void User_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/users/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void User_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.User testEntity = CreateTestEntity();
                PPT.Interfaces.Entities.User respEntity = null;
                try
                {
                    var reqDto = UserConvertor.Convert(testEntity, null);
                    reqDto.Password = "Password1991! 28a2e433081f4289b9caccf286ffc6a9";

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/users/", content);

                    Assert.Equal(HttpStatusCode.Created, respInsert.Result.StatusCode);

                    User respDto = ExtractContentJson<User>(respInsert.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.Login, respDto.Login);
                    Assert.Equal(reqDto.Salt, respDto.Salt);
                    Assert.Equal(reqDto.FirstName, respDto.FirstName);
                    Assert.Equal(reqDto.MiddleName, respDto.MiddleName);
                    Assert.Equal(reqDto.LastName, respDto.LastName);
                    Assert.Equal(reqDto.FriendlyName, respDto.FriendlyName);
                    Assert.Equal(reqDto.UserStatusID, respDto.UserStatusID);
                    Assert.Equal(reqDto.UserTypeID, respDto.UserTypeID);
                    Assert.Equal(reqDto.CreatedDate, respDto.CreatedDate);
                    Assert.Equal(reqDto.ModifiedDate, respDto.ModifiedDate);
                    Assert.Equal(reqDto.ModifiedByID, respDto.ModifiedByID);

                    respEntity = UserConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void User_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.User testEntity = AddTestEntity();
                try
                {
                    testEntity.Login = "Login 28a2e433081f4289b9caccf286ffc6a9";
                    testEntity.PwdHash = "PwdHash 28a2e433081f4289b9caccf286ffc6a9";
                    testEntity.Salt = "Salt 28a2e433081f4289b9caccf286ffc6a9";
                    testEntity.FirstName = "FirstName 28a2e433081f4289b9caccf286ffc6a9";
                    testEntity.MiddleName = "MiddleName 28a2e433081f4289b9caccf286ffc6a9";
                    testEntity.LastName = "LastName 28a2e433081f4289b9caccf286ffc6a9";
                    testEntity.FriendlyName = "FriendlyName 28a2e433081f4289b9caccf286ffc6a9";
                    testEntity.UserStatusID = 3;
                    testEntity.UserTypeID = 2;

                    var reqDto = UserConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/users/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    User respDto = ExtractContentJson<User>(respUpdate.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.Login, respDto.Login);
                    Assert.Equal(reqDto.Salt, respDto.Salt);
                    Assert.Equal(reqDto.FirstName, respDto.FirstName);
                    Assert.Equal(reqDto.MiddleName, respDto.MiddleName);
                    Assert.Equal(reqDto.LastName, respDto.LastName);
                    Assert.Equal(reqDto.FriendlyName, respDto.FriendlyName);
                    Assert.Equal(reqDto.UserStatusID, respDto.UserStatusID);
                    Assert.Equal(reqDto.UserTypeID, respDto.UserTypeID);
                    Assert.Equal(reqDto.CreatedDate, respDto.CreatedDate);
                    Assert.True(DateTime.UtcNow - TimeSpan.FromMinutes(1) < respDto.ModifiedDate);
                    Assert.Equal(respLogin.User.ID, respDto.ModifiedByID);

                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void User_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.User testEntity = CreateTestEntity();
                try
                {
                    testEntity.ID = Int64.MaxValue;
                    testEntity.Login = "Login 28a2e433081f4289b9caccf286ffc6a9";
                    testEntity.PwdHash = "PwdHash 28a2e433081f4289b9caccf286ffc6a9";
                    testEntity.Salt = "Salt 28a2e433081f4289b9caccf286ffc6a9";
                    testEntity.FirstName = "FirstName 28a2e433081f4289b9caccf286ffc6a9";
                    testEntity.MiddleName = "MiddleName 28a2e433081f4289b9caccf286ffc6a9";
                    testEntity.LastName = "LastName 28a2e433081f4289b9caccf286ffc6a9";
                    testEntity.FriendlyName = "FriendlyName 28a2e433081f4289b9caccf286ffc6a9";
                    testEntity.UserStatusID = 3;
                    testEntity.UserTypeID = 2;
                    testEntity.CreatedDate = DateTime.Parse("10/9/2020 9:19:53 AM");
                    testEntity.ModifiedDate = DateTime.Parse("10/9/2020 9:19:53 AM");
                    testEntity.ModifiedByID = 311798;

                    var reqDto = UserConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/users/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(PPT.Interfaces.Entities.User entity)
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

        protected PPT.Interfaces.Entities.User CreateTestEntity()
        {
            var entity = new PPT.Interfaces.Entities.User();
            entity.Login = "Login 54fae047f37b41ac92363a23e7cf7fec";
            entity.PwdHash = "PwdHash 54fae047f37b41ac92363a23e7cf7fec";
            entity.Salt = "Salt 54fae047f37b41ac92363a23e7cf7fec";
            entity.FirstName = "FirstName 54fae047f37b41ac92363a23e7cf7fec";
            entity.MiddleName = "MiddleName 54fae047f37b41ac92363a23e7cf7fec";
            entity.LastName = "LastName 54fae047f37b41ac92363a23e7cf7fec";
            entity.FriendlyName = "FriendlyName 54fae047f37b41ac92363a23e7cf7fec";
            entity.UserStatusID = 4;
            entity.UserTypeID = 1;
            entity.CreatedDate = DateTime.Parse("2/22/2023 1:19:53 PM");
            entity.ModifiedDate = DateTime.Parse("2/22/2023 1:19:53 PM");
            entity.ModifiedByID = 100001;

            return entity;
        }

        protected PPT.Interfaces.Entities.User AddTestEntity()
        {
            PPT.Interfaces.Entities.User result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private PPT.Interfaces.IUserDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            PPT.Interfaces.IUserDal dal = new PPT.DAL.MSSQL.UserDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
