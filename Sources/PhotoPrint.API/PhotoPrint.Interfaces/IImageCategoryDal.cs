


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPT.Interfaces.Entities;
using PPT.Interfaces;

namespace PPT.Interfaces
{
    public interface IImageCategoryDal : IDalBase<ImageCategory>
    {
        ImageCategory Get(System.Int64 ImageID,System.Int64 CategoryID);

        bool Delete(System.Int64 ImageID,System.Int64 CategoryID);

        IList<ImageCategory> GetByImageID(System.Int64 ImageID);
        IList<ImageCategory> GetByCategoryID(System.Int64 CategoryID);
        
            }
}

