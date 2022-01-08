


using PPT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PPT.Services.Dal
{
    public interface ICategoryDal : IDalBase<Category>
    {
        Category Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

            IList<Category> GetByParentID(System.Int64? ParentID);
            IList<Category> GetByCreatedByID(System.Int64 CreatedByID);
            IList<Category> GetByModifiedByID(System.Int64? ModifiedByID);
    
            bool Erase(System.Int64? ID);
            }
}
