


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
    public class AddressTypesController : BaseController
    {
        private readonly Dal.IAddressTypeDal _dalAddressType;
        private readonly ILogger<AddressTypesController> _logger;


        public AddressTypesController( Dal.IAddressTypeDal dalAddressType,
                                    ILogger<AddressTypesController> logger)
        {
            _dalAddressType = dalAddressType; 
            _logger = logger;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalAddressType.GetAll();

            IList<DTO.AddressType> dtos = new List<DTO.AddressType>();

            foreach (var p in entities)
            {
                var dto = AddressTypeConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{id}"), ActionName("GetAddressType")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalAddressType.Get(id);
            if (entity != null)
            {
                var dto = AddressTypeConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"AddressType was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpDelete("{id}"), ActionName("DeleteAddressType")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalAddressType.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalAddressType.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete AddressType [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"AddressType not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertAddressType")]
        public IActionResult Insert(DTO.AddressType dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = AddressTypeConvertor.Convert(dto);

            AddressType newEntity = _dalAddressType.Insert(entity);

            
            response = StatusCode((int)HttpStatusCode.Created, AddressTypeConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateAddressType")]
        public IActionResult Update(DTO.AddressType dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = AddressTypeConvertor.Convert(dto);

            var existingEntity = _dalAddressType.Get(newEntity.ID);           

            if (existingEntity != null)
            {
                                                    AddressType entity = _dalAddressType.Update(newEntity);

                response = Ok(AddressTypeConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"AddressType not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

