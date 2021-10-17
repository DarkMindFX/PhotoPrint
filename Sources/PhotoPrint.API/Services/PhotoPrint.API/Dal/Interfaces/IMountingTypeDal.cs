

using PPT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PPT.PhotoPrint.API.Dal
{
    public interface IMountingTypeDal : IDalBase<MountingType>
    {
        MountingType Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

        }
}
