

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
    class CountryDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(ICountryDal))]
    public class CountryDal: SQLDal, ICountryDal
    {
        public IInitParams CreateInitParams()
        {
            return new CountryDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public Country Get(System.Int64? ID)
        {
            Country result = default(Country);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_Country_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = CountryFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_Country_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }

        
        public IList<Country> GetAll()
        {
            IList<Country> result = base.GetAll<Country>("p_Country_GetAll", CountryFromRow);

            return result;
        }

        public Country Insert(Country entity) 
        {
            Country entityOut = base.Upsert<Country>("p_Country_Insert", entity, AddUpsertParameters, CountryFromRow);

            return entityOut;
        }

        public Country Update(Country entity) 
        {
            Country entityOut = base.Upsert<Country>("p_Country_Update", entity, AddUpsertParameters, CountryFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, Country entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pCountryName = new SqlParameter("@CountryName", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "CountryName", DataRowVersion.Current, (object)entity.CountryName != null ? (object)entity.CountryName : DBNull.Value);   cmd.Parameters.Add(pCountryName); 
                SqlParameter pISO = new SqlParameter("@ISO", System.Data.SqlDbType.NVarChar, 5, ParameterDirection.Input, false, 0, 0, "ISO", DataRowVersion.Current, (object)entity.ISO != null ? (object)entity.ISO : DBNull.Value);   cmd.Parameters.Add(pISO); 
                SqlParameter pIsDeleted = new SqlParameter("@IsDeleted", System.Data.SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsDeleted", DataRowVersion.Current, (object)entity.IsDeleted != null ? (object)entity.IsDeleted : DBNull.Value);   cmd.Parameters.Add(pIsDeleted); 
        
            return cmd;
        }

        protected Country CountryFromRow(DataRow row)
        {
            var entity = new Country();

                    entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
                    entity.CountryName = !DBNull.Value.Equals(row["CountryName"]) ? (System.String)row["CountryName"] : default(System.String);
                    entity.ISO = !DBNull.Value.Equals(row["ISO"]) ? (System.String)row["ISO"] : default(System.String);
                    entity.IsDeleted = !DBNull.Value.Equals(row["IsDeleted"]) ? (System.Boolean)row["IsDeleted"] : default(System.Boolean);
        
            return entity;
        }
        
    }
}
