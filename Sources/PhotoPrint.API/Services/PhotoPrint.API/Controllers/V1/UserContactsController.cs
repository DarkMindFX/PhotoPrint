


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
    public class UserContactsController : BaseController
    {
        private readonly Dal.IUserContactDal _dalUserContact;
        private readonly ILogger<UserContactsController> _logger;


        public UserContactsController( Dal.IUserContactDal dalUserContact,
                                    ILogger<UserContactsController> logger)
        {
            _dalUserContact = dalUserContact; 
            _logger = logger;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalUserContact.GetAll();

            IList<DTO.UserContact> dtos = new List<DTO.UserContact>();

            foreach (var p in entities)
            {
                var dto = UserContactConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{userid}/{contactid}"), ActionName("GetUserContact")]
        public IActionResult Get(System.Int64 userid, System.Int64 contactid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalUserContact.Get(userid, contactid);
            if (entity != null)
            {
                var dto = UserContactConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"UserContact was not found [ids:{userid}, {contactid}]");
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

            var entities = _dalUserContact.GetByUserID(userid);

            IList<DTO.UserContact> dtos = new List<DTO.UserContact>();

            foreach (var p in entities)
            {
                var dto = UserContactConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
        //[Authorize]
        [HttpGet("bycontactid/{contactid}")]
        public IActionResult GetByContactID(System.Int64 contactid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalUserContact.GetByContactID(contactid);

            IList<DTO.UserContact> dtos = new List<DTO.UserContact>();

            foreach (var p in entities)
            {
                var dto = UserContactConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
        
        //[Authorize]
        [HttpDelete("{userid}/{contactid}"), ActionName("DeleteUserContact")]
        public IActionResult Delete(System.Int64 userid, System.Int64 contactid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalUserContact.Get(userid, contactid);

            if (existingEntity != null)
            {
                bool removed = _dalUserContact.Delete(userid, contactid);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete UserContact [ids:{userid}, {contactid}]");
                }
            }
            else
            {
                response = NotFound($"UserContact not found [ids:{userid}, {contactid}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertUserContact")]
        public IActionResult Insert(DTO.UserContact dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = UserContactConvertor.Convert(dto);           

            
            UserContact newEntity = _dalUserContact.Insert(entity);

            response = StatusCode((int)HttpStatusCode.Created, UserContactConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateUserContact")]
        public IActionResult Update(DTO.UserContact dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = UserContactConvertor.Convert(dto);

            var existingEntity = _dalUserContact.Get(newEntity.UserID, newEntity.ContactID);           

            if (existingEntity != null)
            {
                                                    UserContact entity = _dalUserContact.Update(newEntity);

                response = Ok(UserContactConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"UserContact not found [ids:{newEntity.UserID}, {newEntity.ContactID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

