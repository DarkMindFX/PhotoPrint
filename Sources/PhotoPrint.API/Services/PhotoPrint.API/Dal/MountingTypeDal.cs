


using PPT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace PPT.PhotoPrint.API.Dal
{
    [Export(typeof(IMountingTypeDal))]
    public class MountingTypeDal : DalBaseImpl<MountingType, Interfaces.IMountingTypeDal>, IMountingTypeDal
    {

        public MountingTypeDal(Interfaces.IMountingTypeDal dalImpl) : base(dalImpl)
        {
        }

        public MountingType Get(System.Int64? ID)
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
