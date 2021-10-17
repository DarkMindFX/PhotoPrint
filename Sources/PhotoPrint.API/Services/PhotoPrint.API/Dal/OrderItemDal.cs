

using PPT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace PPT.PhotoPrint.API.Dal
{
    [Export(typeof(IOrderItemDal))]
    public class OrderItemDal : DalBaseImpl<OrderItem, Interfaces.IOrderItemDal>, IOrderItemDal
    {

        public OrderItemDal(Interfaces.IOrderItemDal dalImpl) : base(dalImpl)
        {
        }

        public OrderItem Get(System.Int64? ID)
        {
            return _dalImpl.Get(            ID);
        }

        public bool Delete(System.Int64? ID)
        {
            return _dalImpl.Delete(            ID);
        }

        public IList<OrderItem> GetByOrderID(System.Int64 OrderID)
        {
            return _dalImpl.GetByOrderID(OrderID);
        }
        public IList<OrderItem> GetByImageID(System.Int64 ImageID)
        {
            return _dalImpl.GetByImageID(ImageID);
        }
        public IList<OrderItem> GetBySizeID(System.Int64? SizeID)
        {
            return _dalImpl.GetBySizeID(SizeID);
        }
        public IList<OrderItem> GetByFrameTypeID(System.Int64 FrameTypeID)
        {
            return _dalImpl.GetByFrameTypeID(FrameTypeID);
        }
        public IList<OrderItem> GetByFrameSizeID(System.Int64? FrameSizeID)
        {
            return _dalImpl.GetByFrameSizeID(FrameSizeID);
        }
        public IList<OrderItem> GetByMatID(System.Int64 MatID)
        {
            return _dalImpl.GetByMatID(MatID);
        }
        public IList<OrderItem> GetByMaterialTypeID(System.Int64 MaterialTypeID)
        {
            return _dalImpl.GetByMaterialTypeID(MaterialTypeID);
        }
        public IList<OrderItem> GetByMountingTypeID(System.Int64 MountingTypeID)
        {
            return _dalImpl.GetByMountingTypeID(MountingTypeID);
        }
        public IList<OrderItem> GetByPriceCurrencyID(System.Int64 PriceCurrencyID)
        {
            return _dalImpl.GetByPriceCurrencyID(PriceCurrencyID);
        }
        public IList<OrderItem> GetByPrintingHouseID(System.Int64? PrintingHouseID)
        {
            return _dalImpl.GetByPrintingHouseID(PrintingHouseID);
        }
        public IList<OrderItem> GetByCreatedByID(System.Int64 CreatedByID)
        {
            return _dalImpl.GetByCreatedByID(CreatedByID);
        }
        public IList<OrderItem> GetByModifiedByID(System.Int64? ModifiedByID)
        {
            return _dalImpl.GetByModifiedByID(ModifiedByID);
        }
            }
}
