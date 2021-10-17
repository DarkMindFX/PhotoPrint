




using Microsoft.AspNetCore.Mvc;
using System;

namespace PhotoPrint.Utils.Convertors
{
    public class OrderStatusFlowConvertor
    {
        public static DTO.OrderStatusFlow Convert(Interfaces.Entities.OrderStatusFlow entity, IUrlHelper url)
        {
            var dto = new DTO.OrderStatusFlow()
            {
        		        FromStatusID = entity.FromStatusID,

				        ToStatusID = entity.ToStatusID,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetOrderStatusFlow", "orderstatusflows", new { fromstatusid = dto.FromStatusID, tostatusid = dto.ToStatusID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteOrderStatusFlow", "orderstatusflows", new { fromstatusid = dto.FromStatusID, tostatusid = dto.ToStatusID  }), "delete_orderstatusflow", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertOrderStatusFlow", "orderstatusflows"), "insert_orderstatusflow", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateOrderStatusFlow", "orderstatusflows"), "update_orderstatusflow", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.OrderStatusFlow Convert(DTO.OrderStatusFlow dto)
        {
            var entity = new Interfaces.Entities.OrderStatusFlow()
            {
                
        		        FromStatusID = dto.FromStatusID,

				        ToStatusID = dto.ToStatusID,

				
     
            };

            return entity;
        }
    }
}
