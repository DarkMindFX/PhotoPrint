


using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.SqlClient;
using PPT.Common;
using PPT.DAL.MSSQL;
using PPT.Interfaces;
using PPT.Interfaces.Entities;

namespace PPT.DAL.MSSQL 
{
    class ImageCategoryDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IImageCategoryDal))]
    public class ImageCategoryDal: SQLDal, IImageCategoryDal
    {
        public IInitParams CreateInitParams()
        {
            return new ImageCategoryDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public ImageCategory Get(System.Int64 ImageID,System.Int64 CategoryID)
        {
            ImageCategory result = default(ImageCategory);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_ImageCategory_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@ImageID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ImageID);
            
                            AddParameter(   cmd, "@CategoryID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, CategoryID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = ImageCategoryFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64 ImageID,System.Int64 CategoryID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_ImageCategory_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@ImageID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ImageID);
            
                            AddParameter(   cmd, "@CategoryID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, CategoryID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }


                public IList<ImageCategory> GetByImageID(System.Int64 ImageID)
        {
            var entitiesOut = base.GetBy<ImageCategory, System.Int64>("p_ImageCategory_GetByImageID", ImageID, "@ImageID", SqlDbType.BigInt, 0, ImageCategoryFromRow);

            return entitiesOut;
        }
                public IList<ImageCategory> GetByCategoryID(System.Int64 CategoryID)
        {
            var entitiesOut = base.GetBy<ImageCategory, System.Int64>("p_ImageCategory_GetByCategoryID", CategoryID, "@CategoryID", SqlDbType.BigInt, 0, ImageCategoryFromRow);

            return entitiesOut;
        }
        
        public IList<ImageCategory> GetAll()
        {
            IList<ImageCategory> result = base.GetAll<ImageCategory>("p_ImageCategory_GetAll", ImageCategoryFromRow);

            return result;
        }

        public ImageCategory Insert(ImageCategory entity) 
        {
            ImageCategory entityOut = base.Upsert<ImageCategory>("p_ImageCategory_Insert", entity, AddUpsertParameters, ImageCategoryFromRow);

            return entityOut;
        }

        public ImageCategory Update(ImageCategory entity) 
        {
            ImageCategory entityOut = base.Upsert<ImageCategory>("p_ImageCategory_Update", entity, AddUpsertParameters, ImageCategoryFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, ImageCategory entity)
        {
                SqlParameter pImageID = new SqlParameter("@ImageID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ImageID", DataRowVersion.Current, (object)entity.ImageID != null ? (object)entity.ImageID : DBNull.Value);   cmd.Parameters.Add(pImageID); 
                SqlParameter pCategoryID = new SqlParameter("@CategoryID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "CategoryID", DataRowVersion.Current, (object)entity.CategoryID != null ? (object)entity.CategoryID : DBNull.Value);   cmd.Parameters.Add(pCategoryID); 
        
            return cmd;
        }

        protected ImageCategory ImageCategoryFromRow(DataRow row)
        {
            var entity = new ImageCategory();

                    entity.ImageID = !DBNull.Value.Equals(row["ImageID"]) ? (System.Int64)row["ImageID"] : default(System.Int64);
                    entity.CategoryID = !DBNull.Value.Equals(row["CategoryID"]) ? (System.Int64)row["CategoryID"] : default(System.Int64);
        
            return entity;
        }
        
    }
}
