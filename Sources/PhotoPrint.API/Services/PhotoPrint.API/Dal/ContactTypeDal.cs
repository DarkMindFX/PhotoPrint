

using PPT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace PPT.PhotoPrint.API.Dal
{
    [Export(typeof(IContactTypeDal))]
    public class ContactTypeDal : DalBaseImpl<ContactType, Interfaces.IContactTypeDal>, IContactTypeDal
    {

        public ContactTypeDal(Interfaces.IContactTypeDal dalImpl) : base(dalImpl)
        {
        }

        public ContactType Get(System.Int64? ID)
        {
            return _dalImpl.Get(            ID);
        }

        public bool Delete(System.Int64? ID)
        {
            return _dalImpl.Delete(            ID);
        }

            }
}
