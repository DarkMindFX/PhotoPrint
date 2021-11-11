using System;
using System.Collections.Generic;

#nullable disable

namespace PPT.DAL.EF.Models
{
    public partial class Currency
    {
        public Currency()
        {
            Images = new HashSet<Image>();
            OrderItems = new HashSet<OrderItem>();
        }

        public long ID { get; set; }
        public string ISO { get; set; }
        public string CurrencyName { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
