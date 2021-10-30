

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
    class RegionDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IRegionDal))]
    public class RegionDal: SQLDal, IRegionDal
    {
        public IInitParams CreateInitParams()
        {
            return new RegionDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public Region Get(System.Int64? ID)
        {
            Region result = default(Region);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_Region_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = RegionFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_Region_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }

                public IList<Region> GetByCountryID(System.Int64 CountryID)
        {
            var entitiesOut = base.GetBy<Region, System.Int64>("p_Region_GetByCountryID", CountryID, "@CountryID", SqlDbType.BigInt, 0, RegionFromRow);

            return entitiesOut;
        }
        
        public IList<Region> GetAll()
        {
            IList<Region> result = base.GetAll<Region>("p_Region_GetAll", RegionFromRow);

            return result;
        }

        public Region Insert(Region entity) 
        {
            Region entityOut = base.Upsert<Region>("p_Region_Insert", entity, AddUpsertParameters, RegionFromRow);

            return entityOut;
        }

        public Region Update(Region entity) 
        {
            Region entityOut = base.Upsert<Region>("p_Region_Update", entity, AddUpsertParameters, RegionFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, Region entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pRegionName = new SqlParameter("@RegionName", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "RegionName", DataRowVersion.Current, (object)entity.RegionName != null ? (object)entity.RegionName : DBNull.Value);   cmd.Parameters.Add(pRegionName); 
                SqlParameter pCountryID = new SqlParameter("@CountryID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "CountryID", DataRowVersion.Current, (object)entity.CountryID != null ? (object)entity.CountryID : DBNull.Value);   cmd.Parameters.Add(pCountryID); 
                SqlParameter pIsDeleted = new SqlParameter("@IsDeleted", System.Data.SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsDeleted", DataRowVersion.Current, (object)entity.IsDeleted != null ? (object)entity.IsDeleted : DBNull.Value);   cmd.Parameters.Add(pIsDeleted); 
        
            return cmd;
        }

        protected Region RegionFromRow(DataRow row)
        {
            var entity = new Region();

                    entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
                    entity.RegionName = !DBNull.Value.Equals(row["RegionName"]) ? (System.String)row["RegionName"] : default(System.String);
                    entity.CountryID = !DBNull.Value.Equals(row["CountryID"]) ? (System.Int64)row["CountryID"] : default(System.Int64);
                    entity.IsDeleted = !DBNull.Value.Equals(row["IsDeleted"]) ? (System.Boolean)row["IsDeleted"] : default(System.Boolean);
        
            return entity;
        }
        
    }
}
