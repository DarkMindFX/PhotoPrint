

using System;
using System.Collections.Generic;

#nullable disable

namespace PPT.DAL.EF.Models
{
    public partial class UserInteriorThumbnail
    {
        public UserInteriorThumbnail()
        {
        }

        public System.Int64? ID { get; set; }
        public System.Int64 UserID { get; set; }
        public System.String Url { get; set; }



        public virtual User User { get; set; }

    }
}