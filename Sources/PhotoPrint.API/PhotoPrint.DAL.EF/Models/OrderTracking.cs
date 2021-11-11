using System;
using System.Collections.Generic;

#nullable disable

namespace PPT.DAL.EF.Models
{
    public partial class OrderTracking
    {
        public long ID { get; set; }
        public long OrderID { get; set; }
        public long OrderStatusID { get; set; }
        public DateTime SetDate { get; set; }
        public long SetByID { get; set; }
        public string Comment { get; set; }

        public virtual Order Order { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
        public virtual User SetBy { get; set; }
    }
}
