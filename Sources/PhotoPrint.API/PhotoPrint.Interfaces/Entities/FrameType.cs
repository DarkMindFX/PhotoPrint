

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPT.Interfaces.Entities
{
    public class FrameType 
    {
				public System.Int64? ID { get; set; }

				public System.String FrameTypeName { get; set; }

				public System.String Description { get; set; }

				public System.String ThumbnailUrl { get; set; }

				public System.Boolean IsDeleted { get; set; }

				public System.DateTime CreatedDate { get; set; }

				public System.Int64 CreatedByID { get; set; }

				public System.DateTime? ModifiedDate { get; set; }

				public System.Int64? ModifiedByID { get; set; }

				
    }
}
