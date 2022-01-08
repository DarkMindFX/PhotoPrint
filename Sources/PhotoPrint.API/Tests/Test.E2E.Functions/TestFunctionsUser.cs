using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using PPT.Utils.Convertors;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Test.Functions.Common;

namespace Test.E2E.Functions.User
{
    public class TestUserFunctions : FunctionTestBase
    {
        private readonly ILogger logger = TestFactory.CreateLogger();

        public TestUserFunctions()
        {
            _testParams = GetTestParams("GenericFunctionTestSettings");
        }

        [SetUp]
        public void Setup()
        {
            var initParams = GetTestParams("DALInitParams");

            // Function replies on env vars for config
            Environment.SetEnvironmentVariable(PPT.Functions.Common.Constants.ENV_DAL_TYPE, _testParams.Settings["DALType"].ToString());
            Environment.SetEnvironmentVariable(PPT.Functions.Common.Constants.ENV_SQL_CONNECTION_STRING, (string)initParams.Settings["ConnectionString"]);
            Environment.SetEnvironmentVariable(PPT.Functions.Common.Constants.ENV_JWT_SECRET, (string)_testParams.Settings["JWTSecret"]);
            Environment.SetEnvironmentVariable(PPT.Functions.Common.Constants.ENV_SESSION_TIMEOUT, (string)_testParams.Settings["JWTSessionTimeout"]);
        }

        [Test]
        public async Task UsersGetAll_Success()
        {
            var request = TestFactory.CreateHttpRequest();
            var response = (ObjectResult)await(new PPT.Functions.User.V1.GetAll()).Run(request, logger);

            Assert.IsNotNull(response);
            Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);

            var dtos = JsonSerializer.Deserialize<List<PPT.DTO.User>>(response.Value.ToString());

            Assert.NotNull(dtos);
            Assert.IsNotEmpty(dtos);
        }

