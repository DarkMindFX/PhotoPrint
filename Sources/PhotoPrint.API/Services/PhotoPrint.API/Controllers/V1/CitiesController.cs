


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
    public class CitiesController : BaseController
    {
        private readonly Dal.ICityDal _dalCity;
        private readonly ILogger<CitiesController> _logger;


        public CitiesController( Dal.ICityDal dalCity,
                                    ILogger<CitiesController> logger)
        {
            _dalCity = dalCity; 
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalCity.GetAll();

            IList<DTO.City> dtos = new List<DTO.City>();

            foreach (var p in entities)
            {
                var dto = CityConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpGet("{id}"), ActionName("GetCity")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalCity.Get(id);
            if (entity != null)
            {
                var dto = CityConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"City was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpGet("byregionid/{regionid}")]
        public IActionResult GetByRegionID(System.Int64 regionid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalCity.GetByRegionID(regionid);

            IList<DTO.City> dtos = new List<DTO.City>();

            foreach (var p in entities)
            {
                var dto = CityConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
        
        [Authorize]
        [HttpDelete("{id}"), ActionName("DeleteCity")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalCity.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalCity.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete City [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"City not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpPost, ActionName("InsertCity")]
        public IActionResult Insert(DTO.City dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = CityConvertor.Convert(dto);           

            
            City newEntity = _dalCity.Insert(entity);

            response = StatusCode((int)HttpStatusCode.Created, CityConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        [Authorize]
        [HttpPut, ActionName("UpdateCity")]
        public IActionResult Update(DTO.City dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = CityConvertor.Convert(dto);

            var existingEntity = _dalCity.Get(newEntity.ID);           

            if (existingEntity != null)
            {
                                                    City entity = _dalCity.Update(newEntity);

                response = Ok(CityConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"City not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

