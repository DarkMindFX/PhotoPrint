




using Microsoft.AspNetCore.Mvc;
using System;

namespace PPT.Utils.Convertors
{
    public class CityConvertor
    {
        public static DTO.City Convert(Interfaces.Entities.City entity, IUrlHelper url)
        {
            var dto = new DTO.City()
            {
        		        ID = entity.ID,

				        CityName = entity.CityName,

				        RegionID = entity.RegionID,

				        IsDeleted = entity.IsDeleted,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetCity", "cities", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteCity", "cities", new { id = dto.ID  }), "delete_city", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertCity", "cities"), "insert_city", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateCity", "cities"), "update_city", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.City Convert(DTO.City dto)
        {
            var entity = new Interfaces.Entities.City()
            {
                
        		        ID = dto.ID,

				        CityName = dto.CityName,

				        RegionID = dto.RegionID,

				        IsDeleted = dto.IsDeleted,

				
     
            };

            return entity;
        }
    }
}
