using System;
using System.Collections.Generic;

#nullable disable

namespace PPT.DAL.EF.Models
{
    public partial class City
    {
        public City()
        {
            Addresses = new HashSet<Address>();
            DeliveryServiceCities = new HashSet<DeliveryServiceCity>();
        }

        public long ID { get; set; }
        public string CityName { get; set; }
        public long RegionID { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Region Region { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<DeliveryServiceCity> DeliveryServiceCities { get; set; }
    }
}
