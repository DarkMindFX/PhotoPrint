


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
    public class OrderItemsController : BaseController
    {
        private readonly Dal.IOrderItemDal _dalOrderItem;
        private readonly ILogger<OrderItemsController> _logger;


        public OrderItemsController( Dal.IOrderItemDal dalOrderItem,
                                    ILogger<OrderItemsController> logger)
        {
            _dalOrderItem = dalOrderItem; 
            _logger = logger;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalOrderItem.GetAll();

            IList<DTO.OrderItem> dtos = new List<DTO.OrderItem>();

            foreach (var p in entities)
            {
                var dto = OrderItemConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{id}"), ActionName("GetOrderItem")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalOrderItem.Get(id);
            if (entity != null)
            {
                var dto = OrderItemConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"OrderItem was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpDelete("{id}"), ActionName("DeleteOrderItem")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalOrderItem.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalOrderItem.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete OrderItem [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"OrderItem not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertOrderItem")]
        public IActionResult Insert(DTO.OrderItem dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = OrderItemConvertor.Convert(dto);

            OrderItem newEntity = _dalOrderItem.Insert(entity);

            response =StatusCode((int)HttpStatusCode.Created, OrderItemConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateOrderItem")]
        public IActionResult Update(DTO.OrderItem dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = OrderItemConvertor.Convert(dto);

            var existingEntity = _dalOrderItem.Get(newEntity.ID);
            if (existingEntity != null)
            {
                OrderItem entity = _dalOrderItem.Update(newEntity);

                response = Ok(OrderItemConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"OrderItem not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

