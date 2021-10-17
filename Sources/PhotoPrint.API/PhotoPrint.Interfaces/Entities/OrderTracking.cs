

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoPrint.Interfaces.Entities
{
    public class OrderTracking 
    {
				public System.Int64? ID { get; set; }

				public System.Int64 OrderID { get; set; }

				public System.Int64 OrderStatusID { get; set; }

				public System.DateTime SetDate { get; set; }

				public System.Int64 SetByID { get; set; }

				public System.String Comment { get; set; }

				
    }
}
