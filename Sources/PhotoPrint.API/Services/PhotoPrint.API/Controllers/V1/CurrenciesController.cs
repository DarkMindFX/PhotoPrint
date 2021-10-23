


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
    public class CurrenciesController : BaseController
    {
        private readonly Dal.ICurrencyDal _dalCurrency;
        private readonly ILogger<CurrenciesController> _logger;


        public CurrenciesController( Dal.ICurrencyDal dalCurrency,
                                    ILogger<CurrenciesController> logger)
        {
            _dalCurrency = dalCurrency; 
            _logger = logger;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalCurrency.GetAll();

            IList<DTO.Currency> dtos = new List<DTO.Currency>();

            foreach (var p in entities)
            {
                var dto = CurrencyConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{id}"), ActionName("GetCurrency")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalCurrency.Get(id);
            if (entity != null)
            {
                var dto = CurrencyConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"Currency was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        
        //[Authorize]
        [HttpDelete("{id}"), ActionName("DeleteCurrency")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalCurrency.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalCurrency.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete Currency [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"Currency not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertCurrency")]
        public IActionResult Insert(DTO.Currency dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = CurrencyConvertor.Convert(dto);

            Currency newEntity = _dalCurrency.Insert(entity);

            
            response = StatusCode((int)HttpStatusCode.Created, CurrencyConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateCurrency")]
        public IActionResult Update(DTO.Currency dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = CurrencyConvertor.Convert(dto);

            var existingEntity = _dalCurrency.Get(newEntity.ID);           

            if (existingEntity != null)
            {
                                                    Currency entity = _dalCurrency.Update(newEntity);

                response = Ok(CurrencyConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"Currency not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

