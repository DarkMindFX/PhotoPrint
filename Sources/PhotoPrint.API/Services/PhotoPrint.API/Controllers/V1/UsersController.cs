


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

namespace PPT.PhotoPrint.API.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [UnhandledExceptionFilter]
    public class UsersController : BaseController
    {
        private readonly Dal.IUserDal _dalUser;
        private readonly ILogger<UsersController> _logger;
        private readonly IOptions<AppSettings> _appSettings;


        public UsersController(Dal.IUserDal dalUser,
                                    ILogger<UsersController> logger,
                                    IOptions<AppSettings> appSettings)
        {
            _dalUser = dalUser;
            _logger = logger;
            _appSettings = appSettings;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalUser.GetAll();

            IList<DTO.User> dtos = new List<DTO.User>();

            foreach (var p in entities)
            {
                var dto = UserConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{id}"), ActionName("GetUser")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalUser.Get(id);
            if (entity != null)
            {
                var dto = UserConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"User was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpDelete("{id}"), ActionName("DeleteUser")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalUser.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalUser.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete User [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"User not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertUser")]
        public IActionResult Insert(DTO.User dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = UserConvertor.Convert(dto);
            entity.Salt = Helpers.PasswordHelper.GenerateSalt(12);
            entity.PwdHash = Helpers.PasswordHelper.GenerateHash(dto.Password, entity.Salt);

            User newEntity = _dalUser.Insert(entity);

            base.SetCreatedModifiedProperties(entity,
                        "CreatedDate",
                        null);

            response = StatusCode((int)HttpStatusCode.Created, UserConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateUser")]
        public IActionResult Update(DTO.User dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = UserConvertor.Convert(dto);
            

            var existingEntity = _dalUser.Get(newEntity.ID);

            if (existingEntity != null)
            {
                if (!string.IsNullOrEmpty(dto.Password))
                {
                    newEntity.PwdHash = Helpers.PasswordHelper.GenerateHash(dto.Password, existingEntity.Salt);
                }

                newEntity.CreatedDate = existingEntity.CreatedDate;

                base.SetCreatedModifiedProperties(newEntity,
                                        "ModifiedDate",
                                        "ModifiedByID");
                User entity = _dalUser.Update(newEntity);

                response = Ok(UserConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"User not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [AllowAnonymous]
        [HttpPost("login"), ActionName("Login")]
        public IActionResult Login(DTO.LoginRequest dtoLogin)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalUser.GetAll().FirstOrDefault( u => u.Login.ToLower() == dtoLogin.Login.ToLower());
            if (existingEntity != null)
            {
                string pwdHash = Helpers.PasswordHelper.GenerateHash(dtoLogin.Password, existingEntity.Salt);
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
    }
}

