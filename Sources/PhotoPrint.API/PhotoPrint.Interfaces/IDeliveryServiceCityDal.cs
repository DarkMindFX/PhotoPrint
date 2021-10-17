

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoPrint.Interfaces.Entities;

namespace PhotoPrint.Interfaces
{
    public interface IDeliveryServiceCityDal : IDalBase<DeliveryServiceCity>
    {
        DeliveryServiceCity Get(System.Int64 DeliveryServiceID,System.Int64 CityID);

        bool Delete(System.Int64 DeliveryServiceID,System.Int64 CityID);

        IList<DeliveryServiceCity> GetByDeliveryServiceID(System.Int64 DeliveryServiceID);
        IList<DeliveryServiceCity> GetByCityID(System.Int64 CityID);
            }
}

