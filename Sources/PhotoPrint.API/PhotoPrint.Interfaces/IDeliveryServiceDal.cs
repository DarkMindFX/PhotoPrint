

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoPrint.Interfaces.Entities;

namespace PhotoPrint.Interfaces
{
    public interface IDeliveryServiceDal : IDalBase<DeliveryService>
    {
        DeliveryService Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

        IList<DeliveryService> GetByCreatedByID(System.Int64 CreatedByID);
        IList<DeliveryService> GetByModifiedByID(System.Int64? ModifiedByID);
            }
}

