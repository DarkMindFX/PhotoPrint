

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPT.Interfaces.Entities
{
    public class User 
    {
				public System.Int64? ID { get; set; }

				public System.String Login { get; set; }

				public System.String PwdHash { get; set; }

				public System.String Salt { get; set; }

				public System.String FirstName { get; set; }

				public System.String MiddleName { get; set; }

				public System.String LastName { get; set; }

				public System.String FriendlyName { get; set; }

				public System.Int64 UserStatusID { get; set; }

				public System.Int64 UserTypeID { get; set; }

				public System.DateTime CreatedDate { get; set; }

				public System.DateTime? ModifiedDate { get; set; }

				public System.Int64? ModifiedByID { get; set; }

				
    }
}
