




using Microsoft.AspNetCore.Mvc;
using System;

namespace PPT.Utils.Convertors
{
    public class ContactConvertor
    {
        public static DTO.Contact Convert(Interfaces.Entities.Contact entity, IUrlHelper url)
        {
            var dto = new DTO.Contact()
            {
        		        ID = entity.ID,

				        ContactTypeID = entity.ContactTypeID,

				        Title = entity.Title,

				        Comment = entity.Comment,

				        Value = entity.Value,

				        IsDeleted = entity.IsDeleted,

				        CreatedByID = entity.CreatedByID,

				        CreatedDate = entity.CreatedDate,

				        ModifiedByID = entity.ModifiedByID,

				        ModifiedDate = entity.ModifiedDate,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetContact", "contacts", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteContact", "contacts", new { id = dto.ID  }), "delete_contact", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertContact", "contacts"), "insert_contact", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateContact", "contacts"), "update_contact", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.Contact Convert(DTO.Contact dto)
        {
            var entity = new Interfaces.Entities.Contact()
            {
                
        		        ID = dto.ID,

				        ContactTypeID = dto.ContactTypeID,

				        Title = dto.Title,

				        Comment = dto.Comment,

				        Value = dto.Value,

				        IsDeleted = dto.IsDeleted,

				        CreatedByID = dto.CreatedByID,

				        CreatedDate = dto.CreatedDate,

				        ModifiedByID = dto.ModifiedByID,

				        ModifiedDate = dto.ModifiedDate,

				
     
            };

            return entity;
        }
    }
}
