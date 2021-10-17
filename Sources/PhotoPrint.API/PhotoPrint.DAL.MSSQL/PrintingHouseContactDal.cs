

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
    class PrintingHouseContactDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IPrintingHouseContactDal))]
    public class PrintingHouseContactDal: SQLDal, IPrintingHouseContactDal
    {
        public IInitParams CreateInitParams()
        {
            return new PrintingHouseContactDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public PrintingHouseContact Get(System.Int64 PrintingHouseID,System.Int64 ContactID)
        {
            PrintingHouseContact result = default(PrintingHouseContact);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_PrintingHouseContact_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@PrintingHouseID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, PrintingHouseID);
            
                            AddParameter(   cmd, "@ContactID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ContactID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = PrintingHouseContactFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64 PrintingHouseID,System.Int64 ContactID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_PrintingHouseContact_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@PrintingHouseID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, PrintingHouseID);
            
                            AddParameter(   cmd, "@ContactID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ContactID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }

                public IList<PrintingHouseContact> GetByPrintingHouseID(System.Int64 PrintingHouseID)
        {
            var entitiesOut = base.GetBy<PrintingHouseContact, System.Int64>("p_PrintingHouseContact_GetByPrintingHouseID", PrintingHouseID, "@PrintingHouseID", SqlDbType.BigInt, 0, PrintingHouseContactFromRow);

            return entitiesOut;
        }
                public IList<PrintingHouseContact> GetByContactID(System.Int64 ContactID)
        {
            var entitiesOut = base.GetBy<PrintingHouseContact, System.Int64>("p_PrintingHouseContact_GetByContactID", ContactID, "@ContactID", SqlDbType.BigInt, 0, PrintingHouseContactFromRow);

            return entitiesOut;
        }
        
        public IList<PrintingHouseContact> GetAll()
        {
            IList<PrintingHouseContact> result = base.GetAll<PrintingHouseContact>("p_PrintingHouseContact_GetAll", PrintingHouseContactFromRow);

            return result;
        }

        public PrintingHouseContact Insert(PrintingHouseContact entity) 
        {
            PrintingHouseContact entityOut = base.Upsert<PrintingHouseContact>("p_PrintingHouseContact_Insert", entity, AddUpsertParameters, PrintingHouseContactFromRow);

            return entityOut;
        }

        public PrintingHouseContact Update(PrintingHouseContact entity) 
        {
            PrintingHouseContact entityOut = base.Upsert<PrintingHouseContact>("p_PrintingHouseContact_Update", entity, AddUpsertParameters, PrintingHouseContactFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, PrintingHouseContact entity)
        {
                SqlParameter pPrintingHouseID = new SqlParameter("@PrintingHouseID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "PrintingHouseID", DataRowVersion.Current, (object)entity.PrintingHouseID != null ? (object)entity.PrintingHouseID : DBNull.Value);   cmd.Parameters.Add(pPrintingHouseID); 
                SqlParameter pContactID = new SqlParameter("@ContactID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ContactID", DataRowVersion.Current, (object)entity.ContactID != null ? (object)entity.ContactID : DBNull.Value);   cmd.Parameters.Add(pContactID); 
                SqlParameter pIsPrimary = new SqlParameter("@IsPrimary", System.Data.SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsPrimary", DataRowVersion.Current, (object)entity.IsPrimary != null ? (object)entity.IsPrimary : DBNull.Value);   cmd.Parameters.Add(pIsPrimary); 
        
            return cmd;
        }

        protected PrintingHouseContact PrintingHouseContactFromRow(DataRow row)
        {
            var entity = new PrintingHouseContact();

                    entity.PrintingHouseID = !DBNull.Value.Equals(row["PrintingHouseID"]) ? (System.Int64)row["PrintingHouseID"] : default(System.Int64);
                    entity.ContactID = !DBNull.Value.Equals(row["ContactID"]) ? (System.Int64)row["ContactID"] : default(System.Int64);
                    entity.IsPrimary = !DBNull.Value.Equals(row["IsPrimary"]) ? (System.Boolean)row["IsPrimary"] : default(System.Boolean);
        
            return entity;
        }
        
    }
}
