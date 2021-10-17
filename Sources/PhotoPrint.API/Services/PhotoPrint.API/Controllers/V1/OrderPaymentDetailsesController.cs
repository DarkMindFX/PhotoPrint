


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
    public class OrderPaymentDetailsesController : BaseController
    {
        private readonly Dal.IOrderPaymentDetailsDal _dalOrderPaymentDetails;
        private readonly ILogger<OrderPaymentDetailsesController> _logger;


        public OrderPaymentDetailsesController( Dal.IOrderPaymentDetailsDal dalOrderPaymentDetails,
                                    ILogger<OrderPaymentDetailsesController> logger)
        {
            _dalOrderPaymentDetails = dalOrderPaymentDetails; 
            _logger = logger;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalOrderPaymentDetails.GetAll();

            IList<DTO.OrderPaymentDetails> dtos = new List<DTO.OrderPaymentDetails>();

            foreach (var p in entities)
            {
                var dto = OrderPaymentDetailsConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{id}"), ActionName("GetOrderPaymentDetails")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalOrderPaymentDetails.Get(id);
            if (entity != null)
            {
                var dto = OrderPaymentDetailsConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"OrderPaymentDetails was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpDelete("{id}"), ActionName("DeleteOrderPaymentDetails")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalOrderPaymentDetails.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalOrderPaymentDetails.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete OrderPaymentDetails [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"OrderPaymentDetails not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertOrderPaymentDetails")]
        public IActionResult Insert(DTO.OrderPaymentDetails dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = OrderPaymentDetailsConvertor.Convert(dto);

            OrderPaymentDetails newEntity = _dalOrderPaymentDetails.Insert(entity);

            response =StatusCode((int)HttpStatusCode.Created, OrderPaymentDetailsConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateOrderPaymentDetails")]
        public IActionResult Update(DTO.OrderPaymentDetails dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = OrderPaymentDetailsConvertor.Convert(dto);

            var existingEntity = _dalOrderPaymentDetails.Get(newEntity.ID);
            if (existingEntity != null)
            {
                OrderPaymentDetails entity = _dalOrderPaymentDetails.Update(newEntity);

                response = Ok(OrderPaymentDetailsConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"OrderPaymentDetails not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

