


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
    public class ImagesController : BaseController
    {
        private readonly Dal.IImageDal _dalImage;
        private readonly ILogger<ImagesController> _logger;


        public ImagesController( Dal.IImageDal dalImage,
                                    ILogger<ImagesController> logger)
        {
            _dalImage = dalImage; 
            _logger = logger;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalImage.GetAll();

            IList<DTO.Image> dtos = new List<DTO.Image>();

            foreach (var p in entities)
            {
                var dto = ImageConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{id}"), ActionName("GetImage")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalImage.Get(id);
            if (entity != null)
            {
                var dto = ImageConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"Image was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("bypricecurrencyid/{pricecurrencyid}")]
        public IActionResult GetByPriceCurrencyID(System.Int64? pricecurrencyid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalImage.GetByPriceCurrencyID(pricecurrencyid);

            IList<DTO.Image> dtos = new List<DTO.Image>();

            foreach (var p in entities)
            {
                var dto = ImageConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
        //[Authorize]
        [HttpGet("bycreatedbyid/{createdbyid}")]
        public IActionResult GetByCreatedByID(System.Int64 createdbyid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalImage.GetByCreatedByID(createdbyid);

            IList<DTO.Image> dtos = new List<DTO.Image>();

            foreach (var p in entities)
            {
                var dto = ImageConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
        //[Authorize]
        [HttpGet("bymodifiedbyid/{modifiedbyid}")]
        public IActionResult GetByModifiedByID(System.Int64? modifiedbyid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalImage.GetByModifiedByID(modifiedbyid);

            IList<DTO.Image> dtos = new List<DTO.Image>();

            foreach (var p in entities)
            {
                var dto = ImageConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
        
        //[Authorize]
        [HttpDelete("{id}"), ActionName("DeleteImage")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalImage.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalImage.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete Image [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"Image not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertImage")]
        public IActionResult Insert(DTO.Image dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = ImageConvertor.Convert(dto);

            Image newEntity = _dalImage.Insert(entity);

                        base.SetCreatedModifiedProperties(entity, 
                                    "CreatedDate", 
                                    "CreatedByID"); 
            
            response = StatusCode((int)HttpStatusCode.Created, ImageConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateImage")]
        public IActionResult Update(DTO.Image dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = ImageConvertor.Convert(dto);

            var existingEntity = _dalImage.Get(newEntity.ID);           

            if (existingEntity != null)
            {
                        newEntity.CreatedDate = existingEntity.CreatedDate; 
                                    newEntity.CreatedByID = existingEntity.CreatedByID; 
                        
            base.SetCreatedModifiedProperties(newEntity, 
                                    "ModifiedDate", 
                                    "ModifiedByID"); 
                            Image entity = _dalImage.Update(newEntity);

                response = Ok(ImageConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"Image not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

