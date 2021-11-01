using Microsoft.AspNetCore.Mvc.Testing;
using PhotoPrint.Test.E2E.API;
using PPT.DTO;
using PPT.Utils.Convertors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test.E2E.API.Controllers.V1
{
    public class TestRpcUserOperationsController : E2ETestBase, IClassFixture<WebApplicationFactory<PPT.PhotoPrint.API.Startup>>
    {
        public TestRpcUserOperationsController(WebApplicationFactory<PPT.PhotoPrint.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void User_Register_Success()
        {
            using (var client = _factory.CreateClient())
            {
                PPT.Interfaces.Entities.User testUserEntity = CreateTestUserEntity();
                PPT.Interfaces.Entities.Contact testContactEntity = CreateTestContactEntity();

                var reqRegister = new RegisterRequest();
                RegisterResponse respDto = null;

                try
                {
                    reqRegister.User = UserConvertor.Convert(testUserEntity, null);
                    reqRegister.Contact = ContactConvertor.Convert(testContactEntity, null);
                    reqRegister.User.Password = "Password {uuid}";

                    var content = CreateContentJson(reqRegister);

                    var respRegister = client.PostAsync($"/api/v1/users/register", content);

                    Assert.Equal(HttpStatusCode.Created, respRegister.Result.StatusCode);

                    respDto = ExtractContentJson<RegisterResponse>(respRegister.Result.Content);

                    Assert.NotNull(respDto);
                    Assert.NotNull(respDto.User);
                    Assert.NotNull(respDto.User.ID);
                    Assert.NotNull(respDto.Contact);
                    Assert.NotNull(respDto.Contact.ID);
                    Assert.Equal(reqRegister.User.Login, respDto.User.Login);
                    Assert.Equal(reqRegister.User.FirstName, respDto.User.FirstName);
                    Assert.Equal(reqRegister.User.MiddleName, respDto.User.MiddleName);
                    Assert.Equal(reqRegister.User.LastName, respDto.User.LastName);
                    Assert.Equal(reqRegister.User.FriendlyName, respDto.User.FriendlyName);
                    Assert.Equal(reqRegister.User.UserStatusID, respDto.User.UserStatusID);
                    Assert.Equal(reqRegister.User.UserTypeID, respDto.User.UserTypeID);
                    Assert.Equal(reqRegister.Contact.ContactTypeID, respDto.Contact.ContactTypeID);
                    Assert.Equal(reqRegister.Contact.Title, respDto.Contact.Title);
                    Assert.Equal(reqRegister.Contact.Comment, respDto.Contact.Comment);
                    Assert.Equal(reqRegister.Contact.Value, respDto.Contact.Value);
                    Assert.Equal(reqRegister.Contact.IsDeleted, respDto.Contact.IsDeleted);
                }
                finally
                {
                    var respContactEntity = ContactConvertor.Convert(respDto.Contact);
                    RemoveTestEntity(respContactEntity);

                    // TODO: Need To Implement "hard delete"
                    /*
                    var respUserEntity = UserConvertor.Convert(respDto.User);
                    RemoveTestEntity(respUserEntity);
                    */
                }
            }
        }

        [Fact]
        public void User_Register_LoginAlreadyExists()
        {
        }

        [Fact]
        public void User_Register_EmailAlreadyExists()
        {
        }

        [Fact]
        public void User_Register_InvalidUserTypeID()
        {
        }

        [Fact]
        public void User_Register_InvalidUserStatusID()
        {
        }

        [Fact]
        public void User_Login_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                Assert.NotNull(respLogin);                
                Assert.NotNull(respLogin.User);
                Assert.NotNull(respLogin.Token);
                Assert.NotNull(respLogin.User.ID);
                Assert.NotEqual(respLogin.Expires, DateTime.MinValue);
            }
        }

        [Fact]
        public void User_Login_InvalidLogin()
        {
            using (var client = _factory.CreateClient())
            {
                var dtoLogin = new PPT.DTO.LoginRequest()
                {
                    Login = (string)_testParams.Settings["test_invalid_login"],
                    Password = (string)_testParams.Settings["test_user_pwd"]
                };
                var content = CreateContentJson(dtoLogin);

                var respLogin = client.PostAsync($"/api/v1/users/login/", content);

                Assert.Equal(HttpStatusCode.NotFound, respLogin.Result.StatusCode);
            }
        }


        [Fact]
        public void User_Login_InvalidPassword()
        {
            using (var client = _factory.CreateClient())
            {
                var dtoLogin = new PPT.DTO.LoginRequest()
                {
                    Login = (string)_testParams.Settings["test_user_login"],
                    Password = (string)_testParams.Settings["test_invalid_pwd"]
                };
                var content = CreateContentJson(dtoLogin);

                var respLogin = client.PostAsync($"/api/v1/users/login/", content);

                Assert.Equal(HttpStatusCode.Forbidden, respLogin.Result.StatusCode);
            }
        }

        [Fact]
        public void User_Login_LoginEmpty()
        {
            using (var client = _factory.CreateClient())
            {
                var dtoLogin = new PPT.DTO.LoginRequest()
                {
                    Login = string.Empty,
                    Password = (string)_testParams.Settings["test_user_pwd"]
                };
                var content = CreateContentJson(dtoLogin);

                var respLogin = client.PostAsync($"/api/v1/users/login/", content);

                Assert.Equal(HttpStatusCode.NotFound, respLogin.Result.StatusCode);
            }
        }

        [Fact]
        public void User_Login_PasswordEmpty()
        {
            using (var client = _factory.CreateClient())
            {
                var dtoLogin = new PPT.DTO.LoginRequest()
                {
                    Login = (string)_testParams.Settings["test_user_login"],
                    Password = string.Empty
                };
                var content = CreateContentJson(dtoLogin);

                var respLogin = client.PostAsync($"/api/v1/users/login/", content);

                Assert.Equal(HttpStatusCode.InternalServerError, respLogin.Result.StatusCode);
            }
        }

        #region Support methods
        protected PPT.Interfaces.Entities.User CreateTestUserEntity()
        {
            var uuid = Guid.NewGuid().ToString();
            var entity = new PPT.Interfaces.Entities.User();
            entity.Login = $"Login {uuid}";         
            entity.FirstName = $"FirstName {uuid}";
            entity.MiddleName = $"MiddleName {uuid}";
            entity.LastName = $"LastName {uuid}";
            entity.FriendlyName = $"FriendlyName {uuid}";
            entity.UserStatusID = 1;
            entity.UserTypeID = 1;


            return entity;
        }

        protected PPT.Interfaces.Entities.Contact CreateTestContactEntity()
        {
            var uuid = Guid.NewGuid().ToString();
            var entity = new PPT.Interfaces.Entities.Contact();
            entity.ContactTypeID = 1;
            entity.Title = $"Test Register {uuid}";
            entity.Comment = $"Test Register {uuid}";
            entity.Value = $"Test Register {uuid}";
            entity.IsDeleted = false;

            return entity;
        }

        protected bool RemoveTestEntity(PPT.Interfaces.Entities.User entity)
        {
            if (entity != null && entity.ID != null)
            {
                var dal = CreateUserDal();

                return dal.Delete(entity.ID);
            }
            else
            {
                return false;
            }
        }

        protected bool RemoveTestEntity(PPT.Interfaces.Entities.Contact entity)
        {
            if (entity != null && entity.ID != null)
            {
                var dal = CreateContactDal();

                return dal.Delete(entity.ID);
            }
            else
            {
                return false;
            }
        }

        private PPT.Interfaces.IUserDal CreateUserDal()
        {
            var initParams = GetTestParams("DALInitParams");

            PPT.Interfaces.IUserDal dal = new PPT.DAL.MSSQL.UserDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }

        private PPT.Interfaces.IContactDal CreateContactDal()
        {
            var initParams = GetTestParams("DALInitParams");

            PPT.Interfaces.IContactDal dal = new PPT.DAL.MSSQL.ContactDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }

        protected PPT.Interfaces.Entities.User AddTestEntity()
        {
            PPT.Interfaces.Entities.User result = null;

            var entity = CreateTestUserEntity();

            var dal = CreateUserDal();
            result = dal.Insert(entity);

            return result;
        }

        #endregion
    }
}
