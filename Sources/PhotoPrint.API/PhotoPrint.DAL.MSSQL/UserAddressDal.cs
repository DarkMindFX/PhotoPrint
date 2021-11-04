


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
    class UserAddressDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IUserAddressDal))]
    public class UserAddressDal: SQLDal, IUserAddressDal
    {
        public IInitParams CreateInitParams()
        {
            return new UserAddressDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public UserAddress Get(System.Int64 UserID,System.Int64 AddressID)
        {
            UserAddress result = default(UserAddress);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_UserAddress_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@UserID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, UserID);
            
                            AddParameter(   cmd, "@AddressID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, AddressID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = UserAddressFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64 UserID,System.Int64 AddressID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_UserAddress_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@UserID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, UserID);
            
                            AddParameter(   cmd, "@AddressID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, AddressID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }


                public IList<UserAddress> GetByUserID(System.Int64 UserID)
        {
            var entitiesOut = base.GetBy<UserAddress, System.Int64>("p_UserAddress_GetByUserID", UserID, "@UserID", SqlDbType.BigInt, 0, UserAddressFromRow);

            return entitiesOut;
        }
                public IList<UserAddress> GetByAddressID(System.Int64 AddressID)
        {
            var entitiesOut = base.GetBy<UserAddress, System.Int64>("p_UserAddress_GetByAddressID", AddressID, "@AddressID", SqlDbType.BigInt, 0, UserAddressFromRow);

            return entitiesOut;
        }
        
        public IList<UserAddress> GetAll()
        {
            IList<UserAddress> result = base.GetAll<UserAddress>("p_UserAddress_GetAll", UserAddressFromRow);

            return result;
        }

        public UserAddress Insert(UserAddress entity) 
        {
            UserAddress entityOut = base.Upsert<UserAddress>("p_UserAddress_Insert", entity, AddUpsertParameters, UserAddressFromRow);

            return entityOut;
        }

        public UserAddress Update(UserAddress entity) 
        {
            UserAddress entityOut = base.Upsert<UserAddress>("p_UserAddress_Update", entity, AddUpsertParameters, UserAddressFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, UserAddress entity)
        {
                SqlParameter pUserID = new SqlParameter("@UserID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "UserID", DataRowVersion.Current, (object)entity.UserID != null ? (object)entity.UserID : DBNull.Value);   cmd.Parameters.Add(pUserID); 
                SqlParameter pAddressID = new SqlParameter("@AddressID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "AddressID", DataRowVersion.Current, (object)entity.AddressID != null ? (object)entity.AddressID : DBNull.Value);   cmd.Parameters.Add(pAddressID); 
                SqlParameter pIsPrimary = new SqlParameter("@IsPrimary", System.Data.SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsPrimary", DataRowVersion.Current, (object)entity.IsPrimary != null ? (object)entity.IsPrimary : DBNull.Value);   cmd.Parameters.Add(pIsPrimary); 
        
            return cmd;
        }

        protected UserAddress UserAddressFromRow(DataRow row)
        {
            var entity = new UserAddress();

                    entity.UserID = !DBNull.Value.Equals(row["UserID"]) ? (System.Int64)row["UserID"] : default(System.Int64);
                    entity.AddressID = !DBNull.Value.Equals(row["AddressID"]) ? (System.Int64)row["AddressID"] : default(System.Int64);
                    entity.IsPrimary = !DBNull.Value.Equals(row["IsPrimary"]) ? (System.Boolean)row["IsPrimary"] : default(System.Boolean);
        
            return entity;
        }
        
    }
}
