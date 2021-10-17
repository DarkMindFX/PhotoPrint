

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
    class ContactDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IContactDal))]
    public class ContactDal: SQLDal, IContactDal
    {
        public IInitParams CreateInitParams()
        {
            return new ContactDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public Contact Get(System.Int64? ID)
        {
            Contact result = default(Contact);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_Contact_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = ContactFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_Contact_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }

                public IList<Contact> GetByContactTypeID(System.Int64 ContactTypeID)
        {
            var entitiesOut = base.GetBy<Contact, System.Int64>("p_Contact_GetByContactTypeID", ContactTypeID, "@ContactTypeID", SqlDbType.BigInt, 0, ContactFromRow);

            return entitiesOut;
        }
                public IList<Contact> GetByCreatedByID(System.Int64 CreatedByID)
        {
            var entitiesOut = base.GetBy<Contact, System.Int64>("p_Contact_GetByCreatedByID", CreatedByID, "@CreatedByID", SqlDbType.BigInt, 0, ContactFromRow);

            return entitiesOut;
        }
                public IList<Contact> GetByModifiedByID(System.Int64? ModifiedByID)
        {
            var entitiesOut = base.GetBy<Contact, System.Int64?>("p_Contact_GetByModifiedByID", ModifiedByID, "@ModifiedByID", SqlDbType.BigInt, 0, ContactFromRow);

            return entitiesOut;
        }
        
        public IList<Contact> GetAll()
        {
            IList<Contact> result = base.GetAll<Contact>("p_Contact_GetAll", ContactFromRow);

            return result;
        }

        public Contact Insert(Contact entity) 
        {
            Contact entityOut = base.Upsert<Contact>("p_Contact_Insert", entity, AddUpsertParameters, ContactFromRow);

            return entityOut;
        }

        public Contact Update(Contact entity) 
        {
            Contact entityOut = base.Upsert<Contact>("p_Contact_Update", entity, AddUpsertParameters, ContactFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, Contact entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pContactTypeID = new SqlParameter("@ContactTypeID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ContactTypeID", DataRowVersion.Current, (object)entity.ContactTypeID != null ? (object)entity.ContactTypeID : DBNull.Value);   cmd.Parameters.Add(pContactTypeID); 
                SqlParameter pTitle = new SqlParameter("@Title", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "Title", DataRowVersion.Current, (object)entity.Title != null ? (object)entity.Title : DBNull.Value);   cmd.Parameters.Add(pTitle); 
                SqlParameter pComment = new SqlParameter("@Comment", System.Data.SqlDbType.NVarChar, 250, ParameterDirection.Input, false, 0, 0, "Comment", DataRowVersion.Current, (object)entity.Comment != null ? (object)entity.Comment : DBNull.Value);   cmd.Parameters.Add(pComment); 
                SqlParameter pValue = new SqlParameter("@Value", System.Data.SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "Value", DataRowVersion.Current, (object)entity.Value != null ? (object)entity.Value : DBNull.Value);   cmd.Parameters.Add(pValue); 
                SqlParameter pIsDeleted = new SqlParameter("@IsDeleted", System.Data.SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsDeleted", DataRowVersion.Current, (object)entity.IsDeleted != null ? (object)entity.IsDeleted : DBNull.Value);   cmd.Parameters.Add(pIsDeleted); 
                SqlParameter pCreatedByID = new SqlParameter("@CreatedByID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "CreatedByID", DataRowVersion.Current, (object)entity.CreatedByID != null ? (object)entity.CreatedByID : DBNull.Value);   cmd.Parameters.Add(pCreatedByID); 
                SqlParameter pCreatedDate = new SqlParameter("@CreatedDate", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "CreatedDate", DataRowVersion.Current, (object)entity.CreatedDate != null ? (object)entity.CreatedDate : DBNull.Value);   cmd.Parameters.Add(pCreatedDate); 
                SqlParameter pModifiedByID = new SqlParameter("@ModifiedByID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ModifiedByID", DataRowVersion.Current, (object)entity.ModifiedByID != null ? (object)entity.ModifiedByID : DBNull.Value);   cmd.Parameters.Add(pModifiedByID); 
                SqlParameter pModifiedDate = new SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "ModifiedDate", DataRowVersion.Current, (object)entity.ModifiedDate != null ? (object)entity.ModifiedDate : DBNull.Value);   cmd.Parameters.Add(pModifiedDate); 
        
            return cmd;
        }

        protected Contact ContactFromRow(DataRow row)
        {
            var entity = new Contact();

                    entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
                    entity.ContactTypeID = !DBNull.Value.Equals(row["ContactTypeID"]) ? (System.Int64)row["ContactTypeID"] : default(System.Int64);
                    entity.Title = !DBNull.Value.Equals(row["Title"]) ? (System.String)row["Title"] : default(System.String);
                    entity.Comment = !DBNull.Value.Equals(row["Comment"]) ? (System.String)row["Comment"] : default(System.String);
                    entity.Value = !DBNull.Value.Equals(row["Value"]) ? (System.String)row["Value"] : default(System.String);
                    entity.IsDeleted = !DBNull.Value.Equals(row["IsDeleted"]) ? (System.Boolean)row["IsDeleted"] : default(System.Boolean);
                    entity.CreatedByID = !DBNull.Value.Equals(row["CreatedByID"]) ? (System.Int64)row["CreatedByID"] : default(System.Int64);
                    entity.CreatedDate = !DBNull.Value.Equals(row["CreatedDate"]) ? (System.DateTime)row["CreatedDate"] : default(System.DateTime);
                    entity.ModifiedByID = !DBNull.Value.Equals(row["ModifiedByID"]) ? (System.Int64?)row["ModifiedByID"] : default(System.Int64?);
                    entity.ModifiedDate = !DBNull.Value.Equals(row["ModifiedDate"]) ? (System.DateTime?)row["ModifiedDate"] : default(System.DateTime?);
        
            return entity;
        }
        
    }
}
