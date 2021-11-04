


using PPT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace PPT.PhotoPrint.API.Dal
{
    [Export(typeof(IRegionDal))]
    public class RegionDal : DalBaseImpl<Region, Interfaces.IRegionDal>, IRegionDal
    {

        public RegionDal(Interfaces.IRegionDal dalImpl) : base(dalImpl)
        {
        }

        public Region Get(System.Int64? ID)
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

        public IList<Region> GetByCountryID(System.Int64 CountryID)
        {
            return _dalImpl.GetByCountryID(CountryID);
        }
            }
}
