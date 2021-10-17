

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
    class MaterialTypeDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IMaterialTypeDal))]
    public class MaterialTypeDal: SQLDal, IMaterialTypeDal
    {
        public IInitParams CreateInitParams()
        {
            return new MaterialTypeDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public MaterialType Get(System.Int64? ID)
        {
            MaterialType result = default(MaterialType);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_MaterialType_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = MaterialTypeFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_MaterialType_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }

                public IList<MaterialType> GetByCreatedByID(System.Int64 CreatedByID)
        {
            var entitiesOut = base.GetBy<MaterialType, System.Int64>("p_MaterialType_GetByCreatedByID", CreatedByID, "@CreatedByID", SqlDbType.BigInt, 0, MaterialTypeFromRow);

            return entitiesOut;
        }
                public IList<MaterialType> GetByModifiedByID(System.Int64? ModifiedByID)
        {
            var entitiesOut = base.GetBy<MaterialType, System.Int64?>("p_MaterialType_GetByModifiedByID", ModifiedByID, "@ModifiedByID", SqlDbType.BigInt, 0, MaterialTypeFromRow);

            return entitiesOut;
        }
        
        public IList<MaterialType> GetAll()
        {
            IList<MaterialType> result = base.GetAll<MaterialType>("p_MaterialType_GetAll", MaterialTypeFromRow);

            return result;
        }

        public MaterialType Insert(MaterialType entity) 
        {
            MaterialType entityOut = base.Upsert<MaterialType>("p_MaterialType_Insert", entity, AddUpsertParameters, MaterialTypeFromRow);

            return entityOut;
        }

        public MaterialType Update(MaterialType entity) 
        {
            MaterialType entityOut = base.Upsert<MaterialType>("p_MaterialType_Update", entity, AddUpsertParameters, MaterialTypeFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, MaterialType entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pMaterialTypeName = new SqlParameter("@MaterialTypeName", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "MaterialTypeName", DataRowVersion.Current, (object)entity.MaterialTypeName != null ? (object)entity.MaterialTypeName : DBNull.Value);   cmd.Parameters.Add(pMaterialTypeName); 
                SqlParameter pDescription = new SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Current, (object)entity.Description != null ? (object)entity.Description : DBNull.Value);   cmd.Parameters.Add(pDescription); 
                SqlParameter pThumbnailUrl = new SqlParameter("@ThumbnailUrl", System.Data.SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "ThumbnailUrl", DataRowVersion.Current, (object)entity.ThumbnailUrl != null ? (object)entity.ThumbnailUrl : DBNull.Value);   cmd.Parameters.Add(pThumbnailUrl); 
                SqlParameter pIsDeleted = new SqlParameter("@IsDeleted", System.Data.SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsDeleted", DataRowVersion.Current, (object)entity.IsDeleted != null ? (object)entity.IsDeleted : DBNull.Value);   cmd.Parameters.Add(pIsDeleted); 
                SqlParameter pCreatedDate = new SqlParameter("@CreatedDate", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "CreatedDate", DataRowVersion.Current, (object)entity.CreatedDate != null ? (object)entity.CreatedDate : DBNull.Value);   cmd.Parameters.Add(pCreatedDate); 
                SqlParameter pCreatedByID = new SqlParameter("@CreatedByID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "CreatedByID", DataRowVersion.Current, (object)entity.CreatedByID != null ? (object)entity.CreatedByID : DBNull.Value);   cmd.Parameters.Add(pCreatedByID); 
                SqlParameter pModifiedDate = new SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "ModifiedDate", DataRowVersion.Current, (object)entity.ModifiedDate != null ? (object)entity.ModifiedDate : DBNull.Value);   cmd.Parameters.Add(pModifiedDate); 
                SqlParameter pModifiedByID = new SqlParameter("@ModifiedByID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ModifiedByID", DataRowVersion.Current, (object)entity.ModifiedByID != null ? (object)entity.ModifiedByID : DBNull.Value);   cmd.Parameters.Add(pModifiedByID); 
        
            return cmd;
        }

        protected MaterialType MaterialTypeFromRow(DataRow row)
        {
            var entity = new MaterialType();

                    entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
                    entity.MaterialTypeName = !DBNull.Value.Equals(row["MaterialTypeName"]) ? (System.String)row["MaterialTypeName"] : default(System.String);
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
