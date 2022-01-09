using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PPT.Utils.Convertors;
using PPT.Services.Common.Helpers;
using PPT.Services.Dal;
using PPT.Interfaces.Entities;
using System.Net;
using PPT.Functions.Common;

namespace PPT.Functions.User.V1
{
    public class Register : FunctionBase
    {
        private readonly IUserDal _dalUser;
        private readonly IContactDal _dalContact;
        private readonly IUserContactDal _dalUserContact;

        public Register(IHttpContextAccessor httpContextAccessor,
            IUserDal dalUser,
            IContactDal dalContact,
            IUserContactDal dalUserContact) : base(httpContextAccessor)
        {
            _dalUser = dalUser;
            _dalContact = dalContact;
            _dalUserContact = dalUserContact;
        }

        [FunctionName("UsersRegister")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "v1/users/register")] HttpRequest req,
            ILogger log)
        {
            IActionResult result = null;
            var funHelper = new PPT.Functions.Common.FunctionHelper();
            log.LogInformation($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            try
            {

                var content = await new StreamReader(req.Body).ReadToEndAsync();

                var dtoRegister = JsonConvert.DeserializeObject<PPT.DTO.RegisterRequest>(content);

                if (string.IsNullOrEmpty(dtoRegister.User.Password))
                {
                    result = funHelper.CreateResult(HttpStatusCode.BadRequest, null, $"Password is empty" );
                }
                else if (string.IsNullOrEmpty(dtoRegister.User.Login))
                {
                    result = funHelper.CreateResult(HttpStatusCode.BadRequest, null, $"Login is empty");
                }
                else
                {

                    // Inserting new user
                    var entityUser = UserConvertor.Convert(dtoRegister.User);
                    entityUser.Salt = PasswordHelper.GenerateSalt(12);
                    entityUser.PwdHash = PasswordHelper.GenerateHash(dtoRegister.User.Password, entityUser.Salt);

                    funHelper.SetCreatedModifiedProperties(entityUser,
                                "CreatedDate",
                                null);

                    PPT.Interfaces.Entities.User newEntityUser = _dalUser.Insert(entityUser);

                    // Inserting user's contact
                    var entityContact = ContactConvertor.Convert(dtoRegister.Contact);
                    funHelper.SetCreatedModifiedProperties(entityContact,
                                "CreatedDate",
                                null);
                    entityContact.CreatedByID = (long)newEntityUser.ID;

                    PPT.Interfaces.Entities.Contact newEntityContact = _dalContact.Insert(entityContact);

                    // Connecting user & contact
                    var entityUserContact = new PPT.Interfaces.Entities.UserContact();
                    entityUserContact.UserID = (long)newEntityUser.ID;
                    entityUserContact.ContactID = (long)newEntityContact.ID;
                    entityUserContact.IsPrimary = true;
                    UserContact newEntityUserContact = _dalUserContact.Insert(entityUserContact);

                    // Preparing response
                    result = funHelper.CreateResult(HttpStatusCode.Created,
                                            new PPT.DTO.RegisterResponse()
                                            {
                                                User = UserConvertor.Convert(newEntityUser, null),
                                                Contact = ContactConvertor.Convert(newEntityContact, null)
                                            });
                 }

            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
            }

            log.LogInformation($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return result;
        }
    }
}
