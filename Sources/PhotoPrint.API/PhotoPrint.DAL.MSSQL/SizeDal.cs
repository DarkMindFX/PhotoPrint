

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
    class SizeDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(ISizeDal))]
    public class SizeDal : SQLDal, ISizeDal
    {
        public IInitParams CreateInitParams()
        {
            return new SizeDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public Size Get(System.Int64? ID)
        {
            Size result = default(Size);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_Size_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                AddParameter(cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                               ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);


                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = SizeFromRow(ds.Tables[0].Rows[0]);
                }
            }

            return result;
        }

        public bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_Size_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                AddParameter(cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);

                var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }

        public IList<Size> GetByCreatedByID(System.Int64 CreatedByID)
        {
            var entitiesOut = base.GetBy<Size, System.Int64>("p_Size_GetByCreatedByID", CreatedByID, "@CreatedByID", SqlDbType.BigInt, 0, SizeFromRow);

            return entitiesOut;
        }
        public IList<Size> GetByModifiedByID(System.Int64? ModifiedByID)
        {
            var entitiesOut = base.GetBy<Size, System.Int64?>("p_Size_GetByModifiedByID", ModifiedByID, "@ModifiedByID", SqlDbType.BigInt, 0, SizeFromRow);

            return entitiesOut;
        }

        public IList<Size> GetAll()
        {
            IList<Size> result = base.GetAll<Size>("p_Size_GetAll", SizeFromRow);

            return result;
        }

        public Size Insert(Size entity)
        {
            Size entityOut = base.Upsert<Size>("p_Size_Insert", entity, AddUpsertParameters, SizeFromRow);

            return entityOut;
        }

        public Size Update(Size entity)
        {
            Size entityOut = base.Upsert<Size>("p_Size_Update", entity, AddUpsertParameters, SizeFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, Size entity)
        {
            SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value); cmd.Parameters.Add(pID);
            SqlParameter pSizeName = new SqlParameter("@SizeName", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "SizeName", DataRowVersion.Current, (object)entity.SizeName != null ? (object)entity.SizeName : DBNull.Value); cmd.Parameters.Add(pSizeName);
            SqlParameter pWidth = new SqlParameter("@Width", System.Data.SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "Width", DataRowVersion.Current, (object)entity.Width != null ? (object)entity.Width : DBNull.Value); cmd.Parameters.Add(pWidth);
            SqlParameter pHeight = new SqlParameter("@Height", System.Data.SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "Height", DataRowVersion.Current, (object)entity.Height != null ? (object)entity.Height : DBNull.Value); cmd.Parameters.Add(pHeight);
            SqlParameter pIsDeleted = new SqlParameter("@IsDeleted", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "IsDeleted", DataRowVersion.Current, (object)entity.IsDeleted != null ? (object)entity.IsDeleted : DBNull.Value); cmd.Parameters.Add(pIsDeleted);
            SqlParameter pCreatedDate = new SqlParameter("@CreatedDate", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "CreatedDate", DataRowVersion.Current, (object)entity.CreatedDate != null ? (object)entity.CreatedDate : DBNull.Value); cmd.Parameters.Add(pCreatedDate);
            SqlParameter pCreatedByID = new SqlParameter("@CreatedByID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "CreatedByID", DataRowVersion.Current, (object)entity.CreatedByID != null ? (object)entity.CreatedByID : DBNull.Value); cmd.Parameters.Add(pCreatedByID);
            SqlParameter pModifiedDate = new SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "ModifiedDate", DataRowVersion.Current, (object)entity.ModifiedDate != null ? (object)entity.ModifiedDate : DBNull.Value); cmd.Parameters.Add(pModifiedDate);
            SqlParameter pModifiedByID = new SqlParameter("@ModifiedByID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ModifiedByID", DataRowVersion.Current, (object)entity.ModifiedByID != null ? (object)entity.ModifiedByID : DBNull.Value); cmd.Parameters.Add(pModifiedByID);

            return cmd;
        }

        protected Size SizeFromRow(DataRow row)
        {
            var entity = new Size();

            entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
            entity.SizeName = !DBNull.Value.Equals(row["SizeName"]) ? (System.String)row["SizeName"] : default(System.String);
            entity.Width = !DBNull.Value.Equals(row["Width"]) ? (System.Int32)row["Width"] : default(System.Int32);
            entity.Height = !DBNull.Value.Equals(row["Height"]) ? (System.Int32)row["Height"] : default(System.Int32);
            entity.IsDeleted = !DBNull.Value.Equals(row["IsDeleted"]) ? (System.Boolean)row["IsDeleted"] : default(System.Boolean);
            entity.CreatedDate = !DBNull.Value.Equals(row["CreatedDate"]) ? (System.DateTime)row["CreatedDate"] : default(System.DateTime);
            entity.CreatedByID = !DBNull.Value.Equals(row["CreatedByID"]) ? (System.Int64)row["CreatedByID"] : default(System.Int64);
            entity.ModifiedDate = !DBNull.Value.Equals(row["ModifiedDate"]) ? (System.DateTime?)row["ModifiedDate"] : default(System.DateTime?);
            entity.ModifiedByID = !DBNull.Value.Equals(row["ModifiedByID"]) ? (System.Int64?)row["ModifiedByID"] : default(System.Int64?);

            return entity;
        }

    }
}
