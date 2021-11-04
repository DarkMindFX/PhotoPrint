


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPT.Interfaces.Entities;
using PPT.Interfaces;

namespace PPT.Interfaces
{
    public interface IOrderPaymentDetailsDal : IDalBase<OrderPaymentDetails>
    {
        OrderPaymentDetails Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

        IList<OrderPaymentDetails> GetByOrderID(System.Int64 OrderID);
        IList<OrderPaymentDetails> GetByPaymentMethodID(System.Int64 PaymentMethodID);
        IList<OrderPaymentDetails> GetByCreatedByID(System.Int64 CreatedByID);
        IList<OrderPaymentDetails> GetByModifiedByID(System.Int64? ModifiedByID);
        
                bool Erase(System.Int64? ID);
            }
}

