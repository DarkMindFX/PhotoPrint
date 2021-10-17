

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPT.Interfaces.Entities
{
    public class Address 
    {
				public System.Int64? ID { get; set; }

				public System.Int64 AddressTypeID { get; set; }

				public System.String Title { get; set; }

				public System.Int64 CityID { get; set; }

				public System.String Street { get; set; }

				public System.String BuildingNo { get; set; }

				public System.String ApartmentNo { get; set; }

				public System.String Comment { get; set; }

				public System.Int64 CreatedByID { get; set; }

				public System.DateTime CreatedDate { get; set; }

				public System.Int64? ModifiedByID { get; set; }

				public System.DateTime? ModifiedDate { get; set; }

				public System.Boolean IsDeleted { get; set; }

				
    }
}
