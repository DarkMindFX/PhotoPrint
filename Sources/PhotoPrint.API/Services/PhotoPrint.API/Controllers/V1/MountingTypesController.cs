


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
    public class MountingTypesController : BaseController
    {
        private readonly Dal.IMountingTypeDal _dalMountingType;
        private readonly ILogger<MountingTypesController> _logger;


        public MountingTypesController( Dal.IMountingTypeDal dalMountingType,
                                    ILogger<MountingTypesController> logger)
        {
            _dalMountingType = dalMountingType; 
            _logger = logger;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalMountingType.GetAll();

            IList<DTO.MountingType> dtos = new List<DTO.MountingType>();

            foreach (var p in entities)
            {
                var dto = MountingTypeConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{id}"), ActionName("GetMountingType")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalMountingType.Get(id);
            if (entity != null)
            {
                var dto = MountingTypeConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"MountingType was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        
        //[Authorize]
        [HttpDelete("{id}"), ActionName("DeleteMountingType")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalMountingType.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalMountingType.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete MountingType [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"MountingType not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertMountingType")]
        public IActionResult Insert(DTO.MountingType dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = MountingTypeConvertor.Convert(dto);           

                        base.SetCreatedModifiedProperties(entity, 
                                    "CreatedDate", 
                                    "CreatedByID"); 
            
            MountingType newEntity = _dalMountingType.Insert(entity);

            response = StatusCode((int)HttpStatusCode.Created, MountingTypeConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateMountingType")]
        public IActionResult Update(DTO.MountingType dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = MountingTypeConvertor.Convert(dto);

            var existingEntity = _dalMountingType.Get(newEntity.ID);           

            if (existingEntity != null)
            {
                        newEntity.CreatedDate = existingEntity.CreatedDate; 
                                    newEntity.CreatedByID = existingEntity.CreatedByID; 
                        
            base.SetCreatedModifiedProperties(newEntity, 
                                    "ModifiedDate", 
                                    "ModifiedByID"); 
                            MountingType entity = _dalMountingType.Update(newEntity);

                response = Ok(MountingTypeConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"MountingType not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

