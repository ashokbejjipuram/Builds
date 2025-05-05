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
    public class InventoryReports
    {
        public List<ClsTransactionCode> GetTransactionCode(string strBranch)
        {
            List<ClsTransactionCode> trancode = new List<ClsTransactionCode>();
            Database ImpalDB = DataAccess.GetDatabase();
            string strQuery = string.Empty;
            strQuery = "Select distinct a.transaction_type_code,b.Transaction_type_description from inward_header a,transaction_type_master b where a.transaction_type_code = b.transaction_type_code and a.Branch_Code = '" + strBranch + "'";
            trancode.Add(new ClsTransactionCode(string.Empty,string.Empty));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(strQuery);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    ClsTransactionCode tnscode = new ClsTransactionCode();
                    tnscode.ttcode = reader[0].ToString();
                    tnscode.ttdescription = reader[1].ToString();
                    trancode.Add(tnscode); 
                 }
            }
            return trancode;

        }
    }

    public class ClsTransactionCode
    {

        public string _ttcode;
        public string _ttdescription;

        public ClsTransactionCode(string ttcode, string ttdescription)
        {
            _ttcode = ttcode;
            _ttdescription = ttdescription;
        }
        public ClsTransactionCode()
        {

        }

        public string ttcode
        {
            get { return _ttcode; }
            set { _ttcode = value; }
        }
        public string ttdescription
        {
            get { return _ttdescription; }
            set { _ttdescription = value; }
        }
    }
}

