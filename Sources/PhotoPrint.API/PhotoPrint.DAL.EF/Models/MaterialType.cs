using System;
using System.Collections.Generic;

#nullable disable

namespace PPT.DAL.EF.Models
{
    public partial class MaterialType
    {
        public MaterialType()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public long ID { get; set; }
        public string MaterialTypeName { get; set; }
        public string Description { get; set; }
        public string ThumbnailUrl { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedByID { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedByID { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual User ModifiedBy { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
