




using Microsoft.AspNetCore.Mvc;
using System;

namespace PPT.Utils.Convertors
{
    public class AddressConvertor
    {
        public static DTO.Address Convert(Interfaces.Entities.Address entity, IUrlHelper url)
        {
            var dto = new DTO.Address()
            {
        		        ID = entity.ID,

				        AddressTypeID = entity.AddressTypeID,

				        Title = entity.Title,

				        CityID = entity.CityID,

				        Street = entity.Street,

				        BuildingNo = entity.BuildingNo,

				        ApartmentNo = entity.ApartmentNo,

				        Comment = entity.Comment,

				        CreatedByID = entity.CreatedByID,

				        CreatedDate = entity.CreatedDate,

				        ModifiedByID = entity.ModifiedByID,

				        ModifiedDate = entity.ModifiedDate,

				        IsDeleted = entity.IsDeleted,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetAddress", "addresses", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteAddress", "addresses", new { id = dto.ID  }), "delete_address", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertAddress", "addresses"), "insert_address", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateAddress", "addresses"), "update_address", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.Address Convert(DTO.Address dto)
        {
            var entity = new Interfaces.Entities.Address()
            {
                
        		        ID = dto.ID,

				        AddressTypeID = dto.AddressTypeID,

				        Title = dto.Title,

				        CityID = dto.CityID,

				        Street = dto.Street,

				        BuildingNo = dto.BuildingNo,

				        ApartmentNo = dto.ApartmentNo,

				        Comment = dto.Comment,

				        CreatedByID = dto.CreatedByID,

				        CreatedDate = dto.CreatedDate,

				        ModifiedByID = dto.ModifiedByID,

				        ModifiedDate = dto.ModifiedDate,

				        IsDeleted = dto.IsDeleted,

				
     
            };

            return entity;
        }
    }
}
