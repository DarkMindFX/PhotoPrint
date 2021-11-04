


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
    public class TestUserAddressesController : E2ETestBase, IClassFixture<WebApplicationFactory<PPT.PhotoPrint.API.Startup>>
    {
        public TestUserAddressesController(WebApplicationFactory<PPT.PhotoPrint.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void UserAddress_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var respGetAll = client.GetAsync($"/api/v1/useraddresses");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<UserAddress> dtos = ExtractContentJson<List<UserAddress>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void UserAddress_Get_Success()
        {
            PPT.Interfaces.Entities.UserAddress testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                var paramUserID = testEntity.UserID;
                var paramAddressID = testEntity.AddressID;
                    var respGet = client.GetAsync($"/api/v1/useraddresses/{paramUserID}/{paramAddressID}");

                    Assert.Equal(HttpStatusCode.OK, respGet.Result.StatusCode);

                    UserAddress dto = ExtractContentJson<UserAddress>(respGet.Result.Content);

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
        public void UserAddress_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramUserID = Int64.MaxValue;
                var paramAddressID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/useraddresses/{paramUserID}/{paramAddressID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void UserAddress_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                var paramUserID = testEntity.UserID;
                var paramAddressID = testEntity.AddressID;

                    var respDel = client.DeleteAsync($"/api/v1/useraddresses/{paramUserID}/{paramAddressID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void UserAddress_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramUserID = Int64.MaxValue;
                var paramAddressID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/useraddresses/{paramUserID}/{paramAddressID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void UserAddress_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.UserAddress testEntity = CreateTestEntity();
                PPT.Interfaces.Entities.UserAddress respEntity = null;
                try
                {
                    var reqDto = UserAddressConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/useraddresses/", content);

                    Assert.Equal(HttpStatusCode.Created, respInsert.Result.StatusCode);

                    UserAddress respDto = ExtractContentJson<UserAddress>(respInsert.Result.Content);

                                    Assert.NotNull(respDto.UserID);
                                    Assert.NotNull(respDto.AddressID);
                                    Assert.Equal(reqDto.IsPrimary, respDto.IsPrimary);
                
                    respEntity = UserAddressConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void UserAddress_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.UserAddress testEntity = AddTestEntity();
                try
                {
                          testEntity.IsPrimary = false;              
              
                    var reqDto = UserAddressConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/useraddresses/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    UserAddress respDto = ExtractContentJson<UserAddress>(respUpdate.Result.Content);

                                     Assert.NotNull(respDto.UserID);
                                    Assert.NotNull(respDto.AddressID);
                                    Assert.Equal(reqDto.IsPrimary, respDto.IsPrimary);
                
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void UserAddress_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.UserAddress testEntity = CreateTestEntity();
                try
                {
                            testEntity.UserID = 100008;
                            testEntity.AddressID = 100008;
                            testEntity.IsPrimary = false;              
              
                    var reqDto = UserAddressConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/useraddresses/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(PPT.Interfaces.Entities.UserAddress entity)
        {
            if (entity != null)
            {
                var dal = CreateDal();



                return dal.Delete(                        entity.UserID,
                                        entity.AddressID
                );
            }
            else
            {
                return false;
            }
        }

        protected PPT.Interfaces.Entities.UserAddress CreateTestEntity()
        {
            var entity = new PPT.Interfaces.Entities.UserAddress();
                          entity.UserID = 100003;
                            entity.AddressID = 100002;
                            entity.IsPrimary = true;              
              
            return entity;
        }

        protected PPT.Interfaces.Entities.UserAddress AddTestEntity()
        {
            PPT.Interfaces.Entities.UserAddress result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private PPT.Interfaces.IUserAddressDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            PPT.Interfaces.IUserAddressDal dal = new PPT.DAL.MSSQL.UserAddressDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
