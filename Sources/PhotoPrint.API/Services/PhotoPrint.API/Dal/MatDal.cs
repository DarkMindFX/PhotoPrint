


using PPT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace PPT.PhotoPrint.API.Dal
{
    [Export(typeof(IMatDal))]
    public class MatDal : DalBaseImpl<Mat, Interfaces.IMatDal>, IMatDal
    {

        public MatDal(Interfaces.IMatDal dalImpl) : base(dalImpl)
        {
        }

        public Mat Get(System.Int64? ID)
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

        public IList<Mat> GetByCreatedByID(System.Int64 CreatedByID)
        {
            return _dalImpl.GetByCreatedByID(CreatedByID);
        }
        public IList<Mat> GetByModifiedByID(System.Int64? ModifiedByID)
        {
            return _dalImpl.GetByModifiedByID(ModifiedByID);
        }
            }
}
