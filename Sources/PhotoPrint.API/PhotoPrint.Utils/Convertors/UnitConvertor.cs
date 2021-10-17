




using Microsoft.AspNetCore.Mvc;
using System;

namespace PPT.Utils.Convertors
{
    public class UnitConvertor
    {
        public static DTO.Unit Convert(Interfaces.Entities.Unit entity, IUrlHelper url)
        {
            var dto = new DTO.Unit()
            {
        		        ID = entity.ID,

				        UnitName = entity.UnitName,

				        UnitAbbr = entity.UnitAbbr,

				        Description = entity.Description,

				        IsDeleted = entity.IsDeleted,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetUnit", "units", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteUnit", "units", new { id = dto.ID  }), "delete_unit", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertUnit", "units"), "insert_unit", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateUnit", "units"), "update_unit", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.Unit Convert(DTO.Unit dto)
        {
            var entity = new Interfaces.Entities.Unit()
            {
                
        		        ID = dto.ID,

				        UnitName = dto.UnitName,

				        UnitAbbr = dto.UnitAbbr,

				        Description = dto.Description,

				        IsDeleted = dto.IsDeleted,

				
     
            };

            return entity;
        }
    }
}
