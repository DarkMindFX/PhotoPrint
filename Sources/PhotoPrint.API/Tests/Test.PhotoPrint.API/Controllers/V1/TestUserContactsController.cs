


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
    public class TestUserContactsController : E2ETestBase, IClassFixture<WebApplicationFactory<PPT.PhotoPrint.API.Startup>>
    {
        public TestUserContactsController(WebApplicationFactory<PPT.PhotoPrint.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void UserContact_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var respGetAll = client.GetAsync($"/api/v1/usercontacts");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<UserContact> dtos = ExtractContentJson<List<UserContact>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void UserContact_Get_Success()
        {
            PPT.Interfaces.Entities.UserContact testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                var paramUserID = testEntity.UserID;
                var paramContactID = testEntity.ContactID;
                    var respGet = client.GetAsync($"/api/v1/usercontacts/{paramUserID}/{paramContactID}");

                    Assert.Equal(HttpStatusCode.OK, respGet.Result.StatusCode);

                    UserContact dto = ExtractContentJson<UserContact>(respGet.Result.Content);

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
        public void UserContact_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramUserID = Int64.MaxValue;
                var paramContactID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/usercontacts/{paramUserID}/{paramContactID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void UserContact_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                var paramUserID = testEntity.UserID;
                var paramContactID = testEntity.ContactID;

                    var respDel = client.DeleteAsync($"/api/v1/usercontacts/{paramUserID}/{paramContactID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void UserContact_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramUserID = Int64.MaxValue;
                var paramContactID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/usercontacts/{paramUserID}/{paramContactID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void UserContact_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.UserContact testEntity = CreateTestEntity();
                PPT.Interfaces.Entities.UserContact respEntity = null;
                try
                {
                    var reqDto = UserContactConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/usercontacts/", content);

                    Assert.Equal(HttpStatusCode.Created, respInsert.Result.StatusCode);

                    UserContact respDto = ExtractContentJson<UserContact>(respInsert.Result.Content);

                                    Assert.NotNull(respDto.UserID);
                                    Assert.NotNull(respDto.ContactID);
                                    Assert.Equal(reqDto.IsPrimary, respDto.IsPrimary);
                
                    respEntity = UserContactConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void UserContact_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.UserContact testEntity = AddTestEntity();
                try
                {
                          testEntity.IsPrimary = false;              
              
                    var reqDto = UserContactConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/usercontacts/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    UserContact respDto = ExtractContentJson<UserContact>(respUpdate.Result.Content);

                                     Assert.NotNull(respDto.UserID);
                                    Assert.NotNull(respDto.ContactID);
                                    Assert.Equal(reqDto.IsPrimary, respDto.IsPrimary);
                
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void UserContact_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.UserContact testEntity = CreateTestEntity();
                try
                {
                            testEntity.UserID = 100008;
                            testEntity.ContactID = 100011;
                            testEntity.IsPrimary = false;              
              
                    var reqDto = UserContactConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/usercontacts/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(PPT.Interfaces.Entities.UserContact entity)
        {
            if (entity != null)
            {
                var dal = CreateDal();



                return dal.Delete(                        entity.UserID,
                                        entity.ContactID
                );
            }
            else
            {
                return false;
            }
        }

        protected PPT.Interfaces.Entities.UserContact CreateTestEntity()
        {
            var entity = new PPT.Interfaces.Entities.UserContact();
                          entity.UserID = 100011;
                            entity.ContactID = 100020;
                            entity.IsPrimary = true;              
              
            return entity;
        }

        protected PPT.Interfaces.Entities.UserContact AddTestEntity()
        {
            PPT.Interfaces.Entities.UserContact result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private PPT.Interfaces.IUserContactDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            PPT.Interfaces.IUserContactDal dal = new PPT.DAL.MSSQL.UserContactDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
