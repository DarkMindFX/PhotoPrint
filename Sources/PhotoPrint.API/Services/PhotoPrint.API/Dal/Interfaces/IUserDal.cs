


using PPT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PPT.PhotoPrint.API.Dal
{
    public interface IUserDal : IDalBase<User>
    {
        User Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

            IList<User> GetByUserStatusID(System.Int64 UserStatusID);
            IList<User> GetByUserTypeID(System.Int64 UserTypeID);
            IList<User> GetByModifiedByID(System.Int64? ModifiedByID);
    
        }
}
