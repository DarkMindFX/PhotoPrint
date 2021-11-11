using System;
using System.Collections.Generic;

#nullable disable

namespace PPT.DAL.EF.Models
{
    public partial class OrderStatusFlow
    {
        public long FromStatusID { get; set; }
        public long ToStatusID { get; set; }

        public virtual OrderStatus FromStatus { get; set; }
        public virtual OrderStatus ToStatus { get; set; }
    }
}
