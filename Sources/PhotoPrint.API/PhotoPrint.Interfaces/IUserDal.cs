

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoPrint.Interfaces.Entities;

namespace PhotoPrint.Interfaces
{
    public interface IUserDal : IDalBase<User>
    {
        User Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

        IList<User> GetByUserStatusID(System.Int64 UserStatusID);
        IList<User> GetByUserTypeID(System.Int64 UserTypeID);
            }
}

