


using PPT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace PPT.PhotoPrint.API.Dal
{
    [Export(typeof(IUserConfirmationDal))]
    public class UserConfirmationDal : DalBaseImpl<UserConfirmation, Interfaces.IUserConfirmationDal>, IUserConfirmationDal
    {

        public UserConfirmationDal(Interfaces.IUserConfirmationDal dalImpl) : base(dalImpl)
        {
        }

        public UserConfirmation Get(System.Int64? ID)
        {
            return _dalImpl.Get(            ID);
        }

        public bool Delete(System.Int64? ID)
        {
            return _dalImpl.Delete(            ID);
        }


        public IList<UserConfirmation> GetByUserID(System.Int64 UserID)
        {
            return _dalImpl.GetByUserID(UserID);
        }
            }
}
