




using Microsoft.AspNetCore.Mvc;
using System;

namespace PPT.Utils.Convertors
{
    public class UserConvertor
    {
        public static DTO.User Convert(Interfaces.Entities.User entity, IUrlHelper url)
        {
            var dto = new DTO.User()
            {
        		        ID = entity.ID,

				        Login = entity.Login,

				        PwdHash = entity.PwdHash,

				        Salt = entity.Salt,

				        FirstName = entity.FirstName,

				        MiddleName = entity.MiddleName,

				        LastName = entity.LastName,

				        FriendlyName = entity.FriendlyName,

				        UserStatusID = entity.UserStatusID,

				        UserTypeID = entity.UserTypeID,

				        CreatedDate = entity.CreatedDate,

				        ModifiedDate = entity.ModifiedDate,

				        ModifiedByID = entity.ModifiedByID,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetUser", "users", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteUser", "users", new { id = dto.ID  }), "delete_user", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertUser", "users"), "insert_user", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateUser", "users"), "update_user", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.User Convert(DTO.User dto)
        {
            var entity = new Interfaces.Entities.User()
            {
                
        		        ID = dto.ID,

				        Login = dto.Login,

				        PwdHash = dto.PwdHash,

				        Salt = dto.Salt,

				        FirstName = dto.FirstName,

				        MiddleName = dto.MiddleName,

				        LastName = dto.LastName,

				        FriendlyName = dto.FriendlyName,

				        UserStatusID = dto.UserStatusID,

				        UserTypeID = dto.UserTypeID,

				        CreatedDate = dto.CreatedDate,

				        ModifiedDate = dto.ModifiedDate,

				        ModifiedByID = dto.ModifiedByID,

				
     
            };

            return entity;
        }
    }
}
