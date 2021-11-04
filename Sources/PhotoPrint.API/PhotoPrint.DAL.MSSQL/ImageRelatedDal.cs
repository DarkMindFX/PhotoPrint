


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
    class ImageRelatedDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IImageRelatedDal))]
    public class ImageRelatedDal: SQLDal, IImageRelatedDal
    {
        public IInitParams CreateInitParams()
        {
            return new ImageRelatedDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public ImageRelated Get(System.Int64 ImageID,System.Int64 RelatedImageID)
        {
            ImageRelated result = default(ImageRelated);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_ImageRelated_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@ImageID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ImageID);
            
                            AddParameter(   cmd, "@RelatedImageID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, RelatedImageID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = ImageRelatedFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64 ImageID,System.Int64 RelatedImageID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_ImageRelated_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@ImageID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ImageID);
            
                            AddParameter(   cmd, "@RelatedImageID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, RelatedImageID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }


                public IList<ImageRelated> GetByImageID(System.Int64 ImageID)
        {
            var entitiesOut = base.GetBy<ImageRelated, System.Int64>("p_ImageRelated_GetByImageID", ImageID, "@ImageID", SqlDbType.BigInt, 0, ImageRelatedFromRow);

            return entitiesOut;
        }
                public IList<ImageRelated> GetByRelatedImageID(System.Int64 RelatedImageID)
        {
            var entitiesOut = base.GetBy<ImageRelated, System.Int64>("p_ImageRelated_GetByRelatedImageID", RelatedImageID, "@RelatedImageID", SqlDbType.BigInt, 0, ImageRelatedFromRow);

            return entitiesOut;
        }
        
        public IList<ImageRelated> GetAll()
        {
            IList<ImageRelated> result = base.GetAll<ImageRelated>("p_ImageRelated_GetAll", ImageRelatedFromRow);

            return result;
        }

        public ImageRelated Insert(ImageRelated entity) 
        {
            ImageRelated entityOut = base.Upsert<ImageRelated>("p_ImageRelated_Insert", entity, AddUpsertParameters, ImageRelatedFromRow);

            return entityOut;
        }

        public ImageRelated Update(ImageRelated entity) 
        {
            ImageRelated entityOut = base.Upsert<ImageRelated>("p_ImageRelated_Update", entity, AddUpsertParameters, ImageRelatedFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, ImageRelated entity)
        {
                SqlParameter pImageID = new SqlParameter("@ImageID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ImageID", DataRowVersion.Current, (object)entity.ImageID != null ? (object)entity.ImageID : DBNull.Value);   cmd.Parameters.Add(pImageID); 
                SqlParameter pRelatedImageID = new SqlParameter("@RelatedImageID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "RelatedImageID", DataRowVersion.Current, (object)entity.RelatedImageID != null ? (object)entity.RelatedImageID : DBNull.Value);   cmd.Parameters.Add(pRelatedImageID); 
        
            return cmd;
        }

        protected ImageRelated ImageRelatedFromRow(DataRow row)
        {
            var entity = new ImageRelated();

                    entity.ImageID = !DBNull.Value.Equals(row["ImageID"]) ? (System.Int64)row["ImageID"] : default(System.Int64);
                    entity.RelatedImageID = !DBNull.Value.Equals(row["RelatedImageID"]) ? (System.Int64)row["RelatedImageID"] : default(System.Int64);
        
            return entity;
        }
        
    }
}
