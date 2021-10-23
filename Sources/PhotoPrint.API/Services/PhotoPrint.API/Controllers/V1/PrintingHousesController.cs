


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
    public class PrintingHousesController : BaseController
    {
        private readonly Dal.IPrintingHouseDal _dalPrintingHouse;
        private readonly ILogger<PrintingHousesController> _logger;


        public PrintingHousesController( Dal.IPrintingHouseDal dalPrintingHouse,
                                    ILogger<PrintingHousesController> logger)
        {
            _dalPrintingHouse = dalPrintingHouse; 
            _logger = logger;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalPrintingHouse.GetAll();

            IList<DTO.PrintingHouse> dtos = new List<DTO.PrintingHouse>();

            foreach (var p in entities)
            {
                var dto = PrintingHouseConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{id}"), ActionName("GetPrintingHouse")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalPrintingHouse.Get(id);
            if (entity != null)
            {
                var dto = PrintingHouseConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"PrintingHouse was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

                //[Authorize]
        [HttpGet("/bycreatedbyid/:createdbyid")]
        public IActionResult GetByCreatedByID(System.Int64 createdbyid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalPrintingHouse.GetByCreatedByID(createdbyid);

            IList<DTO.PrintingHouse> dtos = new List<DTO.PrintingHouse>();

            foreach (var p in entities)
            {
                var dto = PrintingHouseConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
                //[Authorize]
        [HttpGet("/bymodifiedbyid/:modifiedbyid")]
        public IActionResult GetByModifiedByID(System.Int64? modifiedbyid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalPrintingHouse.GetByModifiedByID(modifiedbyid);

            IList<DTO.PrintingHouse> dtos = new List<DTO.PrintingHouse>();

            foreach (var p in entities)
            {
                var dto = PrintingHouseConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
        
        //[Authorize]
        [HttpDelete("{id}"), ActionName("DeletePrintingHouse")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalPrintingHouse.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalPrintingHouse.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete PrintingHouse [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"PrintingHouse not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertPrintingHouse")]
        public IActionResult Insert(DTO.PrintingHouse dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = PrintingHouseConvertor.Convert(dto);

            PrintingHouse newEntity = _dalPrintingHouse.Insert(entity);

                        base.SetCreatedModifiedProperties(entity, 
                                    "CreatedDate", 
                                    "CreatedByID"); 
            
            response = StatusCode((int)HttpStatusCode.Created, PrintingHouseConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdatePrintingHouse")]
        public IActionResult Update(DTO.PrintingHouse dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = PrintingHouseConvertor.Convert(dto);

            var existingEntity = _dalPrintingHouse.Get(newEntity.ID);           

            if (existingEntity != null)
            {
                        newEntity.CreatedDate = existingEntity.CreatedDate; 
                                    newEntity.CreatedByID = existingEntity.CreatedByID; 
                        
            base.SetCreatedModifiedProperties(newEntity, 
                                    "ModifiedDate", 
                                    "ModifiedByID"); 
                            PrintingHouse entity = _dalPrintingHouse.Update(newEntity);

                response = Ok(PrintingHouseConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"PrintingHouse not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

