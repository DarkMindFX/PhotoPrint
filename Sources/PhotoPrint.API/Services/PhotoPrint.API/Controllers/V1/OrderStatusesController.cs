


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
    public class OrderStatusesController : BaseController
    {
        private readonly Dal.IOrderStatusDal _dalOrderStatus;
        private readonly ILogger<OrderStatusesController> _logger;


        public OrderStatusesController( Dal.IOrderStatusDal dalOrderStatus,
                                    ILogger<OrderStatusesController> logger)
        {
            _dalOrderStatus = dalOrderStatus; 
            _logger = logger;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalOrderStatus.GetAll();

            IList<DTO.OrderStatus> dtos = new List<DTO.OrderStatus>();

            foreach (var p in entities)
            {
                var dto = OrderStatusConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{id}"), ActionName("GetOrderStatus")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalOrderStatus.Get(id);
            if (entity != null)
            {
                var dto = OrderStatusConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"OrderStatus was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpDelete("{id}"), ActionName("DeleteOrderStatus")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalOrderStatus.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalOrderStatus.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete OrderStatus [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"OrderStatus not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertOrderStatus")]
        public IActionResult Insert(DTO.OrderStatus dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = OrderStatusConvertor.Convert(dto);

            OrderStatus newEntity = _dalOrderStatus.Insert(entity);

            
            response = StatusCode((int)HttpStatusCode.Created, OrderStatusConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateOrderStatus")]
        public IActionResult Update(DTO.OrderStatus dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = OrderStatusConvertor.Convert(dto);

            var existingEntity = _dalOrderStatus.Get(newEntity.ID);           

            if (existingEntity != null)
            {
                                                    OrderStatus entity = _dalOrderStatus.Update(newEntity);

                response = Ok(OrderStatusConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"OrderStatus not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

