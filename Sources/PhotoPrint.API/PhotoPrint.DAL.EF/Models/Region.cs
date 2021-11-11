using System;
using System.Collections.Generic;

#nullable disable

namespace PPT.DAL.EF.Models
{
    public partial class Region
    {
        public Region()
        {
            Cities = new HashSet<City>();
        }

        public long ID { get; set; }
        public string RegionName { get; set; }
        public long CountryID { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<City> Cities { get; set; }
    }
}
