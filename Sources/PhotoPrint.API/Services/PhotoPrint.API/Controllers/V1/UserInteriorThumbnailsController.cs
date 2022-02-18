



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
using Microsoft.Extensions.Options;
using PPT.PhotoPrint.API.Helpers;


namespace PPT.PhotoPrint.API.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [UnhandledExceptionFilter]
    public class UserInteriorThumbnailsController : BaseController
    {
        private readonly Dal.IUserInteriorThumbnailDal _dalUserInteriorThumbnail;
        private readonly ILogger<UserInteriorThumbnailsController> _logger;
        private readonly IOptions<AppSettings> _appSettings;


        public UserInteriorThumbnailsController( Dal.IUserInteriorThumbnailDal dalUserInteriorThumbnail,
                                    ILogger<UserInteriorThumbnailsController> logger,
                                    IOptions<AppSettings> appSettings)
        {
            _dalUserInteriorThumbnail = dalUserInteriorThumbnail; 
            _logger = logger;
            _appSettings = appSettings;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalUserInteriorThumbnail.GetAll();

            IList<DTO.UserInteriorThumbnail> dtos = new List<DTO.UserInteriorThumbnail>();

            foreach (var p in entities)
            {
                var dto = UserInteriorThumbnailConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpGet("{id}"), ActionName("GetUserInteriorThumbnail")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalUserInteriorThumbnail.Get(id);
            if (entity != null)
            {
                var dto = UserInteriorThumbnailConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"UserInteriorThumbnail was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpGet("byuserid/{userid}")]
        public IActionResult GetByUserID(System.Int64 userid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalUserInteriorThumbnail.GetByUserID(userid);

            IList<DTO.UserInteriorThumbnail> dtos = new List<DTO.UserInteriorThumbnail>();

            foreach (var p in entities)
            {
                var dto = UserInteriorThumbnailConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
        
        [Authorize]
        [HttpDelete("{id}"), ActionName("DeleteUserInteriorThumbnail")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalUserInteriorThumbnail.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalUserInteriorThumbnail.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete UserInteriorThumbnail [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"UserInteriorThumbnail not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpPost, ActionName("InsertUserInteriorThumbnail")]
        public IActionResult Insert(DTO.UserInteriorThumbnail dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = UserInteriorThumbnailConvertor.Convert(dto);           

            
            UserInteriorThumbnail newEntity = _dalUserInteriorThumbnail.Insert(entity);

            response = StatusCode((int)HttpStatusCode.Created, UserInteriorThumbnailConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        [Authorize]
        [HttpPut, ActionName("UpdateUserInteriorThumbnail")]
        public IActionResult Update(DTO.UserInteriorThumbnail dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = UserInteriorThumbnailConvertor.Convert(dto);

            var existingEntity = _dalUserInteriorThumbnail.Get(newEntity.ID);           

            if (existingEntity != null)
            {
                                                    UserInteriorThumbnail entity = _dalUserInteriorThumbnail.Update(newEntity);

                response = Ok(UserInteriorThumbnailConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"UserInteriorThumbnail not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

