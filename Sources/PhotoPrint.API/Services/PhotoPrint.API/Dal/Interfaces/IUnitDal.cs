


using PPT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PPT.PhotoPrint.API.Dal
{
    public interface IUnitDal : IDalBase<Unit>
    {
        Unit Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

    
            bool Erase(System.Int64? ID);
            }
}
