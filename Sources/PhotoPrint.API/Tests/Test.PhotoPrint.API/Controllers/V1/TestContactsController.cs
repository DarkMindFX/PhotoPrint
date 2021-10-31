

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
    public class TestContactsController : E2ETestBase, IClassFixture<WebApplicationFactory<PPT.PhotoPrint.API.Startup>>
    {
        public TestContactsController(WebApplicationFactory<PPT.PhotoPrint.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void Contact_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var respGetAll = client.GetAsync($"/api/v1/contacts");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<Contact> dtos = ExtractContentJson<List<Contact>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void Contact_Get_Success()
        {
            PPT.Interfaces.Entities.Contact testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                    var paramID = testEntity.ID;
                    var respGet = client.GetAsync($"/api/v1/contacts/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respGet.Result.StatusCode);

                    Contact dto = ExtractContentJson<Contact>(respGet.Result.Content);

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
        public void Contact_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/contacts/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void Contact_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                    var paramID = testEntity.ID;

                    var respDel = client.DeleteAsync($"/api/v1/contacts/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void Contact_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/contacts/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void Contact_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.Contact testEntity = CreateTestEntity();
                PPT.Interfaces.Entities.Contact respEntity = null;
                try
                {
                    var reqDto = ContactConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/contacts/", content);

                    Assert.Equal(HttpStatusCode.Created, respInsert.Result.StatusCode);

                    Contact respDto = ExtractContentJson<Contact>(respInsert.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.ContactTypeID, respDto.ContactTypeID);
                    Assert.Equal(reqDto.Title, respDto.Title);
                    Assert.Equal(reqDto.Comment, respDto.Comment);
                    Assert.Equal(reqDto.Value, respDto.Value);
                    Assert.Equal(reqDto.IsDeleted, respDto.IsDeleted);


                    respEntity = ContactConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void Contact_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.Contact testEntity = AddTestEntity();
                try
                {
                    testEntity.ContactTypeID = 4;
                    testEntity.Title = "Title cafbe206a4634b4e8ae4e7c3f7fbed4e";
                    testEntity.Comment = "Comment cafbe206a4634b4e8ae4e7c3f7fbed4e";
                    testEntity.Value = "Value cafbe206a4634b4e8ae4e7c3f7fbed4e";
                    testEntity.IsDeleted = true;
                    testEntity.CreatedByID = 100004;
                    testEntity.CreatedDate = DateTime.Parse("11/27/2021 5:39:35 PM");
                    testEntity.ModifiedByID = 100001;
                    testEntity.ModifiedDate = DateTime.Parse("11/27/2021 5:39:35 PM");

                    var reqDto = ContactConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/contacts/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    Contact respDto = ExtractContentJson<Contact>(respUpdate.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.ContactTypeID, respDto.ContactTypeID);
                    Assert.Equal(reqDto.Title, respDto.Title);
                    Assert.Equal(reqDto.Comment, respDto.Comment);
                    Assert.Equal(reqDto.Value, respDto.Value);
                    Assert.Equal(reqDto.IsDeleted, respDto.IsDeleted);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void Contact_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                PPT.Interfaces.Entities.Contact testEntity = CreateTestEntity();
                try
                {
                    testEntity.ID = Int64.MaxValue;
                    testEntity.ContactTypeID = 4;
                    testEntity.Title = "Title cafbe206a4634b4e8ae4e7c3f7fbed4e";
                    testEntity.Comment = "Comment cafbe206a4634b4e8ae4e7c3f7fbed4e";
                    testEntity.Value = "Value cafbe206a4634b4e8ae4e7c3f7fbed4e";
                    testEntity.IsDeleted = true;
                    testEntity.CreatedByID = 100004;
                    testEntity.CreatedDate = DateTime.Parse("11/27/2021 5:39:35 PM");
                    testEntity.ModifiedByID = 100001;
                    testEntity.ModifiedDate = DateTime.Parse("11/27/2021 5:39:35 PM");

                    var reqDto = ContactConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/contacts/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(PPT.Interfaces.Entities.Contact entity)
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

        protected PPT.Interfaces.Entities.Contact CreateTestEntity()
        {
            var entity = new PPT.Interfaces.Entities.Contact();
            entity.ContactTypeID = 3;
            entity.Title = "Title 59d7bc634fa141b8a12f7f4be8c46782";
            entity.Comment = "Comment 59d7bc634fa141b8a12f7f4be8c46782";
            entity.Value = "Value 59d7bc634fa141b8a12f7f4be8c46782";
            entity.IsDeleted = false;
            entity.CreatedByID = 100003;
            entity.CreatedDate = DateTime.Parse("8/30/2021 7:25:35 AM");
            entity.ModifiedByID = 100001;
            entity.ModifiedDate = DateTime.Parse("8/30/2021 7:25:35 AM");

            return entity;
        }

        protected PPT.Interfaces.Entities.Contact AddTestEntity()
        {
            PPT.Interfaces.Entities.Contact result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private PPT.Interfaces.IContactDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            PPT.Interfaces.IContactDal dal = new PPT.DAL.MSSQL.ContactDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
