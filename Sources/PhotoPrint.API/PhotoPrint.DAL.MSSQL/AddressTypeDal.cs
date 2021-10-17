

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
    class AddressTypeDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IAddressTypeDal))]
    public class AddressTypeDal: SQLDal, IAddressTypeDal
    {
        public IInitParams CreateInitParams()
        {
            return new AddressTypeDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public AddressType Get(System.Int64? ID)
        {
            AddressType result = default(AddressType);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_AddressType_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = AddressTypeFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_AddressType_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }

        
        public IList<AddressType> GetAll()
        {
            IList<AddressType> result = base.GetAll<AddressType>("p_AddressType_GetAll", AddressTypeFromRow);

            return result;
        }

        public AddressType Insert(AddressType entity) 
        {
            AddressType entityOut = base.Upsert<AddressType>("p_AddressType_Insert", entity, AddUpsertParameters, AddressTypeFromRow);

            return entityOut;
        }

        public AddressType Update(AddressType entity) 
        {
            AddressType entityOut = base.Upsert<AddressType>("p_AddressType_Update", entity, AddUpsertParameters, AddressTypeFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, AddressType entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pAddressTypeName = new SqlParameter("@AddressTypeName", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "AddressTypeName", DataRowVersion.Current, (object)entity.AddressTypeName != null ? (object)entity.AddressTypeName : DBNull.Value);   cmd.Parameters.Add(pAddressTypeName); 
                SqlParameter pIsDeleted = new SqlParameter("@IsDeleted", System.Data.SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsDeleted", DataRowVersion.Current, (object)entity.IsDeleted != null ? (object)entity.IsDeleted : DBNull.Value);   cmd.Parameters.Add(pIsDeleted); 
        
            return cmd;
        }

        protected AddressType AddressTypeFromRow(DataRow row)
        {
            var entity = new AddressType();

                    entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
                    entity.AddressTypeName = !DBNull.Value.Equals(row["AddressTypeName"]) ? (System.String)row["AddressTypeName"] : default(System.String);
                    entity.IsDeleted = !DBNull.Value.Equals(row["IsDeleted"]) ? (System.Boolean)row["IsDeleted"] : default(System.Boolean);
        
            return entity;
        }
        
    }
}
