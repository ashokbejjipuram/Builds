#region Namespace
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
using System.IO;
using System.Globalization;
using System.Configuration;

#endregion Namespace


namespace IMPALLibrary
{
    public class Uitility
    {
        #region Private Variables

        private string filePath;

        private string folderPath;

        private string fileNamePrefix;

        private bool isSuccess = true;
        
        private string result = "success";

        private string[] textLineArray = new string[1000];

        private string dateYYYYMM;

        private string dateYYYYMMDD;

        private DateTime dateTimeValue;

        #endregion Private Variables

        #region Public Variables

        #endregion Public Variables

        #region Constructor
        public Uitility() { }

        #endregion Constructor

        #region User Defined Methods
        public string WriteTextFileToServer(string path, string fileName, bool isPrefixDate)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                folderPath = string.Format("{0:yyyyMM}", DateTime.Now);
                fileNamePrefix = string.Format("{0:yyyyMMdd}", DateTime.Now) + fileName + ".txt";

                if (isPrefixDate)
                {
                    filePath = Path.Combine(path, folderPath);

                    IMPALLibrary.Uitility uitility = new IMPALLibrary.Uitility();
                    if (uitility.CheckDirectoryExists(filePath))
                    {
                        filePath = Path.Combine(filePath, fileNamePrefix);
                        TextFileWriter(filePath, fileName);
                        //if (!CheckFileExists(filePath))
                        //    TextFileWriter(filePath, fileName);
                        //else
                        //    result = "File already Exists";
                    }
                }
                else
                {
                    filePath = Path.Combine(path, fileName + ".txt");
                    TextFileWriter(filePath, fileName);
                }

                result = filePath;
                return result;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
                result = "File Download Failed due to an unoccurred exception";
                return result;
            }
        }

        private string TextFileWriter(string filePath, string fileName)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                StreamWriter streamWriter;
                streamWriter = File.AppendText(filePath);
                streamWriter.Flush();
                streamWriter.Close();
                return result;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
                result = "File Download Failed due to an unoccurred exception";
                return result;
            }
        }

        private string TextFileReader(string filePath, string fileName)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                StreamReader streamReader = new StreamReader(filePath);                
                streamReader.Close();
                return result;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
                result = "File Reading Failed due to an unoccurred exception";
                return result;
            }
        }

        public string[] TextLineSplitter(string textLine, char[] filters)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            textLineArray = textLine.Split(filters);
            return textLineArray;
        }

        public bool CheckDirectoryExists(string path)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (!Directory.Exists(path))
                    if (CreateDirectory(path))
                        isSuccess = true;
                    else
                        isSuccess = false;

                return isSuccess;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
                return false;
            }
        }

        public bool CreateDirectory(string path)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Directory.CreateDirectory(path);
                return true;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
                return false;
            }
        }

        public bool CheckFileExists(string filePath)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                if (File.Exists(filePath))
                    isSuccess = true;
                else
                    isSuccess = false;

                return isSuccess;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
                return false;
            }
        }

        public string DateFormatYYYYMM(string dateTime)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                dateTimeValue = DateTime.ParseExact(dateTime, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                dateYYYYMM = string.Format("{0:yyyyMM}", dateTimeValue);
                return dateYYYYMM;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
                return null;
            }
        }

        public string DateFormatYYYYMMDD(string dateTime)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                dateTimeValue = DateTime.ParseExact(dateTime, "dd/MM/yyyy", CultureInfo.InvariantCulture); 
                dateYYYYMMDD = string.Format("{0:yyyyMMdd}", dateTimeValue);
                return dateYYYYMMDD;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
                return null;
            }
        }

        public int CalculateProgressBar(int totalCount, int runningCount)
        {
            int runningCountValue = 0;
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;

            try
            {
                if (totalCount == 100)
                {
                    runningCountValue = runningCount + 1;
                }
                else if (totalCount < 100)
                {
                    runningCountValue = runningCount + 1;
                }
                else if (totalCount > 100)
                {
                    runningCountValue = runningCount + 1;
                }
                else if (totalCount == 200)
                {
                    runningCountValue = runningCount + 1;
                }
                else if (totalCount < 200)
                {
                    runningCountValue = runningCount + 1;
                }
                else if (totalCount > 200)
                {
                    runningCountValue = runningCount + 1;
                }

                return runningCountValue;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
                return runningCountValue;
            }
        }

        #endregion User Defined Methods
    }


    public static class ConnectionTimeOut
    {
        private static Int32 _TimeOut = Convert.ToInt32(ConfigurationSettings.AppSettings["ConnectionTimeOut"]);
        public static int TimeOut
        {
            get { return _TimeOut; }
            set { _TimeOut = value; }
        }
    }
}
