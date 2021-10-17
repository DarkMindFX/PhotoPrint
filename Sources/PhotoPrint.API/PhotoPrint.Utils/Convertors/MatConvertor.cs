




using Microsoft.AspNetCore.Mvc;
using System;

namespace PPT.Utils.Convertors
{
    public class MatConvertor
    {
        public static DTO.Mat Convert(Interfaces.Entities.Mat entity, IUrlHelper url)
        {
            var dto = new DTO.Mat()
            {
        		        ID = entity.ID,

				        MatName = entity.MatName,

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
                dto.Links.Add(new DTO.Link(url.Action("GetMat", "mats", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteMat", "mats", new { id = dto.ID  }), "delete_mat", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertMat", "mats"), "insert_mat", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateMat", "mats"), "update_mat", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.Mat Convert(DTO.Mat dto)
        {
            var entity = new Interfaces.Entities.Mat()
            {
                
        		        ID = dto.ID,

				        MatName = dto.MatName,

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
