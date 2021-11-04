


using PPT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace PPT.PhotoPrint.API.Dal
{
    [Export(typeof(IPrintingHouseDal))]
    public class PrintingHouseDal : DalBaseImpl<PrintingHouse, Interfaces.IPrintingHouseDal>, IPrintingHouseDal
    {

        public PrintingHouseDal(Interfaces.IPrintingHouseDal dalImpl) : base(dalImpl)
        {
        }

        public PrintingHouse Get(System.Int64? ID)
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

        public IList<PrintingHouse> GetByCreatedByID(System.Int64 CreatedByID)
        {
            return _dalImpl.GetByCreatedByID(CreatedByID);
        }
        public IList<PrintingHouse> GetByModifiedByID(System.Int64? ModifiedByID)
        {
            return _dalImpl.GetByModifiedByID(ModifiedByID);
        }
            }
}
