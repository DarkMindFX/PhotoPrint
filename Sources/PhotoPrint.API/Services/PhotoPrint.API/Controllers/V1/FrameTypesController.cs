


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
    public class FrameTypesController : BaseController
    {
        private readonly Dal.IFrameTypeDal _dalFrameType;
        private readonly ILogger<FrameTypesController> _logger;


        public FrameTypesController( Dal.IFrameTypeDal dalFrameType,
                                    ILogger<FrameTypesController> logger)
        {
            _dalFrameType = dalFrameType; 
            _logger = logger;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalFrameType.GetAll();

            IList<DTO.FrameType> dtos = new List<DTO.FrameType>();

            foreach (var p in entities)
            {
                var dto = FrameTypeConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{id}"), ActionName("GetFrameType")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalFrameType.Get(id);
            if (entity != null)
            {
                var dto = FrameTypeConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"FrameType was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpDelete("{id}"), ActionName("DeleteFrameType")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalFrameType.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalFrameType.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete FrameType [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"FrameType not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertFrameType")]
        public IActionResult Insert(DTO.FrameType dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = FrameTypeConvertor.Convert(dto);

            FrameType newEntity = _dalFrameType.Insert(entity);

            response =StatusCode((int)HttpStatusCode.Created, FrameTypeConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateFrameType")]
        public IActionResult Update(DTO.FrameType dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = FrameTypeConvertor.Convert(dto);

            var existingEntity = _dalFrameType.Get(newEntity.ID);
            if (existingEntity != null)
            {
                FrameType entity = _dalFrameType.Update(newEntity);

                response = Ok(FrameTypeConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"FrameType not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

