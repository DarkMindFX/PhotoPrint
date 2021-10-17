

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
    class UnitDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IUnitDal))]
    public class UnitDal: SQLDal, IUnitDal
    {
        public IInitParams CreateInitParams()
        {
            return new UnitDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public Unit Get(System.Int64? ID)
        {
            Unit result = default(Unit);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_Unit_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = UnitFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_Unit_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }

        
        public IList<Unit> GetAll()
        {
            IList<Unit> result = base.GetAll<Unit>("p_Unit_GetAll", UnitFromRow);

            return result;
        }

        public Unit Insert(Unit entity) 
        {
            Unit entityOut = base.Upsert<Unit>("p_Unit_Insert", entity, AddUpsertParameters, UnitFromRow);

            return entityOut;
        }

        public Unit Update(Unit entity) 
        {
            Unit entityOut = base.Upsert<Unit>("p_Unit_Update", entity, AddUpsertParameters, UnitFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, Unit entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pUnitName = new SqlParameter("@UnitName", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "UnitName", DataRowVersion.Current, (object)entity.UnitName != null ? (object)entity.UnitName : DBNull.Value);   cmd.Parameters.Add(pUnitName); 
                SqlParameter pUnitAbbr = new SqlParameter("@UnitAbbr", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "UnitAbbr", DataRowVersion.Current, (object)entity.UnitAbbr != null ? (object)entity.UnitAbbr : DBNull.Value);   cmd.Parameters.Add(pUnitAbbr); 
                SqlParameter pDescription = new SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Current, (object)entity.Description != null ? (object)entity.Description : DBNull.Value);   cmd.Parameters.Add(pDescription); 
                SqlParameter pIsDeleted = new SqlParameter("@IsDeleted", System.Data.SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsDeleted", DataRowVersion.Current, (object)entity.IsDeleted != null ? (object)entity.IsDeleted : DBNull.Value);   cmd.Parameters.Add(pIsDeleted); 
        
            return cmd;
        }

        protected Unit UnitFromRow(DataRow row)
        {
            var entity = new Unit();

                    entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
                    entity.UnitName = !DBNull.Value.Equals(row["UnitName"]) ? (System.String)row["UnitName"] : default(System.String);
                    entity.UnitAbbr = !DBNull.Value.Equals(row["UnitAbbr"]) ? (System.String)row["UnitAbbr"] : default(System.String);
                    entity.Description = !DBNull.Value.Equals(row["Description"]) ? (System.String)row["Description"] : default(System.String);
                    entity.IsDeleted = !DBNull.Value.Equals(row["IsDeleted"]) ? (System.Boolean)row["IsDeleted"] : default(System.Boolean);
        
            return entity;
        }
        
    }
}
