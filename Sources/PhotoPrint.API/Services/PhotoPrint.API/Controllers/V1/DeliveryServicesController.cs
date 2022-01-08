


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
    public class DeliveryServicesController : BaseController
    {
        private readonly PPT.Services.Dal.IDeliveryServiceDal _dalDeliveryService;
        private readonly ILogger<DeliveryServicesController> _logger;


        public DeliveryServicesController( PPT.Services.Dal.IDeliveryServiceDal dalDeliveryService,
                                    ILogger<DeliveryServicesController> logger)
        {
            _dalDeliveryService = dalDeliveryService; 
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalDeliveryService.GetAll();

            IList<DTO.DeliveryService> dtos = new List<DTO.DeliveryService>();

            foreach (var p in entities)
            {
                var dto = DeliveryServiceConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpGet("{id}"), ActionName("GetDeliveryService")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalDeliveryService.Get(id);
            if (entity != null)
            {
                var dto = DeliveryServiceConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"DeliveryService was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpGet("bycreatedbyid/{createdbyid}")]
        public IActionResult GetByCreatedByID(System.Int64 createdbyid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalDeliveryService.GetByCreatedByID(createdbyid);

            IList<DTO.DeliveryService> dtos = new List<DTO.DeliveryService>();

            foreach (var p in entities)
            {
                var dto = DeliveryServiceConvertor.Convert(p, this.Url);

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

            var entities = _dalDeliveryService.GetByModifiedByID(modifiedbyid);

            IList<DTO.DeliveryService> dtos = new List<DTO.DeliveryService>();

            foreach (var p in entities)
            {
                var dto = DeliveryServiceConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
        
        [Authorize]
        [HttpDelete("{id}"), ActionName("DeleteDeliveryService")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalDeliveryService.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalDeliveryService.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete DeliveryService [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"DeliveryService not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpPost, ActionName("InsertDeliveryService")]
        public IActionResult Insert(DTO.DeliveryService dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = DeliveryServiceConvertor.Convert(dto);           

                        base.SetCreatedModifiedProperties(entity, 
                                    "CreatedDate", 
                                    "CreatedByID"); 
            
            DeliveryService newEntity = _dalDeliveryService.Insert(entity);

            response = StatusCode((int)HttpStatusCode.Created, DeliveryServiceConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        [Authorize]
        [HttpPut, ActionName("UpdateDeliveryService")]
        public IActionResult Update(DTO.DeliveryService dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = DeliveryServiceConvertor.Convert(dto);

            var existingEntity = _dalDeliveryService.Get(newEntity.ID);           

            if (existingEntity != null)
            {
                        newEntity.CreatedDate = existingEntity.CreatedDate; 
                                    newEntity.CreatedByID = existingEntity.CreatedByID; 
                        
            base.SetCreatedModifiedProperties(newEntity, 
                                    "ModifiedDate", 
                                    "ModifiedByID"); 
                            DeliveryService entity = _dalDeliveryService.Update(newEntity);

                response = Ok(DeliveryServiceConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"DeliveryService not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

