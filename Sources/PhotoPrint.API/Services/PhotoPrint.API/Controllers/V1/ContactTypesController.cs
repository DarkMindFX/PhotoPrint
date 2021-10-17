


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
    public class ContactTypesController : BaseController
    {
        private readonly Dal.IContactTypeDal _dalContactType;
        private readonly ILogger<ContactTypesController> _logger;


        public ContactTypesController( Dal.IContactTypeDal dalContactType,
                                    ILogger<ContactTypesController> logger)
        {
            _dalContactType = dalContactType; 
            _logger = logger;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalContactType.GetAll();

            IList<DTO.ContactType> dtos = new List<DTO.ContactType>();

            foreach (var p in entities)
            {
                var dto = ContactTypeConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{id}"), ActionName("GetContactType")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalContactType.Get(id);
            if (entity != null)
            {
                var dto = ContactTypeConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"ContactType was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpDelete("{id}"), ActionName("DeleteContactType")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalContactType.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalContactType.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete ContactType [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"ContactType not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertContactType")]
        public IActionResult Insert(DTO.ContactType dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = ContactTypeConvertor.Convert(dto);

            ContactType newEntity = _dalContactType.Insert(entity);

            response =StatusCode((int)HttpStatusCode.Created, ContactTypeConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateContactType")]
        public IActionResult Update(DTO.ContactType dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = ContactTypeConvertor.Convert(dto);

            var existingEntity = _dalContactType.Get(newEntity.ID);
            if (existingEntity != null)
            {
                ContactType entity = _dalContactType.Update(newEntity);

                response = Ok(ContactTypeConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"ContactType not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

