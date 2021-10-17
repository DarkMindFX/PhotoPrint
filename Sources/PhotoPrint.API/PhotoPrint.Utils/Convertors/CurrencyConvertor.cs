




using Microsoft.AspNetCore.Mvc;
using System;

namespace PPT.Utils.Convertors
{
    public class CurrencyConvertor
    {
        public static DTO.Currency Convert(Interfaces.Entities.Currency entity, IUrlHelper url)
        {
            var dto = new DTO.Currency()
            {
        		        ID = entity.ID,

				        ISO = entity.ISO,

				        CurrencyName = entity.CurrencyName,

				        IsDeleted = entity.IsDeleted,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetCurrency", "currencies", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteCurrency", "currencies", new { id = dto.ID  }), "delete_currency", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertCurrency", "currencies"), "insert_currency", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateCurrency", "currencies"), "update_currency", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.Currency Convert(DTO.Currency dto)
        {
            var entity = new Interfaces.Entities.Currency()
            {
                
        		        ID = dto.ID,

				        ISO = dto.ISO,

				        CurrencyName = dto.CurrencyName,

				        IsDeleted = dto.IsDeleted,

				
     
            };

            return entity;
        }
    }
}
