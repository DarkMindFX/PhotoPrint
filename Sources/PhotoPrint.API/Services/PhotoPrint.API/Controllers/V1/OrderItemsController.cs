


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
        [HttpGet("/byorderid/:orderid")]
        public IActionResult GetByOrderID(System.Int64 orderid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalOrderItem.GetByOrderID(orderid);

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
        [HttpGet("/byimageid/:imageid")]
        public IActionResult GetByImageID(System.Int64 imageid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalOrderItem.GetByImageID(imageid);

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
        [HttpGet("/bysizeid/:sizeid")]
        public IActionResult GetBySizeID(System.Int64? sizeid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalOrderItem.GetBySizeID(sizeid);

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
        [HttpGet("/byframetypeid/:frametypeid")]
        public IActionResult GetByFrameTypeID(System.Int64 frametypeid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalOrderItem.GetByFrameTypeID(frametypeid);

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
        [HttpGet("/byframesizeid/:framesizeid")]
        public IActionResult GetByFrameSizeID(System.Int64? framesizeid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalOrderItem.GetByFrameSizeID(framesizeid);

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
        [HttpGet("/bymatid/:matid")]
        public IActionResult GetByMatID(System.Int64 matid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalOrderItem.GetByMatID(matid);

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
        [HttpGet("/bymaterialtypeid/:materialtypeid")]
        public IActionResult GetByMaterialTypeID(System.Int64 materialtypeid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalOrderItem.GetByMaterialTypeID(materialtypeid);

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
        [HttpGet("/bymountingtypeid/:mountingtypeid")]
        public IActionResult GetByMountingTypeID(System.Int64 mountingtypeid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalOrderItem.GetByMountingTypeID(mountingtypeid);

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
        [HttpGet("/bypricecurrencyid/:pricecurrencyid")]
        public IActionResult GetByPriceCurrencyID(System.Int64 pricecurrencyid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalOrderItem.GetByPriceCurrencyID(pricecurrencyid);

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
        [HttpGet("/byprintinghouseid/:printinghouseid")]
        public IActionResult GetByPrintingHouseID(System.Int64? printinghouseid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalOrderItem.GetByPrintingHouseID(printinghouseid);

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
        [HttpGet("/bycreatedbyid/:createdbyid")]
        public IActionResult GetByCreatedByID(System.Int64 createdbyid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalOrderItem.GetByCreatedByID(createdbyid);

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
        [HttpGet("/bymodifiedbyid/:modifiedbyid")]
        public IActionResult GetByModifiedByID(System.Int64? modifiedbyid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalOrderItem.GetByModifiedByID(modifiedbyid);

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

                        base.SetCreatedModifiedProperties(entity, 
                                    "CreatedDate", 
                                    "CreatedByID"); 
            
            response = StatusCode((int)HttpStatusCode.Created, OrderItemConvertor.Convert(newEntity, this.Url));

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
                        newEntity.CreatedDate = existingEntity.CreatedDate; 
                                    newEntity.CreatedByID = existingEntity.CreatedByID; 
                        
            base.SetCreatedModifiedProperties(newEntity, 
                                    "ModifiedDate", 
                                    "ModifiedByID"); 
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

