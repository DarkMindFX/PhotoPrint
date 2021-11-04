


using PPT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace PPT.PhotoPrint.API.Dal
{
    [Export(typeof(IOrderStatusDal))]
    public class OrderStatusDal : DalBaseImpl<OrderStatus, Interfaces.IOrderStatusDal>, IOrderStatusDal
    {

        public OrderStatusDal(Interfaces.IOrderStatusDal dalImpl) : base(dalImpl)
        {
        }

        public OrderStatus Get(System.Int64? ID)
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

            }
}
