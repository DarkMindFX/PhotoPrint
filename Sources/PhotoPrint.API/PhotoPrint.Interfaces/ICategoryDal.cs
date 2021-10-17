

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoPrint.Interfaces.Entities;

namespace PhotoPrint.Interfaces
{
    public interface ICategoryDal : IDalBase<Category>
    {
        Category Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

        IList<Category> GetByParentID(System.Int64? ParentID);
        IList<Category> GetByCreatedByID(System.Int64 CreatedByID);
        IList<Category> GetByModifiedByID(System.Int64? ModifiedByID);
            }
}

