

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
    class UserContactDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IUserContactDal))]
    public class UserContactDal: SQLDal, IUserContactDal
    {
        public IInitParams CreateInitParams()
        {
            return new UserContactDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public UserContact Get(System.Int64 UserID,System.Int64 ContactID)
        {
            UserContact result = default(UserContact);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_UserContact_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@UserID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, UserID);
            
                            AddParameter(   cmd, "@ContactID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ContactID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = UserContactFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64 UserID,System.Int64 ContactID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_UserContact_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@UserID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, UserID);
            
                            AddParameter(   cmd, "@ContactID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ContactID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }

                public IList<UserContact> GetByUserID(System.Int64 UserID)
        {
            var entitiesOut = base.GetBy<UserContact, System.Int64>("p_UserContact_GetByUserID", UserID, "@UserID", SqlDbType.BigInt, 0, UserContactFromRow);

            return entitiesOut;
        }
                public IList<UserContact> GetByContactID(System.Int64 ContactID)
        {
            var entitiesOut = base.GetBy<UserContact, System.Int64>("p_UserContact_GetByContactID", ContactID, "@ContactID", SqlDbType.BigInt, 0, UserContactFromRow);

            return entitiesOut;
        }
        
        public IList<UserContact> GetAll()
        {
            IList<UserContact> result = base.GetAll<UserContact>("p_UserContact_GetAll", UserContactFromRow);

            return result;
        }

        public UserContact Insert(UserContact entity) 
        {
            UserContact entityOut = base.Upsert<UserContact>("p_UserContact_Insert", entity, AddUpsertParameters, UserContactFromRow);

            return entityOut;
        }

        public UserContact Update(UserContact entity) 
        {
            UserContact entityOut = base.Upsert<UserContact>("p_UserContact_Update", entity, AddUpsertParameters, UserContactFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, UserContact entity)
        {
                SqlParameter pUserID = new SqlParameter("@UserID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "UserID", DataRowVersion.Current, (object)entity.UserID != null ? (object)entity.UserID : DBNull.Value);   cmd.Parameters.Add(pUserID); 
                SqlParameter pContactID = new SqlParameter("@ContactID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ContactID", DataRowVersion.Current, (object)entity.ContactID != null ? (object)entity.ContactID : DBNull.Value);   cmd.Parameters.Add(pContactID); 
                SqlParameter pIsPrimary = new SqlParameter("@IsPrimary", System.Data.SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsPrimary", DataRowVersion.Current, (object)entity.IsPrimary != null ? (object)entity.IsPrimary : DBNull.Value);   cmd.Parameters.Add(pIsPrimary); 
        
            return cmd;
        }

        protected UserContact UserContactFromRow(DataRow row)
        {
            var entity = new UserContact();

                    entity.UserID = !DBNull.Value.Equals(row["UserID"]) ? (System.Int64)row["UserID"] : default(System.Int64);
                    entity.ContactID = !DBNull.Value.Equals(row["ContactID"]) ? (System.Int64)row["ContactID"] : default(System.Int64);
                    entity.IsPrimary = !DBNull.Value.Equals(row["IsPrimary"]) ? (System.Boolean)row["IsPrimary"] : default(System.Boolean);
        
            return entity;
        }
        
    }
}
