




using Microsoft.AspNetCore.Mvc;
using System;

namespace PhotoPrint.Utils.Convertors
{
    public class OrderPaymentDetailsConvertor
    {
        public static DTO.OrderPaymentDetails Convert(Interfaces.Entities.OrderPaymentDetails entity, IUrlHelper url)
        {
            var dto = new DTO.OrderPaymentDetails()
            {
        		        ID = entity.ID,

				        OrderID = entity.OrderID,

				        PaymentMethodID = entity.PaymentMethodID,

				        PaymentTransUID = entity.PaymentTransUID,

				        PaymentDateTime = entity.PaymentDateTime,

				        IsDeleted = entity.IsDeleted,

				        CreatedDate = entity.CreatedDate,

				        CreatedByID = entity.CreatedByID,

				        ModifiedDate = entity.ModifiedDate,

				        ModifiedByID = entity.ModifiedByID,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetOrderPaymentDetails", "orderpaymentdetailses", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteOrderPaymentDetails", "orderpaymentdetailses", new { id = dto.ID  }), "delete_orderpaymentdetails", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertOrderPaymentDetails", "orderpaymentdetailses"), "insert_orderpaymentdetails", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateOrderPaymentDetails", "orderpaymentdetailses"), "update_orderpaymentdetails", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.OrderPaymentDetails Convert(DTO.OrderPaymentDetails dto)
        {
            var entity = new Interfaces.Entities.OrderPaymentDetails()
            {
                
        		        ID = dto.ID,

				        OrderID = dto.OrderID,

				        PaymentMethodID = dto.PaymentMethodID,

				        PaymentTransUID = dto.PaymentTransUID,

				        PaymentDateTime = dto.PaymentDateTime,

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
