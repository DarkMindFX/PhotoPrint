

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
    class ImageDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IImageDal))]
    public class ImageDal: SQLDal, IImageDal
    {
        public IInitParams CreateInitParams()
        {
            return new ImageDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public Image Get(System.Int64? ID)
        {
            Image result = default(Image);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_Image_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = ImageFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_Image_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }

                public IList<Image> GetByPriceCurrencyID(System.Int64? PriceCurrencyID)
        {
            var entitiesOut = base.GetBy<Image, System.Int64?>("p_Image_GetByPriceCurrencyID", PriceCurrencyID, "@PriceCurrencyID", SqlDbType.BigInt, 0, ImageFromRow);

            return entitiesOut;
        }
                public IList<Image> GetByCreatedByID(System.Int64 CreatedByID)
        {
            var entitiesOut = base.GetBy<Image, System.Int64>("p_Image_GetByCreatedByID", CreatedByID, "@CreatedByID", SqlDbType.BigInt, 0, ImageFromRow);

            return entitiesOut;
        }
                public IList<Image> GetByModifiedByID(System.Int64? ModifiedByID)
        {
            var entitiesOut = base.GetBy<Image, System.Int64?>("p_Image_GetByModifiedByID", ModifiedByID, "@ModifiedByID", SqlDbType.BigInt, 0, ImageFromRow);

            return entitiesOut;
        }
        
        public IList<Image> GetAll()
        {
            IList<Image> result = base.GetAll<Image>("p_Image_GetAll", ImageFromRow);

            return result;
        }

        public Image Insert(Image entity) 
        {
            Image entityOut = base.Upsert<Image>("p_Image_Insert", entity, AddUpsertParameters, ImageFromRow);

            return entityOut;
        }

        public Image Update(Image entity) 
        {
            Image entityOut = base.Upsert<Image>("p_Image_Update", entity, AddUpsertParameters, ImageFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, Image entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pTitle = new SqlParameter("@Title", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "Title", DataRowVersion.Current, (object)entity.Title != null ? (object)entity.Title : DBNull.Value);   cmd.Parameters.Add(pTitle); 
                SqlParameter pDescription = new SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Current, (object)entity.Description != null ? (object)entity.Description : DBNull.Value);   cmd.Parameters.Add(pDescription); 
                SqlParameter pOriginUrl = new SqlParameter("@OriginUrl", System.Data.SqlDbType.NVarChar, 3000, ParameterDirection.Input, false, 0, 0, "OriginUrl", DataRowVersion.Current, (object)entity.OriginUrl != null ? (object)entity.OriginUrl : DBNull.Value);   cmd.Parameters.Add(pOriginUrl); 
                SqlParameter pMaxWidth = new SqlParameter("@MaxWidth", System.Data.SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "MaxWidth", DataRowVersion.Current, (object)entity.MaxWidth != null ? (object)entity.MaxWidth : DBNull.Value);   cmd.Parameters.Add(pMaxWidth); 
                SqlParameter pMaxHeight = new SqlParameter("@MaxHeight", System.Data.SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "MaxHeight", DataRowVersion.Current, (object)entity.MaxHeight != null ? (object)entity.MaxHeight : DBNull.Value);   cmd.Parameters.Add(pMaxHeight); 
                SqlParameter pPriceAmount = new SqlParameter("@PriceAmount", System.Data.SqlDbType.Decimal, 0, ParameterDirection.Input, false, 0, 0, "PriceAmount", DataRowVersion.Current, (object)entity.PriceAmount != null ? (object)entity.PriceAmount : DBNull.Value);   cmd.Parameters.Add(pPriceAmount); 
                SqlParameter pPriceCurrencyID = new SqlParameter("@PriceCurrencyID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "PriceCurrencyID", DataRowVersion.Current, (object)entity.PriceCurrencyID != null ? (object)entity.PriceCurrencyID : DBNull.Value);   cmd.Parameters.Add(pPriceCurrencyID); 
                SqlParameter pIsDeleted = new SqlParameter("@IsDeleted", System.Data.SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsDeleted", DataRowVersion.Current, (object)entity.IsDeleted != null ? (object)entity.IsDeleted : DBNull.Value);   cmd.Parameters.Add(pIsDeleted); 
                SqlParameter pCreatedByID = new SqlParameter("@CreatedByID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "CreatedByID", DataRowVersion.Current, (object)entity.CreatedByID != null ? (object)entity.CreatedByID : DBNull.Value);   cmd.Parameters.Add(pCreatedByID); 
                SqlParameter pCreatedDate = new SqlParameter("@CreatedDate", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "CreatedDate", DataRowVersion.Current, (object)entity.CreatedDate != null ? (object)entity.CreatedDate : DBNull.Value);   cmd.Parameters.Add(pCreatedDate); 
                SqlParameter pModifiedByID = new SqlParameter("@ModifiedByID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ModifiedByID", DataRowVersion.Current, (object)entity.ModifiedByID != null ? (object)entity.ModifiedByID : DBNull.Value);   cmd.Parameters.Add(pModifiedByID); 
                SqlParameter pModifiedDate = new SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "ModifiedDate", DataRowVersion.Current, (object)entity.ModifiedDate != null ? (object)entity.ModifiedDate : DBNull.Value);   cmd.Parameters.Add(pModifiedDate); 
        
            return cmd;
        }

        protected Image ImageFromRow(DataRow row)
        {
            var entity = new Image();

                    entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
                    entity.Title = !DBNull.Value.Equals(row["Title"]) ? (System.String)row["Title"] : default(System.String);
                    entity.Description = !DBNull.Value.Equals(row["Description"]) ? (System.String)row["Description"] : default(System.String);
                    entity.OriginUrl = !DBNull.Value.Equals(row["OriginUrl"]) ? (System.String)row["OriginUrl"] : default(System.String);
                    entity.MaxWidth = !DBNull.Value.Equals(row["MaxWidth"]) ? (System.Int32?)row["MaxWidth"] : default(System.Int32?);
                    entity.MaxHeight = !DBNull.Value.Equals(row["MaxHeight"]) ? (System.Int32?)row["MaxHeight"] : default(System.Int32?);
                    entity.PriceAmount = !DBNull.Value.Equals(row["PriceAmount"]) ? (System.Decimal?)row["PriceAmount"] : default(System.Decimal?);
                    entity.PriceCurrencyID = !DBNull.Value.Equals(row["PriceCurrencyID"]) ? (System.Int64?)row["PriceCurrencyID"] : default(System.Int64?);
                    entity.IsDeleted = !DBNull.Value.Equals(row["IsDeleted"]) ? (System.Boolean)row["IsDeleted"] : default(System.Boolean);
                    entity.CreatedByID = !DBNull.Value.Equals(row["CreatedByID"]) ? (System.Int64)row["CreatedByID"] : default(System.Int64);
                    entity.CreatedDate = !DBNull.Value.Equals(row["CreatedDate"]) ? (System.DateTime)row["CreatedDate"] : default(System.DateTime);
                    entity.ModifiedByID = !DBNull.Value.Equals(row["ModifiedByID"]) ? (System.Int64?)row["ModifiedByID"] : default(System.Int64?);
                    entity.ModifiedDate = !DBNull.Value.Equals(row["ModifiedDate"]) ? (System.DateTime?)row["ModifiedDate"] : default(System.DateTime?);
        
            return entity;
        }
        
    }
}
