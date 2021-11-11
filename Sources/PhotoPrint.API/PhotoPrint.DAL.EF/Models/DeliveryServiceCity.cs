using System;
using System.Collections.Generic;

#nullable disable

namespace PPT.DAL.EF.Models
{
    public partial class DeliveryServiceCity
    {
        public long DeliveryServiceID { get; set; }
        public long CityID { get; set; }

        public virtual City City { get; set; }
        public virtual DeliveryService DeliveryService { get; set; }
    }
}
