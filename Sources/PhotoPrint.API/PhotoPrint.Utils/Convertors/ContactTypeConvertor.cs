




using Microsoft.AspNetCore.Mvc;
using System;

namespace PhotoPrint.Utils.Convertors
{
    public class ContactTypeConvertor
    {
        public static DTO.ContactType Convert(Interfaces.Entities.ContactType entity, IUrlHelper url)
        {
            var dto = new DTO.ContactType()
            {
        		        ID = entity.ID,

				        ContactTypeName = entity.ContactTypeName,

				        IsDeleted = entity.IsDeleted,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetContactType", "contacttypes", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteContactType", "contacttypes", new { id = dto.ID  }), "delete_contacttype", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertContactType", "contacttypes"), "insert_contacttype", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateContactType", "contacttypes"), "update_contacttype", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.ContactType Convert(DTO.ContactType dto)
        {
            var entity = new Interfaces.Entities.ContactType()
            {
                
        		        ID = dto.ID,

				        ContactTypeName = dto.ContactTypeName,

				        IsDeleted = dto.IsDeleted,

				
     
            };

            return entity;
        }
    }
}
