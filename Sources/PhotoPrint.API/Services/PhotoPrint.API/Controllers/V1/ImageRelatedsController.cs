


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
    public class ImageRelatedsController : BaseController
    {
        private readonly Dal.IImageRelatedDal _dalImageRelated;
        private readonly ILogger<ImageRelatedsController> _logger;


        public ImageRelatedsController( Dal.IImageRelatedDal dalImageRelated,
                                    ILogger<ImageRelatedsController> logger)
        {
            _dalImageRelated = dalImageRelated; 
            _logger = logger;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalImageRelated.GetAll();

            IList<DTO.ImageRelated> dtos = new List<DTO.ImageRelated>();

            foreach (var p in entities)
            {
                var dto = ImageRelatedConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{imageid}/{relatedimageid}"), ActionName("GetImageRelated")]
        public IActionResult Get(System.Int64 imageid, System.Int64 relatedimageid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalImageRelated.Get(imageid, relatedimageid);
            if (entity != null)
            {
                var dto = ImageRelatedConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"ImageRelated was not found [ids:{imageid}, {relatedimageid}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpDelete("{imageid}/{relatedimageid}"), ActionName("DeleteImageRelated")]
        public IActionResult Delete(System.Int64 imageid, System.Int64 relatedimageid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalImageRelated.Get(imageid, relatedimageid);

            if (existingEntity != null)
            {
                bool removed = _dalImageRelated.Delete(imageid, relatedimageid);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete ImageRelated [ids:{imageid}, {relatedimageid}]");
                }
            }
            else
            {
                response = NotFound($"ImageRelated not found [ids:{imageid}, {relatedimageid}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertImageRelated")]
        public IActionResult Insert(DTO.ImageRelated dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = ImageRelatedConvertor.Convert(dto);

            ImageRelated newEntity = _dalImageRelated.Insert(entity);

            
            response = StatusCode((int)HttpStatusCode.Created, ImageRelatedConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateImageRelated")]
        public IActionResult Update(DTO.ImageRelated dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = ImageRelatedConvertor.Convert(dto);

            var existingEntity = _dalImageRelated.Get(newEntity.ImageID, newEntity.RelatedImageID);           

            if (existingEntity != null)
            {
                                                    ImageRelated entity = _dalImageRelated.Update(newEntity);

                response = Ok(ImageRelatedConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"ImageRelated not found [ids:{newEntity.ImageID}, {newEntity.RelatedImageID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

