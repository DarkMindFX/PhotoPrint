

using PPT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PPT.PhotoPrint.API.Dal
{
    public interface IPrintingHouseDal : IDalBase<PrintingHouse>
    {
        PrintingHouse Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

            IList<PrintingHouse> GetByCreatedByID(System.Int64 CreatedByID);
            IList<PrintingHouse> GetByModifiedByID(System.Int64? ModifiedByID);
        }
}
