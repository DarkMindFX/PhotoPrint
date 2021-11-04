


using PPT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PPT.PhotoPrint.API.Dal
{
    public interface IDeliveryServiceDal : IDalBase<DeliveryService>
    {
        DeliveryService Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

            IList<DeliveryService> GetByCreatedByID(System.Int64 CreatedByID);
            IList<DeliveryService> GetByModifiedByID(System.Int64? ModifiedByID);
    
            bool Erase(System.Int64? ID);
            }
}
