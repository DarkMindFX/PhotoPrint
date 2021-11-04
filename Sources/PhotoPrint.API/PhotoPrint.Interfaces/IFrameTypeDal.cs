


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPT.Interfaces.Entities;
using PPT.Interfaces;

namespace PPT.Interfaces
{
    public interface IFrameTypeDal : IDalBase<FrameType>
    {
        FrameType Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

        IList<FrameType> GetByCreatedByID(System.Int64 CreatedByID);
        IList<FrameType> GetByModifiedByID(System.Int64? ModifiedByID);
        
                bool Erase(System.Int64? ID);
            }
}

