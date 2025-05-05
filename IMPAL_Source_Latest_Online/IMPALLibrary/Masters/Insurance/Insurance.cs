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
using IMPALLibrary;

namespace IMPALLibrary.Masters.Insurance
{
    public class Insurances
    {
        #region To get ClaimNumber
        public List<Insurance> GetClaimNumber(string strBranchCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            List<Insurance> lstClaimNumber = new List<Insurance>();
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                //string sSQL = "select distinct(Claim_Reference_Number) from Insurance_Claims order by Claim_Reference_Number desc";
                string sSQL = "select distinct(Claim_Reference_Number) from Insurance_Claims where SUBSTRING(convert(nvarchar,datestamp,103),7,4) between " + DateTime.Now.AddYears(-2).ToString("yyyy") + " and " + DateTime.Now.ToString("yyyy") + " and Branch_Code = '" + strBranchCode + "' order by Claim_Reference_Number desc";

                lstClaimNumber.Add(new Insurance(""));
                DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
                cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
                {
                    while (reader.Read())
                    {
                        lstClaimNumber.Add(new Insurance(reader[0].ToString()));
                    }
                }
                return lstClaimNumber;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return lstClaimNumber;
        }
        #endregion


        #region To get Classficationcode
        public List<ClassificationCode> GetClassificationCode(string strBranchCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            List<ClassificationCode> lstClassificationCode = new List<ClassificationCode>();
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL = "select distinct(Classification_Code) from Insurance_Claims where Classification_Code is not null and Branch_Code = '" + strBranchCode + "'";
                lstClassificationCode.Add(new ClassificationCode(""));
                DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
                cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
                {
                    while (reader.Read())
                    {
                        lstClassificationCode.Add(new ClassificationCode(reader[0].ToString()));
                    }
                }
                return lstClassificationCode;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return lstClassificationCode;
        }
        #endregion
    }
    

   public class Insurance
   {
       public Insurance(string ClaimNumber)
       {
           _ClaimNumber = ClaimNumber;
       }
      
       public Insurance()
        {

        }
       private string _ClaimNumber;
      
       public string ClaimNumber
       {
           get { return _ClaimNumber; }
           set { _ClaimNumber = value; }
       }
      
   }

   public class ClassificationCode
   {
       public ClassificationCode(string ClassificationCode)
       {
           _ClassificationCode = ClassificationCode;
       }

       public ClassificationCode()
       {

       }
       private string _ClassificationCode;

       public string ClassificationCodes
       {
           get { return _ClassificationCode; }
           set { _ClassificationCode = value; }
       }

   }

}
