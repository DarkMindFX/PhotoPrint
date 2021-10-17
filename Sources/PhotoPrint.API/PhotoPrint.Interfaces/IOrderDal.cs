

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoPrint.Interfaces.Entities;

namespace PhotoPrint.Interfaces
{
    public interface IOrderDal : IDalBase<Order>
    {
        Order Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

        IList<Order> GetByManagerID(System.Int64? ManagerID);
        IList<Order> GetByUserID(System.Int64 UserID);
        IList<Order> GetByContactID(System.Int64 ContactID);
        IList<Order> GetByDeliveryAddressID(System.Int64 DeliveryAddressID);
        IList<Order> GetByDeliveryServiceID(System.Int64 DeliveryServiceID);
        IList<Order> GetByStatusID(System.Int64 StatusID);
        IList<Order> GetByCreatedByID(System.Int64 CreatedByID);
        IList<Order> GetByModifiedByID(System.Int64? ModifiedByID);
            }
}

