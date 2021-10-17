

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoPrint.Interfaces.Entities
{
    public class Size 
    {
				public System.Int64? ID { get; set; }

				public System.String SizeName { get; set; }

				public System.Int32 Width { get; set; }

				public System.Int32 Height { get; set; }

				public System.Int64 IsDeleted { get; set; }

				public System.DateTime CreatedDate { get; set; }

				public System.Int64 CreatedByID { get; set; }

				public System.DateTime? ModifiedDate { get; set; }

				public System.Int64? ModifiedByID { get; set; }

				
    }
}
