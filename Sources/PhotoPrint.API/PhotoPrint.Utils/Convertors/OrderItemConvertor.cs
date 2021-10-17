




using Microsoft.AspNetCore.Mvc;
using System;

namespace PPT.Utils.Convertors
{
    public class OrderItemConvertor
    {
        public static DTO.OrderItem Convert(Interfaces.Entities.OrderItem entity, IUrlHelper url)
        {
            var dto = new DTO.OrderItem()
            {
        		        ID = entity.ID,

				        OrderID = entity.OrderID,

				        ImageID = entity.ImageID,

				        Width = entity.Width,

				        Height = entity.Height,

				        SizeID = entity.SizeID,

				        FrameTypeID = entity.FrameTypeID,

				        FrameSizeID = entity.FrameSizeID,

				        MatID = entity.MatID,

				        MaterialTypeID = entity.MaterialTypeID,

				        MountingTypeID = entity.MountingTypeID,

				        ItemCount = entity.ItemCount,

				        PriceAmountPerItem = entity.PriceAmountPerItem,

				        PriceCurrencyID = entity.PriceCurrencyID,

				        Comments = entity.Comments,

				        PrintingHouseID = entity.PrintingHouseID,

				        IsDeleted = entity.IsDeleted,

				        CreatedDate = entity.CreatedDate,

				        CreatedByID = entity.CreatedByID,

				        ModifiedDate = entity.ModifiedDate,

				        ModifiedByID = entity.ModifiedByID,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetOrderItem", "orderitems", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteOrderItem", "orderitems", new { id = dto.ID  }), "delete_orderitem", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertOrderItem", "orderitems"), "insert_orderitem", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateOrderItem", "orderitems"), "update_orderitem", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.OrderItem Convert(DTO.OrderItem dto)
        {
            var entity = new Interfaces.Entities.OrderItem()
            {
                
        		        ID = dto.ID,

				        OrderID = dto.OrderID,

				        ImageID = dto.ImageID,

				        Width = dto.Width,

				        Height = dto.Height,

				        SizeID = dto.SizeID,

				        FrameTypeID = dto.FrameTypeID,

				        FrameSizeID = dto.FrameSizeID,

				        MatID = dto.MatID,

				        MaterialTypeID = dto.MaterialTypeID,

				        MountingTypeID = dto.MountingTypeID,

				        ItemCount = dto.ItemCount,

				        PriceAmountPerItem = dto.PriceAmountPerItem,

				        PriceCurrencyID = dto.PriceCurrencyID,

				        Comments = dto.Comments,

				        PrintingHouseID = dto.PrintingHouseID,

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
