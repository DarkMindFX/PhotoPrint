

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
    class UserConfirmationDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IUserConfirmationDal))]
    public class UserConfirmationDal: SQLDal, IUserConfirmationDal
    {
        public IInitParams CreateInitParams()
        {
            return new UserConfirmationDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public UserConfirmation Get(System.Int64? ID)
        {
            UserConfirmation result = default(UserConfirmation);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_UserConfirmation_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = UserConfirmationFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_UserConfirmation_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }

                public IList<UserConfirmation> GetByUserID(System.Int64 UserID)
        {
            var entitiesOut = base.GetBy<UserConfirmation, System.Int64>("p_UserConfirmation_GetByUserID", UserID, "@UserID", SqlDbType.BigInt, 0, UserConfirmationFromRow);

            return entitiesOut;
        }
        
        public IList<UserConfirmation> GetAll()
        {
            IList<UserConfirmation> result = base.GetAll<UserConfirmation>("p_UserConfirmation_GetAll", UserConfirmationFromRow);

            return result;
        }

        public UserConfirmation Insert(UserConfirmation entity) 
        {
            UserConfirmation entityOut = base.Upsert<UserConfirmation>("p_UserConfirmation_Insert", entity, AddUpsertParameters, UserConfirmationFromRow);

            return entityOut;
        }

        public UserConfirmation Update(UserConfirmation entity) 
        {
            UserConfirmation entityOut = base.Upsert<UserConfirmation>("p_UserConfirmation_Update", entity, AddUpsertParameters, UserConfirmationFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, UserConfirmation entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pUserID = new SqlParameter("@UserID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "UserID", DataRowVersion.Current, (object)entity.UserID != null ? (object)entity.UserID : DBNull.Value);   cmd.Parameters.Add(pUserID); 
                SqlParameter pConfirmationCode = new SqlParameter("@ConfirmationCode", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "ConfirmationCode", DataRowVersion.Current, (object)entity.ConfirmationCode != null ? (object)entity.ConfirmationCode : DBNull.Value);   cmd.Parameters.Add(pConfirmationCode); 
                SqlParameter pComfirmed = new SqlParameter("@Comfirmed", System.Data.SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "Comfirmed", DataRowVersion.Current, (object)entity.Comfirmed != null ? (object)entity.Comfirmed : DBNull.Value);   cmd.Parameters.Add(pComfirmed); 
                SqlParameter pExpiresDate = new SqlParameter("@ExpiresDate", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "ExpiresDate", DataRowVersion.Current, (object)entity.ExpiresDate != null ? (object)entity.ExpiresDate : DBNull.Value);   cmd.Parameters.Add(pExpiresDate); 
                SqlParameter pConfirmationDate = new SqlParameter("@ConfirmationDate", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "ConfirmationDate", DataRowVersion.Current, (object)entity.ConfirmationDate != null ? (object)entity.ConfirmationDate : DBNull.Value);   cmd.Parameters.Add(pConfirmationDate); 
        
            return cmd;
        }

        protected UserConfirmation UserConfirmationFromRow(DataRow row)
        {
            var entity = new UserConfirmation();

                    entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
                    entity.UserID = !DBNull.Value.Equals(row["UserID"]) ? (System.Int64)row["UserID"] : default(System.Int64);
                    entity.ConfirmationCode = !DBNull.Value.Equals(row["ConfirmationCode"]) ? (System.String)row["ConfirmationCode"] : default(System.String);
                    entity.Comfirmed = !DBNull.Value.Equals(row["Comfirmed"]) ? (System.Boolean)row["Comfirmed"] : default(System.Boolean);
                    entity.ExpiresDate = !DBNull.Value.Equals(row["ExpiresDate"]) ? (System.DateTime)row["ExpiresDate"] : default(System.DateTime);
                    entity.ConfirmationDate = !DBNull.Value.Equals(row["ConfirmationDate"]) ? (System.DateTime?)row["ConfirmationDate"] : default(System.DateTime?);
        
            return entity;
        }
        
    }
}
