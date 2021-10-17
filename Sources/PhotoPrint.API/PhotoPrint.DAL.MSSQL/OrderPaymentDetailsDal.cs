

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
    class OrderPaymentDetailsDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IOrderPaymentDetailsDal))]
    public class OrderPaymentDetailsDal: SQLDal, IOrderPaymentDetailsDal
    {
        public IInitParams CreateInitParams()
        {
            return new OrderPaymentDetailsDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public OrderPaymentDetails Get(System.Int64? ID)
        {
            OrderPaymentDetails result = default(OrderPaymentDetails);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_OrderPaymentDetails_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = OrderPaymentDetailsFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_OrderPaymentDetails_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }

                public IList<OrderPaymentDetails> GetByOrderID(System.Int64 OrderID)
        {
            var entitiesOut = base.GetBy<OrderPaymentDetails, System.Int64>("p_OrderPaymentDetails_GetByOrderID", OrderID, "@OrderID", SqlDbType.BigInt, 0, OrderPaymentDetailsFromRow);

            return entitiesOut;
        }
                public IList<OrderPaymentDetails> GetByPaymentMethodID(System.Int64 PaymentMethodID)
        {
            var entitiesOut = base.GetBy<OrderPaymentDetails, System.Int64>("p_OrderPaymentDetails_GetByPaymentMethodID", PaymentMethodID, "@PaymentMethodID", SqlDbType.BigInt, 0, OrderPaymentDetailsFromRow);

            return entitiesOut;
        }
                public IList<OrderPaymentDetails> GetByCreatedByID(System.Int64 CreatedByID)
        {
            var entitiesOut = base.GetBy<OrderPaymentDetails, System.Int64>("p_OrderPaymentDetails_GetByCreatedByID", CreatedByID, "@CreatedByID", SqlDbType.BigInt, 0, OrderPaymentDetailsFromRow);

            return entitiesOut;
        }
                public IList<OrderPaymentDetails> GetByModifiedByID(System.Int64? ModifiedByID)
        {
            var entitiesOut = base.GetBy<OrderPaymentDetails, System.Int64?>("p_OrderPaymentDetails_GetByModifiedByID", ModifiedByID, "@ModifiedByID", SqlDbType.BigInt, 0, OrderPaymentDetailsFromRow);

            return entitiesOut;
        }
        
        public IList<OrderPaymentDetails> GetAll()
        {
            IList<OrderPaymentDetails> result = base.GetAll<OrderPaymentDetails>("p_OrderPaymentDetails_GetAll", OrderPaymentDetailsFromRow);

            return result;
        }

        public OrderPaymentDetails Insert(OrderPaymentDetails entity) 
        {
            OrderPaymentDetails entityOut = base.Upsert<OrderPaymentDetails>("p_OrderPaymentDetails_Insert", entity, AddUpsertParameters, OrderPaymentDetailsFromRow);

            return entityOut;
        }

        public OrderPaymentDetails Update(OrderPaymentDetails entity) 
        {
            OrderPaymentDetails entityOut = base.Upsert<OrderPaymentDetails>("p_OrderPaymentDetails_Update", entity, AddUpsertParameters, OrderPaymentDetailsFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, OrderPaymentDetails entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pOrderID = new SqlParameter("@OrderID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "OrderID", DataRowVersion.Current, (object)entity.OrderID != null ? (object)entity.OrderID : DBNull.Value);   cmd.Parameters.Add(pOrderID); 
                SqlParameter pPaymentMethodID = new SqlParameter("@PaymentMethodID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "PaymentMethodID", DataRowVersion.Current, (object)entity.PaymentMethodID != null ? (object)entity.PaymentMethodID : DBNull.Value);   cmd.Parameters.Add(pPaymentMethodID); 
                SqlParameter pPaymentTransUID = new SqlParameter("@PaymentTransUID", System.Data.SqlDbType.NVarChar, 250, ParameterDirection.Input, false, 0, 0, "PaymentTransUID", DataRowVersion.Current, (object)entity.PaymentTransUID != null ? (object)entity.PaymentTransUID : DBNull.Value);   cmd.Parameters.Add(pPaymentTransUID); 
                SqlParameter pPaymentDateTime = new SqlParameter("@PaymentDateTime", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "PaymentDateTime", DataRowVersion.Current, (object)entity.PaymentDateTime != null ? (object)entity.PaymentDateTime : DBNull.Value);   cmd.Parameters.Add(pPaymentDateTime); 
                SqlParameter pIsDeleted = new SqlParameter("@IsDeleted", System.Data.SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsDeleted", DataRowVersion.Current, (object)entity.IsDeleted != null ? (object)entity.IsDeleted : DBNull.Value);   cmd.Parameters.Add(pIsDeleted); 
                SqlParameter pCreatedDate = new SqlParameter("@CreatedDate", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "CreatedDate", DataRowVersion.Current, (object)entity.CreatedDate != null ? (object)entity.CreatedDate : DBNull.Value);   cmd.Parameters.Add(pCreatedDate); 
                SqlParameter pCreatedByID = new SqlParameter("@CreatedByID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "CreatedByID", DataRowVersion.Current, (object)entity.CreatedByID != null ? (object)entity.CreatedByID : DBNull.Value);   cmd.Parameters.Add(pCreatedByID); 
                SqlParameter pModifiedDate = new SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "ModifiedDate", DataRowVersion.Current, (object)entity.ModifiedDate != null ? (object)entity.ModifiedDate : DBNull.Value);   cmd.Parameters.Add(pModifiedDate); 
                SqlParameter pModifiedByID = new SqlParameter("@ModifiedByID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ModifiedByID", DataRowVersion.Current, (object)entity.ModifiedByID != null ? (object)entity.ModifiedByID : DBNull.Value);   cmd.Parameters.Add(pModifiedByID); 
        
            return cmd;
        }

        protected OrderPaymentDetails OrderPaymentDetailsFromRow(DataRow row)
        {
            var entity = new OrderPaymentDetails();

                    entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
                    entity.OrderID = !DBNull.Value.Equals(row["OrderID"]) ? (System.Int64)row["OrderID"] : default(System.Int64);
                    entity.PaymentMethodID = !DBNull.Value.Equals(row["PaymentMethodID"]) ? (System.Int64)row["PaymentMethodID"] : default(System.Int64);
                    entity.PaymentTransUID = !DBNull.Value.Equals(row["PaymentTransUID"]) ? (System.String)row["PaymentTransUID"] : default(System.String);
                    entity.PaymentDateTime = !DBNull.Value.Equals(row["PaymentDateTime"]) ? (System.DateTime?)row["PaymentDateTime"] : default(System.DateTime?);
                    entity.IsDeleted = !DBNull.Value.Equals(row["IsDeleted"]) ? (System.Boolean)row["IsDeleted"] : default(System.Boolean);
                    entity.CreatedDate = !DBNull.Value.Equals(row["CreatedDate"]) ? (System.DateTime)row["CreatedDate"] : default(System.DateTime);
                    entity.CreatedByID = !DBNull.Value.Equals(row["CreatedByID"]) ? (System.Int64)row["CreatedByID"] : default(System.Int64);
                    entity.ModifiedDate = !DBNull.Value.Equals(row["ModifiedDate"]) ? (System.DateTime?)row["ModifiedDate"] : default(System.DateTime?);
                    entity.ModifiedByID = !DBNull.Value.Equals(row["ModifiedByID"]) ? (System.Int64?)row["ModifiedByID"] : default(System.Int64?);
        
            return entity;
        }
        
    }
}
