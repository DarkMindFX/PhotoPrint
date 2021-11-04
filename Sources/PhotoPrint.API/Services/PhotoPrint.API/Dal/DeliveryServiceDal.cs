


using PPT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace PPT.PhotoPrint.API.Dal
{
    [Export(typeof(IDeliveryServiceDal))]
    public class DeliveryServiceDal : DalBaseImpl<DeliveryService, Interfaces.IDeliveryServiceDal>, IDeliveryServiceDal
    {

        public DeliveryServiceDal(Interfaces.IDeliveryServiceDal dalImpl) : base(dalImpl)
        {
        }

        public DeliveryService Get(System.Int64? ID)
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

        public IList<DeliveryService> GetByCreatedByID(System.Int64 CreatedByID)
        {
            return _dalImpl.GetByCreatedByID(CreatedByID);
        }
        public IList<DeliveryService> GetByModifiedByID(System.Int64? ModifiedByID)
        {
            return _dalImpl.GetByModifiedByID(ModifiedByID);
        }
            }
}
