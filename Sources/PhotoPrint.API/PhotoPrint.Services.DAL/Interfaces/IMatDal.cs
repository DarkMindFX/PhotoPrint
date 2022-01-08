


using PPT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PPT.Services.Dal
{
    public interface IMatDal : IDalBase<Mat>
    {
        Mat Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

            IList<Mat> GetByCreatedByID(System.Int64 CreatedByID);
            IList<Mat> GetByModifiedByID(System.Int64? ModifiedByID);
    
            bool Erase(System.Int64? ID);
            }
}
