




using Microsoft.AspNetCore.Mvc;
using System;

namespace PhotoPrint.Utils.Convertors
{
    public class UserStatusConvertor
    {
        public static DTO.UserStatus Convert(Interfaces.Entities.UserStatus entity, IUrlHelper url)
        {
            var dto = new DTO.UserStatus()
            {
        		        ID = entity.ID,

				        StatusName = entity.StatusName,

				        IsDeleted = entity.IsDeleted,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetUserStatus", "userstatuses", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteUserStatus", "userstatuses", new { id = dto.ID  }), "delete_userstatus", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertUserStatus", "userstatuses"), "insert_userstatus", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateUserStatus", "userstatuses"), "update_userstatus", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.UserStatus Convert(DTO.UserStatus dto)
        {
            var entity = new Interfaces.Entities.UserStatus()
            {
                
        		        ID = dto.ID,

				        StatusName = dto.StatusName,

				        IsDeleted = dto.IsDeleted,

				
     
            };

            return entity;
        }
    }
}
