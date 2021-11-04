


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
    class OrderTrackingDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IOrderTrackingDal))]
    public class OrderTrackingDal: SQLDal, IOrderTrackingDal
    {
        public IInitParams CreateInitParams()
        {
            return new OrderTrackingDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public OrderTracking Get(System.Int64? ID)
        {
            OrderTracking result = default(OrderTracking);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_OrderTracking_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = OrderTrackingFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_OrderTracking_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }


                public IList<OrderTracking> GetByOrderID(System.Int64 OrderID)
        {
            var entitiesOut = base.GetBy<OrderTracking, System.Int64>("p_OrderTracking_GetByOrderID", OrderID, "@OrderID", SqlDbType.BigInt, 0, OrderTrackingFromRow);

            return entitiesOut;
        }
                public IList<OrderTracking> GetByOrderStatusID(System.Int64 OrderStatusID)
        {
            var entitiesOut = base.GetBy<OrderTracking, System.Int64>("p_OrderTracking_GetByOrderStatusID", OrderStatusID, "@OrderStatusID", SqlDbType.BigInt, 0, OrderTrackingFromRow);

            return entitiesOut;
        }
                public IList<OrderTracking> GetBySetByID(System.Int64 SetByID)
        {
            var entitiesOut = base.GetBy<OrderTracking, System.Int64>("p_OrderTracking_GetBySetByID", SetByID, "@SetByID", SqlDbType.BigInt, 0, OrderTrackingFromRow);

            return entitiesOut;
        }
        
        public IList<OrderTracking> GetAll()
        {
            IList<OrderTracking> result = base.GetAll<OrderTracking>("p_OrderTracking_GetAll", OrderTrackingFromRow);

            return result;
        }

        public OrderTracking Insert(OrderTracking entity) 
        {
            OrderTracking entityOut = base.Upsert<OrderTracking>("p_OrderTracking_Insert", entity, AddUpsertParameters, OrderTrackingFromRow);

            return entityOut;
        }

        public OrderTracking Update(OrderTracking entity) 
        {
            OrderTracking entityOut = base.Upsert<OrderTracking>("p_OrderTracking_Update", entity, AddUpsertParameters, OrderTrackingFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, OrderTracking entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pOrderID = new SqlParameter("@OrderID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "OrderID", DataRowVersion.Current, (object)entity.OrderID != null ? (object)entity.OrderID : DBNull.Value);   cmd.Parameters.Add(pOrderID); 
                SqlParameter pOrderStatusID = new SqlParameter("@OrderStatusID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "OrderStatusID", DataRowVersion.Current, (object)entity.OrderStatusID != null ? (object)entity.OrderStatusID : DBNull.Value);   cmd.Parameters.Add(pOrderStatusID); 
                SqlParameter pSetDate = new SqlParameter("@SetDate", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "SetDate", DataRowVersion.Current, (object)entity.SetDate != null ? (object)entity.SetDate : DBNull.Value);   cmd.Parameters.Add(pSetDate); 
                SqlParameter pSetByID = new SqlParameter("@SetByID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "SetByID", DataRowVersion.Current, (object)entity.SetByID != null ? (object)entity.SetByID : DBNull.Value);   cmd.Parameters.Add(pSetByID); 
                SqlParameter pComment = new SqlParameter("@Comment", System.Data.SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "Comment", DataRowVersion.Current, (object)entity.Comment != null ? (object)entity.Comment : DBNull.Value);   cmd.Parameters.Add(pComment); 
        
            return cmd;
        }

        protected OrderTracking OrderTrackingFromRow(DataRow row)
        {
            var entity = new OrderTracking();

                    entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
                    entity.OrderID = !DBNull.Value.Equals(row["OrderID"]) ? (System.Int64)row["OrderID"] : default(System.Int64);
                    entity.OrderStatusID = !DBNull.Value.Equals(row["OrderStatusID"]) ? (System.Int64)row["OrderStatusID"] : default(System.Int64);
                    entity.SetDate = !DBNull.Value.Equals(row["SetDate"]) ? (System.DateTime)row["SetDate"] : default(System.DateTime);
                    entity.SetByID = !DBNull.Value.Equals(row["SetByID"]) ? (System.Int64)row["SetByID"] : default(System.Int64);
                    entity.Comment = !DBNull.Value.Equals(row["Comment"]) ? (System.String)row["Comment"] : default(System.String);
        
            return entity;
        }
        
    }
}
