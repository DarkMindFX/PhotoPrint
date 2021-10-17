




using Microsoft.AspNetCore.Mvc;
using System;

namespace PPT.Utils.Convertors
{
    public class UserContactConvertor
    {
        public static DTO.UserContact Convert(Interfaces.Entities.UserContact entity, IUrlHelper url)
        {
            var dto = new DTO.UserContact()
            {
        		        UserID = entity.UserID,

				        ContactID = entity.ContactID,

				        IsPrimary = entity.IsPrimary,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetUserContact", "usercontacts", new { userid = dto.UserID, contactid = dto.ContactID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteUserContact", "usercontacts", new { userid = dto.UserID, contactid = dto.ContactID  }), "delete_usercontact", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertUserContact", "usercontacts"), "insert_usercontact", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateUserContact", "usercontacts"), "update_usercontact", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.UserContact Convert(DTO.UserContact dto)
        {
            var entity = new Interfaces.Entities.UserContact()
            {
                
        		        UserID = dto.UserID,

				        ContactID = dto.ContactID,

				        IsPrimary = dto.IsPrimary,

				
     
            };

            return entity;
        }
    }
}
