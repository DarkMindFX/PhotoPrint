

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPT.Interfaces.Entities
{
    public class PaymentMethod 
    {
				public System.Int64? ID { get; set; }

				public System.String Name { get; set; }

				public System.String Description { get; set; }

				public System.Boolean IsDeleted { get; set; }

				
    }
}
