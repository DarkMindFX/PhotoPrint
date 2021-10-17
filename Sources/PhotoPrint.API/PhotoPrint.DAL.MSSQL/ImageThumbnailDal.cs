

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.SqlClient;
using PhotoPrint.Common;
using PhotoPrint.DAL.MSSQL;
using PhotoPrint.Interfaces;
using PhotoPrint.Interfaces.Entities;

namespace PhotoPrint.DAL.MSSQL 
{
    class ImageThumbnailDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IImageThumbnailDal))]
    public class ImageThumbnailDal: SQLDal, IImageThumbnailDal
    {
        public IInitParams CreateInitParams()
        {
            return new ImageThumbnailDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public ImageThumbnail Get(System.Int64? ID)
        {
            ImageThumbnail result = default(ImageThumbnail);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_ImageThumbnail_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = ImageThumbnailFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_ImageThumbnail_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }

                public IList<ImageThumbnail> GetByImageID(System.Int64 ImageID)
        {
            var entitiesOut = base.GetBy<ImageThumbnail, System.Int64>("p_ImageThumbnail_GetByImageID", ImageID, "@ImageID", SqlDbType.BigInt, 0, ImageThumbnailFromRow);

            return entitiesOut;
        }
        
        public IList<ImageThumbnail> GetAll()
        {
            IList<ImageThumbnail> result = base.GetAll<ImageThumbnail>("p_ImageThumbnail_GetAll", ImageThumbnailFromRow);

            return result;
        }

        public ImageThumbnail Insert(ImageThumbnail entity) 
        {
            ImageThumbnail entityOut = base.Upsert<ImageThumbnail>("p_ImageThumbnail_Insert", entity, AddUpsertParameters, ImageThumbnailFromRow);

            return entityOut;
        }

        public ImageThumbnail Update(ImageThumbnail entity) 
        {
            ImageThumbnail entityOut = base.Upsert<ImageThumbnail>("p_ImageThumbnail_Update", entity, AddUpsertParameters, ImageThumbnailFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, ImageThumbnail entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pUrl = new SqlParameter("@Url", System.Data.SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "Url", DataRowVersion.Current, (object)entity.Url != null ? (object)entity.Url : DBNull.Value);   cmd.Parameters.Add(pUrl); 
                SqlParameter pOrder = new SqlParameter("@Order", System.Data.SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "Order", DataRowVersion.Current, (object)entity.Order != null ? (object)entity.Order : DBNull.Value);   cmd.Parameters.Add(pOrder); 
                SqlParameter pImageID = new SqlParameter("@ImageID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ImageID", DataRowVersion.Current, (object)entity.ImageID != null ? (object)entity.ImageID : DBNull.Value);   cmd.Parameters.Add(pImageID); 
        
            return cmd;
        }

        protected ImageThumbnail ImageThumbnailFromRow(DataRow row)
        {
            var entity = new ImageThumbnail();

                    entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
                    entity.Url = !DBNull.Value.Equals(row["Url"]) ? (System.String)row["Url"] : default(System.String);
                    entity.Order = !DBNull.Value.Equals(row["Order"]) ? (System.Int32?)row["Order"] : default(System.Int32?);
                    entity.ImageID = !DBNull.Value.Equals(row["ImageID"]) ? (System.Int64)row["ImageID"] : default(System.Int64);
        
            return entity;
        }
        
    }
}
