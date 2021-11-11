using System;
using System.Collections.Generic;

#nullable disable

namespace PPT.DAL.EF.Models
{
    public partial class OrderStatus
    {
        public OrderStatus()
        {
            OrderStatusFlowFromStatuses = new HashSet<OrderStatusFlow>();
            OrderStatusFlowToStatuses = new HashSet<OrderStatusFlow>();
            OrderTrackings = new HashSet<OrderTracking>();
        }

        public long ID { get; set; }
        public string OrderStatusName { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<OrderStatusFlow> OrderStatusFlowFromStatuses { get; set; }
        public virtual ICollection<OrderStatusFlow> OrderStatusFlowToStatuses { get; set; }
        public virtual ICollection<OrderTracking> OrderTrackings { get; set; }
    }
}
