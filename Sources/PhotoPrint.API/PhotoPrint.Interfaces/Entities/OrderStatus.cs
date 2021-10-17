

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoPrint.Interfaces.Entities
{
    public class OrderStatus 
    {
				public System.Int64? ID { get; set; }

				public System.String OrderStatusName { get; set; }

				public System.Int64 IsDeleted { get; set; }

				
    }
}