        [Test]
        public async Task UsersGetDetails_Success()
        {
            PPT.Interfaces.Entities.User testEntity = AddTestEntity();

            try
            {
                var request = TestFactory.CreateHttpRequest();
                var response = (ObjectResult)await(new PPT.Functions.User.V1.GetDetails()).Run(request, (long)testEntity.ID, logger);

                Assert.IsNotNull(response);
                Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);

                var dto = JsonSerializer.Deserialize<PPT.DTO.User>(response.Value.ToString());

                Assert.NotNull(dto);
                Assert.AreEqual(testEntity.ID, dto.ID);
            }
            finally
            {
                RemoveTestEntity(testEntity);
            }

        }

        [Test]
        public async Task UsersGetDetails_NotFound()
        {
            long id = Int64.MaxValue - 1;
            var request = TestFactory.CreateHttpRequest();
            var response = (ObjectResult)await(new PPT.Functions.User.V1.GetDetails()).Run(request, id, logger);

            Assert.IsNotNull(response);
            Assert.AreEqual((int)HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task UsersDelete_Success()
        {
            PPT.Interfaces.Entities.User testEntity = AddTestEntity();

            try
            {
                var request = TestFactory.CreateHttpRequest();
                var response = (StatusCodeResult)await(new PPT.Functions.User.V1.Delete()).Run(request, (long)testEntity.ID, logger);

                Assert.IsNotNull(response);
                Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);
            }
            finally
            {
                RemoveTestEntity(testEntity);
            }
        }

        [Test]
        public async Task UsersDelete_NotFound()
        {
            long id = Int64.MaxValue - 1;
            var request = TestFactory.CreateHttpRequest();
            var response = (ObjectResult)await(new PPT.Functions.User.V1.Delete()).Run(request, id, logger);

            Assert.IsNotNull(response);
            Assert.AreEqual((int)HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task UsersInsert_Success()
        {
            PPT.Interfaces.Entities.User testEntity = CreateTestEntity();

            try
            {
                var dtoReq = PPT.Utils.Convertors.UserConvertor.Convert(testEntity, null);
                dtoReq.Password = Guid.NewGuid().ToString();
                var request = TestFactory.CreateHttpRequest(dtoReq);
                var response = (ObjectResult)await(new PPT.Functions.User.V1.Insert()).Run(request, logger);

                Assert.IsNotNull(response);
                Assert.AreEqual((int)HttpStatusCode.Created, response.StatusCode);

                var dto = JsonSerializer.Deserialize<PPT.DTO.User>(response.Value.ToString());

                Assert.NotNull(dto);

                testEntity.ID = dto.ID;

                Assert.NotNull(dto.ID);
            }
            finally
            {
                RemoveTestEntity(testEntity);
            }
        }

        [Test]
        public async Task UsersUpdate_Success()
        {
            PPT.Interfaces.Entities.User testEntity = AddTestEntity();

            try
            {
                testEntity.Login = "Login 6d3dfed7c6874e808ac01e4bf718274a";
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

                var request = TestFactory.CreateHttpRequest(reqDto);
                var response = (ObjectResult)await(new PPT.Functions.User.V1.Update()).Run(request, logger);

                Assert.IsNotNull(response);
                Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);
            }
            finally
            {
                RemoveTestEntity(testEntity);
            }
        }

        [Test]
        public async Task UsersUpdate_NotFound()
        {
            PPT.Interfaces.Entities.User testEntity = CreateTestEntity();

            try
            {
                testEntity.ID = Int64.MaxValue - 1;
                testEntity.Login = "Login 6d3dfed7c6874e808ac01e4bf718274a";
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

                var request = TestFactory.CreateHttpRequest(reqDto);
                var response = (ObjectResult)await(new PPT.Functions.User.V1.Update()).Run(request, logger);

                Assert.IsNotNull(response);
                Assert.AreEqual((int)HttpStatusCode.NotFound, response.StatusCode);
            }
            finally
            {
                RemoveTestEntity(testEntity);
            }
        }

        [Test]
        public async Task UsersLogin_Success()
        {
            var dtoReq = new PPT.DTO.LoginRequest()
            {
                Login = _testParams.Settings["test_user_login"].ToString(),
                Password = _testParams.Settings["test_user_pwd"].ToString()
            };

            var request = TestFactory.CreateHttpRequest(dtoReq);
            var response = (ObjectResult)await(new PPT.Functions.User.V1.Login()).Run(request, logger);

            Assert.NotNull(response);
            Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);

            var dtoResp = JsonSerializer.Deserialize<PPT.DTO.LoginResponse>(response.Value.ToString());

            Assert.IsNotNull(dtoResp.Token);
            Assert.IsNotEmpty(dtoResp.Token);
            Assert.AreNotEqual(DateTime.MinValue, dtoResp.Expires);
            Assert.IsNotNull(dtoResp.User);
            Assert.IsNotNull(dtoResp.User.ID);

        }

        [Test]
        public async Task UsersLogin_InvalidLogin()
        {
            var dtoReq = new PPT.DTO.LoginRequest()
            {
                Login = _testParams.Settings["test_invalid_login"].ToString(),
                Password = _testParams.Settings["test_user_pwd"].ToString()
            };

            var request = TestFactory.CreateHttpRequest(dtoReq);
            var response = (ObjectResult)await(new PPT.Functions.User.V1.Login()).Run(request, logger);

            Assert.NotNull(response);
            Assert.AreEqual((int)HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task UsersLogin_InvalidPassword()
        {
            var dtoReq = new PPT.DTO.LoginRequest()
            {
                Login = _testParams.Settings["test_user_login"].ToString(),
                Password = _testParams.Settings["test_invalid_pwd"].ToString()
            };

            var request = TestFactory.CreateHttpRequest(dtoReq);
            var response = (ObjectResult)await( new PPT.Functions.User.V1.Login()).Run(request, logger);

            Assert.NotNull(response);
            Assert.AreEqual((int)HttpStatusCode.Forbidden, response.StatusCode);
        }

        #region Support methods

        protected bool RemoveTestEntity(PPT.Interfaces.Entities.User entity)
        {
            if (entity != null)
            {
                var dal = CreateDal();

                return dal.Delete(entity.ID);
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

            PPT.Interfaces.IUserDal dal = new PPT.DAL.MSSQL.UserDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = Environment.GetEnvironmentVariable("ServiceConfig__DalInitParams__ConnectionString");
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}