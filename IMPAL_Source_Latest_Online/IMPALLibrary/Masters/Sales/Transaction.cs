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
    public class Transaction
    {

        public List<TransactionItems> GetAllTransactionItems()
        {
            List<TransactionItems> lstTransaction = new List<TransactionItems>();
            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "select transaction_type_code,transaction_type_description  from Transaction_type_master order by transaction_type_description";
            lstTransaction.Add(new TransactionItems("0", string.Empty));
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    lstTransaction.Add(new TransactionItems(reader[0].ToString(), reader[1].ToString()));
                }
            }
            return lstTransaction;
        }

    }
    

    public class TransactionItems
    {
        public TransactionItems(string strtransaction_type_code, string strtransaction_type_description)
        {
          TransactionTypeCode=strtransaction_type_code;
        TransactionTypeDescription=strtransaction_type_description;  
 
        }

        public string TransactionTypeCode{get;set;}
        public string TransactionTypeDescription{get;set;}

                
        
    }
}
