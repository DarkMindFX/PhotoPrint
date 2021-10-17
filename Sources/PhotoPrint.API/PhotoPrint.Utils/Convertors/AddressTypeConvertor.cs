




using Microsoft.AspNetCore.Mvc;
using System;

namespace PPT.Utils.Convertors
{
    public class AddressTypeConvertor
    {
        public static DTO.AddressType Convert(Interfaces.Entities.AddressType entity, IUrlHelper url)
        {
            var dto = new DTO.AddressType()
            {
        		        ID = entity.ID,

				        AddressTypeName = entity.AddressTypeName,

				        IsDeleted = entity.IsDeleted,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetAddressType", "addresstypes", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteAddressType", "addresstypes", new { id = dto.ID  }), "delete_addresstype", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertAddressType", "addresstypes"), "insert_addresstype", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateAddressType", "addresstypes"), "update_addresstype", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.AddressType Convert(DTO.AddressType dto)
        {
            var entity = new Interfaces.Entities.AddressType()
            {
                
        		        ID = dto.ID,

				        AddressTypeName = dto.AddressTypeName,

				        IsDeleted = dto.IsDeleted,

				
     
            };

            return entity;
        }
    }
}
