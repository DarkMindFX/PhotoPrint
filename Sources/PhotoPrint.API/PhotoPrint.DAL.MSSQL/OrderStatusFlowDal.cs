

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
    class OrderStatusFlowDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IOrderStatusFlowDal))]
    public class OrderStatusFlowDal: SQLDal, IOrderStatusFlowDal
    {
        public IInitParams CreateInitParams()
        {
            return new OrderStatusFlowDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public OrderStatusFlow Get(System.Int64 FromStatusID,System.Int64 ToStatusID)
        {
            OrderStatusFlow result = default(OrderStatusFlow);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_OrderStatusFlow_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@FromStatusID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, FromStatusID);
            
                            AddParameter(   cmd, "@ToStatusID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ToStatusID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = OrderStatusFlowFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64 FromStatusID,System.Int64 ToStatusID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_OrderStatusFlow_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@FromStatusID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, FromStatusID);
            
                            AddParameter(   cmd, "@ToStatusID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ToStatusID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }

                public IList<OrderStatusFlow> GetByFromStatusID(System.Int64 FromStatusID)
        {
            var entitiesOut = base.GetBy<OrderStatusFlow, System.Int64>("p_OrderStatusFlow_GetByFromStatusID", FromStatusID, "@FromStatusID", SqlDbType.BigInt, 0, OrderStatusFlowFromRow);

            return entitiesOut;
        }
                public IList<OrderStatusFlow> GetByToStatusID(System.Int64 ToStatusID)
        {
            var entitiesOut = base.GetBy<OrderStatusFlow, System.Int64>("p_OrderStatusFlow_GetByToStatusID", ToStatusID, "@ToStatusID", SqlDbType.BigInt, 0, OrderStatusFlowFromRow);

            return entitiesOut;
        }
        
        public IList<OrderStatusFlow> GetAll()
        {
            IList<OrderStatusFlow> result = base.GetAll<OrderStatusFlow>("p_OrderStatusFlow_GetAll", OrderStatusFlowFromRow);

            return result;
        }

        public OrderStatusFlow Insert(OrderStatusFlow entity) 
        {
            OrderStatusFlow entityOut = base.Upsert<OrderStatusFlow>("p_OrderStatusFlow_Insert", entity, AddUpsertParameters, OrderStatusFlowFromRow);

            return entityOut;
        }

        public OrderStatusFlow Update(OrderStatusFlow entity) 
        {
            OrderStatusFlow entityOut = base.Upsert<OrderStatusFlow>("p_OrderStatusFlow_Update", entity, AddUpsertParameters, OrderStatusFlowFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, OrderStatusFlow entity)
        {
                SqlParameter pFromStatusID = new SqlParameter("@FromStatusID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "FromStatusID", DataRowVersion.Current, (object)entity.FromStatusID != null ? (object)entity.FromStatusID : DBNull.Value);   cmd.Parameters.Add(pFromStatusID); 
                SqlParameter pToStatusID = new SqlParameter("@ToStatusID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ToStatusID", DataRowVersion.Current, (object)entity.ToStatusID != null ? (object)entity.ToStatusID : DBNull.Value);   cmd.Parameters.Add(pToStatusID); 
        
            return cmd;
        }

        protected OrderStatusFlow OrderStatusFlowFromRow(DataRow row)
        {
            var entity = new OrderStatusFlow();

                    entity.FromStatusID = !DBNull.Value.Equals(row["FromStatusID"]) ? (System.Int64)row["FromStatusID"] : default(System.Int64);
                    entity.ToStatusID = !DBNull.Value.Equals(row["ToStatusID"]) ? (System.Int64)row["ToStatusID"] : default(System.Int64);
        
            return entity;
        }
        
    }
}
