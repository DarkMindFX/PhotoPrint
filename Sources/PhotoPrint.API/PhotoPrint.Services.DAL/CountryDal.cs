


using PPT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace PPT.Services.Dal
{
    [Export(typeof(ICountryDal))]
    public class CountryDal : DalBaseImpl<Country, Interfaces.ICountryDal>, ICountryDal
    {

        public CountryDal(Interfaces.ICountryDal dalImpl) : base(dalImpl)
        {
        }

        public Country Get(System.Int64? ID)
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

            }
}
