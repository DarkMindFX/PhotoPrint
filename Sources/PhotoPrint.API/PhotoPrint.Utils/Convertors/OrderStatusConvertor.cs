




using Microsoft.AspNetCore.Mvc;
using System;

namespace PhotoPrint.Utils.Convertors
{
    public class OrderStatusConvertor
    {
        public static DTO.OrderStatus Convert(Interfaces.Entities.OrderStatus entity, IUrlHelper url)
        {
            var dto = new DTO.OrderStatus()
            {
        		        ID = entity.ID,

				        OrderStatusName = entity.OrderStatusName,

				        IsDeleted = entity.IsDeleted,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetOrderStatus", "orderstatuses", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteOrderStatus", "orderstatuses", new { id = dto.ID  }), "delete_orderstatus", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertOrderStatus", "orderstatuses"), "insert_orderstatus", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateOrderStatus", "orderstatuses"), "update_orderstatus", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.OrderStatus Convert(DTO.OrderStatus dto)
        {
            var entity = new Interfaces.Entities.OrderStatus()
            {
                
        		        ID = dto.ID,

				        OrderStatusName = dto.OrderStatusName,

				        IsDeleted = dto.IsDeleted,

				
     
            };

            return entity;
        }
    }
}
