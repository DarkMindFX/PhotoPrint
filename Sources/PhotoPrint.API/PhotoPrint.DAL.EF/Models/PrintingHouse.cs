using System;
using System.Collections.Generic;

#nullable disable

namespace PPT.DAL.EF.Models
{
    public partial class PrintingHouse
    {
        public PrintingHouse()
        {
            OrderItems = new HashSet<OrderItem>();
            PrintingHouseAddresses = new HashSet<PrintingHouseAddress>();
            PrintingHouseContacts = new HashSet<PrintingHouseContact>();
        }

        public long ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedByID { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedByID { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual User ModifiedBy { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<PrintingHouseAddress> PrintingHouseAddresses { get; set; }
        public virtual ICollection<PrintingHouseContact> PrintingHouseContacts { get; set; }
    }
}
