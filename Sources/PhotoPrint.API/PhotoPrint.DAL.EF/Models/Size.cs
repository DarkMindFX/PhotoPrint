using System;
using System.Collections.Generic;

#nullable disable

namespace PPT.DAL.EF.Models
{
    public partial class Size
    {
        public Size()
        {
            OrderItemFrameSizes = new HashSet<OrderItem>();
            OrderItemSizes = new HashSet<OrderItem>();
        }

        public long ID { get; set; }
        public string SizeName { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedByID { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedByID { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual User ModifiedBy { get; set; }
        public virtual ICollection<OrderItem> OrderItemFrameSizes { get; set; }
        public virtual ICollection<OrderItem> OrderItemSizes { get; set; }
    }
}
