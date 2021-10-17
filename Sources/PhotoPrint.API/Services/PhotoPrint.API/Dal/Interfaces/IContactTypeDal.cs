

using PPT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PPT.PhotoPrint.API.Dal
{
    public interface IContactTypeDal : IDalBase<ContactType>
    {
        ContactType Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

        }
}
