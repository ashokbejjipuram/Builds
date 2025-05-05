using System;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.Transactions;
namespace IMPALLibrary
{
    public class DataAccess
    {
        public static Database GetDatabase()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Get Database instance
            Database DefaultDb = null;
            try
            {
                DefaultDb = EnterpriseLibraryContainer.Current.GetInstance<Database>();
                if (DefaultDb == null)

                    throw new NotSupportedException("Datastore configuration is not available in the config file.");
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return DefaultDb;
        }

        public static Database GetDatabaseBackUp()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //Get Database instance
            Database DefaultDb = null;
            try
            {
                DefaultDb = EnterpriseLibraryContainer.Current.GetInstance<Database>("IMPALBackupDB");

                if (DefaultDb == null)

                    throw new NotSupportedException("Datastore configuration is not available in the config file.");
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return DefaultDb;
        }

        public static TransactionScope NewReadTransactionScope()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            var transactionOptions = new TransactionOptions();
            transactionOptions.IsolationLevel = IsolationLevel.ReadCommitted;
            transactionOptions.Timeout = TransactionManager.MaximumTimeout;

            return new TransactionScope(TransactionScopeOption.Suppress, transactionOptions);
        }

        public static TransactionScope NewWriteTransactionScope()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            var transactionOptions = new TransactionOptions();
            transactionOptions.IsolationLevel = IsolationLevel.ReadCommitted;
            transactionOptions.Timeout = TransactionManager.MaximumTimeout;

            return new TransactionScope(TransactionScopeOption.Required, transactionOptions);
        }
    }
}