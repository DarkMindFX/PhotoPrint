

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPT.Interfaces.Entities
{
    public class Image 
    {
				public System.Int64? ID { get; set; }

				public System.String Title { get; set; }

				public System.String Description { get; set; }

				public System.String OriginUrl { get; set; }

				public System.Int32? MaxWidth { get; set; }

				public System.Int32? MaxHeight { get; set; }

				public System.Decimal? PriceAmount { get; set; }

				public System.Int64? PriceCurrencyID { get; set; }

				public System.Boolean IsDeleted { get; set; }

				public System.Int64 CreatedByID { get; set; }

				public System.DateTime CreatedDate { get; set; }

				public System.Int64? ModifiedByID { get; set; }

				public System.DateTime? ModifiedDate { get; set; }

				
    }
}
