


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
    public class UserConfirmationsController : BaseController
    {
        private readonly Dal.IUserConfirmationDal _dalUserConfirmation;
        private readonly ILogger<UserConfirmationsController> _logger;


        public UserConfirmationsController( Dal.IUserConfirmationDal dalUserConfirmation,
                                    ILogger<UserConfirmationsController> logger)
        {
            _dalUserConfirmation = dalUserConfirmation; 
            _logger = logger;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalUserConfirmation.GetAll();

            IList<DTO.UserConfirmation> dtos = new List<DTO.UserConfirmation>();

            foreach (var p in entities)
            {
                var dto = UserConfirmationConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{id}"), ActionName("GetUserConfirmation")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalUserConfirmation.Get(id);
            if (entity != null)
            {
                var dto = UserConfirmationConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"UserConfirmation was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("byuserid/{userid}")]
        public IActionResult GetByUserID(System.Int64 userid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalUserConfirmation.GetByUserID(userid);

            IList<DTO.UserConfirmation> dtos = new List<DTO.UserConfirmation>();

            foreach (var p in entities)
            {
                var dto = UserConfirmationConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
        
        //[Authorize]
        [HttpDelete("{id}"), ActionName("DeleteUserConfirmation")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalUserConfirmation.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalUserConfirmation.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete UserConfirmation [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"UserConfirmation not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertUserConfirmation")]
        public IActionResult Insert(DTO.UserConfirmation dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = UserConfirmationConvertor.Convert(dto);           

            
            UserConfirmation newEntity = _dalUserConfirmation.Insert(entity);

            response = StatusCode((int)HttpStatusCode.Created, UserConfirmationConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateUserConfirmation")]
        public IActionResult Update(DTO.UserConfirmation dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = UserConfirmationConvertor.Convert(dto);

            var existingEntity = _dalUserConfirmation.Get(newEntity.ID);           

            if (existingEntity != null)
            {
                                                    UserConfirmation entity = _dalUserConfirmation.Update(newEntity);

                response = Ok(UserConfirmationConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"UserConfirmation not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

