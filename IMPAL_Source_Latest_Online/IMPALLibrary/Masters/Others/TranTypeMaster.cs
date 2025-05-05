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

    public class TranTypeMasters
    {
        public void AddNewTranTypeMaster(string TranTypeCode, string TranTypeDesc, string TransTypeModuleCode, string ParameterNumber, string AffectSales, string Indicator)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addtransaction");
            ImpalDB.AddInParameter(cmd, "@Transaction_Type_Code", DbType.String, TranTypeCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Transaction_Type_Description", DbType.String, TranTypeDesc.Trim());
            ImpalDB.AddInParameter(cmd, "@Module_indicator", DbType.String, TransTypeModuleCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Parameter_Code", DbType.String, ParameterNumber.Trim());
            ImpalDB.AddInParameter(cmd, "@Affect_Sales_Indicator", DbType.String, AffectSales.Trim());
            ImpalDB.AddInParameter(cmd, "@Revenue_Expense_Indicator", DbType.String, Indicator.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }

        public void UpdateTranTypeMaster(string TranTypeCode, string TranTypeDesc, string TransTypeModuleCode, string ParameterNumber, string AffectSales, string Indicator)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_Updtransaction");
            ImpalDB.AddInParameter(cmd, "@Transaction_Type_Code", DbType.String, TranTypeCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Transaction_Type_Description", DbType.String, TranTypeDesc.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }

        public List<ParameterType> GetParameterList()
        {
            List<ParameterType> objParameterTypeList = new List<ParameterType>();

            string sSql = "SELECT DISTINCT PARAMETER_CODE,PARAMETER_DESCRIPTION FROM PARAMETER ORDER BY PARAMETER_CODE";

            Database ImpalDB = DataAccess.GetDatabase();

            ParameterType ParameterTypeList = new ParameterType();
            ParameterTypeList.ParameterTypeName= "-- Select --";
            ParameterTypeList.ParameterTypeCode = "0";
            objParameterTypeList.Add(ParameterTypeList);
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSql);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    ParameterTypeList = new ParameterType();
                    ParameterTypeList.ParameterTypeCode = reader[0].ToString();
                    ParameterTypeList.ParameterTypeName = reader[1].ToString();
                    objParameterTypeList.Add(ParameterTypeList);
                }
            }
            return objParameterTypeList;
        }


        public List<TranTypeMaster> GetAllTranTypes()
        {
            List<TranTypeMaster> objTranTypes = new List<TranTypeMaster>();
            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "select transaction_Type_code,transaction_Type_description,Module_Indicator,Parameter_Code,Affect_Sales_Indicator,Revenue_Expense_Indicator from transaction_Type_Master";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    objTranTypes.Add(new TranTypeMaster(reader["transaction_Type_code"].ToString(), reader["transaction_Type_description"].ToString(),
                        reader["Module_Indicator"].ToString(), reader["Parameter_Code"].ToString(),
                        reader["Affect_Sales_Indicator"].ToString(), reader["Revenue_Expense_Indicator"].ToString()));
                }
            }
            return objTranTypes;
        }
    }

    public class TranTypeMaster
    {
        public string TranTypeCode { get; set; }
        public string TranTypeDesc { get; set; }
        public string TransTypeModuleCode { get; set; }
        public string TransTypeModule { get; set; }
        public string ParameterNumber { get; set; }
        public string AffectSales { get; set; }
        public string Indicator { get; set; }

        public TranTypeMaster() { }

        //public TranTypeMaster(string pModuleCode, string pModuleName)
        //{
        //    TransTypeModuleCode = pModuleCode;
        //    TransTypeModule = pModuleName;
        //}

        public TranTypeMaster(string pTranTypeCode, string pTranTypeDesc, string pModuleCode, string pParameterNumber, string pAffectSales, string pIndicator)
        {
            TranTypeCode = pTranTypeCode;
            TranTypeDesc = pTranTypeDesc;
            TransTypeModuleCode = pModuleCode;
            //TransTypeModule = pModuleName;
            ParameterNumber = pParameterNumber;
            AffectSales = pAffectSales;
            Indicator = pIndicator;
        }
    }

    public class ParameterType
    {
        public string ParameterTypeCode { get; set; }
        public string ParameterTypeName { get; set; }
    }
}
