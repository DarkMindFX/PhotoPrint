

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
    public class TestAddressesController : E2ETestBase, IClassFixture<WebApplicationFactory<PPT.PhotoPrint.API.Startup>>
    {
        public TestAddressesController(WebApplicationFactory<PPT.PhotoPrint.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void Address_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var respGetAll = client.GetAsync($"/api/v1/addresses");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<Address> dtos = ExtractContentJson<List<Address>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void Address_Get_Success()
        {
            PPT.Interfaces.Entities.Address testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                    var paramID = testEntity.ID;
                    var respGet = client.GetAsync($"/api/v1/addresses/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respGet.Result.StatusCode);

                    Address dto = ExtractContentJson<Address>(respGet.Result.Content);

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
        public void Address_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/addresses/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void Address_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                    var paramID = testEntity.ID;

                    var respDel = client.DeleteAsync($"/api/v1/addresses/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void Address_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/addresses/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void Address_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.Address testEntity = CreateTestEntity();
                PPT.Interfaces.Entities.Address respEntity = null;
                try
                {
                    var reqDto = AddressConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/addresses/", content);

                    Assert.Equal(HttpStatusCode.Created, respInsert.Result.StatusCode);

                    Address respDto = ExtractContentJson<Address>(respInsert.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.AddressTypeID, respDto.AddressTypeID);
                    Assert.Equal(reqDto.Title, respDto.Title);
                    Assert.Equal(reqDto.CityID, respDto.CityID);
                    Assert.Equal(reqDto.Street, respDto.Street);
                    Assert.Equal(reqDto.BuildingNo, respDto.BuildingNo);
                    Assert.Equal(reqDto.ApartmentNo, respDto.ApartmentNo);
                    Assert.Equal(reqDto.Comment, respDto.Comment);
                    Assert.Equal(reqDto.CreatedByID, respDto.CreatedByID);
                    Assert.Equal(reqDto.CreatedDate, respDto.CreatedDate);
                    Assert.Equal(reqDto.ModifiedByID, respDto.ModifiedByID);
                    Assert.Equal(reqDto.ModifiedDate, respDto.ModifiedDate);
                    Assert.Equal(reqDto.IsDeleted, respDto.IsDeleted);

                    respEntity = AddressConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void Address_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.Address testEntity = AddTestEntity();
                try
                {
                    testEntity.AddressTypeID = 5;
                    testEntity.Title = "Title 8ca84fecb64846be90009552b960864f";
                    testEntity.CityID = 7;
                    testEntity.Street = "Street 8ca84fecb64846be90009552b960864f";
                    testEntity.BuildingNo = "BuildingNo 8ca84fecb64846be90009552b960864f";
                    testEntity.ApartmentNo = "ApartmentNo 8ca84fecb64846be90009552b960864f";
                    testEntity.Comment = "Comment 8ca84fecb64846be90009552b960864f";
                    testEntity.IsDeleted = true;
                    testEntity.ModifiedByID = respLogin.User.ID;

                    var reqDto = AddressConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/addresses/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    Address respDto = ExtractContentJson<Address>(respUpdate.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.AddressTypeID, respDto.AddressTypeID);
                    Assert.Equal(reqDto.Title, respDto.Title);
                    Assert.Equal(reqDto.CityID, respDto.CityID);
                    Assert.Equal(reqDto.Street, respDto.Street);
                    Assert.Equal(reqDto.BuildingNo, respDto.BuildingNo);
                    Assert.Equal(reqDto.ApartmentNo, respDto.ApartmentNo);
                    Assert.Equal(reqDto.Comment, respDto.Comment);
                    Assert.Equal(reqDto.CreatedByID, respDto.CreatedByID);
                    Assert.Equal(reqDto.CreatedDate, respDto.CreatedDate);
                    Assert.Equal(reqDto.ModifiedByID, respDto.ModifiedByID);
                    Assert.True(DateTime.UtcNow - TimeSpan.FromMinutes(1) < respDto.ModifiedDate);
                    Assert.Equal(reqDto.IsDeleted, respDto.IsDeleted);

                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void Address_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.Address testEntity = CreateTestEntity();
                try
                {
                    testEntity.ID = Int64.MaxValue;
                    testEntity.AddressTypeID = 5;
                    testEntity.Title = "Title 8ca84fecb64846be90009552b960864f";
                    testEntity.CityID = 7;
                    testEntity.Street = "Street 8ca84fecb64846be90009552b960864f";
                    testEntity.BuildingNo = "BuildingNo 8ca84fecb64846be90009552b960864f";
                    testEntity.ApartmentNo = "ApartmentNo 8ca84fecb64846be90009552b960864f";
                    testEntity.Comment = "Comment 8ca84fecb64846be90009552b960864f";
                    testEntity.CreatedByID = 100005;
                    testEntity.CreatedDate = DateTime.Parse("10/29/2021 6:44:52 AM");
                    testEntity.ModifiedByID = 100002;
                    testEntity.ModifiedDate = DateTime.Parse("6/9/2020 2:19:52 PM");
                    testEntity.IsDeleted = true;

                    var reqDto = AddressConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/addresses/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(PPT.Interfaces.Entities.Address entity)
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

        protected PPT.Interfaces.Entities.Address CreateTestEntity()
        {
            var entity = new PPT.Interfaces.Entities.Address();
            entity.AddressTypeID = 11;
            entity.Title = "Title f2c47218c2df42c1962bbff2e75bdb43";
            entity.CityID = 1;
            entity.Street = "Street f2c47218c2df42c1962bbff2e75bdb43";
            entity.BuildingNo = "BuildingNo f2c47218c2df42c1962bbff2e75bdb43";
            entity.ApartmentNo = "ApartmentNo f2c47218c2df42c1962bbff2e75bdb43";
            entity.Comment = "Comment f2c47218c2df42c1962bbff2e75bdb43";
            entity.CreatedByID = 100003;
            entity.CreatedDate = DateTime.Parse("4/20/2022 2:33:52 PM");
            entity.ModifiedByID = 100003;
            entity.ModifiedDate = DateTime.Parse("3/7/2020 6:08:52 AM");
            entity.IsDeleted = true;

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
