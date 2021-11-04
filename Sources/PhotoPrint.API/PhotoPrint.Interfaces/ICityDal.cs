


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPT.Interfaces.Entities;
using PPT.Interfaces;

namespace PPT.Interfaces
{
    public interface ICityDal : IDalBase<City>
    {
        City Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

        IList<City> GetByRegionID(System.Int64 RegionID);
        
                bool Erase(System.Int64? ID);
            }
}

