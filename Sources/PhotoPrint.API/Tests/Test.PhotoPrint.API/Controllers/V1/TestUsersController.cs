

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

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/users/", content);

                    Assert.Equal(HttpStatusCode.Created, respInsert.Result.StatusCode);

                    User respDto = ExtractContentJson<User>(respInsert.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.Login, respDto.Login);
                    Assert.Equal(reqDto.FirstName, respDto.FirstName);
                    Assert.Equal(reqDto.MiddleName, respDto.MiddleName);
                    Assert.Equal(reqDto.LastName, respDto.LastName);
                    Assert.Equal(reqDto.FriendlyName, respDto.FriendlyName);
                    Assert.Equal(reqDto.UserStatusID, respDto.UserStatusID);
                    Assert.Equal(reqDto.UserTypeID, respDto.UserTypeID);


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
                    testEntity.Login = "Login f6256fcfdd8e41398139ddcb9f648fcc";
                    testEntity.FirstName = "FirstName f6256fcfdd8e41398139ddcb9f648fcc";
                    testEntity.MiddleName = "MiddleName f6256fcfdd8e41398139ddcb9f648fcc";
                    testEntity.LastName = "LastName f6256fcfdd8e41398139ddcb9f648fcc";
                    testEntity.FriendlyName = "FriendlyName f6256fcfdd8e41398139ddcb9f648fcc";
                    testEntity.UserStatusID = 3;
                    testEntity.UserTypeID = 1;

                    var reqDto = UserConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/users/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    User respDto = ExtractContentJson<User>(respUpdate.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.Login, respDto.Login);
                    Assert.Equal(reqDto.FirstName, respDto.FirstName);
                    Assert.Equal(reqDto.MiddleName, respDto.MiddleName);
                    Assert.Equal(reqDto.LastName, respDto.LastName);
                    Assert.Equal(reqDto.FriendlyName, respDto.FriendlyName);
                    Assert.Equal(reqDto.UserStatusID, respDto.UserStatusID);
                    Assert.Equal(reqDto.UserTypeID, respDto.UserTypeID);

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
                    testEntity.Login = "Login f6256fcfdd8e41398139ddcb9f648fcc";
                    testEntity.FirstName = "FirstName f6256fcfdd8e41398139ddcb9f648fcc";
                    testEntity.MiddleName = "MiddleName f6256fcfdd8e41398139ddcb9f648fcc";
                    testEntity.LastName = "LastName f6256fcfdd8e41398139ddcb9f648fcc";
                    testEntity.FriendlyName = "FriendlyName f6256fcfdd8e41398139ddcb9f648fcc";
                    testEntity.UserStatusID = 3;
                    testEntity.UserTypeID = 1;
                    testEntity.CreatedDate = DateTime.Parse("3/17/2022 1:05:35 AM");
                    testEntity.ModifiedDate = DateTime.Parse("3/17/2022 1:05:35 AM");
                    testEntity.ModifiedByID = 100004;

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
            entity.Login = "Login 0ea703a12238492ebe51723d5764d15d";
            entity.PwdHash = "PwdHash 0ea703a12238492ebe51723d5764d15d";
            entity.Salt = "Salt 0ea703a12238492ebe51723d5764d15d";
            entity.FirstName = "FirstName 0ea703a12238492ebe51723d5764d15d";
            entity.MiddleName = "MiddleName 0ea703a12238492ebe51723d5764d15d";
            entity.LastName = "LastName 0ea703a12238492ebe51723d5764d15d";
            entity.FriendlyName = "FriendlyName 0ea703a12238492ebe51723d5764d15d";
            entity.UserStatusID = 3;
            entity.UserTypeID = 3;
            entity.CreatedDate = DateTime.Parse("2/7/2019 5:04:35 AM");
            entity.ModifiedDate = DateTime.Parse("2/7/2019 5:04:35 AM");
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
