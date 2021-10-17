




using Microsoft.AspNetCore.Mvc;
using System;

namespace PPT.Utils.Convertors
{
    public class UserAddressConvertor
    {
        public static DTO.UserAddress Convert(Interfaces.Entities.UserAddress entity, IUrlHelper url)
        {
            var dto = new DTO.UserAddress()
            {
        		        UserID = entity.UserID,

				        AddressID = entity.AddressID,

				        IsPrimary = entity.IsPrimary,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetUserAddress", "useraddresses", new { userid = dto.UserID, addressid = dto.AddressID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteUserAddress", "useraddresses", new { userid = dto.UserID, addressid = dto.AddressID  }), "delete_useraddress", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertUserAddress", "useraddresses"), "insert_useraddress", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateUserAddress", "useraddresses"), "update_useraddress", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.UserAddress Convert(DTO.UserAddress dto)
        {
            var entity = new Interfaces.Entities.UserAddress()
            {
                
        		        UserID = dto.UserID,

				        AddressID = dto.AddressID,

				        IsPrimary = dto.IsPrimary,

				
     
            };

            return entity;
        }
    }
}
