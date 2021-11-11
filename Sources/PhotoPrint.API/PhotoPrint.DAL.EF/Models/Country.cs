using System;
using System.Collections.Generic;

#nullable disable

namespace PPT.DAL.EF.Models
{
    public partial class Country
    {
        public Country()
        {
            Regions = new HashSet<Region>();
        }

        public long ID { get; set; }
        public string CountryName { get; set; }
        public string ISO { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Region> Regions { get; set; }
    }
}
