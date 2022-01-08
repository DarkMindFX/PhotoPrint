


using PPT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace PPT.Services.Dal
{
    [Export(typeof(ICategoryDal))]
    public class CategoryDal : DalBaseImpl<Category, Interfaces.ICategoryDal>, ICategoryDal
    {

        public CategoryDal(Interfaces.ICategoryDal dalImpl) : base(dalImpl)
        {
        }

        public Category Get(System.Int64? ID)
        {
            return _dalImpl.Get(            ID);
        }

        public bool Delete(System.Int64? ID)
        {
            return _dalImpl.Delete(            ID);
        }

        public bool Erase(System.Int64? ID)
        {
            return _dalImpl.Delete(            ID);
        }

        public IList<Category> GetByParentID(System.Int64? ParentID)
        {
            return _dalImpl.GetByParentID(ParentID);
        }
        public IList<Category> GetByCreatedByID(System.Int64 CreatedByID)
        {
            return _dalImpl.GetByCreatedByID(CreatedByID);
        }
        public IList<Category> GetByModifiedByID(System.Int64? ModifiedByID)
        {
            return _dalImpl.GetByModifiedByID(ModifiedByID);
        }
            }
}
