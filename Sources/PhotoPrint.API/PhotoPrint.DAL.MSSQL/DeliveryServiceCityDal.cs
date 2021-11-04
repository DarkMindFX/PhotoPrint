


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
    class DeliveryServiceCityDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IDeliveryServiceCityDal))]
    public class DeliveryServiceCityDal: SQLDal, IDeliveryServiceCityDal
    {
        public IInitParams CreateInitParams()
        {
            return new DeliveryServiceCityDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public DeliveryServiceCity Get(System.Int64 DeliveryServiceID,System.Int64 CityID)
        {
            DeliveryServiceCity result = default(DeliveryServiceCity);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_DeliveryServiceCity_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@DeliveryServiceID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, DeliveryServiceID);
            
                            AddParameter(   cmd, "@CityID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, CityID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = DeliveryServiceCityFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64 DeliveryServiceID,System.Int64 CityID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_DeliveryServiceCity_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@DeliveryServiceID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, DeliveryServiceID);
            
                            AddParameter(   cmd, "@CityID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, CityID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }


                public IList<DeliveryServiceCity> GetByDeliveryServiceID(System.Int64 DeliveryServiceID)
        {
            var entitiesOut = base.GetBy<DeliveryServiceCity, System.Int64>("p_DeliveryServiceCity_GetByDeliveryServiceID", DeliveryServiceID, "@DeliveryServiceID", SqlDbType.BigInt, 0, DeliveryServiceCityFromRow);

            return entitiesOut;
        }
                public IList<DeliveryServiceCity> GetByCityID(System.Int64 CityID)
        {
            var entitiesOut = base.GetBy<DeliveryServiceCity, System.Int64>("p_DeliveryServiceCity_GetByCityID", CityID, "@CityID", SqlDbType.BigInt, 0, DeliveryServiceCityFromRow);

            return entitiesOut;
        }
        
        public IList<DeliveryServiceCity> GetAll()
        {
            IList<DeliveryServiceCity> result = base.GetAll<DeliveryServiceCity>("p_DeliveryServiceCity_GetAll", DeliveryServiceCityFromRow);

            return result;
        }

        public DeliveryServiceCity Insert(DeliveryServiceCity entity) 
        {
            DeliveryServiceCity entityOut = base.Upsert<DeliveryServiceCity>("p_DeliveryServiceCity_Insert", entity, AddUpsertParameters, DeliveryServiceCityFromRow);

            return entityOut;
        }

        public DeliveryServiceCity Update(DeliveryServiceCity entity) 
        {
            DeliveryServiceCity entityOut = base.Upsert<DeliveryServiceCity>("p_DeliveryServiceCity_Update", entity, AddUpsertParameters, DeliveryServiceCityFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, DeliveryServiceCity entity)
        {
                SqlParameter pDeliveryServiceID = new SqlParameter("@DeliveryServiceID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "DeliveryServiceID", DataRowVersion.Current, (object)entity.DeliveryServiceID != null ? (object)entity.DeliveryServiceID : DBNull.Value);   cmd.Parameters.Add(pDeliveryServiceID); 
                SqlParameter pCityID = new SqlParameter("@CityID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "CityID", DataRowVersion.Current, (object)entity.CityID != null ? (object)entity.CityID : DBNull.Value);   cmd.Parameters.Add(pCityID); 
        
            return cmd;
        }

        protected DeliveryServiceCity DeliveryServiceCityFromRow(DataRow row)
        {
            var entity = new DeliveryServiceCity();

                    entity.DeliveryServiceID = !DBNull.Value.Equals(row["DeliveryServiceID"]) ? (System.Int64)row["DeliveryServiceID"] : default(System.Int64);
                    entity.CityID = !DBNull.Value.Equals(row["CityID"]) ? (System.Int64)row["CityID"] : default(System.Int64);
        
            return entity;
        }
        
    }
}
