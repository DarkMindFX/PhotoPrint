using System;
using System.Collections.Generic;

#nullable disable

namespace PPT.DAL.EF.Models
{
    public partial class UserConfirmation
    {
        public long ID { get; set; }
        public long UserID { get; set; }
        public string ConfirmationCode { get; set; }
        public bool Comfirmed { get; set; }
        public DateTime ExpiresDate { get; set; }
        public DateTime? ConfirmationDate { get; set; }

        public virtual User User { get; set; }
    }
}
