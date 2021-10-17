




using Microsoft.AspNetCore.Mvc;
using System;

namespace PPT.Utils.Convertors
{
    public class UserTypeConvertor
    {
        public static DTO.UserType Convert(Interfaces.Entities.UserType entity, IUrlHelper url)
        {
            var dto = new DTO.UserType()
            {
        		        ID = entity.ID,

				        UserTypeName = entity.UserTypeName,

				        IsDeleted = entity.IsDeleted,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetUserType", "usertypes", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteUserType", "usertypes", new { id = dto.ID  }), "delete_usertype", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertUserType", "usertypes"), "insert_usertype", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateUserType", "usertypes"), "update_usertype", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.UserType Convert(DTO.UserType dto)
        {
            var entity = new Interfaces.Entities.UserType()
            {
                
        		        ID = dto.ID,

				        UserTypeName = dto.UserTypeName,

				        IsDeleted = dto.IsDeleted,

				
     
            };

            return entity;
        }
    }
}
