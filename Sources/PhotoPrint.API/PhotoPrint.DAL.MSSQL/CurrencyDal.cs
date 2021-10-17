

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
    class CurrencyDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(ICurrencyDal))]
    public class CurrencyDal: SQLDal, ICurrencyDal
    {
        public IInitParams CreateInitParams()
        {
            return new CurrencyDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public Currency Get(System.Int64? ID)
        {
            Currency result = default(Currency);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_Currency_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = CurrencyFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_Currency_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }

        
        public IList<Currency> GetAll()
        {
            IList<Currency> result = base.GetAll<Currency>("p_Currency_GetAll", CurrencyFromRow);

            return result;
        }

        public Currency Insert(Currency entity) 
        {
            Currency entityOut = base.Upsert<Currency>("p_Currency_Insert", entity, AddUpsertParameters, CurrencyFromRow);

            return entityOut;
        }

        public Currency Update(Currency entity) 
        {
            Currency entityOut = base.Upsert<Currency>("p_Currency_Update", entity, AddUpsertParameters, CurrencyFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, Currency entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pISO = new SqlParameter("@ISO", System.Data.SqlDbType.NVarChar, 5, ParameterDirection.Input, false, 0, 0, "ISO", DataRowVersion.Current, (object)entity.ISO != null ? (object)entity.ISO : DBNull.Value);   cmd.Parameters.Add(pISO); 
                SqlParameter pCurrencyName = new SqlParameter("@CurrencyName", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "CurrencyName", DataRowVersion.Current, (object)entity.CurrencyName != null ? (object)entity.CurrencyName : DBNull.Value);   cmd.Parameters.Add(pCurrencyName); 
                SqlParameter pIsDeleted = new SqlParameter("@IsDeleted", System.Data.SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsDeleted", DataRowVersion.Current, (object)entity.IsDeleted != null ? (object)entity.IsDeleted : DBNull.Value);   cmd.Parameters.Add(pIsDeleted); 
        
            return cmd;
        }

        protected Currency CurrencyFromRow(DataRow row)
        {
            var entity = new Currency();

                    entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
                    entity.ISO = !DBNull.Value.Equals(row["ISO"]) ? (System.String)row["ISO"] : default(System.String);
                    entity.CurrencyName = !DBNull.Value.Equals(row["CurrencyName"]) ? (System.String)row["CurrencyName"] : default(System.String);
                    entity.IsDeleted = !DBNull.Value.Equals(row["IsDeleted"]) ? (System.Boolean)row["IsDeleted"] : default(System.Boolean);
        
            return entity;
        }
        
    }
}
