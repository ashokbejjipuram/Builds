using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.ServiceLocation;
using System.Data;
using System.Data.Common;
using System.Collections;

namespace IMPALLibrary
{
    public class SalesTaxes
    {
        public void AddNewSalesTax(string SalesTaxDescription, string SalesTaxIndicator, string SalesTaxPercentage, string SalesTaxReference, string SalesTaxType)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            //// Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addsalestax");
            //ImpalDB.AddInParameter(cmd, GLGroupCode, DbType.String);
            ImpalDB.AddInParameter(cmd, "@Sales_Tax_Description", DbType.String, SalesTaxDescription.Trim());
            ImpalDB.AddInParameter(cmd, "@Sales_Tax_Percentage", DbType.String, SalesTaxPercentage.Trim());
            ImpalDB.AddInParameter(cmd, "@Sales_Tax_Indicator", DbType.String, SalesTaxIndicator.Trim());
            ImpalDB.AddInParameter(cmd, "@Sales_Tax_Reference", DbType.String, SalesTaxReference.Trim());
            ImpalDB.AddInParameter(cmd, "@Sales_Tax_Type", DbType.String, SalesTaxType.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);

        }
        public void UpdateSalesTax(string SalesTaxDescription, string SalesTaxIndicator, string SalesTaxPercentage, string SalesTaxReference, string SalesTaxType)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            //// Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_Updsalestax");
            ImpalDB.AddInParameter(cmd, "@Sales_Tax_Code", DbType.String, SalesTaxType.Trim());
            ImpalDB.AddInParameter(cmd, "@Sales_Tax_Description", DbType.String, SalesTaxDescription.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);

        }

        public List<SalesTax> GetAllSalesTexes(string Filter)
        {
            List<SalesTax> SalesTaxList = new List<SalesTax>();

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = string.Empty;
            sSQL = "select Sales_Tax_Code , Sales_Tax_Description, CAST(ROUND(Sales_Tax_Percentage,2) AS NUMERIC(12,2)) as Sales_Tax_Percentage, " +
                        " Sales_Tax_Indicator, Sales_Tax_Reference ,Sales_Tax_Type from Sales_Tax_Master ";
            if (Filter != "ALL")
                sSQL = sSQL + " Where Sales_Tax_Code ='" + Filter + "'";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    SalesTaxList.Add(new SalesTax(reader["Sales_Tax_Code"].ToString(), reader["Sales_Tax_Description"].ToString(), reader["Sales_Tax_Percentage"].ToString(), reader["Sales_Tax_Indicator"].ToString(), reader["Sales_Tax_Reference"].ToString(), reader["Sales_Tax_Type"].ToString()));
                }
            }
            return SalesTaxList;
        }

        public List<SalesTax> GetAllSalesTexes()
        {
            List<SalesTax> SalesTaxList = new List<SalesTax>();

            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "Select Sales_Tax_Code , Sales_Tax_Description, Sales_Tax_Percentage , " +
                            " Sales_Tax_Indicator, Sales_Tax_Reference,Sales_Tax_Type  from Sales_Tax_Master";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    SalesTaxList.Add(new SalesTax(reader["Sales_Tax_Code"].ToString(), reader["Sales_Tax_Description"].ToString(), reader["Sales_Tax_Percentage"].ToString(), reader["Sales_Tax_Indicator"].ToString(), reader["Sales_Tax_Reference"].ToString(), reader["Sales_Tax_Type"].ToString()));
                }
            }
            return SalesTaxList;
        }
    }

    public class SalesTax
    {
        public SalesTax(string SalesTaxCode, string SalesTaxDescription, string SalesTaxPercentage, string SalesTaxIndicator, string SalesTaxReference,string SalesTaxType)
        {
            _SalesTaxCode = SalesTaxCode;
            _SalesTaxDescription = SalesTaxDescription;
            _SalesTaxPercentage = SalesTaxPercentage;
            _SalesTaxIndicator = SalesTaxIndicator;
            _SalesTaxReference = SalesTaxReference;
            _SalesTaxType = SalesTaxType;

        }
        public SalesTax()
        {

        }
        private string _SalesTaxCode;
        private string _SalesTaxDescription;
        private string _SalesTaxPercentage;
        private string _SalesTaxIndicator;
        private string _SalesTaxReference;
        private string _SalesTaxType;

        public string SalesTaxCode
        {
            get { return _SalesTaxCode; }
            set { _SalesTaxCode = value; }
        }
        public string SalesTaxDescription
        {
            get { return _SalesTaxDescription; }
            set { _SalesTaxDescription = value; }
        }
        public string SalesTaxPercentage
        {
            get { return _SalesTaxPercentage; }
            set { _SalesTaxPercentage = value; }
        }
        public string SalesTaxIndicator
        {
            get { return _SalesTaxIndicator; }
            set { _SalesTaxIndicator = value; }
        }
        public string SalesTaxReference
        {
            get { return _SalesTaxReference; }
            set { _SalesTaxReference = value; }
        }
        public string SalesTaxType
        {
            get { return _SalesTaxType; }
            set { _SalesTaxType = value; }
        }



    }
}
