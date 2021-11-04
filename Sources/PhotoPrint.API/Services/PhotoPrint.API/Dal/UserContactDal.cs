


using PPT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace PPT.PhotoPrint.API.Dal
{
    [Export(typeof(IUserContactDal))]
    public class UserContactDal : DalBaseImpl<UserContact, Interfaces.IUserContactDal>, IUserContactDal
    {

        public UserContactDal(Interfaces.IUserContactDal dalImpl) : base(dalImpl)
        {
        }

        public UserContact Get(System.Int64 UserID,System.Int64 ContactID)
        {
            return _dalImpl.Get(            UserID,            ContactID);
        }

        public bool Delete(System.Int64 UserID,System.Int64 ContactID)
        {
            return _dalImpl.Delete(            UserID,            ContactID);
        }


        public IList<UserContact> GetByUserID(System.Int64 UserID)
        {
            return _dalImpl.GetByUserID(UserID);
        }
        public IList<UserContact> GetByContactID(System.Int64 ContactID)
        {
            return _dalImpl.GetByContactID(ContactID);
        }
            }
}
