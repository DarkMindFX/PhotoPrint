

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoPrint.Interfaces.Entities;

namespace PhotoPrint.Interfaces
{
    public interface IImageDal : IDalBase<Image>
    {
        Image Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

        IList<Image> GetByPriceCurrencyID(System.Int64? PriceCurrencyID);
        IList<Image> GetByCreatedByID(System.Int64 CreatedByID);
        IList<Image> GetByModifiedByID(System.Int64? ModifiedByID);
            }
}

