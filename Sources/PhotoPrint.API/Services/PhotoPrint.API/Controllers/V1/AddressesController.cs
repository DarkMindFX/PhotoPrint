


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
    public class AddressesController : BaseController
    {
        private readonly PPT.Services.Dal.IAddressDal _dalAddress;
        private readonly ILogger<AddressesController> _logger;


        public AddressesController( PPT.Services.Dal.IAddressDal dalAddress,
                                    ILogger<AddressesController> logger)
        {
            _dalAddress = dalAddress; 
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalAddress.GetAll();

            IList<DTO.Address> dtos = new List<DTO.Address>();

            foreach (var p in entities)
            {
                var dto = AddressConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpGet("{id}"), ActionName("GetAddress")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalAddress.Get(id);
            if (entity != null)
            {
                var dto = AddressConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"Address was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpGet("byaddresstypeid/{addresstypeid}")]
        public IActionResult GetByAddressTypeID(System.Int64 addresstypeid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalAddress.GetByAddressTypeID(addresstypeid);

            IList<DTO.Address> dtos = new List<DTO.Address>();

            foreach (var p in entities)
            {
                var dto = AddressConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
        [Authorize]
        [HttpGet("bycityid/{cityid}")]
        public IActionResult GetByCityID(System.Int64 cityid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalAddress.GetByCityID(cityid);

            IList<DTO.Address> dtos = new List<DTO.Address>();

            foreach (var p in entities)
            {
                var dto = AddressConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
        [Authorize]
        [HttpGet("bycreatedbyid/{createdbyid}")]
        public IActionResult GetByCreatedByID(System.Int64 createdbyid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalAddress.GetByCreatedByID(createdbyid);

            IList<DTO.Address> dtos = new List<DTO.Address>();

            foreach (var p in entities)
            {
                var dto = AddressConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
        [Authorize]
        [HttpGet("bymodifiedbyid/{modifiedbyid}")]
        public IActionResult GetByModifiedByID(System.Int64? modifiedbyid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalAddress.GetByModifiedByID(modifiedbyid);

            IList<DTO.Address> dtos = new List<DTO.Address>();

            foreach (var p in entities)
            {
                var dto = AddressConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
        
        [Authorize]
        [HttpDelete("{id}"), ActionName("DeleteAddress")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalAddress.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalAddress.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete Address [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"Address not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [HttpPost, ActionName("InsertAddress")]
        public IActionResult Insert(DTO.Address dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = AddressConvertor.Convert(dto);           

                        base.SetCreatedModifiedProperties(entity, 
                                    "CreatedDate", 
                                    "CreatedByID"); 
            
            Address newEntity = _dalAddress.Insert(entity);

            response = StatusCode((int)HttpStatusCode.Created, AddressConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        [Authorize]
        [HttpPut, ActionName("UpdateAddress")]
        public IActionResult Update(DTO.Address dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = AddressConvertor.Convert(dto);

            var existingEntity = _dalAddress.Get(newEntity.ID);           

            if (existingEntity != null)
            {
                        newEntity.CreatedDate = existingEntity.CreatedDate; 
                                    newEntity.CreatedByID = existingEntity.CreatedByID; 
                        
            base.SetCreatedModifiedProperties(newEntity, 
                                    "ModifiedDate", 
                                    "ModifiedByID"); 
                            Address entity = _dalAddress.Update(newEntity);

                response = Ok(AddressConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"Address not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

