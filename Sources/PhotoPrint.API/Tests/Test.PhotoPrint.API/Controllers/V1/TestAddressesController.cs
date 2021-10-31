

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
                    testEntity.AddressTypeID = 4;
                    testEntity.Title = "Title c5e5bf860f0443e49a63d808d169a8e2";
                    testEntity.CityID = 1;
                    testEntity.Street = "Street c5e5bf860f0443e49a63d808d169a8e2";
                    testEntity.BuildingNo = "BuildingNo c5e5bf860f0443e49a63d808d169a8e2";
                    testEntity.ApartmentNo = "ApartmentNo c5e5bf860f0443e49a63d808d169a8e2";
                    testEntity.Comment = "Comment c5e5bf860f0443e49a63d808d169a8e2";
                    testEntity.CreatedByID = 100007;
                    testEntity.CreatedDate = DateTime.Parse("11/9/2023 6:50:35 PM");
                    testEntity.ModifiedByID = 100009;
                    testEntity.ModifiedDate = DateTime.Parse("3/30/2021 4:37:35 AM");
                    testEntity.IsDeleted = true;

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
                    testEntity.AddressTypeID = 4;
                    testEntity.Title = "Title c5e5bf860f0443e49a63d808d169a8e2";
                    testEntity.CityID = 1;
                    testEntity.Street = "Street c5e5bf860f0443e49a63d808d169a8e2";
                    testEntity.BuildingNo = "BuildingNo c5e5bf860f0443e49a63d808d169a8e2";
                    testEntity.ApartmentNo = "ApartmentNo c5e5bf860f0443e49a63d808d169a8e2";
                    testEntity.Comment = "Comment c5e5bf860f0443e49a63d808d169a8e2";
                    testEntity.CreatedByID = 100007;
                    testEntity.CreatedDate = DateTime.Parse("11/9/2023 6:50:35 PM");
                    testEntity.ModifiedByID = 100009;
                    testEntity.ModifiedDate = DateTime.Parse("3/30/2021 4:37:35 AM");
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
            entity.AddressTypeID = 4;
            entity.Title = "Title 879afb07ddaa499aa82164fa30c631ec";
            entity.CityID = 1;
            entity.Street = "Street 879afb07ddaa499aa82164fa30c631ec";
            entity.BuildingNo = "BuildingNo 879afb07ddaa499aa82164fa30c631ec";
            entity.ApartmentNo = "ApartmentNo 879afb07ddaa499aa82164fa30c631ec";
            entity.Comment = "Comment 879afb07ddaa499aa82164fa30c631ec";
            entity.CreatedByID = 100008;
            entity.CreatedDate = DateTime.Parse("10/1/2020 10:49:35 PM");
            entity.ModifiedByID = 100002;
            entity.ModifiedDate = DateTime.Parse("8/13/2023 8:36:35 AM");
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
