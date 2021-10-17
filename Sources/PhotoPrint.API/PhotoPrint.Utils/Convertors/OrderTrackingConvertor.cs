




using Microsoft.AspNetCore.Mvc;
using System;

namespace PPT.Utils.Convertors
{
    public class OrderTrackingConvertor
    {
        public static DTO.OrderTracking Convert(Interfaces.Entities.OrderTracking entity, IUrlHelper url)
        {
            var dto = new DTO.OrderTracking()
            {
        		        ID = entity.ID,

				        OrderID = entity.OrderID,

				        OrderStatusID = entity.OrderStatusID,

				        SetDate = entity.SetDate,

				        SetByID = entity.SetByID,

				        Comment = entity.Comment,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetOrderTracking", "ordertrackings", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteOrderTracking", "ordertrackings", new { id = dto.ID  }), "delete_ordertracking", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertOrderTracking", "ordertrackings"), "insert_ordertracking", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateOrderTracking", "ordertrackings"), "update_ordertracking", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.OrderTracking Convert(DTO.OrderTracking dto)
        {
            var entity = new Interfaces.Entities.OrderTracking()
            {
                
        		        ID = dto.ID,

				        OrderID = dto.OrderID,

				        OrderStatusID = dto.OrderStatusID,

				        SetDate = dto.SetDate,

				        SetByID = dto.SetByID,

				        Comment = dto.Comment,

				
     
            };

            return entity;
        }
    }
}
