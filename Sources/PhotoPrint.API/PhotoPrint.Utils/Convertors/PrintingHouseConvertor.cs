




using Microsoft.AspNetCore.Mvc;
using System;

namespace PhotoPrint.Utils.Convertors
{
    public class PrintingHouseConvertor
    {
        public static DTO.PrintingHouse Convert(Interfaces.Entities.PrintingHouse entity, IUrlHelper url)
        {
            var dto = new DTO.PrintingHouse()
            {
        		        ID = entity.ID,

				        Name = entity.Name,

				        Description = entity.Description,

				        IsDeleted = entity.IsDeleted,

				        CreatedDate = entity.CreatedDate,

				        CreatedByID = entity.CreatedByID,

				        ModifiedDate = entity.ModifiedDate,

				        ModifiedByID = entity.ModifiedByID,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetPrintingHouse", "printinghouses", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeletePrintingHouse", "printinghouses", new { id = dto.ID  }), "delete_printinghouse", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertPrintingHouse", "printinghouses"), "insert_printinghouse", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdatePrintingHouse", "printinghouses"), "update_printinghouse", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.PrintingHouse Convert(DTO.PrintingHouse dto)
        {
            var entity = new Interfaces.Entities.PrintingHouse()
            {
                
        		        ID = dto.ID,

				        Name = dto.Name,

				        Description = dto.Description,

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
