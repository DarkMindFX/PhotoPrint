using System;
using System.Collections.Generic;

#nullable disable

namespace PPT.DAL.EF.Models
{
    public partial class DeliveryService
    {
        public DeliveryService()
        {
            DeliveryServiceCities = new HashSet<DeliveryServiceCity>();
            Orders = new HashSet<Order>();
        }

        public long ID { get; set; }
        public string DeliveryServiceName { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedByID { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedByID { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual User ModifiedBy { get; set; }
        public virtual ICollection<DeliveryServiceCity> DeliveryServiceCities { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
