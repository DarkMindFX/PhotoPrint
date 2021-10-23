


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
    public class CategoriesController : BaseController
    {
        private readonly Dal.ICategoryDal _dalCategory;
        private readonly ILogger<CategoriesController> _logger;


        public CategoriesController( Dal.ICategoryDal dalCategory,
                                    ILogger<CategoriesController> logger)
        {
            _dalCategory = dalCategory; 
            _logger = logger;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalCategory.GetAll();

            IList<DTO.Category> dtos = new List<DTO.Category>();

            foreach (var p in entities)
            {
                var dto = CategoryConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{id}"), ActionName("GetCategory")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalCategory.Get(id);
            if (entity != null)
            {
                var dto = CategoryConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"Category was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

                //[Authorize]
        [HttpGet("/byparentid/:parentid")]
        public IActionResult GetByParentID(System.Int64? parentid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalCategory.GetByParentID(parentid);

            IList<DTO.Category> dtos = new List<DTO.Category>();

            foreach (var p in entities)
            {
                var dto = CategoryConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
                //[Authorize]
        [HttpGet("/bycreatedbyid/:createdbyid")]
        public IActionResult GetByCreatedByID(System.Int64 createdbyid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalCategory.GetByCreatedByID(createdbyid);

            IList<DTO.Category> dtos = new List<DTO.Category>();

            foreach (var p in entities)
            {
                var dto = CategoryConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
                //[Authorize]
        [HttpGet("/bymodifiedbyid/:modifiedbyid")]
        public IActionResult GetByModifiedByID(System.Int64? modifiedbyid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalCategory.GetByModifiedByID(modifiedbyid);

            IList<DTO.Category> dtos = new List<DTO.Category>();

            foreach (var p in entities)
            {
                var dto = CategoryConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
        
        //[Authorize]
        [HttpDelete("{id}"), ActionName("DeleteCategory")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalCategory.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalCategory.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete Category [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"Category not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertCategory")]
        public IActionResult Insert(DTO.Category dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = CategoryConvertor.Convert(dto);

            Category newEntity = _dalCategory.Insert(entity);

                        base.SetCreatedModifiedProperties(entity, 
                                    "CreatedDate", 
                                    "CreatedByID"); 
            
            response = StatusCode((int)HttpStatusCode.Created, CategoryConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateCategory")]
        public IActionResult Update(DTO.Category dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = CategoryConvertor.Convert(dto);

            var existingEntity = _dalCategory.Get(newEntity.ID);           

            if (existingEntity != null)
            {
                        newEntity.CreatedDate = existingEntity.CreatedDate; 
                                    newEntity.CreatedByID = existingEntity.CreatedByID; 
                        
            base.SetCreatedModifiedProperties(newEntity, 
                                    "ModifiedDate", 
                                    "ModifiedByID"); 
                            Category entity = _dalCategory.Update(newEntity);

                response = Ok(CategoryConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"Category not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

