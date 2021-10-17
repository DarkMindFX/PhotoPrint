




using Microsoft.AspNetCore.Mvc;
using System;

namespace PPT.Utils.Convertors
{
    public class ImageCategoryConvertor
    {
        public static DTO.ImageCategory Convert(Interfaces.Entities.ImageCategory entity, IUrlHelper url)
        {
            var dto = new DTO.ImageCategory()
            {
        		        ImageID = entity.ImageID,

				        CategoryID = entity.CategoryID,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetImageCategory", "imagecategories", new { imageid = dto.ImageID, categoryid = dto.CategoryID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteImageCategory", "imagecategories", new { imageid = dto.ImageID, categoryid = dto.CategoryID  }), "delete_imagecategory", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertImageCategory", "imagecategories"), "insert_imagecategory", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateImageCategory", "imagecategories"), "update_imagecategory", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.ImageCategory Convert(DTO.ImageCategory dto)
        {
            var entity = new Interfaces.Entities.ImageCategory()
            {
                
        		        ImageID = dto.ImageID,

				        CategoryID = dto.CategoryID,

				
     
            };

            return entity;
        }
    }
}
