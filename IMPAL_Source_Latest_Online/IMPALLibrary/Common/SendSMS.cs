using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using IMPALLibrary.Transactions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using IMPALLibrary.Common;
using System.Collections;
using System.Security.Cryptography;
using System.Net;
using System.IO;
using System.Transactions;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;

namespace IMPALLibrary
{
    public class SendSMS
    {
        class SMSAuthentication
        {
            public string key { get; set; }
            public string campaign { get; set; }
            public string routeid { get; set; }
            public string type { get; set; }
            public string contacts { get; set; }
            public string senderid { get; set; }
            public string msg { get; set; }
            public string template_id { get; set; }

        }

        public void SendingSMStoCustomers(string BranchCode, string MobileNo, string SMS, string TemplateId)
        {
            //Exception obj = new Exception();
            GenerateJSON objEncrDecr = new GenerateJSON();

            string status = string.Empty;
            string EndPointURL = string.Empty;

            string response = string.Empty;

            HttpWebResponse httpWebResponse = null;
            HttpWebRequest httpWebRequest = null;

            //try
            //{
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    string SMSSecretKey = ConfigurationManager.AppSettings["MapTech_SMS_SecretKey"].ToString();
                    string Campaign = ConfigurationManager.AppSettings["MapTech_SMS_Campaigan"].ToString();
                    string RouteId = ConfigurationManager.AppSettings["MapTech_SMS_Route"].ToString();
                    string Type = ConfigurationManager.AppSettings["MapTech_SMS_Type"].ToString();
                    string SenderId = ConfigurationManager.AppSettings["MapTech_SMS_SenderId"].ToString();

                    EndPointURL = ConfigurationManager.AppSettings["MapTech_SMS_URL"].ToString();
                    EndPointURL = EndPointURL + "?key=" + SMSSecretKey + "&campaign=" + Campaign + "&routeid=" + RouteId + "&type=" + Type
                                                + "&contacts=" + MobileNo + "&senderid=" + SenderId + "&msg=" + SMS + "&template_id=" + TemplateId;
                    httpWebRequest = (HttpWebRequest)WebRequest.Create(EndPointURL);
                    httpWebRequest.Method = "GET";
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Accept = "application/json";

                    httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                    using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                    {
                        response = streamReader.ReadToEnd();
                        streamReader.Close();
                    }

                    scope.Complete();
                }
            //}
            //catch (Exception ex)
            //{
            //    string errBranch = BranchCode;
            //    obj = new Exception(errBranch + "_" + ex.Message);
            //    throw obj;
            //}
        }
    }
}
