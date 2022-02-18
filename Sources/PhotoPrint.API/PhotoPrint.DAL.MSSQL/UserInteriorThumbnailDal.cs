


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
    class UserInteriorThumbnailDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IUserInteriorThumbnailDal))]
    public class UserInteriorThumbnailDal : SQLDal, IUserInteriorThumbnailDal
    {
        public IInitParams CreateInitParams()
        {
            return new UserInteriorThumbnailDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public UserInteriorThumbnail Get(System.Int64? ID)
        {
            UserInteriorThumbnail result = default(UserInteriorThumbnail);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_UserInteriorThumbnail_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                AddParameter(cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                               ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);


                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = UserInteriorThumbnailFromRow(ds.Tables[0].Rows[0]);
                }
            }

            return result;
        }

        public bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_UserInteriorThumbnail_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                AddParameter(cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);

                var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }


        public IList<UserInteriorThumbnail> GetByUserID(System.Int64 UserID)
        {
            var entitiesOut = base.GetBy<UserInteriorThumbnail, System.Int64>("p_UserInteriorThumbnail_GetByUserID", UserID, "@UserID", SqlDbType.BigInt, 0, UserInteriorThumbnailFromRow);

            return entitiesOut;
        }

        public IList<UserInteriorThumbnail> GetAll()
        {
            IList<UserInteriorThumbnail> result = base.GetAll<UserInteriorThumbnail>("p_UserInteriorThumbnail_GetAll", UserInteriorThumbnailFromRow);

            return result;
        }

        public UserInteriorThumbnail Insert(UserInteriorThumbnail entity)
        {
            UserInteriorThumbnail entityOut = base.Upsert<UserInteriorThumbnail>("p_UserInteriorThumbnail_Insert", entity, AddUpsertParameters, UserInteriorThumbnailFromRow);

            return entityOut;
        }

        public UserInteriorThumbnail Update(UserInteriorThumbnail entity)
        {
            UserInteriorThumbnail entityOut = base.Upsert<UserInteriorThumbnail>("p_UserInteriorThumbnail_Update", entity, AddUpsertParameters, UserInteriorThumbnailFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, UserInteriorThumbnail entity)
        {
            SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value); cmd.Parameters.Add(pID);
            SqlParameter pUserID = new SqlParameter("@UserID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "UserID", DataRowVersion.Current, (object)entity.UserID != null ? (object)entity.UserID : DBNull.Value); cmd.Parameters.Add(pUserID);
            SqlParameter pUrl = new SqlParameter("@Url", System.Data.SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "Url", DataRowVersion.Current, (object)entity.Url != null ? (object)entity.Url : DBNull.Value); cmd.Parameters.Add(pUrl);

            return cmd;
        }

        protected UserInteriorThumbnail UserInteriorThumbnailFromRow(DataRow row)
        {
            var entity = new UserInteriorThumbnail();

            entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
            entity.UserID = !DBNull.Value.Equals(row["UserID"]) ? (System.Int64)row["UserID"] : default(System.Int64);
            entity.Url = !DBNull.Value.Equals(row["Url"]) ? (System.String)row["Url"] : default(System.String);

            return entity;
        }

    }
}
