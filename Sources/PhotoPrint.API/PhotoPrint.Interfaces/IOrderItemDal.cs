


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPT.Interfaces.Entities;
using PPT.Interfaces;

namespace PPT.Interfaces
{
    public interface IOrderItemDal : IDalBase<OrderItem>
    {
        OrderItem Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

        IList<OrderItem> GetByOrderID(System.Int64 OrderID);
        IList<OrderItem> GetByImageID(System.Int64 ImageID);
        IList<OrderItem> GetBySizeID(System.Int64? SizeID);
        IList<OrderItem> GetByFrameTypeID(System.Int64 FrameTypeID);
        IList<OrderItem> GetByFrameSizeID(System.Int64? FrameSizeID);
        IList<OrderItem> GetByMatID(System.Int64 MatID);
        IList<OrderItem> GetByMaterialTypeID(System.Int64 MaterialTypeID);
        IList<OrderItem> GetByMountingTypeID(System.Int64 MountingTypeID);
        IList<OrderItem> GetByPriceCurrencyID(System.Int64 PriceCurrencyID);
        IList<OrderItem> GetByPrintingHouseID(System.Int64? PrintingHouseID);
        IList<OrderItem> GetByCreatedByID(System.Int64 CreatedByID);
        IList<OrderItem> GetByModifiedByID(System.Int64? ModifiedByID);
        
                bool Erase(System.Int64? ID);
            }
}

