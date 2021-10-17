




using Microsoft.AspNetCore.Mvc;
using System;

namespace PPT.Utils.Convertors
{
    public class ImageConvertor
    {
        public static DTO.Image Convert(Interfaces.Entities.Image entity, IUrlHelper url)
        {
            var dto = new DTO.Image()
            {
        		        ID = entity.ID,

				        Title = entity.Title,

				        Description = entity.Description,

				        OriginUrl = entity.OriginUrl,

				        MaxWidth = entity.MaxWidth,

				        MaxHeight = entity.MaxHeight,

				        PriceAmount = entity.PriceAmount,

				        PriceCurrencyID = entity.PriceCurrencyID,

				        IsDeleted = entity.IsDeleted,

				        CreatedByID = entity.CreatedByID,

				        CreatedDate = entity.CreatedDate,

				        ModifiedByID = entity.ModifiedByID,

				        ModifiedDate = entity.ModifiedDate,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetImage", "images", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteImage", "images", new { id = dto.ID  }), "delete_image", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertImage", "images"), "insert_image", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateImage", "images"), "update_image", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.Image Convert(DTO.Image dto)
        {
            var entity = new Interfaces.Entities.Image()
            {
                
        		        ID = dto.ID,

				        Title = dto.Title,

				        Description = dto.Description,

				        OriginUrl = dto.OriginUrl,

				        MaxWidth = dto.MaxWidth,

				        MaxHeight = dto.MaxHeight,

				        PriceAmount = dto.PriceAmount,

				        PriceCurrencyID = dto.PriceCurrencyID,

				        IsDeleted = dto.IsDeleted,

				        CreatedByID = dto.CreatedByID,

				        CreatedDate = dto.CreatedDate,

				        ModifiedByID = dto.ModifiedByID,

				        ModifiedDate = dto.ModifiedDate,

				
     
            };

            return entity;
        }
    }
}
