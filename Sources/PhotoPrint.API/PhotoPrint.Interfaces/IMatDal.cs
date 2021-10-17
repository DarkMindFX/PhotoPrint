

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPT.Interfaces.Entities;
using PPT.Interfaces;

namespace PPT.Interfaces
{
    public interface IMatDal : IDalBase<Mat>
    {
        Mat Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

        IList<Mat> GetByCreatedByID(System.Int64 CreatedByID);
        IList<Mat> GetByModifiedByID(System.Int64? ModifiedByID);
            }
}

