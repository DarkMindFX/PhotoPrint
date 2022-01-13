
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using PPT.Utils.Convertors;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using PPT.Test.Functions.Common;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace PPT.Test.E2E.Functions
{
    public class TestAddressFunctions : FunctionTestBase
    {
        private readonly ILogger _logger = TestFactory.CreateLogger();
        private PPT.Functions.Address.Startup _startup;
        private IHost _host;


        public TestAddressFunctions()
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

            _startup = new PPT.Functions.Address.Startup();
            _host = new HostBuilder()
                .ConfigureWebJobs(_startup.Configure)
                .Build();
        }

        [Test]
        public async Task AddressesGetAll_Success()
        {
            var request = TestFactory.CreateHttpRequest();

            var function = GetFunction<PPT.Functions.Address.V1.GetAll>(_host);

            var response = (ObjectResult)await function.Run(request, _logger);

            Assert.IsNotNull(response);
            Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);

            var dtos = JsonSerializer.Deserialize<List<PPT.DTO.Address>>(response.Value.ToString());

            Assert.NotNull(dtos);
            Assert.IsNotEmpty(dtos);
        }

        [Test]
        public async Task AddressesGetDetails_Success()
        {
            PPT.Interfaces.Entities.Address testEntity = AddTestEntity();

            try
            {
                var request = TestFactory.CreateHttpRequest();
                var response = (ObjectResult)await (GetFunction<PPT.Functions.Address.V1.GetDetails>(_host)).Run(request,
                    testEntity.ID,
                    _logger);

                Assert.IsNotNull(response);
                Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);

                var dto = JsonSerializer.Deserialize<PPT.DTO.Address>(response.Value.ToString());

                Assert.NotNull(dto);
                Assert.AreEqual(testEntity.ID, dto.ID);
            }
            finally
            {
                RemoveTestEntity(testEntity);
            }

        }

        [Test]
        public async Task AddressesGetDetails_NotFound()
        {
            var paramID = Int64.MaxValue;
            var request = TestFactory.CreateHttpRequest();
            var response = (ObjectResult)await (GetFunction<PPT.Functions.Address.V1.GetDetails>(_host)).Run(request,
                paramID,
            _logger);

            Assert.IsNotNull(response);
            Assert.AreEqual((int)HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task AddressesDelete_Success()
        {
            PPT.Interfaces.Entities.Address testEntity = AddTestEntity();

            try
            {
                var request = TestFactory.CreateHttpRequest();
                var response = (StatusCodeResult)await (GetFunction<PPT.Functions.Address.V1.Delete>(_host)).Run(request,
                                    testEntity.ID,

                _logger);

                Assert.IsNotNull(response);
                Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);
            }
            finally
            {
                RemoveTestEntity(testEntity);
            }
        }

        [Test]
        public async Task AddressesDelete_NotFound()
        {
            var paramID = Int64.MaxValue;
            var request = TestFactory.CreateHttpRequest();
            var response = (ObjectResult)await (GetFunction<PPT.Functions.Address.V1.Delete>(_host)).Run(request,
                paramID,
                _logger);

            Assert.IsNotNull(response);
            Assert.AreEqual((int)HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task AddressesInsert_Success()
        {
            PPT.Interfaces.Entities.Address testEntity = CreateTestEntity();

            try
            {
                var dtoReq = PPT.Utils.Convertors.AddressConvertor.Convert(testEntity, null);

                var request = TestFactory.CreateHttpRequest(dtoReq);
                var response = (ObjectResult)await (GetFunction<PPT.Functions.Address.V1.Insert>(_host)).Run(request, _logger);

                Assert.IsNotNull(response);
                Assert.AreEqual((int)HttpStatusCode.Created, response.StatusCode);

                var dto = JsonSerializer.Deserialize<PPT.DTO.Address>(response.Value.ToString());

                Assert.NotNull(dto);

                testEntity.ID = dto.ID;

                Assert.NotNull(dto.ID);
                Assert.AreEqual(dtoReq.AddressTypeID, dto.AddressTypeID);
                Assert.AreEqual(dtoReq.Title, dto.Title);
                Assert.AreEqual(dtoReq.CityID, dto.CityID);
                Assert.AreEqual(dtoReq.Street, dto.Street);
                Assert.AreEqual(dtoReq.BuildingNo, dto.BuildingNo);
                Assert.AreEqual(dtoReq.ApartmentNo, dto.ApartmentNo);
                Assert.AreEqual(dtoReq.Comment, dto.Comment);
                Assert.AreEqual(dtoReq.CreatedByID, dto.CreatedByID);
                Assert.AreEqual(dtoReq.ModifiedByID, dto.ModifiedByID);
                Assert.AreEqual(dtoReq.ModifiedDate, dto.ModifiedDate);
                Assert.AreEqual(dtoReq.IsDeleted, dto.IsDeleted);
            }
            finally
            {
                RemoveTestEntity(testEntity);
            }
        }

        [Test]
        public async Task AddressesUpdate_Success()
        {
            PPT.Interfaces.Entities.Address testEntity = AddTestEntity();

            try
            {
                testEntity.AddressTypeID = 5;
                testEntity.Title = "Title d72a19136b8d4f6281426a14de928e42";
                testEntity.CityID = 1;
                testEntity.Street = "Street d72a19136b8d4f6281426a14de928e42";
                testEntity.BuildingNo = "BuildingNo d72a19136b8d4f6281426a14de928e42";
                testEntity.ApartmentNo = "ApartmentNo d72a19136b8d4f6281426a14de928e42";
                testEntity.Comment = "Comment d72a19136b8d4f6281426a14de928e42";
                testEntity.CreatedByID = 100002;
                testEntity.CreatedDate = DateTime.Parse("5/12/2022 7:09:35 AM");
                testEntity.ModifiedByID = 100006;
                testEntity.ModifiedDate = DateTime.Parse("5/12/2022 7:09:35 AM");
                testEntity.IsDeleted = false;

                var reqDto = AddressConvertor.Convert(testEntity, null);

                var request = TestFactory.CreateHttpRequest(reqDto);
                var response = (ObjectResult)await (GetFunction<PPT.Functions.Address.V1.Update>(_host)).Run(request, _logger);

                Assert.IsNotNull(response);
                Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);

                var dto = JsonSerializer.Deserialize<PPT.DTO.Address>(response.Value.ToString());

                Assert.NotNull(dto.ID);
                Assert.AreEqual(reqDto.AddressTypeID, dto.AddressTypeID);
                Assert.AreEqual(reqDto.Title, dto.Title);
                Assert.AreEqual(reqDto.CityID, dto.CityID);
                Assert.AreEqual(reqDto.Street, dto.Street);
                Assert.AreEqual(reqDto.BuildingNo, dto.BuildingNo);
                Assert.AreEqual(reqDto.ApartmentNo, dto.ApartmentNo);
                Assert.AreEqual(reqDto.Comment, dto.Comment);
                Assert.AreEqual(reqDto.IsDeleted, dto.IsDeleted);
            }
            finally
            {
                RemoveTestEntity(testEntity);
            }
        }

        [Test]
        public async Task AddressesUpdate_NotFound()
        {
            PPT.Interfaces.Entities.Address testEntity = CreateTestEntity();

            try
            {
                testEntity.ID = Int64.MaxValue;
                testEntity.AddressTypeID = 5;
                testEntity.Title = "Title d72a19136b8d4f6281426a14de928e42";
                testEntity.CityID = 1;
                testEntity.Street = "Street d72a19136b8d4f6281426a14de928e42";
                testEntity.BuildingNo = "BuildingNo d72a19136b8d4f6281426a14de928e42";
                testEntity.ApartmentNo = "ApartmentNo d72a19136b8d4f6281426a14de928e42";
                testEntity.Comment = "Comment d72a19136b8d4f6281426a14de928e42";
                testEntity.CreatedByID = 100002;
                testEntity.CreatedDate = DateTime.Parse("5/12/2022 7:09:35 AM");
                testEntity.ModifiedByID = 100006;
                testEntity.ModifiedDate = DateTime.Parse("5/12/2022 7:09:35 AM");
                testEntity.IsDeleted = false;

                var reqDto = AddressConvertor.Convert(testEntity, null);

                var request = TestFactory.CreateHttpRequest(reqDto);
                var response = (ObjectResult)await (GetFunction<PPT.Functions.Address.V1.Update>(_host)).Run(request, _logger);

                Assert.IsNotNull(response);
                Assert.AreEqual((int)HttpStatusCode.NotFound, response.StatusCode);
            }
            finally
            {
                RemoveTestEntity(testEntity);
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(PPT.Interfaces.Entities.Address entity)
        {
            if (entity != null)
            {
                var dal = CreateDal();


                return dal.Erase(entity.ID
                        );
            }
            else
            {
                return false;
            }
        }

        protected PPT.Interfaces.Entities.Address CreateTestEntity()
        {
            var entity = new PPT.Interfaces.Entities.Address();
            entity.AddressTypeID = 1;
            entity.Title = "Title 7c87544f409d45d8bdd430bd786da7d7";
            entity.CityID = 10;
            entity.Street = "Street 7c87544f409d45d8bdd430bd786da7d7";
            entity.BuildingNo = "BuildingNo 7c87544f409d45d8bdd430bd786da7d7";
            entity.ApartmentNo = "ApartmentNo 7c87544f409d45d8bdd430bd786da7d7";
            entity.Comment = "Comment 7c87544f409d45d8bdd430bd786da7d7";
            entity.CreatedByID = 100007;
            entity.CreatedDate = DateTime.Parse("2/10/2022 11:35:35 AM");
            entity.ModifiedByID = 100016;
            entity.ModifiedDate = DateTime.Parse("7/1/2019 9:22:35 PM");
            entity.IsDeleted = false;

            return entity;
        }

        protected PPT.Interfaces.Entities.Address AddTestEntity()
        {
            PPT.Interfaces.Entities.Address result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private PPT.Interfaces.IAddressDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            PPT.Interfaces.IAddressDal dal = new PPT.DAL.MSSQL.AddressDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}