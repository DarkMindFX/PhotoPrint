


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


namespace PPT.PhotoPrint.API.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [UnhandledExceptionFilter]
    public class UserStatusesController : BaseController
    {
        private readonly Dal.IUserStatusDal _dalUserStatus;
        private readonly ILogger<UserStatusesController> _logger;


        public UserStatusesController( Dal.IUserStatusDal dalUserStatus,
                                    ILogger<UserStatusesController> logger)
        {
            _dalUserStatus = dalUserStatus; 
            _logger = logger;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalUserStatus.GetAll();

            IList<DTO.UserStatus> dtos = new List<DTO.UserStatus>();

            foreach (var p in entities)
            {
                var dto = UserStatusConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{id}"), ActionName("GetUserStatus")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalUserStatus.Get(id);
            if (entity != null)
            {
                var dto = UserStatusConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"UserStatus was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpDelete("{id}"), ActionName("DeleteUserStatus")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalUserStatus.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalUserStatus.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete UserStatus [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"UserStatus not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertUserStatus")]
        public IActionResult Insert(DTO.UserStatus dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = UserStatusConvertor.Convert(dto);

            UserStatus newEntity = _dalUserStatus.Insert(entity);

            response =StatusCode((int)HttpStatusCode.Created, UserStatusConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateUserStatus")]
        public IActionResult Update(DTO.UserStatus dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = UserStatusConvertor.Convert(dto);

            var existingEntity = _dalUserStatus.Get(newEntity.ID);
            if (existingEntity != null)
            {
                UserStatus entity = _dalUserStatus.Update(newEntity);

                response = Ok(UserStatusConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"UserStatus not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

