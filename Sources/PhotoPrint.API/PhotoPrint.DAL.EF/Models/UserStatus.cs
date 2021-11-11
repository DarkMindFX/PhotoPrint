using System;
using System.Collections.Generic;

#nullable disable

namespace PPT.DAL.EF.Models
{
    public partial class UserStatus
    {
        public UserStatus()
        {
            Users = new HashSet<User>();
        }

        public long ID { get; set; }
        public string StatusName { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
