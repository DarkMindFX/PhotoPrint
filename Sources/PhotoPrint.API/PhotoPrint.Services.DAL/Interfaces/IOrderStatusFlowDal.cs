


using PPT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PPT.Services.Dal
{
    public interface IOrderStatusFlowDal : IDalBase<OrderStatusFlow>
    {
        OrderStatusFlow Get(System.Int64 FromStatusID,System.Int64 ToStatusID);

        bool Delete(System.Int64 FromStatusID,System.Int64 ToStatusID);

            IList<OrderStatusFlow> GetByFromStatusID(System.Int64 FromStatusID);
            IList<OrderStatusFlow> GetByToStatusID(System.Int64 ToStatusID);
    
        }
}
