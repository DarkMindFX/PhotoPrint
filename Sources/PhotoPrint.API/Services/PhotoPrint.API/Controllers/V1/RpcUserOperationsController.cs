using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using PPT.PhotoPrint.API.Filters;
using PPT.Interfaces.Entities;
using PPT.Utils.Convertors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using PPT.PhotoPrint.API.Helpers;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using PPT.Services.Common.Helpers;

namespace PPT.PhotoPrint.API.Controllers.V1
{
    [Route("api/v1/users")]
    [ApiController]
    [UnhandledExceptionFilter]
    public class RpcUserOperationsController : BaseController
    {

        private readonly Dal.IUserDal _dalUser;
        private readonly Dal.IContactDal _dalContact;
        private readonly Dal.IUserContactDal _dalUserContact;
        private readonly ILogger<UsersController> _logger;
        private readonly IOptions<AppSettings> _appSettings;


        public RpcUserOperationsController(Dal.IUserDal dalUser,
                                        Dal.IContactDal dalContact,
                                        Dal.IUserContactDal dalUserContact,
                                        ILogger<UsersController> logger,
                                        IOptions<AppSettings> appSettings)
        {
            _dalUser = dalUser;
            _dalContact = dalContact;
            _dalUserContact = dalUserContact;
            _logger = logger;
            _appSettings = appSettings;
        }

        [AllowAnonymous]
        [HttpPost("login"), ActionName("Login")]
        public IActionResult Login(DTO.LoginRequest dtoLogin)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalUser.GetAll().FirstOrDefault(u => u.Login.ToLower() == dtoLogin.Login.ToLower());
            if (existingEntity != null)
            {
                string pwdHash = PasswordHelper.GenerateHash(dtoLogin.Password, existingEntity.Salt);
                if (pwdHash.Equals(existingEntity.PwdHash))
                {
                    var dtExpires = DateTime.Now.AddSeconds(_appSettings.Value.SessionTimeout);
                    var sToken = GenerateToken(existingEntity, dtExpires);

                    var dtoResponse = new DTO.LoginResponse()
                    {
                        User = UserConvertor.Convert(existingEntity, this.Url),
                        Token = sToken,
                        Expires = dtExpires
                    };

                    response = Ok(dtoResponse);
                }
                else
                {
                    response = Forbid();
                }
            }
            else
            {
                response = NotFound($"User not found [login:{dtoLogin.Login}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [AllowAnonymous]
        [HttpPost("register"), ActionName("Register")]
        public IActionResult Register(DTO.RegisterRequest dtoRegister)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            if (string.IsNullOrEmpty(dtoRegister.User.Password))
            {
                response = StatusCode((int)HttpStatusCode.BadRequest,
                    new DTO.Error() { Message = $"Password is empty" });
            }
            else if (string.IsNullOrEmpty(dtoRegister.User.Login))
            {
                response = StatusCode((int)HttpStatusCode.BadRequest,
                    new DTO.Error() { Message = $"Login is empty" });
            }
            else
            {

                // Inserting new user
                var entityUser = UserConvertor.Convert(dtoRegister.User);
                entityUser.Salt = PasswordHelper.GenerateSalt(12);
                entityUser.PwdHash = PasswordHelper.GenerateHash(dtoRegister.User.Password, entityUser.Salt);

                base.SetCreatedModifiedProperties(entityUser,
                            "CreatedDate",
                            null);

                User newEntityUser = _dalUser.Insert(entityUser);

                // Inserting user's contact
                var entityContact = ContactConvertor.Convert(dtoRegister.Contact);               
                base.SetCreatedModifiedProperties(entityContact,
                            "CreatedDate",
                            null);
                entityContact.CreatedByID = (long)newEntityUser.ID;

                Contact newEntityContact = _dalContact.Insert(entityContact);

                // Connecting user & contact
                var entityUserContact = new PPT.Interfaces.Entities.UserContact();
                entityUserContact.UserID = (long)newEntityUser.ID;
                entityUserContact.ContactID = (long)newEntityContact.ID;
                entityUserContact.IsPrimary = true;
                UserContact newEntityUserContact = _dalUserContact.Insert(entityUserContact);

                // Preparing response
                response = StatusCode((int)HttpStatusCode.Created,
                                        new DTO.RegisterResponse() { 
                                            User = UserConvertor.Convert(newEntityUser, this.Url),
                                            Contact = ContactConvertor.Convert(newEntityContact, this.Url)
                                        });

                _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");
            }

            return response;
        }

        #region Support methods
        private string GenerateToken(Interfaces.Entities.User user, DateTime expires)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Value.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                    {
                            new Claim("id", user.ID.ToString())
                    }
                ),
                Expires = expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            string sToken = tokenHandler.WriteToken(token);

            return sToken;
        }

        #endregion
    }
}
