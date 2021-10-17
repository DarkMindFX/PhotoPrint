




using Microsoft.AspNetCore.Mvc;
using System;

namespace PhotoPrint.Utils.Convertors
{
    public class CategoryConvertor
    {
        public static DTO.Category Convert(Interfaces.Entities.Category entity, IUrlHelper url)
        {
            var dto = new DTO.Category()
            {
                ID = entity.ID,

                CategoryName = entity.CategoryName,

                Description = entity.Description,

                ParentID = entity.ParentID,

                IsDeleted = entity.IsDeleted,

                CreatedDate = entity.CreatedDate,

                CreatedByID = entity.CreatedByID,

                ModifiedDate = entity.ModifiedDate,

                ModifiedByID = entity.ModifiedByID,


            };

            if (url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetCategory", "categories", new { id = dto.ID }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteCategory", "categories", new { id = dto.ID }), "delete_category", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertCategory", "categories"), "insert_category", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateCategory", "categories"), "update_category", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.Category Convert(DTO.Category dto)
        {
            var entity = new Interfaces.Entities.Category()
            {

                ID = dto.ID,

                CategoryName = dto.CategoryName,

                Description = dto.Description,

                ParentID = dto.ParentID,

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
