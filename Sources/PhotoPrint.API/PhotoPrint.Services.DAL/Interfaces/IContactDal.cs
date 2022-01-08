


using PPT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PPT.Services.Dal
{
    public interface IContactDal : IDalBase<Contact>
    {
        Contact Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

            IList<Contact> GetByContactTypeID(System.Int64 ContactTypeID);
            IList<Contact> GetByCreatedByID(System.Int64 CreatedByID);
            IList<Contact> GetByModifiedByID(System.Int64? ModifiedByID);
    
            bool Erase(System.Int64? ID);
            }
}
