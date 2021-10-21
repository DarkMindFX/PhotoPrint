


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
    public class OrdersController : BaseController
    {
        private readonly Dal.IOrderDal _dalOrder;
        private readonly ILogger<OrdersController> _logger;


        public OrdersController(Dal.IOrderDal dalOrder,
                                    ILogger<OrdersController> logger)
        {
            _dalOrder = dalOrder;
            _logger = logger;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalOrder.GetAll();

            IList<DTO.Order> dtos = new List<DTO.Order>();

            foreach (var p in entities)
            {
                var dto = OrderConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{id}"), ActionName("GetOrder")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalOrder.Get(id);
            if (entity != null)
            {
                var dto = OrderConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"Order was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpDelete("{id}"), ActionName("DeleteOrder")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalOrder.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalOrder.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete Order [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"Order not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertOrder")]
        public IActionResult Insert(DTO.Order dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = OrderConvertor.Convert(dto);

            Order newEntity = _dalOrder.Insert(entity);

            base.SetCreatedModifiedProperties(entity,
                        "CreatedDate",
                        "CreatedByID");

            response = StatusCode((int)HttpStatusCode.Created, OrderConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateOrder")]
        public IActionResult Update(DTO.Order dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = OrderConvertor.Convert(dto);

            var existingEntity = _dalOrder.Get(newEntity.ID);

            if (existingEntity != null)
            {
                newEntity.CreatedDate = existingEntity.CreatedDate;
                newEntity.CreatedByID = existingEntity.CreatedByID;

                base.SetCreatedModifiedProperties(newEntity,
                                        "ModifiedDate",
                                        "ModifiedByID");
                Order entity = _dalOrder.Update(newEntity);

                response = Ok(OrderConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"Order not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

