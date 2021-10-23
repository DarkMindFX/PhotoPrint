


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
    public class PrintingHouseContactsController : BaseController
    {
        private readonly Dal.IPrintingHouseContactDal _dalPrintingHouseContact;
        private readonly ILogger<PrintingHouseContactsController> _logger;


        public PrintingHouseContactsController( Dal.IPrintingHouseContactDal dalPrintingHouseContact,
                                    ILogger<PrintingHouseContactsController> logger)
        {
            _dalPrintingHouseContact = dalPrintingHouseContact; 
            _logger = logger;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalPrintingHouseContact.GetAll();

            IList<DTO.PrintingHouseContact> dtos = new List<DTO.PrintingHouseContact>();

            foreach (var p in entities)
            {
                var dto = PrintingHouseContactConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{printinghouseid}/{contactid}"), ActionName("GetPrintingHouseContact")]
        public IActionResult Get(System.Int64 printinghouseid, System.Int64 contactid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalPrintingHouseContact.Get(printinghouseid, contactid);
            if (entity != null)
            {
                var dto = PrintingHouseContactConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"PrintingHouseContact was not found [ids:{printinghouseid}, {contactid}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

                //[Authorize]
        [HttpGet("/byprintinghouseid/:printinghouseid")]
        public IActionResult GetByPrintingHouseID(System.Int64 printinghouseid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalPrintingHouseContact.GetByPrintingHouseID(printinghouseid);

            IList<DTO.PrintingHouseContact> dtos = new List<DTO.PrintingHouseContact>();

            foreach (var p in entities)
            {
                var dto = PrintingHouseContactConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
                //[Authorize]
        [HttpGet("/bycontactid/:contactid")]
        public IActionResult GetByContactID(System.Int64 contactid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalPrintingHouseContact.GetByContactID(contactid);

            IList<DTO.PrintingHouseContact> dtos = new List<DTO.PrintingHouseContact>();

            foreach (var p in entities)
            {
                var dto = PrintingHouseContactConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
        
        //[Authorize]
        [HttpDelete("{printinghouseid}/{contactid}"), ActionName("DeletePrintingHouseContact")]
        public IActionResult Delete(System.Int64 printinghouseid, System.Int64 contactid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalPrintingHouseContact.Get(printinghouseid, contactid);

            if (existingEntity != null)
            {
                bool removed = _dalPrintingHouseContact.Delete(printinghouseid, contactid);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete PrintingHouseContact [ids:{printinghouseid}, {contactid}]");
                }
            }
            else
            {
                response = NotFound($"PrintingHouseContact not found [ids:{printinghouseid}, {contactid}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertPrintingHouseContact")]
        public IActionResult Insert(DTO.PrintingHouseContact dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = PrintingHouseContactConvertor.Convert(dto);

            PrintingHouseContact newEntity = _dalPrintingHouseContact.Insert(entity);

            
            response = StatusCode((int)HttpStatusCode.Created, PrintingHouseContactConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdatePrintingHouseContact")]
        public IActionResult Update(DTO.PrintingHouseContact dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = PrintingHouseContactConvertor.Convert(dto);

            var existingEntity = _dalPrintingHouseContact.Get(newEntity.PrintingHouseID, newEntity.ContactID);           

            if (existingEntity != null)
            {
                                                    PrintingHouseContact entity = _dalPrintingHouseContact.Update(newEntity);

                response = Ok(PrintingHouseContactConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"PrintingHouseContact not found [ids:{newEntity.PrintingHouseID}, {newEntity.ContactID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

