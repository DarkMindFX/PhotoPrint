using System;
using System.Collections.Generic;

#nullable disable

namespace PPT.DAL.EF.Models
{
    public partial class Category
    {
        public Category()
        {
            ImageCategories = new HashSet<ImageCategory>();
            InverseParent = new HashSet<Category>();
        }

        public long ID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public long? ParentId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedByID { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedByID { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual User ModifiedBy { get; set; }
        public virtual Category Parent { get; set; }
        public virtual ICollection<ImageCategory> ImageCategories { get; set; }
        public virtual ICollection<Category> InverseParent { get; set; }
    }
}
