

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
    class AddressDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IAddressDal))]
    public class AddressDal: SQLDal, IAddressDal
    {
        public IInitParams CreateInitParams()
        {
            return new AddressDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public Address Get(System.Int64? ID)
        {
            Address result = default(Address);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_Address_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = AddressFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_Address_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }

                public IList<Address> GetByAddressTypeID(System.Int64 AddressTypeID)
        {
            var entitiesOut = base.GetBy<Address, System.Int64>("p_Address_GetByAddressTypeID", AddressTypeID, "@AddressTypeID", SqlDbType.BigInt, 0, AddressFromRow);

            return entitiesOut;
        }
                public IList<Address> GetByCityID(System.Int64 CityID)
        {
            var entitiesOut = base.GetBy<Address, System.Int64>("p_Address_GetByCityID", CityID, "@CityID", SqlDbType.BigInt, 0, AddressFromRow);

            return entitiesOut;
        }
                public IList<Address> GetByCreatedByID(System.Int64 CreatedByID)
        {
            var entitiesOut = base.GetBy<Address, System.Int64>("p_Address_GetByCreatedByID", CreatedByID, "@CreatedByID", SqlDbType.BigInt, 0, AddressFromRow);

            return entitiesOut;
        }
                public IList<Address> GetByModifiedByID(System.Int64? ModifiedByID)
        {
            var entitiesOut = base.GetBy<Address, System.Int64?>("p_Address_GetByModifiedByID", ModifiedByID, "@ModifiedByID", SqlDbType.BigInt, 0, AddressFromRow);

            return entitiesOut;
        }
        
        public IList<Address> GetAll()
        {
            IList<Address> result = base.GetAll<Address>("p_Address_GetAll", AddressFromRow);

            return result;
        }

        public Address Insert(Address entity) 
        {
            Address entityOut = base.Upsert<Address>("p_Address_Insert", entity, AddUpsertParameters, AddressFromRow);

            return entityOut;
        }

        public Address Update(Address entity) 
        {
            Address entityOut = base.Upsert<Address>("p_Address_Update", entity, AddUpsertParameters, AddressFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, Address entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pAddressTypeID = new SqlParameter("@AddressTypeID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "AddressTypeID", DataRowVersion.Current, (object)entity.AddressTypeID != null ? (object)entity.AddressTypeID : DBNull.Value);   cmd.Parameters.Add(pAddressTypeID); 
                SqlParameter pTitle = new SqlParameter("@Title", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "Title", DataRowVersion.Current, (object)entity.Title != null ? (object)entity.Title : DBNull.Value);   cmd.Parameters.Add(pTitle); 
                SqlParameter pCityID = new SqlParameter("@CityID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "CityID", DataRowVersion.Current, (object)entity.CityID != null ? (object)entity.CityID : DBNull.Value);   cmd.Parameters.Add(pCityID); 
                SqlParameter pStreet = new SqlParameter("@Street", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "Street", DataRowVersion.Current, (object)entity.Street != null ? (object)entity.Street : DBNull.Value);   cmd.Parameters.Add(pStreet); 
                SqlParameter pBuildingNo = new SqlParameter("@BuildingNo", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "BuildingNo", DataRowVersion.Current, (object)entity.BuildingNo != null ? (object)entity.BuildingNo : DBNull.Value);   cmd.Parameters.Add(pBuildingNo); 
                SqlParameter pApartmentNo = new SqlParameter("@ApartmentNo", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "ApartmentNo", DataRowVersion.Current, (object)entity.ApartmentNo != null ? (object)entity.ApartmentNo : DBNull.Value);   cmd.Parameters.Add(pApartmentNo); 
                SqlParameter pComment = new SqlParameter("@Comment", System.Data.SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "Comment", DataRowVersion.Current, (object)entity.Comment != null ? (object)entity.Comment : DBNull.Value);   cmd.Parameters.Add(pComment); 
                SqlParameter pCreatedByID = new SqlParameter("@CreatedByID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "CreatedByID", DataRowVersion.Current, (object)entity.CreatedByID != null ? (object)entity.CreatedByID : DBNull.Value);   cmd.Parameters.Add(pCreatedByID); 
                SqlParameter pCreatedDate = new SqlParameter("@CreatedDate", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "CreatedDate", DataRowVersion.Current, (object)entity.CreatedDate != null ? (object)entity.CreatedDate : DBNull.Value);   cmd.Parameters.Add(pCreatedDate); 
                SqlParameter pModifiedByID = new SqlParameter("@ModifiedByID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ModifiedByID", DataRowVersion.Current, (object)entity.ModifiedByID != null ? (object)entity.ModifiedByID : DBNull.Value);   cmd.Parameters.Add(pModifiedByID); 
                SqlParameter pModifiedDate = new SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "ModifiedDate", DataRowVersion.Current, (object)entity.ModifiedDate != null ? (object)entity.ModifiedDate : DBNull.Value);   cmd.Parameters.Add(pModifiedDate); 
                SqlParameter pIsDeleted = new SqlParameter("@IsDeleted", System.Data.SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsDeleted", DataRowVersion.Current, (object)entity.IsDeleted != null ? (object)entity.IsDeleted : DBNull.Value);   cmd.Parameters.Add(pIsDeleted); 
        
            return cmd;
        }

        protected Address AddressFromRow(DataRow row)
        {
            var entity = new Address();

                    entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
                    entity.AddressTypeID = !DBNull.Value.Equals(row["AddressTypeID"]) ? (System.Int64)row["AddressTypeID"] : default(System.Int64);
                    entity.Title = !DBNull.Value.Equals(row["Title"]) ? (System.String)row["Title"] : default(System.String);
                    entity.CityID = !DBNull.Value.Equals(row["CityID"]) ? (System.Int64)row["CityID"] : default(System.Int64);
                    entity.Street = !DBNull.Value.Equals(row["Street"]) ? (System.String)row["Street"] : default(System.String);
                    entity.BuildingNo = !DBNull.Value.Equals(row["BuildingNo"]) ? (System.String)row["BuildingNo"] : default(System.String);
                    entity.ApartmentNo = !DBNull.Value.Equals(row["ApartmentNo"]) ? (System.String)row["ApartmentNo"] : default(System.String);
                    entity.Comment = !DBNull.Value.Equals(row["Comment"]) ? (System.String)row["Comment"] : default(System.String);
                    entity.CreatedByID = !DBNull.Value.Equals(row["CreatedByID"]) ? (System.Int64)row["CreatedByID"] : default(System.Int64);
                    entity.CreatedDate = !DBNull.Value.Equals(row["CreatedDate"]) ? (System.DateTime)row["CreatedDate"] : default(System.DateTime);
                    entity.ModifiedByID = !DBNull.Value.Equals(row["ModifiedByID"]) ? (System.Int64?)row["ModifiedByID"] : default(System.Int64?);
                    entity.ModifiedDate = !DBNull.Value.Equals(row["ModifiedDate"]) ? (System.DateTime?)row["ModifiedDate"] : default(System.DateTime?);
                    entity.IsDeleted = !DBNull.Value.Equals(row["IsDeleted"]) ? (System.Boolean)row["IsDeleted"] : default(System.Boolean);
        
            return entity;
        }
        
    }
}
