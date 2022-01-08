


using PPT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PPT.Services.Dal
{
    public interface IImageThumbnailDal : IDalBase<ImageThumbnail>
    {
        ImageThumbnail Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

            IList<ImageThumbnail> GetByImageID(System.Int64 ImageID);
    
        }
}
