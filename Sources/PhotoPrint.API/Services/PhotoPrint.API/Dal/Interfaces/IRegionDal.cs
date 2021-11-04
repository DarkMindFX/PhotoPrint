


using PPT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PPT.PhotoPrint.API.Dal
{
    public interface IRegionDal : IDalBase<Region>
    {
        Region Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

            IList<Region> GetByCountryID(System.Int64 CountryID);
    
            bool Erase(System.Int64? ID);
            }
}
