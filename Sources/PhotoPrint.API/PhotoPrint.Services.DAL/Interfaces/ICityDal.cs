


using PPT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PPT.Services.Dal
{
    public interface ICityDal : IDalBase<City>
    {
        City Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

            IList<City> GetByRegionID(System.Int64 RegionID);
    
            bool Erase(System.Int64? ID);
            }
}
