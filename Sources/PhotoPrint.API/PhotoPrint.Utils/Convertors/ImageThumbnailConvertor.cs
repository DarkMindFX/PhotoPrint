




using Microsoft.AspNetCore.Mvc;
using System;

namespace PhotoPrint.Utils.Convertors
{
    public class ImageThumbnailConvertor
    {
        public static DTO.ImageThumbnail Convert(Interfaces.Entities.ImageThumbnail entity, IUrlHelper url)
        {
            var dto = new DTO.ImageThumbnail()
            {
        		        ID = entity.ID,

				        Url = entity.Url,

				        Order = entity.Order,

				        ImageID = entity.ImageID,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetImageThumbnail", "imagethumbnails", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteImageThumbnail", "imagethumbnails", new { id = dto.ID  }), "delete_imagethumbnail", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertImageThumbnail", "imagethumbnails"), "insert_imagethumbnail", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateImageThumbnail", "imagethumbnails"), "update_imagethumbnail", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.ImageThumbnail Convert(DTO.ImageThumbnail dto)
        {
            var entity = new Interfaces.Entities.ImageThumbnail()
            {
                
        		        ID = dto.ID,

				        Url = dto.Url,

				        Order = dto.Order,

				        ImageID = dto.ImageID,

				
     
            };

            return entity;
        }
    }
}
