


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
    public class UnitsController : BaseController
    {
        private readonly Dal.IUnitDal _dalUnit;
        private readonly ILogger<UnitsController> _logger;


        public UnitsController( Dal.IUnitDal dalUnit,
                                    ILogger<UnitsController> logger)
        {
            _dalUnit = dalUnit; 
            _logger = logger;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalUnit.GetAll();

            IList<DTO.Unit> dtos = new List<DTO.Unit>();

            foreach (var p in entities)
            {
                var dto = UnitConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{id}"), ActionName("GetUnit")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalUnit.Get(id);
            if (entity != null)
            {
                var dto = UnitConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"Unit was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        
        //[Authorize]
        [HttpDelete("{id}"), ActionName("DeleteUnit")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalUnit.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalUnit.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete Unit [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"Unit not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertUnit")]
        public IActionResult Insert(DTO.Unit dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = UnitConvertor.Convert(dto);           

            
            Unit newEntity = _dalUnit.Insert(entity);

            response = StatusCode((int)HttpStatusCode.Created, UnitConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateUnit")]
        public IActionResult Update(DTO.Unit dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = UnitConvertor.Convert(dto);

            var existingEntity = _dalUnit.Get(newEntity.ID);           

            if (existingEntity != null)
            {
                                                    Unit entity = _dalUnit.Update(newEntity);

                response = Ok(UnitConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"Unit not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

