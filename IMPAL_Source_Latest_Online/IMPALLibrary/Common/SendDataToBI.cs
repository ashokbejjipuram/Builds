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
using System.Reflection;

namespace IMPALLibrary
{
    public class SendDataToBI
    {
        class BIAuthentication
        {
            public string clientid { get; set; }
            public string username { get; set; }
        }

        class BIPortalData
        {
            public string Data { get; set; }
        }

        public void SendingDataToBI(string UserId, int Indicator)
        {
            //Exception obj = new Exception();
            GenerateJSON objEncrDecr = new GenerateJSON();

            string Token = string.Empty;
            string EndPointURL = string.Empty;
            string decrSEK = string.Empty;
            string decrSEKplain = string.Empty;
            string bdoAuthtoken = string.Empty;

            string response = string.Empty;
            var AuthValues = new BIAuthentication();
            SalesTransactions salesTransactions = new SalesTransactions();
            HttpWebResponse httpWebResponse = null;
            HttpWebRequest httpWebRequest = null;
            StreamWriter dataStreamWriter = null;
            StreamReader dataStreamReader = null;
            JObject jobj = null;
            string JSONDataFormat = string.Empty;
            string JSONData = string.Empty;
            DataSet Datasetresult;

            if (Indicator == 1)
                UpdateDataTransferStatus("0", UserId);

            //using (TransactionScope scope = DataAccess.NewWriteTransactionScope())
            //{
                string AuthUserName = ConfigurationManager.AppSettings["Prod_BI_UserName"].ToString();
                string AuthPassword = ConfigurationManager.AppSettings["Prod_BI_Password"].ToString();
                string PublicKeyURL = ConfigurationManager.AppSettings["Prod_BI_PublicKeyURL"].ToString();
                string TokenAccessURL = ConfigurationManager.AppSettings["Prod_BI_TokenAccessURL"].ToString();
                string UpdateStockURL = ConfigurationManager.AppSettings["Prod_BI_UpdateStockURL"].ToString();
                string UpdateSalesURL = ConfigurationManager.AppSettings["Prod_BI_UpdateSalesURL"].ToString();

                /*BEGIN SENDING AUTH REQUEST TO BI PORTAL*/

                EndPointURL = PublicKeyURL + "?username=" + AuthUserName;
                httpWebRequest = (HttpWebRequest)WebRequest.Create(EndPointURL);
                httpWebRequest.Method = "GET";

                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                dataStreamReader = new StreamReader(httpWebResponse.GetResponseStream());
                response = dataStreamReader.ReadToEnd();
                dataStreamReader.Close();


                /* USING BI DLL FOR BI DATA */
                //string dllFile = System.Web.HttpContext.Current.Server.MapPath(@"~/App_Code/BrakesIndiaSecurity.dll");
                //var assmebly = Assembly.LoadFile(dllFile);                
                //var type = assmebly.GetType("BrakesIndiaSecurity.BrakesIndiaSecurity");
                //var obj = Activator.CreateInstance(type);
                //var methods =  type.GetMethod("Encrypt"); 
                //var result = methods.Invoke(obj, new object[] { AuthPassword, response.Replace("\"", "").Trim() });

                string ClientScretKeyEncr = RSAEncryption(response.Replace("\"", "").Trim(), AuthPassword);

                UpdateDataTransferStatus("1", "");

                /*END SENDING AUTH REQUEST TO BI PORTAL*/

                /*BEGIN SENDING SECRET KEY FOR TOKEN TO BI PORTAL*/

                if (httpWebRequest != null)
                {
                    httpWebRequest.Headers.Clear();
                    httpWebRequest = null;
                }

                EndPointURL = TokenAccessURL + "?username=" + AuthUserName + "&password=" + ClientScretKeyEncr;
                httpWebRequest = (HttpWebRequest)WebRequest.Create(EndPointURL);
                httpWebRequest.Method = "GET";

                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                dataStreamReader = new StreamReader(httpWebResponse.GetResponseStream());
                response = dataStreamReader.ReadToEnd();
                dataStreamReader.Close();

                jobj = JObject.Parse(response);

                Token = jobj["Token"].ToString().Replace("\"", "").Trim();

                UpdateDataTransferStatus("2", Token);

                /*END SENDING SECRET KEY FOR TOKEN TO BI PORTAL*/

                /*BEGIN SENDING STOCK DATA TO BI PORTAL*/

                if (Indicator == 1)
                {
                    Datasetresult = GetBIStockDetails();

                    JSONDataFormat = JsonConvert.SerializeObject(GenerateBIStockJSONData(Datasetresult), Formatting.Indented);
                    JSONData = JSONDataFormat.Substring(3, JSONDataFormat.Length - 4).Trim();

                    if (httpWebRequest != null)
                    {
                        httpWebRequest.Headers.Clear();
                        httpWebRequest = null;
                    }

                    httpWebRequest = (HttpWebRequest)WebRequest.Create(UpdateStockURL);
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Headers.Add("Authorization", "Bearer " + Token);
                    httpWebRequest.Method = "POST";

                    dataStreamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
                    dataStreamWriter.Write(JSONData);
                    dataStreamWriter.Close();

                    httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                    response = string.Empty;

                    dataStreamReader = new StreamReader(httpWebResponse.GetResponseStream());
                    response = dataStreamReader.ReadToEnd();
                    dataStreamReader.Close();

                    UpdateDataTransferStatus("3", "");
                }

                /*END SENDING STOCK DATA TO BI PORTAL*/

                /*BEGIN SENDING SALES DATA TO BI PORTAL*/

                if (httpWebRequest != null)
                {
                    httpWebRequest.Headers.Clear();
                    httpWebRequest = null;
                }

                JSONDataFormat = string.Empty;
                JSONData = string.Empty;
                response = string.Empty;

                Datasetresult = GetBISalesDetails();

                JSONDataFormat = JsonConvert.SerializeObject(GenerateBISalesJSONData(Datasetresult), Formatting.Indented);
                JSONData = JSONDataFormat.Substring(3, JSONDataFormat.Length - 4).Trim();                

                httpWebRequest = (HttpWebRequest)WebRequest.Create(UpdateSalesURL);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Headers.Add("Authorization", "Bearer " + Token);
                httpWebRequest.Method = "POST";

                dataStreamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
                dataStreamWriter.Write(JSONData);
                dataStreamWriter.Close();

                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();                

                dataStreamReader = new StreamReader(httpWebResponse.GetResponseStream());
                response = dataStreamReader.ReadToEnd();
                dataStreamReader.Close();

                UpdateDataTransferStatus("4", "");

                /*END SENDING SALES DATA TO BI PORTAL*/

            //    scope.Complete();
            //}
        }

