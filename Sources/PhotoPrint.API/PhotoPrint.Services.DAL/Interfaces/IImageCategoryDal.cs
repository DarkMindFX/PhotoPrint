


using PPT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PPT.Services.Dal
{
    public interface IImageCategoryDal : IDalBase<ImageCategory>
    {
        ImageCategory Get(System.Int64 ImageID,System.Int64 CategoryID);

        bool Delete(System.Int64 ImageID,System.Int64 CategoryID);

            IList<ImageCategory> GetByImageID(System.Int64 ImageID);
            IList<ImageCategory> GetByCategoryID(System.Int64 CategoryID);
    
        }
}
