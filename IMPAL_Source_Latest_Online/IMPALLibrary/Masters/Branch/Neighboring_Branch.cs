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
    #region This class is used for Branch Master database get/ins/update methods.
    [Serializable]
    public class Neighboring_Branch
    {
        public Neighboring_Branch()
        { 
        
        }

        public List<NeighboringBranchItems> GetNeignboringBranchDetails(string strBranchCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            
            List<NeighboringBranchItems> objGetNeighboringDetaails = new List<NeighboringBranchItems>();
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();

                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetNeighbouringBranch");
                ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, strBranchCode);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        NeighboringBranchItems objTransState = new NeighboringBranchItems();
                        objTransState.Priority = Convert.ToInt32(reader["Priority"].ToString());
                        objTransState.Freight_Percent = Convert.ToDouble(reader["freight_percent"].ToString());
                        objTransState.Neighboring_Branch_Code = reader["NBC"].ToString();
                        objTransState.Neighboring_Branch_Name = reader["NBN"].ToString();
                        objGetNeighboringDetaails.Add(objTransState);
                    }
                }

            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return objGetNeighboringDetaails;
        }


        public int InsBranchDetails(List<NeighboringBranchItems> objInsValue, string strBranchCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            int iRowCount = -1;

            Database ImpalDB = DataAccess.GetDatabase();

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    for (int i = 0; i < objInsValue.Count; i++)
                    {
                        DbCommand cmd = ImpalDB.GetStoredProcCommand("Usp_addnbr");
                        ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                        ImpalDB.AddInParameter(cmd, "@Nbr_Code", DbType.String, objInsValue[i].Neighboring_Branch_Code);

                        if (objInsValue[i].Priority.ToString() == "")
                            ImpalDB.AddInParameter(cmd, "@Priority", DbType.Int32, System.DBNull.Value);
                        else
                            ImpalDB.AddInParameter(cmd, "@Priority", DbType.Int32, Convert.ToInt32(objInsValue[i].Priority));

                        if (objInsValue[i].Freight_Percent.ToString() == "")
                            ImpalDB.AddInParameter(cmd, "@Freight_Percent", DbType.Double, System.DBNull.Value);
                        else
                            ImpalDB.AddInParameter(cmd, "@Freight_Percent", DbType.Double, Convert.ToDouble(objInsValue[i].Freight_Percent));
                        cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                        iRowCount = ImpalDB.ExecuteNonQuery(cmd);
                    }
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return iRowCount;
        }
    }

    #endregion


    #region This class is used for Branch Master Custom-DataType
    [Serializable]
    public class NeighboringBranchItems
    {
        public NeighboringBranchItems()
        { }
        public string Neighboring_Branch_Code {get; set;}
        public string Neighboring_Branch_Name {get; set;}
        public int Priority { get; set; }
        public double Freight_Percent { get; set; }
        public string Neighboring_Branch { get; set; }
          
    }
    #endregion
}
