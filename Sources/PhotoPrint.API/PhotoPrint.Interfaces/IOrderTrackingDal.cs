

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoPrint.Interfaces.Entities;

namespace PhotoPrint.Interfaces
{
    public interface IOrderTrackingDal : IDalBase<OrderTracking>
    {
        OrderTracking Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

        IList<OrderTracking> GetByOrderID(System.Int64 OrderID);
        IList<OrderTracking> GetByOrderStatusID(System.Int64 OrderStatusID);
        IList<OrderTracking> GetBySetByID(System.Int64 SetByID);
            }
}

