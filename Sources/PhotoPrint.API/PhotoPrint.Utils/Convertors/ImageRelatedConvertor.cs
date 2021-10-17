




using Microsoft.AspNetCore.Mvc;
using System;

namespace PPT.Utils.Convertors
{
    public class ImageRelatedConvertor
    {
        public static DTO.ImageRelated Convert(Interfaces.Entities.ImageRelated entity, IUrlHelper url)
        {
            var dto = new DTO.ImageRelated()
            {
        		        ImageID = entity.ImageID,

				        RelatedImageID = entity.RelatedImageID,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetImageRelated", "imagerelateds", new { imageid = dto.ImageID, relatedimageid = dto.RelatedImageID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteImageRelated", "imagerelateds", new { imageid = dto.ImageID, relatedimageid = dto.RelatedImageID  }), "delete_imagerelated", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertImageRelated", "imagerelateds"), "insert_imagerelated", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateImageRelated", "imagerelateds"), "update_imagerelated", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.ImageRelated Convert(DTO.ImageRelated dto)
        {
            var entity = new Interfaces.Entities.ImageRelated()
            {
                
        		        ImageID = dto.ImageID,

				        RelatedImageID = dto.RelatedImageID,

				
     
            };

            return entity;
        }
    }
}
