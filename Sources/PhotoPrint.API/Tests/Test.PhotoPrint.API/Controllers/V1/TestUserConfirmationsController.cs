


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
    public class TestUserConfirmationsController : E2ETestBase, IClassFixture<WebApplicationFactory<PPT.PhotoPrint.API.Startup>>
    {
        public TestUserConfirmationsController(WebApplicationFactory<PPT.PhotoPrint.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void UserConfirmation_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var respGetAll = client.GetAsync($"/api/v1/userconfirmations");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<UserConfirmation> dtos = ExtractContentJson<List<UserConfirmation>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void UserConfirmation_Get_Success()
        {
            PPT.Interfaces.Entities.UserConfirmation testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                var paramID = testEntity.ID;
                    var respGet = client.GetAsync($"/api/v1/userconfirmations/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respGet.Result.StatusCode);

                    UserConfirmation dto = ExtractContentJson<UserConfirmation>(respGet.Result.Content);

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
        public void UserConfirmation_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/userconfirmations/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void UserConfirmation_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                var paramID = testEntity.ID;

                    var respDel = client.DeleteAsync($"/api/v1/userconfirmations/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void UserConfirmation_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/userconfirmations/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void UserConfirmation_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.UserConfirmation testEntity = CreateTestEntity();
                PPT.Interfaces.Entities.UserConfirmation respEntity = null;
                try
                {
                    var reqDto = UserConfirmationConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/userconfirmations/", content);

                    Assert.Equal(HttpStatusCode.Created, respInsert.Result.StatusCode);

                    UserConfirmation respDto = ExtractContentJson<UserConfirmation>(respInsert.Result.Content);

                                    Assert.NotNull(respDto.ID);
                                    Assert.Equal(reqDto.UserID, respDto.UserID);
                                    Assert.Equal(reqDto.ConfirmationCode, respDto.ConfirmationCode);
                                    Assert.Equal(reqDto.Comfirmed, respDto.Comfirmed);
                                    Assert.Equal(reqDto.ExpiresDate, respDto.ExpiresDate);
                                    Assert.Equal(reqDto.ConfirmationDate, respDto.ConfirmationDate);
                
                    respEntity = UserConfirmationConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void UserConfirmation_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.UserConfirmation testEntity = AddTestEntity();
                try
                {
                          testEntity.UserID = 100008 ;
                            testEntity.ConfirmationCode = "ConfirmationCode b23ed26eeec7410b933d98188a09abc9";
                            testEntity.Comfirmed = false;              
                            testEntity.ExpiresDate = DateTime.Parse("12/7/2020 6:54:38 AM");
                            testEntity.ConfirmationDate = DateTime.Parse("12/7/2020 6:54:38 AM");
              
                    var reqDto = UserConfirmationConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/userconfirmations/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    UserConfirmation respDto = ExtractContentJson<UserConfirmation>(respUpdate.Result.Content);

                                     Assert.NotNull(respDto.ID);
                                    Assert.Equal(reqDto.UserID, respDto.UserID);
                                    Assert.Equal(reqDto.ConfirmationCode, respDto.ConfirmationCode);
                                    Assert.Equal(reqDto.Comfirmed, respDto.Comfirmed);
                                    Assert.Equal(reqDto.ExpiresDate, respDto.ExpiresDate);
                                    Assert.Equal(reqDto.ConfirmationDate, respDto.ConfirmationDate);
                
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void UserConfirmation_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.UserConfirmation testEntity = CreateTestEntity();
                try
                {
                             testEntity.ID = Int64.MaxValue;
                             testEntity.UserID = 100008;
                            testEntity.ConfirmationCode = "ConfirmationCode b23ed26eeec7410b933d98188a09abc9";
                            testEntity.Comfirmed = false;              
                            testEntity.ExpiresDate = DateTime.Parse("12/7/2020 6:54:38 AM");
                            testEntity.ConfirmationDate = DateTime.Parse("12/7/2020 6:54:38 AM");
              
                    var reqDto = UserConfirmationConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/userconfirmations/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(PPT.Interfaces.Entities.UserConfirmation entity)
        {
            if (entity != null)
            {
                var dal = CreateDal();



                return dal.Delete(                        entity.ID
                );
            }
            else
            {
                return false;
            }
        }

        protected PPT.Interfaces.Entities.UserConfirmation CreateTestEntity()
        {
            var entity = new PPT.Interfaces.Entities.UserConfirmation();
                          entity.UserID = 100001;
                            entity.ConfirmationCode = "ConfirmationCode 79c04aa4be284861862073c5c3d01dfa";
                            entity.Comfirmed = true;              
                            entity.ExpiresDate = DateTime.Parse("9/8/2020 8:40:38 PM");
                            entity.ConfirmationDate = DateTime.Parse("9/8/2020 8:40:38 PM");
              
            return entity;
        }

        protected PPT.Interfaces.Entities.UserConfirmation AddTestEntity()
        {
            PPT.Interfaces.Entities.UserConfirmation result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private PPT.Interfaces.IUserConfirmationDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            PPT.Interfaces.IUserConfirmationDal dal = new PPT.DAL.MSSQL.UserConfirmationDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
