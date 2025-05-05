using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.SqlClient;

namespace IMPALLibrary
{
    public class Backup
    {

        public int BackupFile(string strPath, string strDB)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int result = 0;

            try
            {
                SqlConnection dbConn = null;
                string ConnectionString = ImpalDB.ConnectionString;                               
                dbConn = new SqlConnection(ConnectionString);
                dbConn.Open();

                string Query = "BACKUP DATABASE " + strDB + " TO DISK = '" + strPath + "'";
                SqlCommand dbCmd = new SqlCommand(Query, dbConn);
                dbCmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                dbCmd.ExecuteNonQuery();


                result = 1;
            }
            catch (Exception exp)
            {
                result = -1;
                throw exp;
            }

            return result;

        }

        public int RestoreFile(string strPath, string strDB)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int result = 0;

            try
            {
                SqlConnection dbConn = null;
                string ConnectionString = ImpalDB.ConnectionString;

                //string ConnectionString = "Data Source=20.201.110.166\\IMPAL;Initial Catalog=Master;User ID=sa;Password=csc1234$";
                ConnectionString = ConnectionString.Replace(strDB, "Master");
                dbConn = new SqlConnection(ConnectionString);
                dbConn.Open();

                string Query = "RESTORE DATABASE " + strDB + " FROM DISK = '" + strPath + "'";
                SqlCommand dbCmd = new SqlCommand(Query, dbConn);
                dbCmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                dbCmd.ExecuteNonQuery();

                result = 1;
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
