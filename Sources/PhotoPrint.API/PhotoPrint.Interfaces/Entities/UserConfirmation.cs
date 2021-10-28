

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPT.Interfaces.Entities
{
    public class UserConfirmation 
    {
				public System.Int64? ID { get; set; }

				public System.Int64 UserID { get; set; }

				public System.String ConfirmationCode { get; set; }

				public System.Boolean Comfirmed { get; set; }

				public System.DateTime ExpiresDate { get; set; }

				public System.DateTime? ConfirmationDate { get; set; }

				
    }
}
