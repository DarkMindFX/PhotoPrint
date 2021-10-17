




using Microsoft.AspNetCore.Mvc;
using System;

namespace PhotoPrint.Utils.Convertors
{
    public class SizeConvertor
    {
        public static DTO.Size Convert(Interfaces.Entities.Size entity, IUrlHelper url)
        {
            var dto = new DTO.Size()
            {
        		        ID = entity.ID,

				        SizeName = entity.SizeName,

				        Width = entity.Width,

				        Height = entity.Height,

				        IsDeleted = entity.IsDeleted,

				        CreatedDate = entity.CreatedDate,

				        CreatedByID = entity.CreatedByID,

				        ModifiedDate = entity.ModifiedDate,

				        ModifiedByID = entity.ModifiedByID,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetSize", "sizes", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteSize", "sizes", new { id = dto.ID  }), "delete_size", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertSize", "sizes"), "insert_size", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateSize", "sizes"), "update_size", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.Size Convert(DTO.Size dto)
        {
            var entity = new Interfaces.Entities.Size()
            {
                
        		        ID = dto.ID,

				        SizeName = dto.SizeName,

				        Width = dto.Width,

				        Height = dto.Height,

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
