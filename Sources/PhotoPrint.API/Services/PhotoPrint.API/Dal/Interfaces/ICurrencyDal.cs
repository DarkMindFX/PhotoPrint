


using PPT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PPT.PhotoPrint.API.Dal
{
    public interface ICurrencyDal : IDalBase<Currency>
    {
        Currency Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

    
            bool Erase(System.Int64? ID);
            }
}
