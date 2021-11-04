


using PPT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PPT.PhotoPrint.API.Dal
{
    public interface IMaterialTypeDal : IDalBase<MaterialType>
    {
        MaterialType Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

            IList<MaterialType> GetByCreatedByID(System.Int64 CreatedByID);
            IList<MaterialType> GetByModifiedByID(System.Int64? ModifiedByID);
    
            bool Erase(System.Int64? ID);
            }
}
