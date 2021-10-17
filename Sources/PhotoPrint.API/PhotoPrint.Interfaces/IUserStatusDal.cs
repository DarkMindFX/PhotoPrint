

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoPrint.Interfaces.Entities;

namespace PhotoPrint.Interfaces
{
    public interface IUserStatusDal : IDalBase<UserStatus>
    {
        UserStatus Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

            }
}

