




using Microsoft.AspNetCore.Mvc;
using System;

namespace PPT.Utils.Convertors
{
    public class OrderConvertor
    {
        public static DTO.Order Convert(Interfaces.Entities.Order entity, IUrlHelper url)
        {
            var dto = new DTO.Order()
            {
        		        ID = entity.ID,

				        ManagerID = entity.ManagerID,

				        UserID = entity.UserID,

				        ContactID = entity.ContactID,

				        DeliveryAddressID = entity.DeliveryAddressID,

				        DeliveryServiceID = entity.DeliveryServiceID,

				        StatusID = entity.StatusID,

				        Comments = entity.Comments,

				        IsDeleted = entity.IsDeleted,

				        CreatedDate = entity.CreatedDate,

				        CreatedByID = entity.CreatedByID,

				        ModifiedDate = entity.ModifiedDate,

				        ModifiedByID = entity.ModifiedByID,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetOrder", "orders", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteOrder", "orders", new { id = dto.ID  }), "delete_order", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertOrder", "orders"), "insert_order", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateOrder", "orders"), "update_order", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.Order Convert(DTO.Order dto)
        {
            var entity = new Interfaces.Entities.Order()
            {
                
        		        ID = dto.ID,

				        ManagerID = dto.ManagerID,

				        UserID = dto.UserID,

				        ContactID = dto.ContactID,

				        DeliveryAddressID = dto.DeliveryAddressID,

				        DeliveryServiceID = dto.DeliveryServiceID,

				        StatusID = dto.StatusID,

				        Comments = dto.Comments,

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
