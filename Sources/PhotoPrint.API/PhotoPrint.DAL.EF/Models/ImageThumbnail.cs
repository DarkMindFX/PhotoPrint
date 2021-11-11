using System;
using System.Collections.Generic;

#nullable disable

namespace PPT.DAL.EF.Models
{
    public partial class ImageThumbnail
    {
        public long ID { get; set; }
        public string Url { get; set; }
        public int? Order { get; set; }
        public long ImageID { get; set; }

        public virtual Image Image { get; set; }
    }
}