        public string RSAEncryption(string publicKey, string Password)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] cipherbytes;
            rsa.FromXmlString(publicKey);
            cipherbytes = rsa.Encrypt(Encoding.Unicode.GetBytes(Password), false);

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < cipherbytes.Length; i++)
            {
                builder.Append(cipherbytes[i].ToString() + ",");
            }

            return builder.ToString().Substring(0, builder.Length - 1);
        }

        public ICollection<StockDataCollection> stockdataCollection { get; set; }

        public ICollection<StockDataCollection> GenerateBIStockJSONData(DataSet ds)
        {
            Exception obj = new Exception();

            try
            {
                this.stockdataCollection = new List<StockDataCollection>
                    { new StockDataCollection {

                        StockDetails = GetStockDetailsItemList(ds)
                    }};
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return this.stockdataCollection;
        }

        public class StockDataCollection
        {
            public List<StockItem> StockDetails { get; set; }
        }

        public class StockItem
        {
            public string CustomerCode { get; set; }
            public string PartNo { get; set; }
            public int StockQuantity { get; set; }
        }

        public List<StockItem> GetStockDetailsItemList(DataSet ds)
        {
            List<StockItem> objItem = new List<StockItem>();

            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                objItem.Add(new StockItem
                {
                    CustomerCode = ds.Tables[0].Rows[i]["CustomerCode"].ToString().Trim(),
                    PartNo = ds.Tables[0].Rows[i]["PartNo"].ToString().Trim(),
                    StockQuantity = Convert.ToInt32(ds.Tables[0].Rows[i]["StockQuantity"].ToString().Trim()),
                });
            }

            return objItem;
        }

        public DataSet GetBIStockDetails()
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();
            try
            {
                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetBrakesIndiaStock_Data");
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                ds = ImpalDB.ExecuteDataSet(cmd);
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SendDataToBI), exp);
            }

            return ds;
        }        





        public ICollection<SalesDataCollection> salesdataCollection { get; set; }

        public ICollection<SalesDataCollection> GenerateBISalesJSONData(DataSet ds)
        {
            Exception obj = new Exception();

            try
            {
                this.salesdataCollection = new List<SalesDataCollection>
                    { new SalesDataCollection {

                        SalesData = GetSalesDetailsItemList(ds)
                    }};
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return this.salesdataCollection;
        }

        public class SalesDataCollection
        {
            public List<SalesItem> SalesData { get; set; }
        }

        public class SalesItem
        {
            public string DistributorCode { get; set; }
            public string DistributorName { get; set; }
            public string DealerCode { get; set; }
            public string DealerName { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string Address3 { get; set; }
            public string Address4 { get; set; }
            public string City { get; set; }
            public string District { get; set; }
            public string State { get; set; }
            public string PinCode { get; set; }
            public string Category { get; set; }
            public string InvoiceNo { get; set; }
            public string PartNo { get; set; }
            public string InvoiceDate { get; set; }
            public int InvoiceQuantity { get; set; }
            public string InvoiceValue { get; set; }
        }

        public List<SalesItem> GetSalesDetailsItemList(DataSet ds)
        {
            List<SalesItem> objItem = new List<SalesItem>();

            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                objItem.Add(new SalesItem
                {
                    DistributorCode = ds.Tables[0].Rows[i]["branch_code"].ToString().Trim(),
                    DistributorName = ds.Tables[0].Rows[i]["branch_name"].ToString().Trim(),
                    DealerCode = ds.Tables[0].Rows[i]["customer_code"].ToString().Trim(),
                    DealerName = ds.Tables[0].Rows[i]["customer_name"].ToString().Trim(),
                    Address1 = ds.Tables[0].Rows[i]["address1"].ToString().Trim(),
                    Address2 = ds.Tables[0].Rows[i]["address2"].ToString().Trim(),
                    Address3 = ds.Tables[0].Rows[i]["address3"].ToString().Trim(),
                    Address4 = ds.Tables[0].Rows[i]["address4"].ToString().Trim(),
                    City = ds.Tables[0].Rows[i]["City"].ToString().Trim(),
                    District = ds.Tables[0].Rows[i]["district"].ToString().Trim(),
                    State = ds.Tables[0].Rows[i]["state_name"].ToString().Trim(),
                    PinCode = ds.Tables[0].Rows[i]["PinCode"].ToString().Trim(),
                    Category = ds.Tables[0].Rows[i]["retail"].ToString().Trim(),
                    InvoiceNo = ds.Tables[0].Rows[i]["document_number"].ToString().Trim(),
                    PartNo = ds.Tables[0].Rows[i]["supplier_part_number"].ToString().Trim(),
                    InvoiceDate = ds.Tables[0].Rows[i]["document_date"].ToString().Trim(),
                    InvoiceQuantity = Convert.ToInt32(ds.Tables[0].Rows[i]["item_quantity"].ToString().Trim()),
                    InvoiceValue = ds.Tables[0].Rows[i]["val"].ToString().Trim(),
                });
            }

            return objItem;
        }

        public DataSet GetBISalesDetails()
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet ds = new DataSet();
            try
            {
                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetBrakesIndiaSales_Data");
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                ds = ImpalDB.ExecuteDataSet(cmd);
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SendDataToBI), exp);
            }

            return ds;
        }

        public void UpdateDataTransferStatus(string Indicator, string Variable)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            try
            {
                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_UpdBI_DataTransfer_Status");
                ImpalDB.AddInParameter(cmd, "@Indicator", DbType.String, Indicator.Trim());
                ImpalDB.AddInParameter(cmd, "@Variable", DbType.String, Variable.Trim());
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                ImpalDB.ExecuteNonQuery(cmd);
            }
            catch (Exception exp)
            {
                Log.WriteException(typeof(SendDataToBI), exp);
            }
        }
    }
}
