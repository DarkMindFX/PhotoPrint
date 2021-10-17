

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoPrint.Interfaces.Entities
{
    public class Currency 
    {
				public System.Int64? ID { get; set; }

				public System.String ISO { get; set; }

				public System.String CurrencyName { get; set; }

				public System.Boolean IsDeleted { get; set; }

				
    }
}
