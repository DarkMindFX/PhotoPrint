

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoPrint.Interfaces.Entities
{
    public class Region 
    {
				public System.Int64? ID { get; set; }

				public System.String RegionName { get; set; }

				public System.Int64 CountryID { get; set; }

				public System.Int64 IsDeleted { get; set; }

				
    }
}
