


using PPT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PPT.Services.Dal
{
    public interface IMountingTypeDal : IDalBase<MountingType>
    {
        MountingType Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

    
            bool Erase(System.Int64? ID);
            }
}
