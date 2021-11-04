


using PPT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PPT.PhotoPrint.API.Dal
{
    public interface ISizeDal : IDalBase<Size>
    {
        Size Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

            IList<Size> GetByCreatedByID(System.Int64 CreatedByID);
            IList<Size> GetByModifiedByID(System.Int64? ModifiedByID);
    
            bool Erase(System.Int64? ID);
            }
}
