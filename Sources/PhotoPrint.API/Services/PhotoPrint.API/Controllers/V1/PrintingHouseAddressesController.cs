


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
    public class PrintingHouseAddressesController : BaseController
    {
        private readonly Dal.IPrintingHouseAddressDal _dalPrintingHouseAddress;
        private readonly ILogger<PrintingHouseAddressesController> _logger;


        public PrintingHouseAddressesController( Dal.IPrintingHouseAddressDal dalPrintingHouseAddress,
                                    ILogger<PrintingHouseAddressesController> logger)
        {
            _dalPrintingHouseAddress = dalPrintingHouseAddress; 
            _logger = logger;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalPrintingHouseAddress.GetAll();

            IList<DTO.PrintingHouseAddress> dtos = new List<DTO.PrintingHouseAddress>();

            foreach (var p in entities)
            {
                var dto = PrintingHouseAddressConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{printinghouseid}/{addressid}"), ActionName("GetPrintingHouseAddress")]
        public IActionResult Get(System.Int64 printinghouseid, System.Int64 addressid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalPrintingHouseAddress.Get(printinghouseid, addressid);
            if (entity != null)
            {
                var dto = PrintingHouseAddressConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"PrintingHouseAddress was not found [ids:{printinghouseid}, {addressid}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("byprintinghouseid/{printinghouseid}")]
        public IActionResult GetByPrintingHouseID(System.Int64 printinghouseid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalPrintingHouseAddress.GetByPrintingHouseID(printinghouseid);

            IList<DTO.PrintingHouseAddress> dtos = new List<DTO.PrintingHouseAddress>();

            foreach (var p in entities)
            {
                var dto = PrintingHouseAddressConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
        //[Authorize]
        [HttpGet("byaddressid/{addressid}")]
        public IActionResult GetByAddressID(System.Int64 addressid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalPrintingHouseAddress.GetByAddressID(addressid);

            IList<DTO.PrintingHouseAddress> dtos = new List<DTO.PrintingHouseAddress>();

            foreach (var p in entities)
            {
                var dto = PrintingHouseAddressConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
        
        //[Authorize]
        [HttpDelete("{printinghouseid}/{addressid}"), ActionName("DeletePrintingHouseAddress")]
        public IActionResult Delete(System.Int64 printinghouseid, System.Int64 addressid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalPrintingHouseAddress.Get(printinghouseid, addressid);

            if (existingEntity != null)
            {
                bool removed = _dalPrintingHouseAddress.Delete(printinghouseid, addressid);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete PrintingHouseAddress [ids:{printinghouseid}, {addressid}]");
                }
            }
            else
            {
                response = NotFound($"PrintingHouseAddress not found [ids:{printinghouseid}, {addressid}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertPrintingHouseAddress")]
        public IActionResult Insert(DTO.PrintingHouseAddress dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = PrintingHouseAddressConvertor.Convert(dto);           

            
            PrintingHouseAddress newEntity = _dalPrintingHouseAddress.Insert(entity);

            response = StatusCode((int)HttpStatusCode.Created, PrintingHouseAddressConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdatePrintingHouseAddress")]
        public IActionResult Update(DTO.PrintingHouseAddress dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = PrintingHouseAddressConvertor.Convert(dto);

            var existingEntity = _dalPrintingHouseAddress.Get(newEntity.PrintingHouseID, newEntity.AddressID);           

            if (existingEntity != null)
            {
                                                    PrintingHouseAddress entity = _dalPrintingHouseAddress.Update(newEntity);

                response = Ok(PrintingHouseAddressConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"PrintingHouseAddress not found [ids:{newEntity.PrintingHouseID}, {newEntity.AddressID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

