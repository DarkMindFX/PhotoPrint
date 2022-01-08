


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
    public class OrderStatusFlowsController : BaseController
    {
        private readonly PPT.Services.Dal.IOrderStatusFlowDal _dalOrderStatusFlow;
        private readonly ILogger<OrderStatusFlowsController> _logger;


        public OrderStatusFlowsController( PPT.Services.Dal.IOrderStatusFlowDal dalOrderStatusFlow,
                                    ILogger<OrderStatusFlowsController> logger)
        {
            _dalOrderStatusFlow = dalOrderStatusFlow; 
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalOrderStatusFlow.GetAll();

            IList<DTO.OrderStatusFlow> dtos = new List<DTO.OrderStatusFlow>();

            foreach (var p in entities)
            {
                var dto = OrderStatusFlowConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpGet("{fromstatusid}/{tostatusid}"), ActionName("GetOrderStatusFlow")]
        public IActionResult Get(System.Int64 fromstatusid, System.Int64 tostatusid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalOrderStatusFlow.Get(fromstatusid, tostatusid);
            if (entity != null)
            {
                var dto = OrderStatusFlowConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"OrderStatusFlow was not found [ids:{fromstatusid}, {tostatusid}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpGet("byfromstatusid/{fromstatusid}")]
        public IActionResult GetByFromStatusID(System.Int64 fromstatusid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalOrderStatusFlow.GetByFromStatusID(fromstatusid);

            IList<DTO.OrderStatusFlow> dtos = new List<DTO.OrderStatusFlow>();

            foreach (var p in entities)
            {
                var dto = OrderStatusFlowConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
        [Authorize]
        [HttpGet("bytostatusid/{tostatusid}")]
        public IActionResult GetByToStatusID(System.Int64 tostatusid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalOrderStatusFlow.GetByToStatusID(tostatusid);

            IList<DTO.OrderStatusFlow> dtos = new List<DTO.OrderStatusFlow>();

            foreach (var p in entities)
            {
                var dto = OrderStatusFlowConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
        
        [Authorize]
        [HttpDelete("{fromstatusid}/{tostatusid}"), ActionName("DeleteOrderStatusFlow")]
        public IActionResult Delete(System.Int64 fromstatusid, System.Int64 tostatusid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalOrderStatusFlow.Get(fromstatusid, tostatusid);

            if (existingEntity != null)
            {
                bool removed = _dalOrderStatusFlow.Delete(fromstatusid, tostatusid);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete OrderStatusFlow [ids:{fromstatusid}, {tostatusid}]");
                }
            }
            else
            {
                response = NotFound($"OrderStatusFlow not found [ids:{fromstatusid}, {tostatusid}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [Authorize]
        [HttpPost, ActionName("InsertOrderStatusFlow")]
        public IActionResult Insert(DTO.OrderStatusFlow dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = OrderStatusFlowConvertor.Convert(dto);           

            
            OrderStatusFlow newEntity = _dalOrderStatusFlow.Insert(entity);

            response = StatusCode((int)HttpStatusCode.Created, OrderStatusFlowConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        [Authorize]
        [HttpPut, ActionName("UpdateOrderStatusFlow")]
        public IActionResult Update(DTO.OrderStatusFlow dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = OrderStatusFlowConvertor.Convert(dto);

            var existingEntity = _dalOrderStatusFlow.Get(newEntity.FromStatusID, newEntity.ToStatusID);           

            if (existingEntity != null)
            {
                                                    OrderStatusFlow entity = _dalOrderStatusFlow.Update(newEntity);

                response = Ok(OrderStatusFlowConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"OrderStatusFlow not found [ids:{newEntity.FromStatusID}, {newEntity.ToStatusID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

