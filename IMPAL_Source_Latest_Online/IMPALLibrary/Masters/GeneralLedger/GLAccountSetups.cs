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
    public class GLAccountSetups
    { 
        public List<GLAccountHeader> GetControlNumber()
        {
            List<GLAccountHeader> lstControlNumber = new List<GLAccountHeader>();

            GLAccountHeader objNumber = new GLAccountHeader();
            objNumber.Control_Number = "0";
            objNumber.Transaction_Type_Description = "-- Select --";
            lstControlNumber.Add(objNumber);

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select  distinct a.transaction_type_code,control_number, transaction_type_description  from Chart_Of_Account_Control a, transaction_type_master b where b.transaction_type_code=a.transaction_type_code ";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    objNumber = new GLAccountHeader();
                    objNumber.Control_Number = reader[1].ToString();
                    objNumber.Transaction_Type_Description = reader[2].ToString();
                    lstControlNumber.Add(objNumber);
                }
            }

            return lstControlNumber;
        }

        public List<GLAccountHeader> GetSerialNumber(string controlNumber)
        {
            List<GLAccountHeader> lstSerialNumber = new List<GLAccountHeader>();

            GLAccountHeader objSerialNumber = new GLAccountHeader();
            objSerialNumber.Serial_Number = "0";
            objSerialNumber.Serial_Number = "-- Select --";
            lstSerialNumber.Add(objSerialNumber);

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select Serial_Number from Chart_Of_Account_Control ";
            sSQL = sSQL + "where Control_Number = '" + controlNumber + "'";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    objSerialNumber = new GLAccountHeader();
                    objSerialNumber.Serial_Number = reader[0].ToString();
                    lstSerialNumber.Add(objSerialNumber);
                }
            }

            return lstSerialNumber;
        }

        public List<GLAccountHeader> GetAccountHeader(string strControlNumber, string strSerialNumber)
        {
            List<GLAccountHeader> lstAccountHeader = new List<GLAccountHeader>();
            GLAccountHeader objAccount = new GLAccountHeader();
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetAccountSetup");


            ImpalDB.AddInParameter(cmd, "@Control_Number", DbType.String, strControlNumber.Trim());
            ImpalDB.AddInParameter(cmd, "@Serial_Number", DbType.String, strSerialNumber.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    objAccount = new GLAccountHeader();
                    objAccount.Serial_Number = reader["Serial_Number"].ToString();
                    objAccount.Control_Number = reader["Control_Number"].ToString();
                    objAccount.Transaction_Type_Description = reader["Transaction_Type_Description"].ToString();
                    objAccount.Transaction_Type_Code = reader["Transaction_Type_Code"].ToString();
                    objAccount.Debit_GL_Main_Code = reader[2].ToString();
                    objAccount.Debit_GL_Sub_Code = reader[4].ToString();
                    objAccount.Debit_GL_Account_Code = reader[6].ToString();

                    objAccount.GL_Main_Code = reader[7].ToString();
                    objAccount.GL_Sub_Code = reader[8].ToString();
                    objAccount.GL_Account_Code = reader[9].ToString();

                    objAccount.Control_Description = reader["Control_Description"].ToString();

                    lstAccountHeader.Add(objAccount);
                }
            }

            return lstAccountHeader;
        }

        public List<GLAccountHeader> GetTransactionType()
        {
            List<GLAccountHeader> lstTransactionType = new List<GLAccountHeader>();

            GLAccountHeader objTransType = new GLAccountHeader();
            objTransType.Transaction_Type_Code = "0";
            objTransType.Transaction_Type_Description = "-- Select --";
            lstTransactionType.Add(objTransType);

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select Transaction_type_Code,Transaction_type_Description from Transaction_Type_Master ";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    objTransType = new GLAccountHeader();
                    objTransType.Transaction_Type_Code = reader[0].ToString();
                    objTransType.Transaction_Type_Description = reader[1].ToString();
                    lstTransactionType.Add(objTransType);
                }
            }

            return lstTransactionType;
        }

        public List<GLAccountHeader> GetDGLMain(bool assign)
        {
            List<GLAccountHeader> lstDGLMain = new List<GLAccountHeader>();

            GLAccountHeader objDGLMain = new GLAccountHeader();
            objDGLMain.Debit_GL_Main_Code = "0";
            objDGLMain.Debit_GL_Main_Description = "-- Select --";
            lstDGLMain.Add(objDGLMain);

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = string.Empty;

            if (assign)
            {
                sSQL = "select distinct a.Debit_Gl_Main_Code,b.Gl_Main_Description from Chart_Of_account_Control a,Gl_Master b where a.Debit_Gl_Main_Code=b.Gl_Main_Code ";
            }
            else
            {
                sSQL = "select distinct a.Gl_Main_code,b.GL_Main_Description from GL_Account_master a,GL_Master b where a.GL_Main_Code=b.GL_Main_Code ";
            }
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    objDGLMain = new GLAccountHeader();
                    objDGLMain.Debit_GL_Main_Code = reader[0].ToString();
                    objDGLMain.Debit_GL_Main_Description = reader[1].ToString();
                    lstDGLMain.Add(objDGLMain);
                }
            }

            return lstDGLMain;
        }


        public List<GLAccountHeader> GetCGLMain(bool assign)
        {
            List<GLAccountHeader> lstCGLMain = new List<GLAccountHeader>();

            GLAccountHeader objCGLMain = new GLAccountHeader();
            objCGLMain.GL_Main_Code = "0";
            objCGLMain.GL_Main_Description = "-- Select --";
            lstCGLMain.Add(objCGLMain);

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = string.Empty;

            if (assign)
            {
                sSQL = "select distinct a.Credit_Gl_Main_Code,b.Gl_Main_Description from Chart_Of_account_Control a,Gl_Master b where a.Credit_Gl_Main_Code=b.Gl_Main_Code";
            }
            else
            {
                sSQL = "select distinct a.Gl_Main_code,b.GL_Main_Description from GL_Account_master a,Gl_Master b where a.GL_Main_Code=b.GL_Main_Code ";
            }
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    objCGLMain = new GLAccountHeader();
                    objCGLMain.GL_Main_Code = reader[0].ToString();
                    objCGLMain.GL_Main_Description = reader[1].ToString();
                    lstCGLMain.Add(objCGLMain);
                }
            }

            return lstCGLMain;
        }

        public List<GLAccountHeader> GetDGLSub(string strDGLMain, bool assign)
        {
            List<GLAccountHeader> lstDGLSub = new List<GLAccountHeader>();

            GLAccountHeader objDGLSub = new GLAccountHeader();
            objDGLSub.Debit_GL_Sub_Code = "0";
            objDGLSub.Debit_GL_Sub_Description = "-- Select --";
            lstDGLSub.Add(objDGLSub);

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = string.Empty;
            if (assign)
            {
                sSQL = "select distinct a.Debit_Gl_Sub_Code,b.Gl_Sub_Description from Chart_Of_Account_Control a,Gl_Sub_Master b  ";
                sSQL = sSQL + "where a.Debit_Gl_Sub_Code=b.Gl_Sub_Code and a.debit_GL_main_code=" + "'" + strDGLMain + "'" + " and a.Debit_GL_Main_Code=b.gl_main_code ";

            }
            else
            {
                sSQL = "select distinct a.GL_Sub_Code,b.Gl_Sub_Description from GL_Account_Master a,Gl_Sub_Master b ";
                sSQL = sSQL + "where a.Gl_Sub_Code=b.Gl_Sub_Code and a.Gl_Main_Code=" + "'" + strDGLMain + "'" + " and a.gl_main_code=b.gl_main_code ";
            }
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    objDGLSub = new GLAccountHeader();
                    objDGLSub.Debit_GL_Sub_Code = reader[0].ToString();
                    objDGLSub.Debit_GL_Sub_Description = reader[1].ToString();
                    lstDGLSub.Add(objDGLSub);
                }
            }

            return lstDGLSub;
        }


        public List<GLAccountHeader> GetCGLSub(string strCGLMain, bool assign)
        {
            List<GLAccountHeader> lstCGLSub = new List<GLAccountHeader>();

            GLAccountHeader objCGLSub = new GLAccountHeader();
            objCGLSub.GL_Sub_Code = "0";
            objCGLSub.GL_Sub_Description = "-- Select --";
            lstCGLSub.Add(objCGLSub);

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = string.Empty;

            if (assign)
            {
                sSQL = "select distinct a.Credit_Gl_Sub_Code,b.Gl_Sub_Description from Chart_Of_Account_Control a,Gl_Sub_Master b ";
                sSQL = sSQL + "where a.Credit_Gl_Sub_Code=b.Gl_Sub_Code and Credit_GL_main_code=" + "'" + strCGLMain + "'" + " and a.Credit_GL_Main_Code=b.gl_main_code  ";


            }
            else
            {
                sSQL = "select distinct a.Gl_Sub_Code,b.GL_Sub_Description from GL_Account_Master a,GL_Sub_Master b  ";
                sSQL = sSQL + "where a.GL_Sub_Code=b.GL_Sub_Code and a.GL_Main_Code=" + "'" + strCGLMain + "'" + " and a.gl_main_code=b.gl_main_code  ";
            }
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    objCGLSub = new GLAccountHeader();
                    objCGLSub.GL_Sub_Code = reader[0].ToString();
                    objCGLSub.GL_Sub_Description = reader[1].ToString();
                    lstCGLSub.Add(objCGLSub);
                }
            }

            return lstCGLSub;
        }



        public List<GLAccountHeader> GetDGLAccountCode(string strDGLMain, string strDGLSub, string strTransType, bool assign)
        {
            List<GLAccountHeader> lstDGLAccountCode = new List<GLAccountHeader>();

            GLAccountHeader objDGLAccountCode = new GLAccountHeader();
            objDGLAccountCode.Debit_GL_Account_Code = "0";
            objDGLAccountCode.Debit_AccountCode_Description = "-- Select --";
            lstDGLAccountCode.Add(objDGLAccountCode);

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = string.Empty;

            if (assign)
            {
                sSQL = "select distinct a.Debit_GL_Account_Code,b.Description from Chart_Of_Account_Control a,Gl_Account_Master b  ";
                sSQL = sSQL + "where  a.Debit_GL_Account_Code = b.Gl_Account_Code and a.Debit_GL_Sub_Code = b.GL_Sub_Code and ";
                sSQL = sSQL + "Debit_Gl_Main_Code=" + "'" + strDGLMain + "'" + " and Debit_GL_Sub_Code=" + "'" + strDGLSub + "'"; // and Transaction_Type_Code = " + "'" + strTransType + "'";
            }
            else
            {
                sSQL = "select distinct GL_Account_Code,Description from Gl_Account_Master  ";
                sSQL = sSQL + "where  Gl_Main_Code=" + "'" + strDGLMain + "'" + " and GL_Sub_Code=" + "'" + strDGLSub + "'";
            }
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    objDGLAccountCode = new GLAccountHeader();
                    objDGLAccountCode.Debit_GL_Account_Code = reader[0].ToString();
                    objDGLAccountCode.Debit_AccountCode_Description = reader[1].ToString();
                    lstDGLAccountCode.Add(objDGLAccountCode);
                }
            }

            return lstDGLAccountCode;
        }


        public List<GLAccountHeader> GetCGLAccountCode(string strCGLMain, string strCGLSub, bool assign)
        {
            List<GLAccountHeader> lstCGLAccountCode = new List<GLAccountHeader>();

            GLAccountHeader objCGLAccountCode = new GLAccountHeader();
            objCGLAccountCode.GL_Account_Code = "0";
            objCGLAccountCode.Description = "-- Select --";
            lstCGLAccountCode.Add(objCGLAccountCode);

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = string.Empty;

            if (assign)
            {
                sSQL = "select distinct a.Credit_GL_Account_Code,Description from Chart_Of_Account_Control a,Gl_Account_Master b ";
                sSQL = sSQL + "where a.Credit_GL_Account_Code=b.Gl_Account_Code and a.Credit_GL_Sub_Code = b.GL_Sub_Code and ";
                sSQL = sSQL + "Credit_Gl_Main_Code=" + "'" + strCGLMain + "'" + " and Credit_GL_Sub_Code=" + "'" + strCGLSub + "'";

            }
            else
            {
                sSQL = "select distinct GL_Account_Code,Description from Gl_Account_Master ";
                sSQL = sSQL + "where  Gl_Main_Code=" + "'" + strCGLMain + "'" + " and GL_Sub_Code=" + "'" + strCGLSub + "'";
            }
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    objCGLAccountCode = new GLAccountHeader();
                    objCGLAccountCode.GL_Account_Code = reader[0].ToString();
                    objCGLAccountCode.Description = reader[1].ToString();
                    lstCGLAccountCode.Add(objCGLAccountCode);
                }
            }

            return lstCGLAccountCode;
        }


        public GLAccountHeader AddNewGLAccountEntry(ref GLAccountHeader glAccountHeader)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            
            try
            {
                string controlNumber = string.Empty;
                string serialNumber = string.Empty;

                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addaccountsetup");
                ImpalDB.AddInParameter(cmd, "@Debit_GL_Main_Code", DbType.String, glAccountHeader.Debit_GL_Main_Code);
                ImpalDB.AddInParameter(cmd, "@Transaction_Type_Code", DbType.String, glAccountHeader.Transaction_Type_Code);
                ImpalDB.AddInParameter(cmd, "@Debit_Gl_sub_code", DbType.String, glAccountHeader.Debit_GL_Sub_Code);
                ImpalDB.AddInParameter(cmd, "@Control_Description", DbType.String, glAccountHeader.Control_Description);
                ImpalDB.AddInParameter(cmd, "@Debit_GL_Account_Code", DbType.String, glAccountHeader.Debit_GL_Account_Code);
                ImpalDB.AddInParameter(cmd, "@Credit_Gl_Main_Code", DbType.String, glAccountHeader.GL_Main_Code);
                ImpalDB.AddInParameter(cmd, "@Credit_Gl_Sub_Code", DbType.String, glAccountHeader.GL_Sub_Code);
                ImpalDB.AddInParameter(cmd, "@Credit_Gl_Account_Code", DbType.String, glAccountHeader.GL_Account_Code);
                                
                DataSet ds = new DataSet();
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                ds = ImpalDB.ExecuteDataSet(cmd);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    glAccountHeader.Control_Number = ds.Tables[0].Rows[0][0].ToString();
                    glAccountHeader.Serial_Number = ds.Tables[0].Rows[0][1].ToString();
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return glAccountHeader;
        }
    }


    public class GLAccountHeader
    {
        public string Control_Number { get; set; }
        public string Transaction_Type_Description { get; set; }
        public string Serial_Number { get; set; }
        public string Transaction_Type_Code { get; set; }
        public string Control_Description { get; set; }
        public string Debit_GL_Main_Code { get; set; }
        public string Debit_GL_Main_Description { get; set; }
        public string GL_Main_Code { get; set; }
        public string GL_Main_Description { get; set; }
        public string Debit_GL_Sub_Code { get; set; }
        public string Debit_GL_Sub_Description { get; set; }
        public string GL_Sub_Code { get; set; }
        public string GL_Sub_Description { get; set; }
        public string Debit_GL_Account_Code { get; set; }
        public string Debit_AccountCode_Description { get; set; }
        public string GL_Account_Code { get; set; }
        public string Description { get; set; }



    }



}
