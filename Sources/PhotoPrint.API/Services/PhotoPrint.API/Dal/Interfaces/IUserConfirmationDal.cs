

using PPT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PPT.PhotoPrint.API.Dal
{
    public interface IUserConfirmationDal : IDalBase<UserConfirmation>
    {
        UserConfirmation Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

            IList<UserConfirmation> GetByUserID(System.Int64 UserID);
        }
}
