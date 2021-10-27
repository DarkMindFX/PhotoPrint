


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
    public class PaymentMethodsController : BaseController
    {
        private readonly Dal.IPaymentMethodDal _dalPaymentMethod;
        private readonly ILogger<PaymentMethodsController> _logger;


        public PaymentMethodsController( Dal.IPaymentMethodDal dalPaymentMethod,
                                    ILogger<PaymentMethodsController> logger)
        {
            _dalPaymentMethod = dalPaymentMethod; 
            _logger = logger;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalPaymentMethod.GetAll();

            IList<DTO.PaymentMethod> dtos = new List<DTO.PaymentMethod>();

            foreach (var p in entities)
            {
                var dto = PaymentMethodConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{id}"), ActionName("GetPaymentMethod")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalPaymentMethod.Get(id);
            if (entity != null)
            {
                var dto = PaymentMethodConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"PaymentMethod was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        
        //[Authorize]
        [HttpDelete("{id}"), ActionName("DeletePaymentMethod")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalPaymentMethod.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalPaymentMethod.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete PaymentMethod [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"PaymentMethod not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertPaymentMethod")]
        public IActionResult Insert(DTO.PaymentMethod dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = PaymentMethodConvertor.Convert(dto);           

            
            PaymentMethod newEntity = _dalPaymentMethod.Insert(entity);

            response = StatusCode((int)HttpStatusCode.Created, PaymentMethodConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdatePaymentMethod")]
        public IActionResult Update(DTO.PaymentMethod dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = PaymentMethodConvertor.Convert(dto);

            var existingEntity = _dalPaymentMethod.Get(newEntity.ID);           

            if (existingEntity != null)
            {
                                                    PaymentMethod entity = _dalPaymentMethod.Update(newEntity);

                response = Ok(PaymentMethodConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"PaymentMethod not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

