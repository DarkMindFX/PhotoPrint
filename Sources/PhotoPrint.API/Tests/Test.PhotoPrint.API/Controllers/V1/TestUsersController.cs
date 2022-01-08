


using PPT.DTO;
using PPT.Utils.Convertors;
using PPT.Test.E2E.API;
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
                    testEntity.Login = "Login 6d3dfed7c6874e808ac01e4bf718274a";
                    testEntity.PwdHash = "PwdHash 6d3dfed7c6874e808ac01e4bf718274a";
                    testEntity.Salt = "Salt 6d3dfed7c6874e808ac01e4bf718274a";
                    testEntity.FirstName = "FirstName 6d3dfed7c6874e808ac01e4bf718274a";
                    testEntity.MiddleName = "MiddleName 6d3dfed7c6874e808ac01e4bf718274a";
                    testEntity.LastName = "LastName 6d3dfed7c6874e808ac01e4bf718274a";
                    testEntity.FriendlyName = "FriendlyName 6d3dfed7c6874e808ac01e4bf718274a";
                    testEntity.UserStatusID = 3;
                    testEntity.UserTypeID = 2;
                    testEntity.CreatedDate = DateTime.Parse("5/18/2019 7:15:38 AM");
                    testEntity.ModifiedDate = DateTime.Parse("5/18/2019 7:15:38 AM");
                    testEntity.ModifiedByID = 100004;

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
                    testEntity.Login = "Login 6d3dfed7c6874e808ac01e4bf718274a";
                    testEntity.PwdHash = "PwdHash 6d3dfed7c6874e808ac01e4bf718274a";
                    testEntity.Salt = "Salt 6d3dfed7c6874e808ac01e4bf718274a";
                    testEntity.FirstName = "FirstName 6d3dfed7c6874e808ac01e4bf718274a";
                    testEntity.MiddleName = "MiddleName 6d3dfed7c6874e808ac01e4bf718274a";
                    testEntity.LastName = "LastName 6d3dfed7c6874e808ac01e4bf718274a";
                    testEntity.FriendlyName = "FriendlyName 6d3dfed7c6874e808ac01e4bf718274a";
                    testEntity.UserStatusID = 3;
                    testEntity.UserTypeID = 2;
                    testEntity.CreatedDate = DateTime.Parse("5/18/2019 7:15:38 AM");
                    testEntity.ModifiedDate = DateTime.Parse("5/18/2019 7:15:38 AM");
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



                return dal.Delete(entity.ID
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
            entity.Login = "Login 15ec8c1dd9ab44ea9a21f020cbb0b419";
            entity.PwdHash = "PwdHash 15ec8c1dd9ab44ea9a21f020cbb0b419";
            entity.Salt = "Salt 15ec8c1dd9ab44ea9a21f020cbb0b419";
            entity.FirstName = "FirstName 15ec8c1dd9ab44ea9a21f020cbb0b419";
            entity.MiddleName = "MiddleName 15ec8c1dd9ab44ea9a21f020cbb0b419";
            entity.LastName = "LastName 15ec8c1dd9ab44ea9a21f020cbb0b419";
            entity.FriendlyName = "FriendlyName 15ec8c1dd9ab44ea9a21f020cbb0b419";
            entity.UserStatusID = 2;
            entity.UserTypeID = 4;
            entity.CreatedDate = DateTime.Parse("5/18/2023 1:53:38 PM");
            entity.ModifiedDate = DateTime.Parse("5/18/2023 1:53:38 PM");
            entity.ModifiedByID = 100011;

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
