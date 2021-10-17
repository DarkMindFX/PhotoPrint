

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPT.Interfaces.Entities
{
    public class Contact 
    {
				public System.Int64? ID { get; set; }

				public System.Int64 ContactTypeID { get; set; }

				public System.String Title { get; set; }

				public System.String Comment { get; set; }

				public System.String Value { get; set; }

				public System.Boolean IsDeleted { get; set; }

				public System.Int64 CreatedByID { get; set; }

				public System.DateTime CreatedDate { get; set; }

				public System.Int64? ModifiedByID { get; set; }

				public System.DateTime? ModifiedDate { get; set; }

				
    }
}
