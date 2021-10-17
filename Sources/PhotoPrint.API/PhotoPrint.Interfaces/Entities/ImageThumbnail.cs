

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoPrint.Interfaces.Entities
{
    public class ImageThumbnail 
    {
				public System.Int64? ID { get; set; }

				public System.String Url { get; set; }

				public System.Int32? Order { get; set; }

				public System.Int64 ImageID { get; set; }

				
    }
}
