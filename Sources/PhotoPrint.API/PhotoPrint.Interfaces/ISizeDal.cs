

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoPrint.Interfaces.Entities;

namespace PhotoPrint.Interfaces
{
    public interface ISizeDal : IDalBase<Size>
    {
        Size Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

        IList<Size> GetByCreatedByID(System.Int64 CreatedByID);
        IList<Size> GetByModifiedByID(System.Int64? ModifiedByID);
            }
}

