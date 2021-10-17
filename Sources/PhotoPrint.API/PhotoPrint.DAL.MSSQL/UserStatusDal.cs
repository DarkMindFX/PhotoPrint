

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
    class UserStatusDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IUserStatusDal))]
    public class UserStatusDal: SQLDal, IUserStatusDal
    {
        public IInitParams CreateInitParams()
        {
            return new UserStatusDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public UserStatus Get(System.Int64? ID)
        {
            UserStatus result = default(UserStatus);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_UserStatus_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = UserStatusFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_UserStatus_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }

        
        public IList<UserStatus> GetAll()
        {
            IList<UserStatus> result = base.GetAll<UserStatus>("p_UserStatus_GetAll", UserStatusFromRow);

            return result;
        }

        public UserStatus Insert(UserStatus entity) 
        {
            UserStatus entityOut = base.Upsert<UserStatus>("p_UserStatus_Insert", entity, AddUpsertParameters, UserStatusFromRow);

            return entityOut;
        }

        public UserStatus Update(UserStatus entity) 
        {
            UserStatus entityOut = base.Upsert<UserStatus>("p_UserStatus_Update", entity, AddUpsertParameters, UserStatusFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, UserStatus entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pStatusName = new SqlParameter("@StatusName", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "StatusName", DataRowVersion.Current, (object)entity.StatusName != null ? (object)entity.StatusName : DBNull.Value);   cmd.Parameters.Add(pStatusName); 
                SqlParameter pIsDeleted = new SqlParameter("@IsDeleted", System.Data.SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsDeleted", DataRowVersion.Current, (object)entity.IsDeleted != null ? (object)entity.IsDeleted : DBNull.Value);   cmd.Parameters.Add(pIsDeleted); 
        
            return cmd;
        }

        protected UserStatus UserStatusFromRow(DataRow row)
        {
            var entity = new UserStatus();

                    entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
                    entity.StatusName = !DBNull.Value.Equals(row["StatusName"]) ? (System.String)row["StatusName"] : default(System.String);
                    entity.IsDeleted = !DBNull.Value.Equals(row["IsDeleted"]) ? (System.Boolean)row["IsDeleted"] : default(System.Boolean);
        
            return entity;
        }
        
    }
}
