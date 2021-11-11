using System;
using System.Collections.Generic;

#nullable disable

namespace PPT.DAL.EF.Models
{
    public partial class PrintingHouseContact
    {
        public long PrintingHouseID { get; set; }
        public long ContactId { get; set; }
        public bool IsPrimary { get; set; }

        public virtual Contact Contact { get; set; }
        public virtual PrintingHouse PrintingHouse { get; set; }
    }
}
