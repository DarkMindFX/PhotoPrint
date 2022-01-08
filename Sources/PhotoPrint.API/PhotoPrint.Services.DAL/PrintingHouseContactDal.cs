


using PPT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace PPT.Services.Dal
{
    [Export(typeof(IPrintingHouseContactDal))]
    public class PrintingHouseContactDal : DalBaseImpl<PrintingHouseContact, Interfaces.IPrintingHouseContactDal>, IPrintingHouseContactDal
    {

        public PrintingHouseContactDal(Interfaces.IPrintingHouseContactDal dalImpl) : base(dalImpl)
        {
        }

        public PrintingHouseContact Get(System.Int64 PrintingHouseID,System.Int64 ContactID)
        {
            return _dalImpl.Get(            PrintingHouseID,            ContactID);
        }

        public bool Delete(System.Int64 PrintingHouseID,System.Int64 ContactID)
        {
            return _dalImpl.Delete(            PrintingHouseID,            ContactID);
        }


        public IList<PrintingHouseContact> GetByPrintingHouseID(System.Int64 PrintingHouseID)
        {
            return _dalImpl.GetByPrintingHouseID(PrintingHouseID);
        }
        public IList<PrintingHouseContact> GetByContactID(System.Int64 ContactID)
        {
            return _dalImpl.GetByContactID(ContactID);
        }
            }
}
