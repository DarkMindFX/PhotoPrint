

using PPT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace PPT.PhotoPrint.API.Dal
{
    [Export(typeof(IImageCategoryDal))]
    public class ImageCategoryDal : DalBaseImpl<ImageCategory, Interfaces.IImageCategoryDal>, IImageCategoryDal
    {

        public ImageCategoryDal(Interfaces.IImageCategoryDal dalImpl) : base(dalImpl)
        {
        }

        public ImageCategory Get(System.Int64 ImageID,System.Int64 CategoryID)
        {
            return _dalImpl.Get(            ImageID,            CategoryID);
        }

        public bool Delete(System.Int64 ImageID,System.Int64 CategoryID)
        {
            return _dalImpl.Delete(            ImageID,            CategoryID);
        }

        public IList<ImageCategory> GetByImageID(System.Int64 ImageID)
        {
            return _dalImpl.GetByImageID(ImageID);
        }
        public IList<ImageCategory> GetByCategoryID(System.Int64 CategoryID)
        {
            return _dalImpl.GetByCategoryID(CategoryID);
        }
            }
}
