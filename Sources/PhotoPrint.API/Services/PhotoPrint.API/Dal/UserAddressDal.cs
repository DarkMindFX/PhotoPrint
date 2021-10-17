

using PPT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace PPT.PhotoPrint.API.Dal
{
    [Export(typeof(IUserAddressDal))]
    public class UserAddressDal : DalBaseImpl<UserAddress, Interfaces.IUserAddressDal>, IUserAddressDal
    {

        public UserAddressDal(Interfaces.IUserAddressDal dalImpl) : base(dalImpl)
        {
        }

        public UserAddress Get(System.Int64 UserID,System.Int64 AddressID)
        {
            return _dalImpl.Get(            UserID,            AddressID);
        }

        public bool Delete(System.Int64 UserID,System.Int64 AddressID)
        {
            return _dalImpl.Delete(            UserID,            AddressID);
        }

        public IList<UserAddress> GetByUserID(System.Int64 UserID)
        {
            return _dalImpl.GetByUserID(UserID);
        }
        public IList<UserAddress> GetByAddressID(System.Int64 AddressID)
        {
            return _dalImpl.GetByAddressID(AddressID);
        }
            }
}
