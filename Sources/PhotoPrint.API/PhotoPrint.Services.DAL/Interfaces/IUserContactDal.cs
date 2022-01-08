


using PPT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PPT.Services.Dal
{
    public interface IUserContactDal : IDalBase<UserContact>
    {
        UserContact Get(System.Int64 UserID,System.Int64 ContactID);

        bool Delete(System.Int64 UserID,System.Int64 ContactID);

            IList<UserContact> GetByUserID(System.Int64 UserID);
            IList<UserContact> GetByContactID(System.Int64 ContactID);
    
        }
}
