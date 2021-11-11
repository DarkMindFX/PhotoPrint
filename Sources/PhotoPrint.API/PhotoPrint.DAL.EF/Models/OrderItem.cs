using System;
using System.Collections.Generic;

#nullable disable

namespace PPT.DAL.EF.Models
{
    public partial class OrderItem
    {
        public long ID { get; set; }
        public long OrderID { get; set; }
        public long ImageID { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public long? SizeID { get; set; }
        public long FrameTypeID { get; set; }
        public long? FrameSizeID { get; set; }
        public long MatID { get; set; }
        public long MaterialTypeID { get; set; }
        public long MountingTypeID { get; set; }
        public int ItemCount { get; set; }
        public decimal PriceAmountPerItem { get; set; }
        public long PriceCurrencyID { get; set; }
        public string Comments { get; set; }
        public long? PrintingHouseID { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedByID { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedByID { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual Size FrameSize { get; set; }
        public virtual FrameType FrameType { get; set; }
        public virtual Image Image { get; set; }
        public virtual Mat Mat { get; set; }
        public virtual MaterialType MaterialType { get; set; }
        public virtual User ModifiedBy { get; set; }
        public virtual MountingType MountingType { get; set; }
        public virtual Order Order { get; set; }
        public virtual Currency PriceCurrency { get; set; }
        public virtual PrintingHouse PrintingHouse { get; set; }
        public virtual Size Size { get; set; }
    }
}
