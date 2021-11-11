using System;
using System.Collections.Generic;

#nullable disable

namespace PPT.DAL.EF.Models
{
    public partial class PrintingHouseAddress
    {
        public long PrintingHouseID { get; set; }
        public long AddressId { get; set; }
        public bool IsPrimary { get; set; }

        public virtual Address Address { get; set; }
        public virtual PrintingHouse PrintingHouse { get; set; }
    }
}
