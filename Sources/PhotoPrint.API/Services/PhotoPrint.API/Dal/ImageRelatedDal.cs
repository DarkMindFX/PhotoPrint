


using PPT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace PPT.PhotoPrint.API.Dal
{
    [Export(typeof(IImageRelatedDal))]
    public class ImageRelatedDal : DalBaseImpl<ImageRelated, Interfaces.IImageRelatedDal>, IImageRelatedDal
    {

        public ImageRelatedDal(Interfaces.IImageRelatedDal dalImpl) : base(dalImpl)
        {
        }

        public ImageRelated Get(System.Int64 ImageID,System.Int64 RelatedImageID)
        {
            return _dalImpl.Get(            ImageID,            RelatedImageID);
        }

        public bool Delete(System.Int64 ImageID,System.Int64 RelatedImageID)
        {
            return _dalImpl.Delete(            ImageID,            RelatedImageID);
        }


        public IList<ImageRelated> GetByImageID(System.Int64 ImageID)
        {
            return _dalImpl.GetByImageID(ImageID);
        }
        public IList<ImageRelated> GetByRelatedImageID(System.Int64 RelatedImageID)
        {
            return _dalImpl.GetByRelatedImageID(RelatedImageID);
        }
            }
}
