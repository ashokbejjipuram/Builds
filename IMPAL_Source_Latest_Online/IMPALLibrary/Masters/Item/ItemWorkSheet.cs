using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;

namespace IMPALLibrary.Masters
{
    public class ItemWorkSheet
    {
        public void ExecuteItemCode(string strSupplierCode,string strBranchCode)
        {
            List<Item> lstItems = new List<Item>();
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = "SELECT Item_Code FROM Item_WorkSheet where substring(item_code,1,3) = '" + strSupplierCode + "' and Branch_Code = '" + strBranchCode + "' Order by Supplier_Part_Number";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sQuery);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    DbCommand dbcmd = ImpalDB.GetStoredProcCommand("usp_worksheet_new");

                    ImpalDB.AddInParameter(dbcmd, "@Branch_Code", DbType.String, strBranchCode);
                    ImpalDB.AddInParameter(dbcmd, "@Item_Code", DbType.String, reader[0].ToString());
                    dbcmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                    int intProcStatus = ImpalDB.ExecuteNonQuery(dbcmd);
                }
            }
        }
    }

    public class ItemWS
    {
        public string Code {set;get;}
        
        public ItemWS(string Codes)
        {
            Code = Codes;

        }
    }
}
