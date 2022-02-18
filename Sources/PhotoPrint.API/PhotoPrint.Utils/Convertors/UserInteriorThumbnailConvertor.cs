





using Microsoft.AspNetCore.Mvc;
using System;

namespace PPT.Utils.Convertors
{
    public class UserInteriorThumbnailConvertor
    {
        public static DTO.UserInteriorThumbnail Convert(Interfaces.Entities.UserInteriorThumbnail entity, IUrlHelper url)
        {
            var dto = new DTO.UserInteriorThumbnail()
            {
                ID = entity.ID,

                UserID = entity.UserID,

                Url = entity.Url,


            };

            if (url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetUserInteriorThumbnail", "userinteriorthumbnails", new { id = dto.ID }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteUserInteriorThumbnail", "userinteriorthumbnails", new { id = dto.ID }), "delete_userinteriorthumbnail", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertUserInteriorThumbnail", "userinteriorthumbnails"), "insert_userinteriorthumbnail", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateUserInteriorThumbnail", "userinteriorthumbnails"), "update_userinteriorthumbnail", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.UserInteriorThumbnail Convert(DTO.UserInteriorThumbnail dto)
        {
            var entity = new Interfaces.Entities.UserInteriorThumbnail()
            {
                ID = dto.ID,

                UserID = dto.UserID,

                Url = dto.Url,
            };

            return entity;
        }
    }
}
