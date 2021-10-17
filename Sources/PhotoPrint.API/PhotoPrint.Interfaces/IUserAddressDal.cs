

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoPrint.Interfaces.Entities;

namespace PhotoPrint.Interfaces
{
    public interface IUserAddressDal : IDalBase<UserAddress>
    {
        UserAddress Get(System.Int64 UserID,System.Int64 AddressID);

        bool Delete(System.Int64 UserID,System.Int64 AddressID);

        IList<UserAddress> GetByUserID(System.Int64 UserID);
        IList<UserAddress> GetByAddressID(System.Int64 AddressID);
            }
}

