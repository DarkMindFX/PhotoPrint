


using PPT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace PPT.Services.Dal
{
    [Export(typeof(IUnitDal))]
    public class UnitDal : DalBaseImpl<Unit, Interfaces.IUnitDal>, IUnitDal
    {

        public UnitDal(Interfaces.IUnitDal dalImpl) : base(dalImpl)
        {
        }

        public Unit Get(System.Int64? ID)
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
