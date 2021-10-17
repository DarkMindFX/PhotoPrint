

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoPrint.Interfaces.Entities
{
    public class OrderPaymentDetails 
    {
				public System.Int64? ID { get; set; }

				public System.Int64 OrderID { get; set; }

				public System.Int64 PaymentMethodID { get; set; }

				public System.String PaymentTransUID { get; set; }

				public System.DateTime? PaymentDateTime { get; set; }

				public System.Boolean IsDeleted { get; set; }

				public System.DateTime CreatedDate { get; set; }

				public System.Int64 CreatedByID { get; set; }

				public System.DateTime? ModifiedDate { get; set; }

				public System.Int64? ModifiedByID { get; set; }

				
    }
}
