




using Microsoft.AspNetCore.Mvc;
using System;

namespace PPT.Utils.Convertors
{
    public class RegionConvertor
    {
        public static DTO.Region Convert(Interfaces.Entities.Region entity, IUrlHelper url)
        {
            var dto = new DTO.Region()
            {
        		        ID = entity.ID,

				        RegionName = entity.RegionName,

				        CountryID = entity.CountryID,

				        IsDeleted = entity.IsDeleted,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetRegion", "regions", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteRegion", "regions", new { id = dto.ID  }), "delete_region", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertRegion", "regions"), "insert_region", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateRegion", "regions"), "update_region", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.Region Convert(DTO.Region dto)
        {
            var entity = new Interfaces.Entities.Region()
            {
                
        		        ID = dto.ID,

				        RegionName = dto.RegionName,

				        CountryID = dto.CountryID,

				        IsDeleted = dto.IsDeleted,

				
     
            };

            return entity;
        }
    }
}
