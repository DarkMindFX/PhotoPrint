

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoPrint.Interfaces.Entities
{
    public class OrderItem 
    {
				public System.Int64? ID { get; set; }

				public System.Int64 OrderID { get; set; }

				public System.Int64 ImageID { get; set; }

				public System.Int32? Width { get; set; }

				public System.Int32? Height { get; set; }

				public System.Int64? SizeID { get; set; }

				public System.Int64 FrameTypeID { get; set; }

				public System.Int64? FrameSizeID { get; set; }

				public System.Int64 MatID { get; set; }

				public System.Int64 MaterialTypeID { get; set; }

				public System.Int64 MountingTypeID { get; set; }

				public System.Int32 ItemCount { get; set; }

				public System.Decimal PriceAmountPerItem { get; set; }

				public System.Int64 PriceCurrencyID { get; set; }

				public System.String Comments { get; set; }

				public System.Int64? PrintingHouseID { get; set; }

				public System.Boolean IsDeleted { get; set; }

				public System.DateTime CreatedDate { get; set; }

				public System.Int64 CreatedByID { get; set; }

				public System.DateTime? ModifiedDate { get; set; }

				public System.Int64? ModifiedByID { get; set; }

				
    }
}
