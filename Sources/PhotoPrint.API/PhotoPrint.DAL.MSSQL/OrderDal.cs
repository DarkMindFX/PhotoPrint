


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
    class OrderDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IOrderDal))]
    public class OrderDal: SQLDal, IOrderDal
    {
        public IInitParams CreateInitParams()
        {
            return new OrderDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public Order Get(System.Int64? ID)
        {
            Order result = default(Order);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_Order_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = OrderFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_Order_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }


        public bool Erase(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_Order_Erase", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }

                public IList<Order> GetByManagerID(System.Int64? ManagerID)
        {
            var entitiesOut = base.GetBy<Order, System.Int64?>("p_Order_GetByManagerID", ManagerID, "@ManagerID", SqlDbType.BigInt, 0, OrderFromRow);

            return entitiesOut;
        }
                public IList<Order> GetByUserID(System.Int64 UserID)
        {
            var entitiesOut = base.GetBy<Order, System.Int64>("p_Order_GetByUserID", UserID, "@UserID", SqlDbType.BigInt, 0, OrderFromRow);

            return entitiesOut;
        }
                public IList<Order> GetByContactID(System.Int64 ContactID)
        {
            var entitiesOut = base.GetBy<Order, System.Int64>("p_Order_GetByContactID", ContactID, "@ContactID", SqlDbType.BigInt, 0, OrderFromRow);

            return entitiesOut;
        }
                public IList<Order> GetByDeliveryAddressID(System.Int64 DeliveryAddressID)
        {
            var entitiesOut = base.GetBy<Order, System.Int64>("p_Order_GetByDeliveryAddressID", DeliveryAddressID, "@DeliveryAddressID", SqlDbType.BigInt, 0, OrderFromRow);

            return entitiesOut;
        }
                public IList<Order> GetByDeliveryServiceID(System.Int64 DeliveryServiceID)
        {
            var entitiesOut = base.GetBy<Order, System.Int64>("p_Order_GetByDeliveryServiceID", DeliveryServiceID, "@DeliveryServiceID", SqlDbType.BigInt, 0, OrderFromRow);

            return entitiesOut;
        }
                public IList<Order> GetByCreatedByID(System.Int64 CreatedByID)
        {
            var entitiesOut = base.GetBy<Order, System.Int64>("p_Order_GetByCreatedByID", CreatedByID, "@CreatedByID", SqlDbType.BigInt, 0, OrderFromRow);

            return entitiesOut;
        }
                public IList<Order> GetByModifiedByID(System.Int64? ModifiedByID)
        {
            var entitiesOut = base.GetBy<Order, System.Int64?>("p_Order_GetByModifiedByID", ModifiedByID, "@ModifiedByID", SqlDbType.BigInt, 0, OrderFromRow);

            return entitiesOut;
        }
        
        public IList<Order> GetAll()
        {
            IList<Order> result = base.GetAll<Order>("p_Order_GetAll", OrderFromRow);

            return result;
        }

        public Order Insert(Order entity) 
        {
            Order entityOut = base.Upsert<Order>("p_Order_Insert", entity, AddUpsertParameters, OrderFromRow);

            return entityOut;
        }

        public Order Update(Order entity) 
        {
            Order entityOut = base.Upsert<Order>("p_Order_Update", entity, AddUpsertParameters, OrderFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, Order entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pManagerID = new SqlParameter("@ManagerID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ManagerID", DataRowVersion.Current, (object)entity.ManagerID != null ? (object)entity.ManagerID : DBNull.Value);   cmd.Parameters.Add(pManagerID); 
                SqlParameter pUserID = new SqlParameter("@UserID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "UserID", DataRowVersion.Current, (object)entity.UserID != null ? (object)entity.UserID : DBNull.Value);   cmd.Parameters.Add(pUserID); 
                SqlParameter pContactID = new SqlParameter("@ContactID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ContactID", DataRowVersion.Current, (object)entity.ContactID != null ? (object)entity.ContactID : DBNull.Value);   cmd.Parameters.Add(pContactID); 
                SqlParameter pDeliveryAddressID = new SqlParameter("@DeliveryAddressID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "DeliveryAddressID", DataRowVersion.Current, (object)entity.DeliveryAddressID != null ? (object)entity.DeliveryAddressID : DBNull.Value);   cmd.Parameters.Add(pDeliveryAddressID); 
                SqlParameter pDeliveryServiceID = new SqlParameter("@DeliveryServiceID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "DeliveryServiceID", DataRowVersion.Current, (object)entity.DeliveryServiceID != null ? (object)entity.DeliveryServiceID : DBNull.Value);   cmd.Parameters.Add(pDeliveryServiceID); 
                SqlParameter pComments = new SqlParameter("@Comments", System.Data.SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "Comments", DataRowVersion.Current, (object)entity.Comments != null ? (object)entity.Comments : DBNull.Value);   cmd.Parameters.Add(pComments); 
                SqlParameter pIsDeleted = new SqlParameter("@IsDeleted", System.Data.SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsDeleted", DataRowVersion.Current, (object)entity.IsDeleted != null ? (object)entity.IsDeleted : DBNull.Value);   cmd.Parameters.Add(pIsDeleted); 
                SqlParameter pCreatedDate = new SqlParameter("@CreatedDate", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "CreatedDate", DataRowVersion.Current, (object)entity.CreatedDate != null ? (object)entity.CreatedDate : DBNull.Value);   cmd.Parameters.Add(pCreatedDate); 
                SqlParameter pCreatedByID = new SqlParameter("@CreatedByID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "CreatedByID", DataRowVersion.Current, (object)entity.CreatedByID != null ? (object)entity.CreatedByID : DBNull.Value);   cmd.Parameters.Add(pCreatedByID); 
                SqlParameter pModifiedDate = new SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "ModifiedDate", DataRowVersion.Current, (object)entity.ModifiedDate != null ? (object)entity.ModifiedDate : DBNull.Value);   cmd.Parameters.Add(pModifiedDate); 
                SqlParameter pModifiedByID = new SqlParameter("@ModifiedByID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ModifiedByID", DataRowVersion.Current, (object)entity.ModifiedByID != null ? (object)entity.ModifiedByID : DBNull.Value);   cmd.Parameters.Add(pModifiedByID); 
        
            return cmd;
        }

        protected Order OrderFromRow(DataRow row)
        {
            var entity = new Order();

                    entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
                    entity.ManagerID = !DBNull.Value.Equals(row["ManagerID"]) ? (System.Int64?)row["ManagerID"] : default(System.Int64?);
                    entity.UserID = !DBNull.Value.Equals(row["UserID"]) ? (System.Int64)row["UserID"] : default(System.Int64);
                    entity.ContactID = !DBNull.Value.Equals(row["ContactID"]) ? (System.Int64)row["ContactID"] : default(System.Int64);
                    entity.DeliveryAddressID = !DBNull.Value.Equals(row["DeliveryAddressID"]) ? (System.Int64)row["DeliveryAddressID"] : default(System.Int64);
                    entity.DeliveryServiceID = !DBNull.Value.Equals(row["DeliveryServiceID"]) ? (System.Int64)row["DeliveryServiceID"] : default(System.Int64);
                    entity.Comments = !DBNull.Value.Equals(row["Comments"]) ? (System.String)row["Comments"] : default(System.String);
                    entity.IsDeleted = !DBNull.Value.Equals(row["IsDeleted"]) ? (System.Boolean)row["IsDeleted"] : default(System.Boolean);
                    entity.CreatedDate = !DBNull.Value.Equals(row["CreatedDate"]) ? (System.DateTime)row["CreatedDate"] : default(System.DateTime);
                    entity.CreatedByID = !DBNull.Value.Equals(row["CreatedByID"]) ? (System.Int64)row["CreatedByID"] : default(System.Int64);
                    entity.ModifiedDate = !DBNull.Value.Equals(row["ModifiedDate"]) ? (System.DateTime?)row["ModifiedDate"] : default(System.DateTime?);
                    entity.ModifiedByID = !DBNull.Value.Equals(row["ModifiedByID"]) ? (System.Int64?)row["ModifiedByID"] : default(System.Int64?);
        
            return entity;
        }
        
    }
}
