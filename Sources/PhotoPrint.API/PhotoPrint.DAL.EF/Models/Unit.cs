using System;
using System.Collections.Generic;

#nullable disable

namespace PPT.DAL.EF.Models
{
    public partial class Unit
    {
        public long ID { get; set; }
        public string UnitName { get; set; }
        public string UnitAbbr { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
    }
}
