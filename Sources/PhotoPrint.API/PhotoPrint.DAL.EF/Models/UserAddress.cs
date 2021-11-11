using System;
using System.Collections.Generic;

#nullable disable

namespace PPT.DAL.EF.Models
{
    public partial class UserAddress
    {
        public long UserId { get; set; }
        public long AddressID { get; set; }
        public bool IsPrimary { get; set; }

        public virtual Address Address { get; set; }
        public virtual User User { get; set; }
    }
}
