


using PPT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace PPT.PhotoPrint.API.Dal
{
    [Export(typeof(IOrderTrackingDal))]
    public class OrderTrackingDal : DalBaseImpl<OrderTracking, Interfaces.IOrderTrackingDal>, IOrderTrackingDal
    {

        public OrderTrackingDal(Interfaces.IOrderTrackingDal dalImpl) : base(dalImpl)
        {
        }

        public OrderTracking Get(System.Int64? ID)
        {
            return _dalImpl.Get(            ID);
        }

        public bool Delete(System.Int64? ID)
        {
            return _dalImpl.Delete(            ID);
        }


        public IList<OrderTracking> GetByOrderID(System.Int64 OrderID)
        {
            return _dalImpl.GetByOrderID(OrderID);
        }
        public IList<OrderTracking> GetByOrderStatusID(System.Int64 OrderStatusID)
        {
            return _dalImpl.GetByOrderStatusID(OrderStatusID);
        }
        public IList<OrderTracking> GetBySetByID(System.Int64 SetByID)
        {
            return _dalImpl.GetBySetByID(SetByID);
        }
            }
}
