


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
    class PrintingHouseAddressDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IPrintingHouseAddressDal))]
    public class PrintingHouseAddressDal: SQLDal, IPrintingHouseAddressDal
    {
        public IInitParams CreateInitParams()
        {
            return new PrintingHouseAddressDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public PrintingHouseAddress Get(System.Int64 PrintingHouseID,System.Int64 AddressID)
        {
            PrintingHouseAddress result = default(PrintingHouseAddress);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_PrintingHouseAddress_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@PrintingHouseID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, PrintingHouseID);
            
                            AddParameter(   cmd, "@AddressID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, AddressID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = PrintingHouseAddressFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64 PrintingHouseID,System.Int64 AddressID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_PrintingHouseAddress_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@PrintingHouseID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, PrintingHouseID);
            
                            AddParameter(   cmd, "@AddressID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, AddressID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }


                public IList<PrintingHouseAddress> GetByPrintingHouseID(System.Int64 PrintingHouseID)
        {
            var entitiesOut = base.GetBy<PrintingHouseAddress, System.Int64>("p_PrintingHouseAddress_GetByPrintingHouseID", PrintingHouseID, "@PrintingHouseID", SqlDbType.BigInt, 0, PrintingHouseAddressFromRow);

            return entitiesOut;
        }
                public IList<PrintingHouseAddress> GetByAddressID(System.Int64 AddressID)
        {
            var entitiesOut = base.GetBy<PrintingHouseAddress, System.Int64>("p_PrintingHouseAddress_GetByAddressID", AddressID, "@AddressID", SqlDbType.BigInt, 0, PrintingHouseAddressFromRow);

            return entitiesOut;
        }
        
        public IList<PrintingHouseAddress> GetAll()
        {
            IList<PrintingHouseAddress> result = base.GetAll<PrintingHouseAddress>("p_PrintingHouseAddress_GetAll", PrintingHouseAddressFromRow);

            return result;
        }

        public PrintingHouseAddress Insert(PrintingHouseAddress entity) 
        {
            PrintingHouseAddress entityOut = base.Upsert<PrintingHouseAddress>("p_PrintingHouseAddress_Insert", entity, AddUpsertParameters, PrintingHouseAddressFromRow);

            return entityOut;
        }

        public PrintingHouseAddress Update(PrintingHouseAddress entity) 
        {
            PrintingHouseAddress entityOut = base.Upsert<PrintingHouseAddress>("p_PrintingHouseAddress_Update", entity, AddUpsertParameters, PrintingHouseAddressFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, PrintingHouseAddress entity)
        {
                SqlParameter pPrintingHouseID = new SqlParameter("@PrintingHouseID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "PrintingHouseID", DataRowVersion.Current, (object)entity.PrintingHouseID != null ? (object)entity.PrintingHouseID : DBNull.Value);   cmd.Parameters.Add(pPrintingHouseID); 
                SqlParameter pAddressID = new SqlParameter("@AddressID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "AddressID", DataRowVersion.Current, (object)entity.AddressID != null ? (object)entity.AddressID : DBNull.Value);   cmd.Parameters.Add(pAddressID); 
                SqlParameter pIsPrimary = new SqlParameter("@IsPrimary", System.Data.SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsPrimary", DataRowVersion.Current, (object)entity.IsPrimary != null ? (object)entity.IsPrimary : DBNull.Value);   cmd.Parameters.Add(pIsPrimary); 
        
            return cmd;
        }

        protected PrintingHouseAddress PrintingHouseAddressFromRow(DataRow row)
        {
            var entity = new PrintingHouseAddress();

                    entity.PrintingHouseID = !DBNull.Value.Equals(row["PrintingHouseID"]) ? (System.Int64)row["PrintingHouseID"] : default(System.Int64);
                    entity.AddressID = !DBNull.Value.Equals(row["AddressID"]) ? (System.Int64)row["AddressID"] : default(System.Int64);
                    entity.IsPrimary = !DBNull.Value.Equals(row["IsPrimary"]) ? (System.Boolean)row["IsPrimary"] : default(System.Boolean);
        
            return entity;
        }
        
    }
}
