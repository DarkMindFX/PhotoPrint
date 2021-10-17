

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
    class FrameTypeDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IFrameTypeDal))]
    public class FrameTypeDal: SQLDal, IFrameTypeDal
    {
        public IInitParams CreateInitParams()
        {
            return new FrameTypeDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public FrameType Get(System.Int64? ID)
        {
            FrameType result = default(FrameType);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_FrameType_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = FrameTypeFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_FrameType_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }

                public IList<FrameType> GetByCreatedByID(System.Int64 CreatedByID)
        {
            var entitiesOut = base.GetBy<FrameType, System.Int64>("p_FrameType_GetByCreatedByID", CreatedByID, "@CreatedByID", SqlDbType.BigInt, 0, FrameTypeFromRow);

            return entitiesOut;
        }
                public IList<FrameType> GetByModifiedByID(System.Int64? ModifiedByID)
        {
            var entitiesOut = base.GetBy<FrameType, System.Int64?>("p_FrameType_GetByModifiedByID", ModifiedByID, "@ModifiedByID", SqlDbType.BigInt, 0, FrameTypeFromRow);

            return entitiesOut;
        }
        
        public IList<FrameType> GetAll()
        {
            IList<FrameType> result = base.GetAll<FrameType>("p_FrameType_GetAll", FrameTypeFromRow);

            return result;
        }

        public FrameType Insert(FrameType entity) 
        {
            FrameType entityOut = base.Upsert<FrameType>("p_FrameType_Insert", entity, AddUpsertParameters, FrameTypeFromRow);

            return entityOut;
        }

        public FrameType Update(FrameType entity) 
        {
            FrameType entityOut = base.Upsert<FrameType>("p_FrameType_Update", entity, AddUpsertParameters, FrameTypeFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, FrameType entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pFrameTypeName = new SqlParameter("@FrameTypeName", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "FrameTypeName", DataRowVersion.Current, (object)entity.FrameTypeName != null ? (object)entity.FrameTypeName : DBNull.Value);   cmd.Parameters.Add(pFrameTypeName); 
                SqlParameter pDescription = new SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Current, (object)entity.Description != null ? (object)entity.Description : DBNull.Value);   cmd.Parameters.Add(pDescription); 
                SqlParameter pThumbnailUrl = new SqlParameter("@ThumbnailUrl", System.Data.SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "ThumbnailUrl", DataRowVersion.Current, (object)entity.ThumbnailUrl != null ? (object)entity.ThumbnailUrl : DBNull.Value);   cmd.Parameters.Add(pThumbnailUrl); 
                SqlParameter pIsDeleted = new SqlParameter("@IsDeleted", System.Data.SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsDeleted", DataRowVersion.Current, (object)entity.IsDeleted != null ? (object)entity.IsDeleted : DBNull.Value);   cmd.Parameters.Add(pIsDeleted); 
                SqlParameter pCreatedDate = new SqlParameter("@CreatedDate", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "CreatedDate", DataRowVersion.Current, (object)entity.CreatedDate != null ? (object)entity.CreatedDate : DBNull.Value);   cmd.Parameters.Add(pCreatedDate); 
                SqlParameter pCreatedByID = new SqlParameter("@CreatedByID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "CreatedByID", DataRowVersion.Current, (object)entity.CreatedByID != null ? (object)entity.CreatedByID : DBNull.Value);   cmd.Parameters.Add(pCreatedByID); 
                SqlParameter pModifiedDate = new SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "ModifiedDate", DataRowVersion.Current, (object)entity.ModifiedDate != null ? (object)entity.ModifiedDate : DBNull.Value);   cmd.Parameters.Add(pModifiedDate); 
                SqlParameter pModifiedByID = new SqlParameter("@ModifiedByID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ModifiedByID", DataRowVersion.Current, (object)entity.ModifiedByID != null ? (object)entity.ModifiedByID : DBNull.Value);   cmd.Parameters.Add(pModifiedByID); 
        
            return cmd;
        }

        protected FrameType FrameTypeFromRow(DataRow row)
        {
            var entity = new FrameType();

                    entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
                    entity.FrameTypeName = !DBNull.Value.Equals(row["FrameTypeName"]) ? (System.String)row["FrameTypeName"] : default(System.String);
                    entity.Description = !DBNull.Value.Equals(row["Description"]) ? (System.String)row["Description"] : default(System.String);
                    entity.ThumbnailUrl = !DBNull.Value.Equals(row["ThumbnailUrl"]) ? (System.String)row["ThumbnailUrl"] : default(System.String);
                    entity.IsDeleted = !DBNull.Value.Equals(row["IsDeleted"]) ? (System.Boolean)row["IsDeleted"] : default(System.Boolean);
                    entity.CreatedDate = !DBNull.Value.Equals(row["CreatedDate"]) ? (System.DateTime)row["CreatedDate"] : default(System.DateTime);
                    entity.CreatedByID = !DBNull.Value.Equals(row["CreatedByID"]) ? (System.Int64)row["CreatedByID"] : default(System.Int64);
                    entity.ModifiedDate = !DBNull.Value.Equals(row["ModifiedDate"]) ? (System.DateTime?)row["ModifiedDate"] : default(System.DateTime?);
                    entity.ModifiedByID = !DBNull.Value.Equals(row["ModifiedByID"]) ? (System.Int64?)row["ModifiedByID"] : default(System.Int64?);
        
            return entity;
        }
        
    }
}
