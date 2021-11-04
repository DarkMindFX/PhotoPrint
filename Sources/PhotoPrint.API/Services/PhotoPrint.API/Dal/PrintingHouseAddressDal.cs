


using PPT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace PPT.PhotoPrint.API.Dal
{
    [Export(typeof(IPrintingHouseAddressDal))]
    public class PrintingHouseAddressDal : DalBaseImpl<PrintingHouseAddress, Interfaces.IPrintingHouseAddressDal>, IPrintingHouseAddressDal
    {

        public PrintingHouseAddressDal(Interfaces.IPrintingHouseAddressDal dalImpl) : base(dalImpl)
        {
        }

        public PrintingHouseAddress Get(System.Int64 PrintingHouseID,System.Int64 AddressID)
        {
            return _dalImpl.Get(            PrintingHouseID,            AddressID);
        }

        public bool Delete(System.Int64 PrintingHouseID,System.Int64 AddressID)
        {
            return _dalImpl.Delete(            PrintingHouseID,            AddressID);
        }


        public IList<PrintingHouseAddress> GetByPrintingHouseID(System.Int64 PrintingHouseID)
        {
            return _dalImpl.GetByPrintingHouseID(PrintingHouseID);
        }
        public IList<PrintingHouseAddress> GetByAddressID(System.Int64 AddressID)
        {
            return _dalImpl.GetByAddressID(AddressID);
        }
            }
}
