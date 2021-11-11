using System;
using System.Collections.Generic;

#nullable disable

namespace PPT.DAL.EF.Models
{
    public partial class ImageRelated
    {
        public long ImageID { get; set; }
        public long RelatedImageID { get; set; }

        public virtual Image Image { get; set; }
        public virtual Image RelatedImage { get; set; }
    }
}
