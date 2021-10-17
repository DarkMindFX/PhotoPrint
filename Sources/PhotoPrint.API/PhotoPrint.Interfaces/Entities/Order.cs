

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPT.Interfaces.Entities
{
    public class Order 
    {
				public System.Int64? ID { get; set; }

				public System.Int64? ManagerID { get; set; }

				public System.Int64 UserID { get; set; }

				public System.Int64 ContactID { get; set; }

				public System.Int64 DeliveryAddressID { get; set; }

				public System.Int64 DeliveryServiceID { get; set; }

				public System.String Comments { get; set; }

				public System.Boolean IsDeleted { get; set; }

				public System.DateTime CreatedDate { get; set; }

				public System.Int64 CreatedByID { get; set; }

				public System.DateTime? ModifiedDate { get; set; }

				public System.Int64? ModifiedByID { get; set; }

				
    }
}
