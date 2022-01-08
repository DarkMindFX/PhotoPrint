


using PPT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace PPT.Services.Dal
{
    [Export(typeof(IAddressTypeDal))]
    public class AddressTypeDal : DalBaseImpl<AddressType, Interfaces.IAddressTypeDal>, IAddressTypeDal
    {

        public AddressTypeDal(Interfaces.IAddressTypeDal dalImpl) : base(dalImpl)
        {
        }

        public AddressType Get(System.Int64? ID)
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
