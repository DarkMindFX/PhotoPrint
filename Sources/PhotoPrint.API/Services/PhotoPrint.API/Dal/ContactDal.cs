

using PPT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace PPT.PhotoPrint.API.Dal
{
    [Export(typeof(IContactDal))]
    public class ContactDal : DalBaseImpl<Contact, Interfaces.IContactDal>, IContactDal
    {

        public ContactDal(Interfaces.IContactDal dalImpl) : base(dalImpl)
        {
        }

        public Contact Get(System.Int64? ID)
        {
            return _dalImpl.Get(            ID);
        }

        public bool Delete(System.Int64? ID)
        {
            return _dalImpl.Delete(            ID);
        }

        public IList<Contact> GetByContactTypeID(System.Int64 ContactTypeID)
        {
            return _dalImpl.GetByContactTypeID(ContactTypeID);
        }
        public IList<Contact> GetByCreatedByID(System.Int64 CreatedByID)
        {
            return _dalImpl.GetByCreatedByID(CreatedByID);
        }
        public IList<Contact> GetByModifiedByID(System.Int64? ModifiedByID)
        {
            return _dalImpl.GetByModifiedByID(ModifiedByID);
        }
            }
}
