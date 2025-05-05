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
    public class EinvAuthGen
    {
        class EinvAuthentication
        {
            public string clientid { get; set; }
            public string clientsecretencrypted { get; set; }
            public string appsecretkey { get; set; }
        }

        class EinvInvData
        {
            public string Data { get; set; }
        }

        public void EinvoiceAuthentication(string JSONData, string BranchCode, string DocNumber, string Indicator, string Action)
        {
            Exception obj = new Exception();
            GenerateJSON objEncrDecr = new GenerateJSON();

            string status = string.Empty;
            string EndPointURL = string.Empty;
            string decrSEK = string.Empty;
            string decrSEKplain = string.Empty;
            string bdoAuthtoken = string.Empty;
            string ClientId = ConfigurationManager.AppSettings["Prod_Einvoice_Clientid"].ToString();
            string varble = null;
            string EncrytedJSON = string.Empty;

            string response = string.Empty;
            byte[] requestByte = null;
            var JsonRequest = varble;
            var AuthValues = new EinvAuthentication();

            HttpWebResponse httpWebResponse = null;
            HttpWebRequest httpWebRequest = null;
            Stream dataStream = null;
            EinvResponse einvresp = null;
            JObject jobj = null;

            try
            {
                using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
                {
                    Tuple1<string, string, string, string> BDOstatus = GetBDOauthresponse();

                    if (BDOstatus.Item1 == "0")
                    {
                        EndPointURL = ConfigurationManager.AppSettings["Prod_Einvoice_BDOauthURL"].ToString();
                        httpWebRequest = (HttpWebRequest)WebRequest.Create(EndPointURL);
                        httpWebRequest.Method = "POST";
                        httpWebRequest.ContentType = "application/json";
                        httpWebRequest.Accept = "application/json";

                        //string ClientId = ConfigurationManager.AppSettings["Einvoice_Clientid"].ToString();
                        //string ClientSecretKey = ConfigurationManager.AppSettings["Einvoice_ClientSecretkey"].ToString();
                        //string AppSecretKey = ConfigurationManager.AppSettings["Einvoice_AppSecretKey"].ToString();
                        //string publicKey = ConfigurationManager.AppSettings["Einvoice_PublicKey"].ToString();
                        //string ClientScretKeyEncr = ConfigurationManager.AppSettings["Einvoice_ClientSecretKeyEncr"].ToString(); //EncryptionRSA(ClientSecretKey, publicKey);
                        //string AppScretKeyEncr = ConfigurationManager.AppSettings["Einvoice_AppSecretKeyEncr"].ToString(); //EncryptionRSA(AppSecretKey, publicKey);

                        string ClientSecretKey = ConfigurationManager.AppSettings["Prod_Einvoice_ClientSecretkey"].ToString();
                        string AppSecretKey = ConfigurationManager.AppSettings["Prod_Einvoice_AppSecretKey"].ToString();
                        string publicKey = ConfigurationManager.AppSettings["Prod_Einvoice_PublicKey"].ToString();
                        string ClientScretKeyEncr = ConfigurationManager.AppSettings["Prod_Einvoice_ClientSecretKeyEncr"].ToString(); //EncryptionRSA(ClientSecretKey, publicKey);
                        string AppScretKeyEncr = ConfigurationManager.AppSettings["Prod_Einvoice_AppSecretKeyEncr"].ToString(); //EncryptionRSA(AppSecretKey, publicKey);

                        //string ClientScretKeyEncr = objEncrDecr.RSAEncryption(ClientSecretKey);
                        //string AppScretKeyEncr = objEncrDecr.RSAEncryption(AppSecretKey);

                        AuthValues = new EinvAuthentication() { clientid = ClientId, clientsecretencrypted = ClientScretKeyEncr, appsecretkey = AppScretKeyEncr };

                        JsonRequest = JsonConvert.SerializeObject(AuthValues);

                        requestByte = Encoding.UTF8.GetBytes(JsonRequest);

                        dataStream = httpWebRequest.GetRequestStream();
                        dataStream.Write(requestByte, 0, requestByte.Length);
                        dataStream.Close();

                        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                        httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                        using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                        {
                            response = streamReader.ReadToEnd();
                            streamReader.Close();
                        }

                        jobj = JObject.Parse(response.Replace("status", "Status"));

                        status = jobj["Status"].ToString().Replace("\"", "").Trim();

                        if (status.Trim() == "0")
                        {
                            einvresp = new EinvResponse(status, string.Empty, string.Empty, string.Empty, jobj["ErrorMsg"].ToString().Replace("\"", "").Trim());
                            obj = new Exception(String.Format("{0} : {1} : Error Msg : {2}", "Einvoicing Authentication Failed", DocNumber, jobj["ErrorMsg"].ToString().Replace("\"", "").Trim()));
                            throw obj;
                        }
                        else
                        {
                            einvresp = new EinvResponse(status, jobj["bdo_authtoken"].ToString().Replace("\"", "").Trim(), jobj["bdo_sek"].ToString().Replace("\"", "").Trim(),
                                                        jobj["expiry"].ToString().Replace("\"", "").Trim(), string.Empty);

                            Tuple2<string, string> DecodedSEKkey = objEncrDecr.DecryptBySymmetricKey(einvresp.bdoSEK, AppSecretKey);

                            decrSEK = DecodedSEKkey.Item1;
                            decrSEKplain = DecodedSEKkey.Item2;
                            bdoAuthtoken = einvresp.bdoAuthtoken;

                            InsBDOauthresponse(BranchCode, DocNumber, jobj["Status"].ToString().Replace("\"", "").Trim(), jobj["bdo_authtoken"].ToString().Replace("\"", "").Trim(),
                                               jobj["bdo_sek"].ToString().Replace("\"", "").Trim(), jobj["expiry"].ToString().Replace("\"", "").Trim(), decrSEK, decrSEKplain);
                        }
                    }

                    if (!(BDOstatus.Item2 == "" || BDOstatus.Item3 == "" || BDOstatus.Item4 == "") || !(einvresp.bdoAuthtoken == "" || decrSEK == "" || decrSEKplain == ""))
                    {
                        if (!(BDOstatus.Item2 == "" || BDOstatus.Item3 == "" || BDOstatus.Item4 == ""))
                            EncrytedJSON = objEncrDecr.EncryptBySymmetricKey(JSONData, BDOstatus.Item3);
                        else if (!(einvresp.bdoAuthtoken == "" || decrSEK == "" || decrSEKplain == ""))
                            EncrytedJSON = objEncrDecr.EncryptBySymmetricKey(JSONData, decrSEK);
                        else
                            EncrytedJSON = null;

                        if (!(EncrytedJSON == "" || EncrytedJSON == null))
                        {
                            if (httpWebRequest != null)
                            {
                                httpWebRequest.Headers.Clear();
                                httpWebRequest = null;
                            }

                            if (Action == "CANIRN")
                                EndPointURL = ConfigurationManager.AppSettings["Prod_Einvoice_BDOCancelURL"].ToString();
                            else if (Action == "GETIRN")
                                EndPointURL = ConfigurationManager.AppSettings["Prod_Einvoice_BDOgetIRNURL"].ToString();
                            else if (Action == "GENIRN")
                                EndPointURL = ConfigurationManager.AppSettings["Prod_Einvoice_BDOgenIrnURL"].ToString();

                            httpWebRequest = (HttpWebRequest)WebRequest.Create(EndPointURL);
                            httpWebRequest.Method = "POST";
                            httpWebRequest.ContentType = "application/json";
                            httpWebRequest.Accept = "application/json";

                            httpWebRequest.Headers.Add("client_id", ClientId);

                            if (!(BDOstatus.Item2 == "" || BDOstatus.Item3 == "" || BDOstatus.Item4 == ""))
                                httpWebRequest.Headers.Add("bdo_authtoken", BDOstatus.Item2);
                            else if (!(einvresp.bdoAuthtoken == "" || decrSEK == "" || decrSEKplain == ""))
                                httpWebRequest.Headers.Add("bdo_authtoken", einvresp.bdoAuthtoken);

                            if (Action != "GETIRN")
                                httpWebRequest.Headers.Add("action", Action);

                            var AuthValues1 = new EinvInvData() { Data = EncrytedJSON };

                            JsonRequest = JsonConvert.SerializeObject(AuthValues1);

                            requestByte = Encoding.UTF8.GetBytes(JsonRequest);

                            dataStream = httpWebRequest.GetRequestStream();
                            dataStream.Write(requestByte, 0, requestByte.Length);
                            dataStream.Close();

                            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                            httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                            response = string.Empty;

                            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                            {
                                response = streamReader.ReadToEnd();
                                streamReader.Close();
                            }

                            if (!response.Contains("status"))
                            {
                                if (httpWebRequest != null)
                                {
                                    httpWebRequest.Headers.Clear();
                                    httpWebRequest = null;
                                }

                                if (httpWebResponse != null)
                                {
                                    httpWebResponse.Headers.Clear();
                                    httpWebResponse = null;
                                }

                                jobj = JObject.Parse(response);

                                if (response.Contains("Duplicate IRN"))
                                {
                                    if (jobj["InfoDtls"] != null)
                                    {
                                        EndPointURL = ConfigurationManager.AppSettings["Prod_Einvoice_BDOgetIRNDuplicateURL"].ToString();

                                        var str = jobj["InfoDtls"].ToString().Replace("\"", "").Replace("=", @""":""").Replace(", ", @""", """).Replace("{", @"{""").Replace("}", @"""}");

                                        JsonRequest = JsonConvert.SerializeObject(str).ToString();                                        

                                        EndPointURL = EndPointURL + jobj["Irn"].ToString().Replace("\"", "").Trim();


                                        httpWebRequest = (HttpWebRequest)WebRequest.Create(EndPointURL);
                                        httpWebRequest.Method = "GET";
                                        httpWebRequest.ContentType = "application/json";
                                        httpWebRequest.Accept = "application/json";

                                        httpWebRequest.Headers.Add("client_id", ClientId);

                                        if (!(BDOstatus.Item2 == "" || BDOstatus.Item3 == "" || BDOstatus.Item4 == ""))
                                            httpWebRequest.Headers.Add("bdo_authtoken", BDOstatus.Item2);
                                        else if (!(einvresp.bdoAuthtoken == "" || decrSEK == "" || decrSEKplain == ""))
                                            httpWebRequest.Headers.Add("bdo_authtoken", einvresp.bdoAuthtoken);

                                        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                                        httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                                        response = string.Empty;

                                        using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                                        {
                                            response = streamReader.ReadToEnd();
                                            streamReader.Close();
                                        }

                                        jobj = JObject.Parse(response.Replace("status", "Status"));

                                        status = jobj["Status"].ToString().Replace("\"", "").Trim();

                                        einvresp = null;
                                    }
                                }
                                else
                                {
                                    obj = new Exception(BranchCode + "_" + DocNumber + "_" + jobj["Error"].ToString());
                                    throw obj;
                                }
                            }
                            else
                            {
                                jobj = JObject.Parse(response.Replace("status", "Status"));

                                status = jobj["Status"].ToString().Replace("\"", "").Trim();

                                einvresp = null;

                                if (status.Trim() == "0")
                                {
                                    if (Action == "GETIRN")
                                    {
                                        einvresp = new EinvResponse(status, string.Empty, string.Empty, string.Empty, jobj["Error"].ToString().Replace("\"", "").Trim());
                                        obj = new Exception(String.Format("{0} : {1} : Error Msg : {2}", string.Empty, DocNumber, jobj["Error"].ToString().Replace("\"", "").Trim()));
                                        throw obj;
                                    }
                                    if (Action == "GENIRN")
                                    {
                                        einvresp = new EinvResponse(status, string.Empty, string.Empty, string.Empty, jobj["Error"].ToString().Replace("\"", "").Trim());
                                        obj = new Exception(String.Format("{0} : {1} : Error Msg : {2}", "Einvoicing JSON Data Upload Failed", DocNumber, jobj["Error"].ToString().Replace("\"", "").Trim()));
                                        throw obj;
                                    }
                                }
                                else
                                {
                                    SalesTransactions salesTransactions = new SalesTransactions();

                                    if (Action == "CANIRN")
                                    {
                                        if (jobj["ErrorMsg"] == null)
                                        {
                                            salesTransactions.AddEinvoiceCancelDetails(BranchCode, DocNumber, jobj["Irn"].ToString().Replace("\"", "").Trim(), jobj["CancelDate"].ToString().Replace("\"", "").Trim());
                                        }
                                        else
                                        {
                                            obj = new Exception(BranchCode + "_" + DocNumber + "_" + jobj["ErrorMsg"].ToString());
                                            throw obj;
                                        }
                                    }
                                    else if (Action == "GETIRN")
                                    {
                                        Tuple2<string, string> Finaloutput = null;

                                        if (!(BDOstatus.Item2 == "" || BDOstatus.Item3 == "" || BDOstatus.Item4 == ""))
                                            Finaloutput = objEncrDecr.DecryptBySymmetricKey(jobj["Data"].ToString().Replace("\"", "").Trim(), BDOstatus.Item4);
                                        else if (!(bdoAuthtoken == "" || decrSEK == "" || decrSEKplain == ""))
                                            Finaloutput = objEncrDecr.DecryptBySymmetricKey(jobj["Data"].ToString().Replace("\"", "").Trim(), decrSEKplain);

                                        string FinalOutputDec = Finaloutput.Item1;
                                        string FinalOutput = Finaloutput.Item2;

                                        jobj = JObject.Parse(FinalOutput);

                                        string SrcQRimagePath = System.Web.HttpContext.Current.Server.MapPath(@"\images\qr_code.png");
                                        string DestQRimagePath = System.Web.HttpContext.Current.Server.MapPath(@"\QRcodes\");
                                        GenerateQRcode genQRcode = new GenerateQRcode();

                                        if (jobj["ErrorMsg"] == null)
                                        {
                                            byte[] imageData = genQRcode.GenerateInvQRCode(jobj["SignedQRCode"].ToString().Replace("\"", "").Trim(), SrcQRimagePath, DestQRimagePath, DocNumber.Replace("/", "_"));

                                            salesTransactions.AddEinvoiceIRNDetails(BranchCode, DocNumber, jobj["AckNo"].ToString().Replace("\"", "").Trim(), jobj["AckDt"].ToString().Replace("\"", "").Trim(),
                                                                                 string.Empty, jobj["Irn"].ToString().Replace("\"", "").Trim(), string.Empty, jobj["EwbNo"].ToString().Replace("\"", "").Trim(),
                                                                                 jobj["EwbDt"].ToString().Replace("\"", "").Trim(), string.Empty,
                                                                                 jobj["SignedQRCode"].ToString().Replace("\"", "").Trim(), jobj["SignedInvoice"].ToString().Replace("\"", "").Trim(), imageData);
                                        }
                                        else
                                        {
                                            obj = new Exception(BranchCode + "_" + DocNumber + "_" + jobj["ErrorMsg"].ToString());
                                            throw obj;
                                        }
                                    }
                                    else if (Action == "GENIRN")
                                    {
                                        Tuple2<string, string> Finaloutput = null;

                                        if (!(BDOstatus.Item2 == "" || BDOstatus.Item3 == "" || BDOstatus.Item4 == ""))
                                            Finaloutput = objEncrDecr.DecryptBySymmetricKey(jobj["Data"].ToString().Replace("\"", "").Trim(), BDOstatus.Item4);
                                        else if (!(bdoAuthtoken == "" || decrSEK == "" || decrSEKplain == ""))
                                            Finaloutput = objEncrDecr.DecryptBySymmetricKey(jobj["Data"].ToString().Replace("\"", "").Trim(), decrSEKplain);

                                        string FinalOutputDec = Finaloutput.Item1;
                                        string FinalOutput = Finaloutput.Item2;

                                        jobj = JObject.Parse(FinalOutput);

                                        string SrcQRimagePath = System.Web.HttpContext.Current.Server.MapPath(@"\images\qr_code.png");
                                        string DestQRimagePath = System.Web.HttpContext.Current.Server.MapPath(@"\QRcodes\");
                                        GenerateQRcode genQRcode = new GenerateQRcode();

                                        if (jobj["ErrorMsg"] == null)
                                        {
                                            byte[] imageData = genQRcode.GenerateInvQRCode(jobj["SignedQRCode"].ToString().Replace("\"", "").Trim(), SrcQRimagePath, DestQRimagePath, DocNumber.Replace("/", "_"));

                                            salesTransactions.AddEinvoiceDetails(BranchCode, DocNumber, jobj["AckNo"].ToString().Replace("\"", "").Trim(), jobj["AckDt"].ToString().Replace("\"", "").Trim(),
                                                                                 jobj["bdoAckNo"].ToString().Replace("\"", "").Trim(), jobj["Irn"].ToString().Replace("\"", "").Trim(),
                                                                                 jobj["QRCode"].ToString().Replace("\"", "").Trim(), jobj["EwbNo"].ToString().Replace("\"", "").Trim(),
                                                                                 jobj["EwbDt"].ToString().Replace("\"", "").Trim(), jobj["Remarks"].ToString().Replace("\"", "").Trim(),
                                                                                 jobj["SignedQRCode"].ToString().Replace("\"", "").Trim(), jobj["SignedInvoice"].ToString().Replace("\"", "").Trim(), imageData);
                                        }
                                        else
                                        {
                                            obj = new Exception(BranchCode + "_" + DocNumber + "_" + jobj["ErrorMsg"].ToString());
                                            throw obj;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        throw obj;
                    }

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                string errBranch = BranchCode;
                obj = new Exception(errBranch + "_" + ex.Message);
                throw obj;
            }
        }

        public void EinvoiceAuthenticationB2C(string B2CQRCodeData, string BranchCode, string DocNumber, string Indicator, string Action)
        {
            string SrcQRimagePath = System.Web.HttpContext.Current.Server.MapPath(@"\images\qr_code.png");
            string DestQRimagePath = System.Web.HttpContext.Current.Server.MapPath(@"\QRcodes\");
            GenerateQRcode genQRcode = new GenerateQRcode();

            byte[] imageData = genQRcode.GenerateInvQRCodeB2C(B2CQRCodeData, SrcQRimagePath, DestQRimagePath, DocNumber.Replace("/", "_"));

            SalesTransactions salesTransactions = new SalesTransactions();
            salesTransactions.AddEinvoiceDetails(BranchCode, DocNumber, "UNREGISTERED", "", "UNREGISTERED", "", "", "", "", "", "", "", imageData);
        }

        public void EinvoiceQRupdate(string DocNumber, string SignedQRCode)
        {
            SalesTransactions salesTransactions = new SalesTransactions();

            try
            {
                string SrcQRimagePath = System.Web.HttpContext.Current.Server.MapPath(@"\images\qr_code.png");
                string DestQRimagePath = System.Web.HttpContext.Current.Server.MapPath(@"\QRcodes\");
                GenerateQRcode genQRcode = new GenerateQRcode();

                byte[] imageData = genQRcode.GenerateInvQRCode(SignedQRCode, SrcQRimagePath, DestQRimagePath, DocNumber.Replace("/", "_"));

                salesTransactions.AddEinvoiceDetailsNew(DocNumber, imageData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsBDOauthresponse(string strBranchCode, string DocumentNumber, string bdo_status, string bdo_authtoken, string bdo_sek, string expiry, string decrSEK, string decrSEKplain)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_AddBDOauthResponse");
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Document_Number", DbType.String, DocumentNumber.Trim());
            ImpalDB.AddInParameter(cmd, "@bdo_status", DbType.String, bdo_status.Trim());
            ImpalDB.AddInParameter(cmd, "@bdo_authtoken", DbType.String, bdo_authtoken.Trim());
            ImpalDB.AddInParameter(cmd, "@bdo_sek", DbType.String, bdo_sek.Trim());
            ImpalDB.AddInParameter(cmd, "@bdo_expiry", DbType.String, expiry.Trim());
            ImpalDB.AddInParameter(cmd, "@bdo_decrSEK", DbType.String, decrSEK.Trim());
            ImpalDB.AddInParameter(cmd, "@bdo_decrSEKplain", DbType.String, decrSEKplain.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }

        public Tuple1<string, string, string, string> GetBDOauthresponse()
        {
            string bdoStatus = string.Empty;
            string bdoAuthtoken = string.Empty;
            string bdoSEK = string.Empty;
            string bdoSEKplain = string.Empty;

            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetSqlStringCommand("Select Status,bdo_authtoken,bdo_decrSEK,bdo_decrSEKplain from BDO_Authentication WITH (NOLOCK) where Convert(Date,Datestamp,103) = '" + DateTime.Now.Date.ToString("MM/dd/yyyy") + "'");
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    bdoStatus = reader[0].ToString();
                    bdoAuthtoken = reader[1].ToString();
                    bdoSEK = reader[2].ToString();
                    bdoSEKplain = reader[3].ToString();
                }
            }

            if (bdoStatus == "" || bdoStatus == null)
                bdoStatus = "0";

            return new Tuple1<string, string, string, string>(bdoStatus, bdoAuthtoken, bdoSEK, bdoSEKplain);
        }
    }
}
