


using PPT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace PPT.PhotoPrint.API.Dal
{
    [Export(typeof(IOrderPaymentDetailsDal))]
    public class OrderPaymentDetailsDal : DalBaseImpl<OrderPaymentDetails, Interfaces.IOrderPaymentDetailsDal>, IOrderPaymentDetailsDal
    {

        public OrderPaymentDetailsDal(Interfaces.IOrderPaymentDetailsDal dalImpl) : base(dalImpl)
        {
        }

        public OrderPaymentDetails Get(System.Int64? ID)
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

        public IList<OrderPaymentDetails> GetByOrderID(System.Int64 OrderID)
        {
            return _dalImpl.GetByOrderID(OrderID);
        }
        public IList<OrderPaymentDetails> GetByPaymentMethodID(System.Int64 PaymentMethodID)
        {
            return _dalImpl.GetByPaymentMethodID(PaymentMethodID);
        }
        public IList<OrderPaymentDetails> GetByCreatedByID(System.Int64 CreatedByID)
        {
            return _dalImpl.GetByCreatedByID(CreatedByID);
        }
        public IList<OrderPaymentDetails> GetByModifiedByID(System.Int64? ModifiedByID)
        {
            return _dalImpl.GetByModifiedByID(ModifiedByID);
        }
            }
}
