

using PPT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PPT.PhotoPrint.API.Dal
{
    public interface IOrderPaymentDetailsDal : IDalBase<OrderPaymentDetails>
    {
        OrderPaymentDetails Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

            IList<OrderPaymentDetails> GetByOrderID(System.Int64 OrderID);
            IList<OrderPaymentDetails> GetByPaymentMethodID(System.Int64 PaymentMethodID);
            IList<OrderPaymentDetails> GetByCreatedByID(System.Int64 CreatedByID);
            IList<OrderPaymentDetails> GetByModifiedByID(System.Int64? ModifiedByID);
        }
}
