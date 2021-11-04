


using PPT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace PPT.PhotoPrint.API.Dal
{
    [Export(typeof(IUserStatusDal))]
    public class UserStatusDal : DalBaseImpl<UserStatus, Interfaces.IUserStatusDal>, IUserStatusDal
    {

        public UserStatusDal(Interfaces.IUserStatusDal dalImpl) : base(dalImpl)
        {
        }

        public UserStatus Get(System.Int64? ID)
        {
            return _dalImpl.Get(            ID);
        }

        public bool Delete(System.Int64? ID)
        {
            return _dalImpl.Delete(            ID);
        }

        public bool Erase(System.Int64? ID)
        {
            return _dalImpl.Delete(            ID);
        }

            }
}
