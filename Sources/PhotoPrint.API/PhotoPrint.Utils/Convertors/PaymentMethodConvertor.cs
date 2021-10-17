




using Microsoft.AspNetCore.Mvc;
using System;

namespace PPT.Utils.Convertors
{
    public class PaymentMethodConvertor
    {
        public static DTO.PaymentMethod Convert(Interfaces.Entities.PaymentMethod entity, IUrlHelper url)
        {
            var dto = new DTO.PaymentMethod()
            {
        		        ID = entity.ID,

				        Name = entity.Name,

				        Description = entity.Description,

				        IsDeleted = entity.IsDeleted,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetPaymentMethod", "paymentmethods", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeletePaymentMethod", "paymentmethods", new { id = dto.ID  }), "delete_paymentmethod", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertPaymentMethod", "paymentmethods"), "insert_paymentmethod", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdatePaymentMethod", "paymentmethods"), "update_paymentmethod", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.PaymentMethod Convert(DTO.PaymentMethod dto)
        {
            var entity = new Interfaces.Entities.PaymentMethod()
            {
                
        		        ID = dto.ID,

				        Name = dto.Name,

				        Description = dto.Description,

				        IsDeleted = dto.IsDeleted,

				
     
            };

            return entity;
        }
    }
}
