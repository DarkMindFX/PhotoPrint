using System;
using System.Collections.Generic;

#nullable disable

namespace PPT.DAL.EF.Models
{
    public partial class MountingType
    {
        public MountingType()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public long ID { get; set; }
        public string MountingTypeName { get; set; }
        public string Description { get; set; }
        public string ThumbnailUrl { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedByID { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedByID { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
