




using Microsoft.AspNetCore.Mvc;
using System;

namespace PPT.Utils.Convertors
{
    public class MaterialTypeConvertor
    {
        public static DTO.MaterialType Convert(Interfaces.Entities.MaterialType entity, IUrlHelper url)
        {
            var dto = new DTO.MaterialType()
            {
        		        ID = entity.ID,

				        MaterialTypeName = entity.MaterialTypeName,

				        Description = entity.Description,

				        ThumbnailUrl = entity.ThumbnailUrl,

				        IsDeleted = entity.IsDeleted,

				        CreatedDate = entity.CreatedDate,

				        CreatedByID = entity.CreatedByID,

				        ModifiedDate = entity.ModifiedDate,

				        ModifiedByID = entity.ModifiedByID,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetMaterialType", "materialtypes", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteMaterialType", "materialtypes", new { id = dto.ID  }), "delete_materialtype", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertMaterialType", "materialtypes"), "insert_materialtype", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateMaterialType", "materialtypes"), "update_materialtype", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.MaterialType Convert(DTO.MaterialType dto)
        {
            var entity = new Interfaces.Entities.MaterialType()
            {
                
        		        ID = dto.ID,

				        MaterialTypeName = dto.MaterialTypeName,

				        Description = dto.Description,

				        ThumbnailUrl = dto.ThumbnailUrl,

				        IsDeleted = dto.IsDeleted,

				        CreatedDate = dto.CreatedDate,

				        CreatedByID = dto.CreatedByID,

				        ModifiedDate = dto.ModifiedDate,

				        ModifiedByID = dto.ModifiedByID,

				
     
            };

            return entity;
        }
    }
}
