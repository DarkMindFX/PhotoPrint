


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
    public class MaterialTypesController : BaseController
    {
        private readonly Dal.IMaterialTypeDal _dalMaterialType;
        private readonly ILogger<MaterialTypesController> _logger;


        public MaterialTypesController( Dal.IMaterialTypeDal dalMaterialType,
                                    ILogger<MaterialTypesController> logger)
        {
            _dalMaterialType = dalMaterialType; 
            _logger = logger;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalMaterialType.GetAll();

            IList<DTO.MaterialType> dtos = new List<DTO.MaterialType>();

            foreach (var p in entities)
            {
                var dto = MaterialTypeConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{id}"), ActionName("GetMaterialType")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalMaterialType.Get(id);
            if (entity != null)
            {
                var dto = MaterialTypeConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"MaterialType was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpDelete("{id}"), ActionName("DeleteMaterialType")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalMaterialType.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalMaterialType.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete MaterialType [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"MaterialType not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertMaterialType")]
        public IActionResult Insert(DTO.MaterialType dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = MaterialTypeConvertor.Convert(dto);

            MaterialType newEntity = _dalMaterialType.Insert(entity);

            response =StatusCode((int)HttpStatusCode.Created, MaterialTypeConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateMaterialType")]
        public IActionResult Update(DTO.MaterialType dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = MaterialTypeConvertor.Convert(dto);

            var existingEntity = _dalMaterialType.Get(newEntity.ID);
            if (existingEntity != null)
            {
                MaterialType entity = _dalMaterialType.Update(newEntity);

                response = Ok(MaterialTypeConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"MaterialType not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

