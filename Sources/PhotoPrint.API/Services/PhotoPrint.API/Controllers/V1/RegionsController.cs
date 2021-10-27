


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
    public class RegionsController : BaseController
    {
        private readonly Dal.IRegionDal _dalRegion;
        private readonly ILogger<RegionsController> _logger;


        public RegionsController( Dal.IRegionDal dalRegion,
                                    ILogger<RegionsController> logger)
        {
            _dalRegion = dalRegion; 
            _logger = logger;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalRegion.GetAll();

            IList<DTO.Region> dtos = new List<DTO.Region>();

            foreach (var p in entities)
            {
                var dto = RegionConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{id}"), ActionName("GetRegion")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalRegion.Get(id);
            if (entity != null)
            {
                var dto = RegionConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"Region was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("bycountryid/{countryid}")]
        public IActionResult GetByCountryID(System.Int64 countryid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalRegion.GetByCountryID(countryid);

            IList<DTO.Region> dtos = new List<DTO.Region>();

            foreach (var p in entities)
            {
                var dto = RegionConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
        
        //[Authorize]
        [HttpDelete("{id}"), ActionName("DeleteRegion")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalRegion.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalRegion.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete Region [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"Region not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertRegion")]
        public IActionResult Insert(DTO.Region dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = RegionConvertor.Convert(dto);           

            
            Region newEntity = _dalRegion.Insert(entity);

            response = StatusCode((int)HttpStatusCode.Created, RegionConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateRegion")]
        public IActionResult Update(DTO.Region dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = RegionConvertor.Convert(dto);

            var existingEntity = _dalRegion.Get(newEntity.ID);           

            if (existingEntity != null)
            {
                                                    Region entity = _dalRegion.Update(newEntity);

                response = Ok(RegionConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"Region not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

