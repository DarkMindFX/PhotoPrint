




using Microsoft.AspNetCore.Mvc;
using System;

namespace PPT.Utils.Convertors
{
    public class CountryConvertor
    {
        public static DTO.Country Convert(Interfaces.Entities.Country entity, IUrlHelper url)
        {
            var dto = new DTO.Country()
            {
        		        ID = entity.ID,

				        CountryName = entity.CountryName,

				        ISO = entity.ISO,

				        IsDeleted = entity.IsDeleted,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetCountry", "countries", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteCountry", "countries", new { id = dto.ID  }), "delete_country", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertCountry", "countries"), "insert_country", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateCountry", "countries"), "update_country", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.Country Convert(DTO.Country dto)
        {
            var entity = new Interfaces.Entities.Country()
            {
                
        		        ID = dto.ID,

				        CountryName = dto.CountryName,

				        ISO = dto.ISO,

				        IsDeleted = dto.IsDeleted,

				
     
            };

            return entity;
        }
    }
}
