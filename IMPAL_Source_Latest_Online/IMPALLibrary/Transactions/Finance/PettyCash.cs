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
using System.Transactions;

namespace IMPALLibrary
{
    public class PettyCashEntity
    {
        public string PettyCashNumber { set; get; }
        public string PettyCashDate { set; get; }
        public string BranchCode { set; get; }
        public string DescriptionName { set; get; }
        public string TotalAmount { set; get; }

        public string ErrorCode { get; set; }
        public string ErrorMsg { get; set; }

        public List<PettyCashItem> Items { get; set; }
    }

    public class PettyCashItem
    {
        public string Serial_Number { get; set; }
        public string Chart_of_Account_Code { set; get; }
        public string Amount { set; get; }
        public string Remarks { set; get; }

    }

    public class ddlPettyCashNumber
    {
        public string Petty_Cash_Number { set; get; }
    }

    public class PettyCashTransactions
    {
        public List<string> GetPettyCahshNumber(string strBranchCode)
        {
            List<String> lstPettyCashNo = new List<String>();
            lstPettyCashNo.Add(string.Empty);
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL = " Select Petty_Cash_Number from Petty_Cash_Header where Branch_Code ='" + strBranchCode + "' order by Petty_Cash_Number desc";
                DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
                cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
                {
                    while (reader.Read())
                    {
                        lstPettyCashNo.Add(Convert.ToString(reader["Petty_Cash_Number"]));
                    }
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }

            return lstPettyCashNo;
        }

        public PettyCashEntity GetPettyCashDetails(string strPettyCashNumber, string strBranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_GetPettyCashDetailsByNumber");
            ImpalDB.AddInParameter(cmd, "@Petty_Cash_Number", DbType.String, strPettyCashNumber);
			ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, strBranchCode);																		 
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            DataSet ds = ImpalDB.ExecuteDataSet(cmd);

            PettyCashEntity pettycashEntity = new PettyCashEntity();
            PettyCashItem pettycashItem = null;

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                if (dr != null)
                {
                    pettycashEntity.PettyCashDate = dr["Petty_Cash_Date"].ToString();
                    pettycashEntity.BranchCode = dr["Branch_Code"].ToString();
                    pettycashEntity.DescriptionName = dr["Remarks"].ToString();
                    pettycashEntity.TotalAmount = TwoDecimalConversion(dr["Amount"].ToString());

                    pettycashEntity.Items = new List<PettyCashItem>();
                }

                if (ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow drItem in ds.Tables[1].Rows)
                    {
                        pettycashItem = new PettyCashItem();

                        pettycashItem.Chart_of_Account_Code = drItem["Chart_of_account_Code"].ToString();
                        pettycashItem.Amount = drItem["Amount"].ToString();
                        pettycashItem.Remarks = drItem["Remarks"].ToString();

                        pettycashEntity.Items.Add(pettycashItem);
                    }
                }
            }
            return pettycashEntity;
        }

        private string TwoDecimalConversion(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
                return "0.00";
            else
                return string.Format("{0:0.00}", Convert.ToDecimal(strValue));
        }

        public int AddPettyCashDetails(ref PettyCashEntity pettycashEntity)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int result = 0;
            string PettyCashNumber = "";

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_AddPettyCash1");
                    ImpalDB.AddInParameter(cmd, "@Petty_Cash_Number", DbType.String, PettyCashNumber);
                    ImpalDB.AddInParameter(cmd, "@Petty_Cash_Date", DbType.String, pettycashEntity.PettyCashDate);
                    ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, pettycashEntity.BranchCode);
                    ImpalDB.AddInParameter(cmd, "@Remarks", DbType.String, pettycashEntity.DescriptionName);
                    ImpalDB.AddInParameter(cmd, "@Amount", DbType.Currency, pettycashEntity.TotalAmount);
                    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    PettyCashNumber = ImpalDB.ExecuteScalar(cmd).ToString();

                    foreach (PettyCashItem pcItem in pettycashEntity.Items)
                    {
                        DbCommand innercmd = ImpalDB.GetStoredProcCommand("usp_AddPettyCash2");
                        ImpalDB.AddInParameter(innercmd, "@petty_Cash_Number", DbType.String, PettyCashNumber);
                        ImpalDB.AddInParameter(innercmd, "@Chart_Of_Account", DbType.String, pcItem.Chart_of_Account_Code);
                        ImpalDB.AddInParameter(innercmd, "@Amount", DbType.Currency, pcItem.Amount);
                        ImpalDB.AddInParameter(innercmd, "@Remarks", DbType.String, pcItem.Remarks);
                        ImpalDB.AddInParameter(innercmd, "@Indicator", DbType.String, "");
                        //objValue[i].Chart_of_Account_Code
                        innercmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(innercmd);
                    }

                    DbCommand cmdGL = ImpalDB.GetStoredProcCommand("usp_addglpc1");

                    ImpalDB.AddInParameter(cmdGL, "@doc_no", DbType.String, PettyCashNumber);
                    ImpalDB.AddInParameter(cmdGL, "@Branch_Code", DbType.String, pettycashEntity.BranchCode);
                    cmdGL.CommandTimeout = ConnectionTimeOut.TimeOut;
                    ImpalDB.ExecuteNonQuery(cmdGL);

                    pettycashEntity.PettyCashNumber = PettyCashNumber;
                    pettycashEntity.ErrorCode = "0";
                    pettycashEntity.ErrorMsg = "";
                    result = 1;
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                result = -1;
                throw exp;
            }
            return result;
        }
    }
}
