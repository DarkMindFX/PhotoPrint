using System;
using System.Collections.Generic;

#nullable disable

namespace PPT.DAL.EF.Models
{
    public partial class ImageCategory
    {
        public long ImageID { get; set; }
        public long CategoryID { get; set; }

        public virtual Category Category { get; set; }
        public virtual Image Image { get; set; }
    }
}
