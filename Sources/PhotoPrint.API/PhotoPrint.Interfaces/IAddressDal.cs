

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoPrint.Interfaces.Entities;

namespace PhotoPrint.Interfaces
{
    public interface IAddressDal : IDalBase<Address>
    {
        Address Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

        IList<Address> GetByAddressTypeID(System.Int64 AddressTypeID);
        IList<Address> GetByCityID(System.Int64 CityID);
        IList<Address> GetByCreatedByID(System.Int64 CreatedByID);
        IList<Address> GetByModifiedByID(System.Int64? ModifiedByID);
            }
}

