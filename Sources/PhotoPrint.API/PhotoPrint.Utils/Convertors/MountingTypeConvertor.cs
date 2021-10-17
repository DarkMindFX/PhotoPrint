




using Microsoft.AspNetCore.Mvc;
using System;

namespace PPT.Utils.Convertors
{
    public class MountingTypeConvertor
    {
        public static DTO.MountingType Convert(Interfaces.Entities.MountingType entity, IUrlHelper url)
        {
            var dto = new DTO.MountingType()
            {
        		        ID = entity.ID,

				        MountingTypeName = entity.MountingTypeName,

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
                dto.Links.Add(new DTO.Link(url.Action("GetMountingType", "mountingtypes", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteMountingType", "mountingtypes", new { id = dto.ID  }), "delete_mountingtype", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertMountingType", "mountingtypes"), "insert_mountingtype", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateMountingType", "mountingtypes"), "update_mountingtype", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.MountingType Convert(DTO.MountingType dto)
        {
            var entity = new Interfaces.Entities.MountingType()
            {
                
        		        ID = dto.ID,

				        MountingTypeName = dto.MountingTypeName,

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
