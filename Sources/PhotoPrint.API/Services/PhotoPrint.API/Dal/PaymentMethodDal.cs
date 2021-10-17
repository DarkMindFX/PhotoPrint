

using PPT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace PPT.PhotoPrint.API.Dal
{
    [Export(typeof(IPaymentMethodDal))]
    public class PaymentMethodDal : DalBaseImpl<PaymentMethod, Interfaces.IPaymentMethodDal>, IPaymentMethodDal
    {

        public PaymentMethodDal(Interfaces.IPaymentMethodDal dalImpl) : base(dalImpl)
        {
        }

        public PaymentMethod Get(System.Int64? ID)
        {
            return _dalImpl.Get(            ID);
        }

        public bool Delete(System.Int64? ID)
        {
            return _dalImpl.Delete(            ID);
        }

            }
}
