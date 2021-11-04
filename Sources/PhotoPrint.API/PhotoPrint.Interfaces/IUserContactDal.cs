


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPT.Interfaces.Entities;
using PPT.Interfaces;

namespace PPT.Interfaces
{
    public interface IUserContactDal : IDalBase<UserContact>
    {
        UserContact Get(System.Int64 UserID,System.Int64 ContactID);

        bool Delete(System.Int64 UserID,System.Int64 ContactID);

        IList<UserContact> GetByUserID(System.Int64 UserID);
        IList<UserContact> GetByContactID(System.Int64 ContactID);
        
            }
}

