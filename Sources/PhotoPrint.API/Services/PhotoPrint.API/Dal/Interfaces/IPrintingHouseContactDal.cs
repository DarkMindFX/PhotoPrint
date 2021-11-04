


using PPT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PPT.PhotoPrint.API.Dal
{
    public interface IPrintingHouseContactDal : IDalBase<PrintingHouseContact>
    {
        PrintingHouseContact Get(System.Int64 PrintingHouseID,System.Int64 ContactID);

        bool Delete(System.Int64 PrintingHouseID,System.Int64 ContactID);

            IList<PrintingHouseContact> GetByPrintingHouseID(System.Int64 PrintingHouseID);
            IList<PrintingHouseContact> GetByContactID(System.Int64 ContactID);
    
        }
}
