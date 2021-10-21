


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
    public class DeliveryServiceCitiesController : BaseController
    {
        private readonly Dal.IDeliveryServiceCityDal _dalDeliveryServiceCity;
        private readonly ILogger<DeliveryServiceCitiesController> _logger;


        public DeliveryServiceCitiesController( Dal.IDeliveryServiceCityDal dalDeliveryServiceCity,
                                    ILogger<DeliveryServiceCitiesController> logger)
        {
            _dalDeliveryServiceCity = dalDeliveryServiceCity; 
            _logger = logger;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalDeliveryServiceCity.GetAll();

            IList<DTO.DeliveryServiceCity> dtos = new List<DTO.DeliveryServiceCity>();

            foreach (var p in entities)
            {
                var dto = DeliveryServiceCityConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{deliveryserviceid}/{cityid}"), ActionName("GetDeliveryServiceCity")]
        public IActionResult Get(System.Int64 deliveryserviceid, System.Int64 cityid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalDeliveryServiceCity.Get(deliveryserviceid, cityid);
            if (entity != null)
            {
                var dto = DeliveryServiceCityConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"DeliveryServiceCity was not found [ids:{deliveryserviceid}, {cityid}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpDelete("{deliveryserviceid}/{cityid}"), ActionName("DeleteDeliveryServiceCity")]
        public IActionResult Delete(System.Int64 deliveryserviceid, System.Int64 cityid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalDeliveryServiceCity.Get(deliveryserviceid, cityid);

            if (existingEntity != null)
            {
                bool removed = _dalDeliveryServiceCity.Delete(deliveryserviceid, cityid);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete DeliveryServiceCity [ids:{deliveryserviceid}, {cityid}]");
                }
            }
            else
            {
                response = NotFound($"DeliveryServiceCity not found [ids:{deliveryserviceid}, {cityid}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertDeliveryServiceCity")]
        public IActionResult Insert(DTO.DeliveryServiceCity dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = DeliveryServiceCityConvertor.Convert(dto);

            DeliveryServiceCity newEntity = _dalDeliveryServiceCity.Insert(entity);

            
            response = StatusCode((int)HttpStatusCode.Created, DeliveryServiceCityConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateDeliveryServiceCity")]
        public IActionResult Update(DTO.DeliveryServiceCity dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = DeliveryServiceCityConvertor.Convert(dto);

            var existingEntity = _dalDeliveryServiceCity.Get(newEntity.DeliveryServiceID, newEntity.CityID);           

            if (existingEntity != null)
            {
                                                    DeliveryServiceCity entity = _dalDeliveryServiceCity.Update(newEntity);

                response = Ok(DeliveryServiceCityConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"DeliveryServiceCity not found [ids:{newEntity.DeliveryServiceID}, {newEntity.CityID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

