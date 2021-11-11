using System;
using System.Collections.Generic;

#nullable disable

namespace PPT.DAL.EF.Models
{
    public partial class ContactType
    {
        public ContactType()
        {
            Contacts = new HashSet<Contact>();
        }

        public long ID { get; set; }
        public string ContactTypeName { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
