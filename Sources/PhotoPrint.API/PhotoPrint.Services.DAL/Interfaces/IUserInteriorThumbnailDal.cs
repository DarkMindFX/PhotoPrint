


using PPT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PPT.Services.Dal
{
    public interface IUserInteriorThumbnailDal : IDalBase<UserInteriorThumbnail>
    {
        UserInteriorThumbnail Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

            IList<UserInteriorThumbnail> GetByUserID(System.Int64 UserID);
    
        }
}
