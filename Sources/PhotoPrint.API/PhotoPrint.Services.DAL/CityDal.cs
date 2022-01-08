


using PPT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace PPT.Services.Dal
{
    [Export(typeof(ICityDal))]
    public class CityDal : DalBaseImpl<City, Interfaces.ICityDal>, ICityDal
    {

        public CityDal(Interfaces.ICityDal dalImpl) : base(dalImpl)
        {
        }

        public City Get(System.Int64? ID)
        {
            return _dalImpl.Get(            ID);
        }

        public bool Delete(System.Int64? ID)
        {
            return _dalImpl.Delete(            ID);
        }

        public bool Erase(System.Int64? ID)
        {
            return _dalImpl.Delete(            ID);
        }

        public IList<City> GetByRegionID(System.Int64 RegionID)
        {
            return _dalImpl.GetByRegionID(RegionID);
        }
            }
}
