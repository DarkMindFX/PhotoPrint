using System;
using System.Collections.Generic;

#nullable disable

namespace PPT.DAL.EF.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
            OrderPaymentDetails = new HashSet<OrderPaymentDetail>();
            OrderTrackings = new HashSet<OrderTracking>();
        }

        public long ID { get; set; }
        public long? ManagerID { get; set; }
        public long UserID { get; set; }
        public long ContactID { get; set; }
        public long DeliveryAddressID { get; set; }
        public long DeliveryServiceID { get; set; }
        public string Comments { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedByID { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedByID { get; set; }

        public virtual Contact Contact { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Address DeliveryAddress { get; set; }
        public virtual DeliveryService DeliveryService { get; set; }
        public virtual User Manager { get; set; }
        public virtual User ModifiedBy { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<OrderPaymentDetail> OrderPaymentDetails { get; set; }
        public virtual ICollection<OrderTracking> OrderTrackings { get; set; }
    }
}
