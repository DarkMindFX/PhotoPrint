




using Microsoft.AspNetCore.Mvc;
using System;

namespace PPT.Utils.Convertors
{
    public class UserConfirmationConvertor
    {
        public static DTO.UserConfirmation Convert(Interfaces.Entities.UserConfirmation entity, IUrlHelper url)
        {
            var dto = new DTO.UserConfirmation()
            {
        		        ID = entity.ID,

				        UserID = entity.UserID,

				        ConfirmationCode = entity.ConfirmationCode,

				        Comfirmed = entity.Comfirmed,

				        ExpiresDate = entity.ExpiresDate,

				        ConfirmationDate = entity.ConfirmationDate,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetUserConfirmation", "userconfirmations", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteUserConfirmation", "userconfirmations", new { id = dto.ID  }), "delete_userconfirmation", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertUserConfirmation", "userconfirmations"), "insert_userconfirmation", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateUserConfirmation", "userconfirmations"), "update_userconfirmation", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.UserConfirmation Convert(DTO.UserConfirmation dto)
        {
            var entity = new Interfaces.Entities.UserConfirmation()
            {
                
        		        ID = dto.ID,

				        UserID = dto.UserID,

				        ConfirmationCode = dto.ConfirmationCode,

				        Comfirmed = dto.Comfirmed,

				        ExpiresDate = dto.ExpiresDate,

				        ConfirmationDate = dto.ConfirmationDate,

				
     
            };

            return entity;
        }
    }
}
