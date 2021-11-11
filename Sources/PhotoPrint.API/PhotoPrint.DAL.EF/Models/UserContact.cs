using System;
using System.Collections.Generic;

#nullable disable

namespace PPT.DAL.EF.Models
{
    public partial class UserContact
    {
        public long UserID { get; set; }
        public long ContactID { get; set; }
        public bool IsPrimary { get; set; }

        public virtual Contact Contact { get; set; }
        public virtual User User { get; set; }
    }
}
