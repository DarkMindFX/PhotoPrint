


using PPT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace PPT.PhotoPrint.API.Dal
{
    [Export(typeof(IImageThumbnailDal))]
    public class ImageThumbnailDal : DalBaseImpl<ImageThumbnail, Interfaces.IImageThumbnailDal>, IImageThumbnailDal
    {

        public ImageThumbnailDal(Interfaces.IImageThumbnailDal dalImpl) : base(dalImpl)
        {
        }

        public ImageThumbnail Get(System.Int64? ID)
        {
            return _dalImpl.Get(            ID);
        }

        public bool Delete(System.Int64? ID)
        {
            return _dalImpl.Delete(            ID);
        }


        public IList<ImageThumbnail> GetByImageID(System.Int64 ImageID)
        {
            return _dalImpl.GetByImageID(ImageID);
        }
            }
}
