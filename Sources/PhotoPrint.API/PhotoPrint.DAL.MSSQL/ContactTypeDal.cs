

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
    class ContactTypeDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IContactTypeDal))]
    public class ContactTypeDal: SQLDal, IContactTypeDal
    {
        public IInitParams CreateInitParams()
        {
            return new ContactTypeDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public ContactType Get(System.Int64? ID)
        {
            ContactType result = default(ContactType);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_ContactType_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = ContactTypeFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_ContactType_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }

        
        public IList<ContactType> GetAll()
        {
            IList<ContactType> result = base.GetAll<ContactType>("p_ContactType_GetAll", ContactTypeFromRow);

            return result;
        }

        public ContactType Insert(ContactType entity) 
        {
            ContactType entityOut = base.Upsert<ContactType>("p_ContactType_Insert", entity, AddUpsertParameters, ContactTypeFromRow);

            return entityOut;
        }

        public ContactType Update(ContactType entity) 
        {
            ContactType entityOut = base.Upsert<ContactType>("p_ContactType_Update", entity, AddUpsertParameters, ContactTypeFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, ContactType entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pContactTypeName = new SqlParameter("@ContactTypeName", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "ContactTypeName", DataRowVersion.Current, (object)entity.ContactTypeName != null ? (object)entity.ContactTypeName : DBNull.Value);   cmd.Parameters.Add(pContactTypeName); 
                SqlParameter pIsDeleted = new SqlParameter("@IsDeleted", System.Data.SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsDeleted", DataRowVersion.Current, (object)entity.IsDeleted != null ? (object)entity.IsDeleted : DBNull.Value);   cmd.Parameters.Add(pIsDeleted); 
        
            return cmd;
        }

        protected ContactType ContactTypeFromRow(DataRow row)
        {
            var entity = new ContactType();

                    entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
                    entity.ContactTypeName = !DBNull.Value.Equals(row["ContactTypeName"]) ? (System.String)row["ContactTypeName"] : default(System.String);
                    entity.IsDeleted = !DBNull.Value.Equals(row["IsDeleted"]) ? (System.Boolean)row["IsDeleted"] : default(System.Boolean);
        
            return entity;
        }
        
    }
}
