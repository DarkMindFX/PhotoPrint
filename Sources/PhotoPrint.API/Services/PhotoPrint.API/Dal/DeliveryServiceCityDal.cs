


using PPT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace PPT.PhotoPrint.API.Dal
{
    [Export(typeof(IDeliveryServiceCityDal))]
    public class DeliveryServiceCityDal : DalBaseImpl<DeliveryServiceCity, Interfaces.IDeliveryServiceCityDal>, IDeliveryServiceCityDal
    {

        public DeliveryServiceCityDal(Interfaces.IDeliveryServiceCityDal dalImpl) : base(dalImpl)
        {
        }

        public DeliveryServiceCity Get(System.Int64 DeliveryServiceID,System.Int64 CityID)
        {
            return _dalImpl.Get(            DeliveryServiceID,            CityID);
        }

        public bool Delete(System.Int64 DeliveryServiceID,System.Int64 CityID)
        {
            return _dalImpl.Delete(            DeliveryServiceID,            CityID);
        }


        public IList<DeliveryServiceCity> GetByDeliveryServiceID(System.Int64 DeliveryServiceID)
        {
            return _dalImpl.GetByDeliveryServiceID(DeliveryServiceID);
        }
        public IList<DeliveryServiceCity> GetByCityID(System.Int64 CityID)
        {
            return _dalImpl.GetByCityID(CityID);
        }
            }
}
