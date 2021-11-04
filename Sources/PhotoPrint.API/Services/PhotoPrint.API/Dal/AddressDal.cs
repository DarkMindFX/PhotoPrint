


using PPT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace PPT.PhotoPrint.API.Dal
{
    [Export(typeof(IAddressDal))]
    public class AddressDal : DalBaseImpl<Address, Interfaces.IAddressDal>, IAddressDal
    {

        public AddressDal(Interfaces.IAddressDal dalImpl) : base(dalImpl)
        {
        }

        public Address Get(System.Int64? ID)
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

        public IList<Address> GetByAddressTypeID(System.Int64 AddressTypeID)
        {
            return _dalImpl.GetByAddressTypeID(AddressTypeID);
        }
        public IList<Address> GetByCityID(System.Int64 CityID)
        {
            return _dalImpl.GetByCityID(CityID);
        }
        public IList<Address> GetByCreatedByID(System.Int64 CreatedByID)
        {
            return _dalImpl.GetByCreatedByID(CreatedByID);
        }
        public IList<Address> GetByModifiedByID(System.Int64? ModifiedByID)
        {
            return _dalImpl.GetByModifiedByID(ModifiedByID);
        }
            }
}
