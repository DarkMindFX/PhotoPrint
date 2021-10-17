

using PPT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PPT.PhotoPrint.API.Dal
{
    public interface IImageRelatedDal : IDalBase<ImageRelated>
    {
        ImageRelated Get(System.Int64 ImageID,System.Int64 RelatedImageID);

        bool Delete(System.Int64 ImageID,System.Int64 RelatedImageID);

        }
}
