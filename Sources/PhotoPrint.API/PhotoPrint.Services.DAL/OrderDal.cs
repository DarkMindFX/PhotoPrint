


using PPT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace PPT.Services.Dal
{
    [Export(typeof(IOrderDal))]
    public class OrderDal : DalBaseImpl<Order, Interfaces.IOrderDal>, IOrderDal
    {

        public OrderDal(Interfaces.IOrderDal dalImpl) : base(dalImpl)
        {
        }

        public Order Get(System.Int64? ID)
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

        public IList<Order> GetByManagerID(System.Int64? ManagerID)
        {
            return _dalImpl.GetByManagerID(ManagerID);
        }
        public IList<Order> GetByUserID(System.Int64 UserID)
        {
            return _dalImpl.GetByUserID(UserID);
        }
        public IList<Order> GetByContactID(System.Int64 ContactID)
        {
            return _dalImpl.GetByContactID(ContactID);
        }
        public IList<Order> GetByDeliveryAddressID(System.Int64 DeliveryAddressID)
        {
            return _dalImpl.GetByDeliveryAddressID(DeliveryAddressID);
        }
        public IList<Order> GetByDeliveryServiceID(System.Int64 DeliveryServiceID)
        {
            return _dalImpl.GetByDeliveryServiceID(DeliveryServiceID);
        }
        public IList<Order> GetByCreatedByID(System.Int64 CreatedByID)
        {
            return _dalImpl.GetByCreatedByID(CreatedByID);
        }
        public IList<Order> GetByModifiedByID(System.Int64? ModifiedByID)
        {
            return _dalImpl.GetByModifiedByID(ModifiedByID);
        }
            }
}
