


using PPT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace PPT.Services.Dal
{
    [Export(typeof(IUserInteriorThumbnailDal))]
    public class UserInteriorThumbnailDal : DalBaseImpl<UserInteriorThumbnail, Interfaces.IUserInteriorThumbnailDal>, IUserInteriorThumbnailDal
    {

        public UserInteriorThumbnailDal(Interfaces.IUserInteriorThumbnailDal dalImpl) : base(dalImpl)
        {
        }

        public UserInteriorThumbnail Get(System.Int64? ID)
        {
            return _dalImpl.Get(            ID);
        }

        public bool Delete(System.Int64? ID)
        {
            return _dalImpl.Delete(            ID);
        }


        public IList<UserInteriorThumbnail> GetByUserID(System.Int64 UserID)
        {
            return _dalImpl.GetByUserID(UserID);
        }
            }
}
