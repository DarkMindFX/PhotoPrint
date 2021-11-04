


using PPT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace PPT.PhotoPrint.API.Dal
{
    [Export(typeof(IOrderStatusFlowDal))]
    public class OrderStatusFlowDal : DalBaseImpl<OrderStatusFlow, Interfaces.IOrderStatusFlowDal>, IOrderStatusFlowDal
    {

        public OrderStatusFlowDal(Interfaces.IOrderStatusFlowDal dalImpl) : base(dalImpl)
        {
        }

        public OrderStatusFlow Get(System.Int64 FromStatusID,System.Int64 ToStatusID)
        {
            return _dalImpl.Get(            FromStatusID,            ToStatusID);
        }

        public bool Delete(System.Int64 FromStatusID,System.Int64 ToStatusID)
        {
            return _dalImpl.Delete(            FromStatusID,            ToStatusID);
        }


        public IList<OrderStatusFlow> GetByFromStatusID(System.Int64 FromStatusID)
        {
            return _dalImpl.GetByFromStatusID(FromStatusID);
        }
        public IList<OrderStatusFlow> GetByToStatusID(System.Int64 ToStatusID)
        {
            return _dalImpl.GetByToStatusID(ToStatusID);
        }
            }
}
