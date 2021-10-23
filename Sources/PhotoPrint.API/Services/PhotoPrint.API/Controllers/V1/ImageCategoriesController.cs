


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
    public class ImageCategoriesController : BaseController
    {
        private readonly Dal.IImageCategoryDal _dalImageCategory;
        private readonly ILogger<ImageCategoriesController> _logger;


        public ImageCategoriesController( Dal.IImageCategoryDal dalImageCategory,
                                    ILogger<ImageCategoriesController> logger)
        {
            _dalImageCategory = dalImageCategory; 
            _logger = logger;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalImageCategory.GetAll();

            IList<DTO.ImageCategory> dtos = new List<DTO.ImageCategory>();

            foreach (var p in entities)
            {
                var dto = ImageCategoryConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{imageid}/{categoryid}"), ActionName("GetImageCategory")]
        public IActionResult Get(System.Int64 imageid, System.Int64 categoryid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalImageCategory.Get(imageid, categoryid);
            if (entity != null)
            {
                var dto = ImageCategoryConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"ImageCategory was not found [ids:{imageid}, {categoryid}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("byimageid/{imageid}")]
        public IActionResult GetByImageID(System.Int64 imageid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalImageCategory.GetByImageID(imageid);

            IList<DTO.ImageCategory> dtos = new List<DTO.ImageCategory>();

            foreach (var p in entities)
            {
                var dto = ImageCategoryConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
        //[Authorize]
        [HttpGet("bycategoryid/{categoryid}")]
        public IActionResult GetByCategoryID(System.Int64 categoryid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalImageCategory.GetByCategoryID(categoryid);

            IList<DTO.ImageCategory> dtos = new List<DTO.ImageCategory>();

            foreach (var p in entities)
            {
                var dto = ImageCategoryConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
        
        //[Authorize]
        [HttpDelete("{imageid}/{categoryid}"), ActionName("DeleteImageCategory")]
        public IActionResult Delete(System.Int64 imageid, System.Int64 categoryid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalImageCategory.Get(imageid, categoryid);

            if (existingEntity != null)
            {
                bool removed = _dalImageCategory.Delete(imageid, categoryid);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete ImageCategory [ids:{imageid}, {categoryid}]");
                }
            }
            else
            {
                response = NotFound($"ImageCategory not found [ids:{imageid}, {categoryid}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertImageCategory")]
        public IActionResult Insert(DTO.ImageCategory dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = ImageCategoryConvertor.Convert(dto);

            ImageCategory newEntity = _dalImageCategory.Insert(entity);

            
            response = StatusCode((int)HttpStatusCode.Created, ImageCategoryConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateImageCategory")]
        public IActionResult Update(DTO.ImageCategory dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = ImageCategoryConvertor.Convert(dto);

            var existingEntity = _dalImageCategory.Get(newEntity.ImageID, newEntity.CategoryID);           

            if (existingEntity != null)
            {
                                                    ImageCategory entity = _dalImageCategory.Update(newEntity);

                response = Ok(ImageCategoryConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"ImageCategory not found [ids:{newEntity.ImageID}, {newEntity.CategoryID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

