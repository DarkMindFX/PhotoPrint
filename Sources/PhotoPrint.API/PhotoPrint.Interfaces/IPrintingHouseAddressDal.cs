

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoPrint.Interfaces.Entities;

namespace PhotoPrint.Interfaces
{
    public interface IPrintingHouseAddressDal : IDalBase<PrintingHouseAddress>
    {
        PrintingHouseAddress Get(System.Int64 PrintingHouseID,System.Int64 AddressID);

        bool Delete(System.Int64 PrintingHouseID,System.Int64 AddressID);

        IList<PrintingHouseAddress> GetByPrintingHouseID(System.Int64 PrintingHouseID);
        IList<PrintingHouseAddress> GetByAddressID(System.Int64 AddressID);
            }
}

