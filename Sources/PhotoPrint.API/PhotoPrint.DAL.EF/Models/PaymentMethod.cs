using System;
using System.Collections.Generic;

#nullable disable

namespace PPT.DAL.EF.Models
{
    public partial class PaymentMethod
    {
        public PaymentMethod()
        {
            OrderPaymentDetails = new HashSet<OrderPaymentDetail>();
        }

        public long ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<OrderPaymentDetail> OrderPaymentDetails { get; set; }
    }
}
