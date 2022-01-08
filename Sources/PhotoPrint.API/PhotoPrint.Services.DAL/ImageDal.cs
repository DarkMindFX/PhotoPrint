


using PPT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace PPT.Services.Dal
{
    [Export(typeof(IImageDal))]
    public class ImageDal : DalBaseImpl<Image, Interfaces.IImageDal>, IImageDal
    {

        public ImageDal(Interfaces.IImageDal dalImpl) : base(dalImpl)
        {
        }

        public Image Get(System.Int64? ID)
        {
            return _dalImpl.Get(            ID);
        }

        public bool Delete(System.Int64? ID)
        {
            return _dalImpl.Delete(            ID);
        }

        public bool Erase(System.Int64? ID)
        {
            return _dalImpl.Delete(            ID);
        }

        public IList<Image> GetByPriceCurrencyID(System.Int64? PriceCurrencyID)
        {
            return _dalImpl.GetByPriceCurrencyID(PriceCurrencyID);
        }
        public IList<Image> GetByCreatedByID(System.Int64 CreatedByID)
        {
            return _dalImpl.GetByCreatedByID(CreatedByID);
        }
        public IList<Image> GetByModifiedByID(System.Int64? ModifiedByID)
        {
            return _dalImpl.GetByModifiedByID(ModifiedByID);
        }
            }
}
