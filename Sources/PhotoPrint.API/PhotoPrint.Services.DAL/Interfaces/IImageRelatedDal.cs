


using PPT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PPT.Services.Dal
{
    public interface IImageRelatedDal : IDalBase<ImageRelated>
    {
        ImageRelated Get(System.Int64 ImageID,System.Int64 RelatedImageID);

        bool Delete(System.Int64 ImageID,System.Int64 RelatedImageID);

            IList<ImageRelated> GetByImageID(System.Int64 ImageID);
            IList<ImageRelated> GetByRelatedImageID(System.Int64 RelatedImageID);
    
        }
}
