

using PPT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace PPT.PhotoPrint.API.Dal
{
    [Export(typeof(IUserTypeDal))]
    public class UserTypeDal : DalBaseImpl<UserType, Interfaces.IUserTypeDal>, IUserTypeDal
    {

        public UserTypeDal(Interfaces.IUserTypeDal dalImpl) : base(dalImpl)
        {
        }

        public UserType Get(System.Int64? ID)
        {
            return _dalImpl.Get(            ID);
        }

        public bool Delete(System.Int64? ID)
        {
            return _dalImpl.Delete(            ID);
        }

            }
}
