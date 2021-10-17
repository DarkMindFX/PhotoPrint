




using Microsoft.AspNetCore.Mvc;
using System;

namespace PhotoPrint.Utils.Convertors
{
    public class DeliveryServiceConvertor
    {
        public static DTO.DeliveryService Convert(Interfaces.Entities.DeliveryService entity, IUrlHelper url)
        {
            var dto = new DTO.DeliveryService()
            {
        		        ID = entity.ID,

				        DeliveryServiceName = entity.DeliveryServiceName,

				        Description = entity.Description,

				        IsDeleted = entity.IsDeleted,

				        CreatedDate = entity.CreatedDate,

				        CreatedByID = entity.CreatedByID,

				        ModifiedDate = entity.ModifiedDate,

				        ModifiedByID = entity.ModifiedByID,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetDeliveryService", "deliveryservices", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteDeliveryService", "deliveryservices", new { id = dto.ID  }), "delete_deliveryservice", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertDeliveryService", "deliveryservices"), "insert_deliveryservice", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateDeliveryService", "deliveryservices"), "update_deliveryservice", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.DeliveryService Convert(DTO.DeliveryService dto)
        {
            var entity = new Interfaces.Entities.DeliveryService()
            {
                
        		        ID = dto.ID,

				        DeliveryServiceName = dto.DeliveryServiceName,

				        Description = dto.Description,

				        IsDeleted = dto.IsDeleted,

				        CreatedDate = dto.CreatedDate,

				        CreatedByID = dto.CreatedByID,

				        ModifiedDate = dto.ModifiedDate,

				        ModifiedByID = dto.ModifiedByID,

				
     
            };

            return entity;
        }
    }
}
