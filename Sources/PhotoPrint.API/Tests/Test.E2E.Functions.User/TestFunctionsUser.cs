using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
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
            Environment.SetEnvironmentVariable("ServiceConfig__DALType", _testParams.Settings["DALType"].ToString());
            Environment.SetEnvironmentVariable("ServiceConfig__DalInitParams__ConnectionString", (string)initParams.Settings["ConnectionString"]);
        }

        [Test]
        public async Task UsersGetAll_Success()
        {
            var request = TestFactory.CreateHttpRequest();
            var response = (ObjectResult)await PPT.Functions.User.V1.GetAll.Run(request, logger);

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
                var response = (ObjectResult)await PPT.Functions.User.V1.GetDetails.Run(request, (long)testEntity.ID, logger);

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
            var response = (ObjectResult)await PPT.Functions.User.V1.GetDetails.Run(request, id, logger);

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
                var response = (StatusCodeResult)await PPT.Functions.User.V1.Delete.Run(request, (long)testEntity.ID, logger);

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
            var response = (ObjectResult)await PPT.Functions.User.V1.Delete.Run(request, id, logger);

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
                var response = (ObjectResult)await PPT.Functions.User.V1.Insert.Run(request, logger);

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