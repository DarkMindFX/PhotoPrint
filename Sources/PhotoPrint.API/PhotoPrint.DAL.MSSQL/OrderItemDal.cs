

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
    class OrderItemDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IOrderItemDal))]
    public class OrderItemDal: SQLDal, IOrderItemDal
    {
        public IInitParams CreateInitParams()
        {
            return new OrderItemDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public OrderItem Get(System.Int64? ID)
        {
            OrderItem result = default(OrderItem);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_OrderItem_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = OrderItemFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_OrderItem_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }

                public IList<OrderItem> GetByOrderID(System.Int64 OrderID)
        {
            var entitiesOut = base.GetBy<OrderItem, System.Int64>("p_OrderItem_GetByOrderID", OrderID, "@OrderID", SqlDbType.BigInt, 0, OrderItemFromRow);

            return entitiesOut;
        }
                public IList<OrderItem> GetByImageID(System.Int64 ImageID)
        {
            var entitiesOut = base.GetBy<OrderItem, System.Int64>("p_OrderItem_GetByImageID", ImageID, "@ImageID", SqlDbType.BigInt, 0, OrderItemFromRow);

            return entitiesOut;
        }
                public IList<OrderItem> GetBySizeID(System.Int64? SizeID)
        {
            var entitiesOut = base.GetBy<OrderItem, System.Int64?>("p_OrderItem_GetBySizeID", SizeID, "@SizeID", SqlDbType.BigInt, 0, OrderItemFromRow);

            return entitiesOut;
        }
                public IList<OrderItem> GetByFrameTypeID(System.Int64 FrameTypeID)
        {
            var entitiesOut = base.GetBy<OrderItem, System.Int64>("p_OrderItem_GetByFrameTypeID", FrameTypeID, "@FrameTypeID", SqlDbType.BigInt, 0, OrderItemFromRow);

            return entitiesOut;
        }
                public IList<OrderItem> GetByFrameSizeID(System.Int64? FrameSizeID)
        {
            var entitiesOut = base.GetBy<OrderItem, System.Int64?>("p_OrderItem_GetByFrameSizeID", FrameSizeID, "@FrameSizeID", SqlDbType.BigInt, 0, OrderItemFromRow);

            return entitiesOut;
        }
                public IList<OrderItem> GetByMatID(System.Int64 MatID)
        {
            var entitiesOut = base.GetBy<OrderItem, System.Int64>("p_OrderItem_GetByMatID", MatID, "@MatID", SqlDbType.BigInt, 0, OrderItemFromRow);

            return entitiesOut;
        }
                public IList<OrderItem> GetByMaterialTypeID(System.Int64 MaterialTypeID)
        {
            var entitiesOut = base.GetBy<OrderItem, System.Int64>("p_OrderItem_GetByMaterialTypeID", MaterialTypeID, "@MaterialTypeID", SqlDbType.BigInt, 0, OrderItemFromRow);

            return entitiesOut;
        }
                public IList<OrderItem> GetByMountingTypeID(System.Int64 MountingTypeID)
        {
            var entitiesOut = base.GetBy<OrderItem, System.Int64>("p_OrderItem_GetByMountingTypeID", MountingTypeID, "@MountingTypeID", SqlDbType.BigInt, 0, OrderItemFromRow);

            return entitiesOut;
        }
                public IList<OrderItem> GetByPriceCurrencyID(System.Int64 PriceCurrencyID)
        {
            var entitiesOut = base.GetBy<OrderItem, System.Int64>("p_OrderItem_GetByPriceCurrencyID", PriceCurrencyID, "@PriceCurrencyID", SqlDbType.BigInt, 0, OrderItemFromRow);

            return entitiesOut;
        }
                public IList<OrderItem> GetByPrintingHouseID(System.Int64? PrintingHouseID)
        {
            var entitiesOut = base.GetBy<OrderItem, System.Int64?>("p_OrderItem_GetByPrintingHouseID", PrintingHouseID, "@PrintingHouseID", SqlDbType.BigInt, 0, OrderItemFromRow);

            return entitiesOut;
        }
                public IList<OrderItem> GetByCreatedByID(System.Int64 CreatedByID)
        {
            var entitiesOut = base.GetBy<OrderItem, System.Int64>("p_OrderItem_GetByCreatedByID", CreatedByID, "@CreatedByID", SqlDbType.BigInt, 0, OrderItemFromRow);

            return entitiesOut;
        }
                public IList<OrderItem> GetByModifiedByID(System.Int64? ModifiedByID)
        {
            var entitiesOut = base.GetBy<OrderItem, System.Int64?>("p_OrderItem_GetByModifiedByID", ModifiedByID, "@ModifiedByID", SqlDbType.BigInt, 0, OrderItemFromRow);

            return entitiesOut;
        }
        
        public IList<OrderItem> GetAll()
        {
            IList<OrderItem> result = base.GetAll<OrderItem>("p_OrderItem_GetAll", OrderItemFromRow);

            return result;
        }

        public OrderItem Insert(OrderItem entity) 
        {
            OrderItem entityOut = base.Upsert<OrderItem>("p_OrderItem_Insert", entity, AddUpsertParameters, OrderItemFromRow);

            return entityOut;
        }

        public OrderItem Update(OrderItem entity) 
        {
            OrderItem entityOut = base.Upsert<OrderItem>("p_OrderItem_Update", entity, AddUpsertParameters, OrderItemFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, OrderItem entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pOrderID = new SqlParameter("@OrderID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "OrderID", DataRowVersion.Current, (object)entity.OrderID != null ? (object)entity.OrderID : DBNull.Value);   cmd.Parameters.Add(pOrderID); 
                SqlParameter pImageID = new SqlParameter("@ImageID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ImageID", DataRowVersion.Current, (object)entity.ImageID != null ? (object)entity.ImageID : DBNull.Value);   cmd.Parameters.Add(pImageID); 
                SqlParameter pWidth = new SqlParameter("@Width", System.Data.SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "Width", DataRowVersion.Current, (object)entity.Width != null ? (object)entity.Width : DBNull.Value);   cmd.Parameters.Add(pWidth); 
                SqlParameter pHeight = new SqlParameter("@Height", System.Data.SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "Height", DataRowVersion.Current, (object)entity.Height != null ? (object)entity.Height : DBNull.Value);   cmd.Parameters.Add(pHeight); 
                SqlParameter pSizeID = new SqlParameter("@SizeID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "SizeID", DataRowVersion.Current, (object)entity.SizeID != null ? (object)entity.SizeID : DBNull.Value);   cmd.Parameters.Add(pSizeID); 
                SqlParameter pFrameTypeID = new SqlParameter("@FrameTypeID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "FrameTypeID", DataRowVersion.Current, (object)entity.FrameTypeID != null ? (object)entity.FrameTypeID : DBNull.Value);   cmd.Parameters.Add(pFrameTypeID); 
                SqlParameter pFrameSizeID = new SqlParameter("@FrameSizeID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "FrameSizeID", DataRowVersion.Current, (object)entity.FrameSizeID != null ? (object)entity.FrameSizeID : DBNull.Value);   cmd.Parameters.Add(pFrameSizeID); 
                SqlParameter pMatID = new SqlParameter("@MatID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "MatID", DataRowVersion.Current, (object)entity.MatID != null ? (object)entity.MatID : DBNull.Value);   cmd.Parameters.Add(pMatID); 
                SqlParameter pMaterialTypeID = new SqlParameter("@MaterialTypeID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "MaterialTypeID", DataRowVersion.Current, (object)entity.MaterialTypeID != null ? (object)entity.MaterialTypeID : DBNull.Value);   cmd.Parameters.Add(pMaterialTypeID); 
                SqlParameter pMountingTypeID = new SqlParameter("@MountingTypeID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "MountingTypeID", DataRowVersion.Current, (object)entity.MountingTypeID != null ? (object)entity.MountingTypeID : DBNull.Value);   cmd.Parameters.Add(pMountingTypeID); 
                SqlParameter pItemCount = new SqlParameter("@ItemCount", System.Data.SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "ItemCount", DataRowVersion.Current, (object)entity.ItemCount != null ? (object)entity.ItemCount : DBNull.Value);   cmd.Parameters.Add(pItemCount); 
                SqlParameter pPriceAmountPerItem = new SqlParameter("@PriceAmountPerItem", System.Data.SqlDbType.Decimal, 0, ParameterDirection.Input, false, 0, 0, "PriceAmountPerItem", DataRowVersion.Current, (object)entity.PriceAmountPerItem != null ? (object)entity.PriceAmountPerItem : DBNull.Value);   cmd.Parameters.Add(pPriceAmountPerItem); 
                SqlParameter pPriceCurrencyID = new SqlParameter("@PriceCurrencyID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "PriceCurrencyID", DataRowVersion.Current, (object)entity.PriceCurrencyID != null ? (object)entity.PriceCurrencyID : DBNull.Value);   cmd.Parameters.Add(pPriceCurrencyID); 
                SqlParameter pComments = new SqlParameter("@Comments", System.Data.SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "Comments", DataRowVersion.Current, (object)entity.Comments != null ? (object)entity.Comments : DBNull.Value);   cmd.Parameters.Add(pComments); 
                SqlParameter pPrintingHouseID = new SqlParameter("@PrintingHouseID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "PrintingHouseID", DataRowVersion.Current, (object)entity.PrintingHouseID != null ? (object)entity.PrintingHouseID : DBNull.Value);   cmd.Parameters.Add(pPrintingHouseID); 
                SqlParameter pIsDeleted = new SqlParameter("@IsDeleted", System.Data.SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsDeleted", DataRowVersion.Current, (object)entity.IsDeleted != null ? (object)entity.IsDeleted : DBNull.Value);   cmd.Parameters.Add(pIsDeleted); 
                SqlParameter pCreatedDate = new SqlParameter("@CreatedDate", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "CreatedDate", DataRowVersion.Current, (object)entity.CreatedDate != null ? (object)entity.CreatedDate : DBNull.Value);   cmd.Parameters.Add(pCreatedDate); 
                SqlParameter pCreatedByID = new SqlParameter("@CreatedByID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "CreatedByID", DataRowVersion.Current, (object)entity.CreatedByID != null ? (object)entity.CreatedByID : DBNull.Value);   cmd.Parameters.Add(pCreatedByID); 
                SqlParameter pModifiedDate = new SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "ModifiedDate", DataRowVersion.Current, (object)entity.ModifiedDate != null ? (object)entity.ModifiedDate : DBNull.Value);   cmd.Parameters.Add(pModifiedDate); 
                SqlParameter pModifiedByID = new SqlParameter("@ModifiedByID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ModifiedByID", DataRowVersion.Current, (object)entity.ModifiedByID != null ? (object)entity.ModifiedByID : DBNull.Value);   cmd.Parameters.Add(pModifiedByID); 
        
            return cmd;
        }

        protected OrderItem OrderItemFromRow(DataRow row)
        {
            var entity = new OrderItem();

                    entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
                    entity.OrderID = !DBNull.Value.Equals(row["OrderID"]) ? (System.Int64)row["OrderID"] : default(System.Int64);
                    entity.ImageID = !DBNull.Value.Equals(row["ImageID"]) ? (System.Int64)row["ImageID"] : default(System.Int64);
                    entity.Width = !DBNull.Value.Equals(row["Width"]) ? (System.Int32?)row["Width"] : default(System.Int32?);
                    entity.Height = !DBNull.Value.Equals(row["Height"]) ? (System.Int32?)row["Height"] : default(System.Int32?);
                    entity.SizeID = !DBNull.Value.Equals(row["SizeID"]) ? (System.Int64?)row["SizeID"] : default(System.Int64?);
                    entity.FrameTypeID = !DBNull.Value.Equals(row["FrameTypeID"]) ? (System.Int64)row["FrameTypeID"] : default(System.Int64);
                    entity.FrameSizeID = !DBNull.Value.Equals(row["FrameSizeID"]) ? (System.Int64?)row["FrameSizeID"] : default(System.Int64?);
                    entity.MatID = !DBNull.Value.Equals(row["MatID"]) ? (System.Int64)row["MatID"] : default(System.Int64);
                    entity.MaterialTypeID = !DBNull.Value.Equals(row["MaterialTypeID"]) ? (System.Int64)row["MaterialTypeID"] : default(System.Int64);
                    entity.MountingTypeID = !DBNull.Value.Equals(row["MountingTypeID"]) ? (System.Int64)row["MountingTypeID"] : default(System.Int64);
                    entity.ItemCount = !DBNull.Value.Equals(row["ItemCount"]) ? (System.Int32)row["ItemCount"] : default(System.Int32);
                    entity.PriceAmountPerItem = !DBNull.Value.Equals(row["PriceAmountPerItem"]) ? (System.Decimal)row["PriceAmountPerItem"] : default(System.Decimal);
                    entity.PriceCurrencyID = !DBNull.Value.Equals(row["PriceCurrencyID"]) ? (System.Int64)row["PriceCurrencyID"] : default(System.Int64);
                    entity.Comments = !DBNull.Value.Equals(row["Comments"]) ? (System.String)row["Comments"] : default(System.String);
                    entity.PrintingHouseID = !DBNull.Value.Equals(row["PrintingHouseID"]) ? (System.Int64?)row["PrintingHouseID"] : default(System.Int64?);
                    entity.IsDeleted = !DBNull.Value.Equals(row["IsDeleted"]) ? (System.Boolean)row["IsDeleted"] : default(System.Boolean);
                    entity.CreatedDate = !DBNull.Value.Equals(row["CreatedDate"]) ? (System.DateTime)row["CreatedDate"] : default(System.DateTime);
                    entity.CreatedByID = !DBNull.Value.Equals(row["CreatedByID"]) ? (System.Int64)row["CreatedByID"] : default(System.Int64);
                    entity.ModifiedDate = !DBNull.Value.Equals(row["ModifiedDate"]) ? (System.DateTime?)row["ModifiedDate"] : default(System.DateTime?);
                    entity.ModifiedByID = !DBNull.Value.Equals(row["ModifiedByID"]) ? (System.Int64?)row["ModifiedByID"] : default(System.Int64?);
        
            return entity;
        }
        
    }
}
