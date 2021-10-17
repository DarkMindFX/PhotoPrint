

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoPrint.Interfaces.Entities
{
    public class City 
    {
				public System.Int64? ID { get; set; }

				public System.String CityName { get; set; }

				public System.Int64 RegionID { get; set; }

				public System.Boolean IsDeleted { get; set; }

				
    }
}
