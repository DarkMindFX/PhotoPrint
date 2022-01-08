


using PPT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace PPT.Services.Dal
{
    [Export(typeof(ICurrencyDal))]
    public class CurrencyDal : DalBaseImpl<Currency, Interfaces.ICurrencyDal>, ICurrencyDal
    {

        public CurrencyDal(Interfaces.ICurrencyDal dalImpl) : base(dalImpl)
        {
        }

        public Currency Get(System.Int64? ID)
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
