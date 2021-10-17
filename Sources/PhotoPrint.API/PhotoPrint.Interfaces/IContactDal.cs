

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoPrint.Interfaces.Entities;

namespace PhotoPrint.Interfaces
{
    public interface IContactDal : IDalBase<Contact>
    {
        Contact Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

        IList<Contact> GetByContactTypeID(System.Int64 ContactTypeID);
        IList<Contact> GetByCreatedByID(System.Int64 CreatedByID);
        IList<Contact> GetByModifiedByID(System.Int64? ModifiedByID);
            }
}

