




using Microsoft.AspNetCore.Mvc;
using System;

namespace PPT.Utils.Convertors
{
    public class FrameTypeConvertor
    {
        public static DTO.FrameType Convert(Interfaces.Entities.FrameType entity, IUrlHelper url)
        {
            var dto = new DTO.FrameType()
            {
        		        ID = entity.ID,

				        FrameTypeName = entity.FrameTypeName,

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
                dto.Links.Add(new DTO.Link(url.Action("GetFrameType", "frametypes", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteFrameType", "frametypes", new { id = dto.ID  }), "delete_frametype", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertFrameType", "frametypes"), "insert_frametype", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateFrameType", "frametypes"), "update_frametype", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.FrameType Convert(DTO.FrameType dto)
        {
            var entity = new Interfaces.Entities.FrameType()
            {
                
        		        ID = dto.ID,

				        FrameTypeName = dto.FrameTypeName,

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
