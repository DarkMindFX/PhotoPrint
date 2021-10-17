


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
    public class OrderTrackingsController : BaseController
    {
        private readonly Dal.IOrderTrackingDal _dalOrderTracking;
        private readonly ILogger<OrderTrackingsController> _logger;


        public OrderTrackingsController( Dal.IOrderTrackingDal dalOrderTracking,
                                    ILogger<OrderTrackingsController> logger)
        {
            _dalOrderTracking = dalOrderTracking; 
            _logger = logger;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalOrderTracking.GetAll();

            IList<DTO.OrderTracking> dtos = new List<DTO.OrderTracking>();

            foreach (var p in entities)
            {
                var dto = OrderTrackingConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{id}"), ActionName("GetOrderTracking")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalOrderTracking.Get(id);
            if (entity != null)
            {
                var dto = OrderTrackingConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"OrderTracking was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpDelete("{id}"), ActionName("DeleteOrderTracking")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalOrderTracking.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalOrderTracking.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete OrderTracking [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"OrderTracking not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertOrderTracking")]
        public IActionResult Insert(DTO.OrderTracking dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = OrderTrackingConvertor.Convert(dto);

            OrderTracking newEntity = _dalOrderTracking.Insert(entity);

            response =StatusCode((int)HttpStatusCode.Created, OrderTrackingConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateOrderTracking")]
        public IActionResult Update(DTO.OrderTracking dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = OrderTrackingConvertor.Convert(dto);

            var existingEntity = _dalOrderTracking.Get(newEntity.ID);
            if (existingEntity != null)
            {
                OrderTracking entity = _dalOrderTracking.Update(newEntity);

                response = Ok(OrderTrackingConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"OrderTracking not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

