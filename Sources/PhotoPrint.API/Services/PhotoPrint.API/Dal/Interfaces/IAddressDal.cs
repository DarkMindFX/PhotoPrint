


using PPT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PPT.PhotoPrint.API.Dal
{
    public interface IAddressDal : IDalBase<Address>
    {
        Address Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

            IList<Address> GetByAddressTypeID(System.Int64 AddressTypeID);
            IList<Address> GetByCityID(System.Int64 CityID);
            IList<Address> GetByCreatedByID(System.Int64 CreatedByID);
            IList<Address> GetByModifiedByID(System.Int64? ModifiedByID);
    
            bool Erase(System.Int64? ID);
            }
}
