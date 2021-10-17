

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoPrint.Interfaces.Entities;

namespace PhotoPrint.Interfaces
{
    public interface IMaterialTypeDal : IDalBase<MaterialType>
    {
        MaterialType Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

        IList<MaterialType> GetByCreatedByID(System.Int64 CreatedByID);
        IList<MaterialType> GetByModifiedByID(System.Int64? ModifiedByID);
            }
}

