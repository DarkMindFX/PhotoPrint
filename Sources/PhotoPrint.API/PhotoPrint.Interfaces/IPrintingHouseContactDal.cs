

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoPrint.Interfaces.Entities;

namespace PhotoPrint.Interfaces
{
    public interface IPrintingHouseContactDal : IDalBase<PrintingHouseContact>
    {
        PrintingHouseContact Get(System.Int64 PrintingHouseID,System.Int64 ContactID);

        bool Delete(System.Int64 PrintingHouseID,System.Int64 ContactID);

        IList<PrintingHouseContact> GetByPrintingHouseID(System.Int64 PrintingHouseID);
        IList<PrintingHouseContact> GetByContactID(System.Int64 ContactID);
            }
}

