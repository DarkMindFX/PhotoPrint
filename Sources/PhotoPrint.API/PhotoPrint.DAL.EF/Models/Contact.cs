using System;
using System.Collections.Generic;

#nullable disable

namespace PPT.DAL.EF.Models
{
    public partial class Contact
    {
        public Contact()
        {
            Orders = new HashSet<Order>();
            PrintingHouseContacts = new HashSet<PrintingHouseContact>();
            UserContacts = new HashSet<UserContact>();
        }

        public long ID { get; set; }
        public long ContactTypeID { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public string Value { get; set; }
        public bool IsDeleted { get; set; }
        public long CreatedByID { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ModifiedByID { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ContactType ContactType { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual User ModifiedBy { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<PrintingHouseContact> PrintingHouseContacts { get; set; }
        public virtual ICollection<UserContact> UserContacts { get; set; }
    }
}
