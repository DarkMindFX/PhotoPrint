

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoPrint.Interfaces.Entities
{
    public class DeliveryService 
    {
				public System.Int64? ID { get; set; }

				public System.String DeliveryServiceName { get; set; }

				public System.String Description { get; set; }

				public System.Boolean IsDeleted { get; set; }

				public System.DateTime CreatedDate { get; set; }

				public System.Int64 CreatedByID { get; set; }

				public System.DateTime? ModifiedDate { get; set; }

				public System.Int64? ModifiedByID { get; set; }

				
    }
}
