

using PPT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PPT.PhotoPrint.API.Dal
{
    public interface IPrintingHouseAddressDal : IDalBase<PrintingHouseAddress>
    {
        PrintingHouseAddress Get(System.Int64 PrintingHouseID,System.Int64 AddressID);

        bool Delete(System.Int64 PrintingHouseID,System.Int64 AddressID);

            IList<PrintingHouseAddress> GetByPrintingHouseID(System.Int64 PrintingHouseID);
            IList<PrintingHouseAddress> GetByAddressID(System.Int64 AddressID);
        }
}
