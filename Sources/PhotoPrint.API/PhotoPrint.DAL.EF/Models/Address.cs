using System;
using System.Collections.Generic;

#nullable disable

namespace PPT.DAL.EF.Models
{
    public partial class Address
    {
        public Address()
        {
            Orders = new HashSet<Order>();
            PrintingHouseAddresses = new HashSet<PrintingHouseAddress>();
            UserAddresses = new HashSet<UserAddress>();
        }

        public long ID { get; set; }
        public long AddressTypeID { get; set; }
        public string Title { get; set; }
        public long CityID { get; set; }
        public string Street { get; set; }
        public string BuildingNo { get; set; }
        public string ApartmentNo { get; set; }
        public string Comment { get; set; }
        public long CreatedByID { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ModifiedByID { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }

        public virtual AddressType AddressType { get; set; }
        public virtual City City { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual User ModifiedBy { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<PrintingHouseAddress> PrintingHouseAddresses { get; set; }
        public virtual ICollection<UserAddress> UserAddresses { get; set; }
    }
}
