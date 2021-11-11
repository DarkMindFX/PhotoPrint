using System;
using System.Collections.Generic;

#nullable disable

namespace PPT.DAL.EF.Models
{
    public partial class VOrder
    {
        public long OrderID { get; set; }
        public string Manager { get; set; }
        public string Client { get; set; }
        public string OrderStatus { get; set; }
        public string DeliveryService { get; set; }
        public string PaymentMethod { get; set; }
        public string TransactionId { get; set; }
        public long ID { get; set; }
        public long? ManagerId { get; set; }
        public long UserID { get; set; }
        public long ContactID { get; set; }
        public long DeliveryAddressId { get; set; }
        public long DeliveryServiceId { get; set; }
        public string Comments { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedByID { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedByID { get; set; }
    }
}
