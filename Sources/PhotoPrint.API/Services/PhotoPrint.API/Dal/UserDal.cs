


using PPT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace PPT.PhotoPrint.API.Dal
{
    [Export(typeof(IUserDal))]
    public class UserDal : DalBaseImpl<User, Interfaces.IUserDal>, IUserDal
    {

        public UserDal(Interfaces.IUserDal dalImpl) : base(dalImpl)
        {
        }

        public User Get(System.Int64? ID)
        {
            return _dalImpl.Get(            ID);
        }

        public bool Delete(System.Int64? ID)
        {
            return _dalImpl.Delete(            ID);
        }


        public IList<User> GetByUserStatusID(System.Int64 UserStatusID)
        {
            return _dalImpl.GetByUserStatusID(UserStatusID);
        }
        public IList<User> GetByUserTypeID(System.Int64 UserTypeID)
        {
            return _dalImpl.GetByUserTypeID(UserTypeID);
        }
        public IList<User> GetByModifiedByID(System.Int64? ModifiedByID)
        {
            return _dalImpl.GetByModifiedByID(ModifiedByID);
        }
            }
}
