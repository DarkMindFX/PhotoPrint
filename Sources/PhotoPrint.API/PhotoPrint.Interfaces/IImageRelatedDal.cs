

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPT.Interfaces.Entities;
using PPT.Interfaces;

namespace PPT.Interfaces
{
    public interface IImageRelatedDal : IDalBase<ImageRelated>
    {
        ImageRelated Get(System.Int64 ImageID,System.Int64 RelatedImageID);

        bool Delete(System.Int64 ImageID,System.Int64 RelatedImageID);

            }
}

