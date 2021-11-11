using System;
using System.Collections.Generic;

#nullable disable

namespace PPT.DAL.EF.Models
{
    public partial class OrderPaymentDetail
    {
        public long ID { get; set; }
        public long OrderId { get; set; }
        public long PaymentMethodID { get; set; }
        public string PaymentTransUID { get; set; }
        public DateTime? PaymentDateTime { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedByID { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedByID { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual User ModifiedBy { get; set; }
        public virtual Order Order { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
    }
}
