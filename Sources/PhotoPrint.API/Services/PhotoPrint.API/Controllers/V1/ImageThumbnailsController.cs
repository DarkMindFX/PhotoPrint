


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
    public class ImageThumbnailsController : BaseController
    {
        private readonly Dal.IImageThumbnailDal _dalImageThumbnail;
        private readonly ILogger<ImageThumbnailsController> _logger;


        public ImageThumbnailsController( Dal.IImageThumbnailDal dalImageThumbnail,
                                    ILogger<ImageThumbnailsController> logger)
        {
            _dalImageThumbnail = dalImageThumbnail; 
            _logger = logger;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalImageThumbnail.GetAll();

            IList<DTO.ImageThumbnail> dtos = new List<DTO.ImageThumbnail>();

            foreach (var p in entities)
            {
                var dto = ImageThumbnailConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{id}"), ActionName("GetImageThumbnail")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalImageThumbnail.Get(id);
            if (entity != null)
            {
                var dto = ImageThumbnailConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"ImageThumbnail was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpDelete("{id}"), ActionName("DeleteImageThumbnail")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalImageThumbnail.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalImageThumbnail.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete ImageThumbnail [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"ImageThumbnail not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertImageThumbnail")]
        public IActionResult Insert(DTO.ImageThumbnail dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = ImageThumbnailConvertor.Convert(dto);

            ImageThumbnail newEntity = _dalImageThumbnail.Insert(entity);

            
            response = StatusCode((int)HttpStatusCode.Created, ImageThumbnailConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateImageThumbnail")]
        public IActionResult Update(DTO.ImageThumbnail dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = ImageThumbnailConvertor.Convert(dto);

            var existingEntity = _dalImageThumbnail.Get(newEntity.ID);           

            if (existingEntity != null)
            {
                                                    ImageThumbnail entity = _dalImageThumbnail.Update(newEntity);

                response = Ok(ImageThumbnailConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"ImageThumbnail not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

