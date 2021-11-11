using System;
using System.Collections.Generic;

#nullable disable

namespace PPT.DAL.EF.Models
{
    public partial class AddressType
    {
        public AddressType()
        {
            Addresses = new HashSet<Address>();
        }

        public long ID { get; set; }
        public string AddressTypeName { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}
