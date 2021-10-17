

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
    class CityDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(ICityDal))]
    public class CityDal: SQLDal, ICityDal
    {
        public IInitParams CreateInitParams()
        {
            return new CityDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public City Get(System.Int64? ID)
        {
            City result = default(City);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_City_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = CityFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_City_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }

                public IList<City> GetByRegionID(System.Int64 RegionID)
        {
            var entitiesOut = base.GetBy<City, System.Int64>("p_City_GetByRegionID", RegionID, "@RegionID", SqlDbType.BigInt, 0, CityFromRow);

            return entitiesOut;
        }
        
        public IList<City> GetAll()
        {
            IList<City> result = base.GetAll<City>("p_City_GetAll", CityFromRow);

            return result;
        }

        public City Insert(City entity) 
        {
            City entityOut = base.Upsert<City>("p_City_Insert", entity, AddUpsertParameters, CityFromRow);

            return entityOut;
        }

        public City Update(City entity) 
        {
            City entityOut = base.Upsert<City>("p_City_Update", entity, AddUpsertParameters, CityFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, City entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pCityName = new SqlParameter("@CityName", System.Data.SqlDbType.NVarChar, 250, ParameterDirection.Input, false, 0, 0, "CityName", DataRowVersion.Current, (object)entity.CityName != null ? (object)entity.CityName : DBNull.Value);   cmd.Parameters.Add(pCityName); 
                SqlParameter pRegionID = new SqlParameter("@RegionID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "RegionID", DataRowVersion.Current, (object)entity.RegionID != null ? (object)entity.RegionID : DBNull.Value);   cmd.Parameters.Add(pRegionID); 
                SqlParameter pIsDeleted = new SqlParameter("@IsDeleted", System.Data.SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsDeleted", DataRowVersion.Current, (object)entity.IsDeleted != null ? (object)entity.IsDeleted : DBNull.Value);   cmd.Parameters.Add(pIsDeleted); 
        
            return cmd;
        }

        protected City CityFromRow(DataRow row)
        {
            var entity = new City();

                    entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
                    entity.CityName = !DBNull.Value.Equals(row["CityName"]) ? (System.String)row["CityName"] : default(System.String);
                    entity.RegionID = !DBNull.Value.Equals(row["RegionID"]) ? (System.Int64)row["RegionID"] : default(System.Int64);
                    entity.IsDeleted = !DBNull.Value.Equals(row["IsDeleted"]) ? (System.Boolean)row["IsDeleted"] : default(System.Boolean);
        
            return entity;
        }
        
    }
}
