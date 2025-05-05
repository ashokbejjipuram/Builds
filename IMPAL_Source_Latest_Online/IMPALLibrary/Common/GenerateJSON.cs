using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Transactions;

namespace IMPALLibrary.Common
{
    public class GenerateJSON
    {
        public string GenerateInvoiceJSONDataB2C(DataSet ds, string BranchCode)
        {
            string B2CQRCode = string.Empty;

            if (ds.Tables[0].Rows[0]["Customer_Code"] != null)
            {
                B2CQRCode = "upi://pay?pa=impal3@icici&pn=INDIA MOTOR PARTS AND ACCESSORIES LIMITED&am=" + Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[0]["ValDtls_TotVal"].ToString()), 2)
                            + "&cu=INR&tr=" + ds.Tables[0].Rows[0]["Document_Number"].ToString().Trim() + "&tn=SGSTIN:" + ds.Tables[0].Rows[0]["Seller_GST"].ToString().Trim()
                            + ",ACCNUM:IMPAL9" + ds.Tables[0].Rows[0]["Customer_Code"].ToString().Trim() + ",IFSC:ICIC0000106,DOCDATE:" + DateTime.ParseExact(ds.Tables[0].Rows[0]["Document_Date"].ToString().Replace("-", "/").Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                            + ",CGST:" + Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[0]["ValDtls_CgstVal"].ToString().Trim()), 2)
                            + ",SGST:" + Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[0]["ValDtls_SgstVal"].ToString().Trim()), 2)
                            + ",IGST:" + Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[0]["ValDtls_IgstVal"].ToString().Trim()), 2)
                            + ",CESS:" + Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[0]["ValDtls_CesVal"].ToString().Trim()), 2);
            }

            return B2CQRCode;
        }

        public ICollection<InvoiceDataCollection> InvoiceCollection { get; set; }

        public ICollection<InvoiceDataCollection> GenerateInvoiceJSONData(DataSet ds, string BranchCode)
        {
            Exception obj = new Exception();

            try
            {
                using (TransactionScope scope = DataAccess.NewReadTransactionScope())
                {
                    this.InvoiceCollection = new List<InvoiceDataCollection>
                    { new InvoiceDataCollection {

                        TranDtls = new TranDtls { TaxSch = "GST", SupTyp = "B2B", RegRev = "N", EcmGstin = null, IgstonIntra = "N", supplydir = null },

                        DocDtls = new DocDtls { Typ = ds.Tables[0].Rows[0]["Doc_Type"].ToString().Trim(), No = ds.Tables[0].Rows[0]["Document_Number"].ToString().Trim(),
                                                Dt = ds.Tables[0].Rows[0]["Document_Date"].ToString().Trim()},

                        //SellerDtls = new SellerDtls{Gstin="23ADDPT0274H030", LglNm="India Motor Parts & Accessories Limited",TrdNm="India Motor Parts & Accessories Limited",
                        //                            Addr1="6-8, Usha Ganj, Chhawani", Addr2="Indore",
                        //                            Loc="Chhawani", Pin= "452001", Stcd= "23", Ph= "9499014967", Em="chennai@impal.net" },

                        SellerDtls = new SellerDtls{Gstin= ds.Tables[0].Rows[0]["Seller_GST"].ToString().Trim() , LglNm=ds.Tables[0].Rows[0]["Seller_LegalName"].ToString().Trim(),
                                                    TrdNm = ds.Tables[0].Rows[0]["Seller_TradeName"].ToString().Trim(), Addr1=ds.Tables[0].Rows[0]["Seller_Address1"].ToString().Trim(),
                                                    Addr2 = ds.Tables[0].Rows[0]["Seller_Address2"].ToString().Trim(), Loc=ds.Tables[0].Rows[0]["Seller_Location"].ToString().Trim(),
                                                    Pin =ds.Tables[0].Rows[0]["Seller_PinCode"].ToString().Trim(), Stcd =ds.Tables[0].Rows[0]["Seller_StateCode"].ToString().Trim(),
                                                    Ph = ds.Tables[0].Rows[0]["Seller_Phone"].ToString().Trim(), Em = ds.Tables[0].Rows[0]["Seller_Email"].ToString().Trim() },

                        BuyerDtls = new BuyerDtls{Gstin = ds.Tables[0].Rows[0]["Buyer_GST"].ToString().Trim(),LglNm=ds.Tables[0].Rows[0]["Buyer_LegalName"].ToString().Trim(),
                                                  TrdNm = ds.Tables[0].Rows[0]["Buyer_TradeName"].ToString().Trim(),Pos= Convert.ToInt32(ds.Tables[0].Rows[0]["Buyer_PoS"].ToString().Trim()),
                                                  Addr1= ds.Tables[0].Rows[0]["Buyer_Address1"].ToString().Trim(),Addr2= ds.Tables[0].Rows[0]["Buyer_Address2"].ToString().Trim(),
                                                  Loc=ds.Tables[0].Rows[0]["Buyer_Location"].ToString().Trim(), Pin=ds.Tables[0].Rows[0]["Buyer_Pincode"].ToString().Trim(),
                                                  Stcd =ds.Tables[0].Rows[0]["Buyer_StateCode"].ToString().Trim(),Ph=ds.Tables[0].Rows[0]["Buyer_Phone"].ToString().Trim(),
                                                  Em = ds.Tables[0].Rows[0]["Buyer_Email"].ToString().Trim()},

                        DispDtls = new DispDtls{Nm=ds.Tables[0].Rows[0]["Despatch_Name"].ToString().Trim(),Loc=ds.Tables[0].Rows[0]["Despatch_Location"].ToString().Trim(),
                                                Addr1=ds.Tables[0].Rows[0]["Despatch_Address1"].ToString().Trim(),Addr2=ds.Tables[0].Rows[0]["Despatch_Address2"].ToString().Trim(),
                                                Pin=ds.Tables[0].Rows[0]["Despatch_Pincode"].ToString().Trim(),Stcd=ds.Tables[0].Rows[0]["Despatch_StateCode"].ToString().Trim()},

                        ShipDtls = new ShipDtls{Gstin=ds.Tables[0].Rows[0]["Shipment_GST"].ToString().Trim(),LglNm=ds.Tables[0].Rows[0]["Shipment_LegalName"].ToString().Trim(),
                                                TrdNm =ds.Tables[0].Rows[0]["Shipment_TradeName"].ToString().Trim(),Loc=ds.Tables[0].Rows[0]["Shipment_Location"].ToString().Trim(),
                                                Addr1 =ds.Tables[0].Rows[0]["Shipment_Address1"].ToString().Trim(),Addr2 =ds.Tables[0].Rows[0]["Shipment_Address2"].ToString().Trim(),
                                                Pin =ds.Tables[0].Rows[0]["Shipment_Pincode"].ToString().Trim(),Stcd=ds.Tables[0].Rows[0]["Shipment_StateCode"].ToString()},

                        ItemList = GenerateMultiItems(ds),

                        ValDtls= new ValDtls(Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[0]["ValDtls_AssVal"].ToString().Trim()), 2),Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[0]["ValDtls_CgstVal"].ToString().Trim()), 2),
                                             Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[0]["ValDtls_SgstVal"].ToString().Trim()), 2),Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[0]["ValDtls_IgstVal"].ToString().Trim()), 2),
                                             0,Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[0]["ValDtls_CesVal"].ToString().Trim()), 2),0,0,
                                             Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[0]["ValDtls_RndOffAmt"].ToString().Trim()), 2),Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[0]["ValDtls_TotVal"].ToString()), 2),null),

                        PayDtls = new PayDtls(Convert.ToInt32(ds.Tables[0].Rows[0]["PayDtls_Crday"].ToString().Trim()),Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[0]["PayDtls_Paidamt"].ToString().Trim()), 2),
                                              Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[0]["PayDtls_Paidamt"].ToString().Trim()), 2)),

                        RefDtls = new RefDtls {InvRm="INVRM", DocPerdDtls = new DocPerdDtls{InvStDt=ds.Tables[0].Rows[0]["Document_Date"].ToString().Trim(), InvEndDt=ds.Tables[0].Rows[0]["Document_Date"].ToString().Trim()},
                                               PrecDocDtls = new List<PrecDocDtl> { new PrecDocDtl()},  ContrDtls= new List<ContrDtls> { new ContrDtls() }},

                        AddlDocDtls = new List<AddlDocDtls> { new AddlDocDtls { Url="URL", Docs="DOC", Info="DocINFO"} },

                        ExpDtls = new ExpDtls(),

                        EwbDtls = new EwbDtls()
                    }};

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                string errBranch = BranchCode;
                obj = new Exception(errBranch + "_" + ex.Message); ;
                throw obj;
            }

            return this.InvoiceCollection;
        }

        public GenerateJSON()
        {

        }

        public ItemList GenerateMultiItems(DataSet ds)
        {
            List<Item> objItem = new List<Item> { };
            ItemList objItemList = null;

            for (int i = 0; i <= ds.Tables[1].Rows.Count - 1; i++)
            {
                objItem.Add(new Item
                {
                    SlNo = i+1,
                    PrdDesc = ds.Tables[1].Rows[i]["PrdDesc"].ToString().Trim(),
                    IsServc = ds.Tables[1].Rows[i]["IsServ"].ToString().Trim(),
                    HsnCd = ds.Tables[1].Rows[i]["HsnCd"].ToString().Trim(),
                    BchDtls = new BchDtls { Nm = ds.Tables[1].Rows[i]["BchDtls_Nm"].ToString().Trim(), Expdt = null, wrDt = null },
                    Barcde = null,
                    Qty = Convert.ToInt32(ds.Tables[1].Rows[i]["Qty"].ToString()),
                    FreeQty = Convert.ToInt32(ds.Tables[1].Rows[i]["FreeQty"].ToString()),
                    Unit = "NOS",
                    UnitPrice = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["UnitPrice"].ToString()), 2),
                    TotAmt = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["TotAmt"].ToString()), 2),
                    Discount = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["Discount"].ToString()), 2),
                    PreTaxVal = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["PreTaxVal"].ToString()), 2),
                    AssAmt = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["AssAmt"].ToString()), 2),
                    IgstRt = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["IGST_Rate"].ToString()), 2),
                    IgstAmt = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["IGST_Amount"].ToString()), 2),
                    CgstRt = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["CGST_Rate"].ToString()), 2),
                    CgstAmt = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["CGST_Amount"].ToString()), 2),
                    SgstRt = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["SGST_Rate"].ToString()), 2),
                    SgstAmt = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["SGST_Amount"].ToString()), 2),
                    CesRt = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["CESS_Rate"].ToString()), 2),
                    CesAmt = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["CESS_Amount"].ToString()), 2),
                    CesNonAdvlAmt = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["CesNonAdvlAmt"].ToString()), 2),
                    StateCesRt = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["StateCesRt"].ToString()), 2),
                    StateCesAmt = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["StateCesAmt"].ToString()), 2),
                    StateCesNonAdvlAmt = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["StateCesNonAdvlAmt"].ToString()), 2),
                    OthChrg = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["OthChrg"].ToString()), 2),
                    TotItemVal = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["TotItemVal"].ToString()), 2),
                    OrdLineRef = ds.Tables[1].Rows[i]["OrdLineRef"].ToString(),
                    OrgCntry = "IN",
                    PrdSlNo = null,

                    AttribDtls = new List<AttribDtl> { new AttribDtl { Nm = null, Val = null } },

                    EGST = new EGST
                    {
                        nilrated_amt = "0",
                        exempted_amt = "0",
                        non_gst_amt = "0",
                        reason = null,
                        debit_gl_id = "1",
                        debit_gl_name = "DGL",
                        credit_gl_id = "2",
                        credit_gl_name = "CGL",
                        sublocation = "SUBLOC"
                    }

                });
            }

            objItemList = new ItemList { Item = objItem };

            return objItemList;
        }
        public class InvoiceDataCollection
        {
            public TranDtls TranDtls { get; set; }
            public DocDtls DocDtls { get; set; }
            public SellerDtls SellerDtls { get; set; }
            public BuyerDtls BuyerDtls { get; set; }
            public DispDtls DispDtls { get; set; }
            public ShipDtls ShipDtls { get; set; }
            public ItemList ItemList { get; set; }
            public ValDtls ValDtls { get; set; }
            public PayDtls PayDtls { get; set; }
            public RefDtls RefDtls { get; set; }
            public List<AddlDocDtls> AddlDocDtls { get; set; }
            public ExpDtls ExpDtls { get; set; }
            public EwbDtls EwbDtls { get; set; }
        }


        public class TranDtls
        {
            public string TaxSch { get; set; }
            public string SupTyp { get; set; }
            public string RegRev { get; set; }
            public string EcmGstin { get; set; }
            public string IgstonIntra { get; set; }
            public object supplydir { get; set; }
        }

        public class DocDtls
        {
            public string Typ { get; set; }
            public string No { get; set; }
            public string Dt { get; set; }
        }

        public class SellerDtls
        {
            public string Gstin { get; set; }
            public string LglNm { get; set; }
            public string TrdNm { get; set; }
            public string Addr1 { get; set; }
            public string Addr2 { get; set; }
            public string Loc { get; set; }
            public string Pin { get; set; }
            public string Stcd { get; set; }
            public string Ph { get; set; }
            public string Em { get; set; }
        }

        public class BuyerDtls
        {
            public string Gstin { get; set; }
            public string LglNm { get; set; }
            public string TrdNm { get; set; }
            public int Pos { get; set; }
            public string Addr1 { get; set; }
            public string Addr2 { get; set; }
            public string Loc { get; set; }
            public string Pin { get; set; }
            public string Stcd { get; set; }
            public string Ph { get; set; }
            public string Em { get; set; }
        }

        public class DispDtls
        {
            public string Nm { get; set; }
            public string Loc { get; set; }
            public string Addr1 { get; set; }
            public string Addr2 { get; set; }
            public string Pin { get; set; }
            public string Stcd { get; set; }
        }

        public class ShipDtls
        {
            public string Gstin { get; set; }
            public string LglNm { get; set; }
            public string TrdNm { get; set; }
            public string Loc { get; set; }
            public string Addr1 { get; set; }
            public string Addr2 { get; set; }
            public string Pin { get; set; }
            public string Stcd { get; set; }
        }

        public class BchDtls
        {
            public string Nm { get; set; }
            public string Expdt { get; set; }
            public string wrDt { get; set; }
        }

        public class AttribDtl
        {
            public string Nm { get; set; }
            public string Val { get; set; }
        }

        public class EGST
        {
            public string nilrated_amt { get; set; }
            public string exempted_amt { get; set; }
            public string non_gst_amt { get; set; }
            public string reason { get; set; }
            public string debit_gl_id { get; set; }
            public string debit_gl_name { get; set; }
            public string credit_gl_id { get; set; }
            public string credit_gl_name { get; set; }
            public string sublocation { get; set; }
        }

        public class Item
        {
            public int SlNo { get; set; }
            public string PrdDesc { get; set; }
            public string IsServc { get; set; }
            public string HsnCd { get; set; }
            public BchDtls BchDtls { get; set; }
            public string Barcde { get; set; }
            public int Qty { get; set; }
            public int FreeQty { get; set; }
            public string Unit { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal TotAmt { get; set; }
            public decimal Discount { get; set; }
            public decimal PreTaxVal { get; set; }
            public decimal AssAmt { get; set; }
            public decimal IgstRt { get; set; }
            public decimal IgstAmt { get; set; }
            public decimal CgstRt { get; set; }
            public decimal CgstAmt { get; set; }
            public decimal SgstRt { get; set; }
            public decimal SgstAmt { get; set; }
            public decimal CesRt { get; set; }
            public decimal CesAmt { get; set; }
            public decimal CesNonAdvlAmt { get; set; }
            public decimal StateCesRt { get; set; }
            public decimal StateCesAmt { get; set; }
            public decimal StateCesNonAdvlAmt { get; set; }
            public decimal OthChrg { get; set; }
            public decimal TotItemVal { get; set; }
            public string OrdLineRef { get; set; }
            public string OrgCntry { get; set; }
            public object PrdSlNo { get; set; }
            public List<AttribDtl> AttribDtls { get; set; }
            public EGST EGST { get; set; }
        }

        public class ItemList
        {
            public List<Item> Item { get; set; }
        }

        public class ValDtls
        {
            public decimal AssVal { get; set; }
            public decimal CgstVal { get; set; }
            public decimal SgstVal { get; set; }
            public decimal IgstVal { get; set; }
            public decimal CesVal { get; set; }
            public decimal StCesVal { get; set; }
            public decimal Discount { get; set; }
            public decimal OthChrg { get; set; }
            public decimal RndOffAmt { get; set; }
            public decimal TotInvVal { get; set; }
            public object TotInvValFc { get; set; }

            public ValDtls(decimal AssVal, decimal CgstVal, decimal SgstVal, decimal IgstVal, decimal CesVal, decimal StCesVal,
                decimal Discount, decimal OthChrg, decimal RndOffAmt, decimal TotInvVal, object TotInvValFc)
            {
                this.AssVal = AssVal; this.CgstVal = CgstVal; this.SgstVal = SgstVal; this.IgstVal = IgstVal;
                this.CesVal = CesVal; this.StCesVal = StCesVal; this.Discount = Discount; this.OthChrg = OthChrg;
                this.RndOffAmt = RndOffAmt; this.TotInvVal = TotInvVal; this.TotInvValFc = TotInvValFc;
            }
        }

        public class PayDtls
        {
            public string Nm { get; set; }
            public string Accdet { get; set; }
            public string Mode { get; set; }
            public string Fininsbr { get; set; }
            public string Payterm { get; set; }
            public string Payinstr { get; set; }
            public string Crtrn { get; set; }
            public string Dirdr { get; set; }
            public int Crday { get; set; }
            public decimal Paidamt { get; set; }
            public decimal Paymtdue { get; set; }

            public PayDtls(int Crday, decimal Paidamt, decimal Paymtdue)
            {
                this.Nm = "STATE BANK OF INDIA"; this.Accdet = "00000030796647005"; this.Mode = "CASH";
                this.Fininsbr = "SBIN0007347"; this.Payterm = null; this.Payinstr = null; this.Crtrn = null;
                this.Dirdr = null; this.Crday = Crday; this.Paidamt = Paidamt; this.Paymtdue = Paymtdue;
            }
        }

        public class DocPerdDtls
        {
            public string InvStDt { get; set; }
            public string InvEndDt { get; set; }
        }

        public class PrecDocDtl
        {
            public string InvNo { get; set; }
            public string InvDt { get; set; }
            public string OthRefNo { get; set; }
        }

        public class ContrDtls
        {
            public string RecAdvRefr { get; set; }
            public string RecAdvDt { get; set; }
            public string Tendrefr { get; set; }
            public string Contrrefr { get; set; }
            public string Extrefr { get; set; }
            public string Projrefr { get; set; }
            public string Porefr { get; set; }
            public string PoRefDt { get; set; }
        }

        public class RefDtls
        {
            public string InvRm { get; set; }
            public DocPerdDtls DocPerdDtls { get; set; }
            public List<PrecDocDtl> PrecDocDtls { get; set; }
            public List<ContrDtls> ContrDtls { get; set; }
        }

        public class AddlDocDtls
        {
            public string Url { get; set; }
            public string Docs { get; set; }
            public string Info { get; set; }
        }

        public class ExpDtls
        {
            public string ShipBNo { get; set; }
            public string ShipBDt { get; set; }
            public string Port { get; set; }
            public string RefClm { get; set; }
            public string ForCur { get; set; }
            public string CntCode { get; set; }
            public string ExpDuty { get; set; }
        }

        public class EwbDtls
        {
            public string Transid { get; set; }
            public string Transname { get; set; }
            public string Distance { get; set; }
            public string Transdocno { get; set; }
            public string TransdocDt { get; set; }
            public string Vehno { get; set; }
            public string Vehtype { get; set; }
            public string TransMode { get; set; }
        }

        public Tuple2<string, string> DecryptBySymmetricKey(string encryptedSek, string AppSecretKey)
        {
            //Decrypting SEK
            try
            {
                byte[] secretKey = Encoding.UTF8.GetBytes(AppSecretKey);
                byte[] dataToDecrypt = Convert.FromBase64String(encryptedSek);
                var keyBytes = secretKey;
                AesManaged tdes = new AesManaged();
                tdes.KeySize = 256;
                tdes.BlockSize = 128;
                tdes.Key = keyBytes;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;
                ICryptoTransform decrypt__1 = tdes.CreateDecryptor();
                byte[] deCipher = decrypt__1.TransformFinalBlock(dataToDecrypt, 0, dataToDecrypt.Length);
                tdes.Clear();

                string decrSEK = Convert.ToBase64String(deCipher); //AES Decrypted Output (Base64)

                string decrSEKplain = Encoding.UTF8.GetString(deCipher, 0, deCipher.Length); //AES Decrypted Plain Text

                return new Tuple2<string, string>(decrSEK, decrSEKplain);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string EncryptBySymmetricKey(string jsondata, string sek)
        {
            //Encrypting SEK
            try
            {
                byte[] dataToEncrypt = Encoding.UTF8.GetBytes(jsondata);
                var keyBytes = Convert.FromBase64String(sek);
                AesManaged tdes = new AesManaged();
                tdes.KeySize = 256;
                tdes.BlockSize = 128;
                tdes.Key = keyBytes;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;
                ICryptoTransform encrypt__1 = tdes.CreateEncryptor();
                byte[] deCipher = encrypt__1.TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);
                tdes.Clear();
                string EK_result = Convert.ToBase64String(deCipher);
                return EK_result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string RSAEncryption(string KeyData)
        {
            string publicKey = ConfigurationManager.AppSettings["Einvoice_PublicKey"].ToString();

            string pubkey = @"<RSAKeyValue><Modulus>" + publicKey + "</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] cipherbytes;
            rsa.FromXmlString(pubkey);
            cipherbytes = rsa.Encrypt(Encoding.UTF8.GetBytes(KeyData), false);

            return Convert.ToBase64String(cipherbytes);
        }

    }
    public class EinvResponse
    {
        public EinvResponse(string status, string bdoAuthtoken, string bdoSEK, string expiry, string ErrorMsg)
        {
            this.status = status;
            this.bdoAuthtoken = bdoAuthtoken;
            this.bdoSEK = bdoSEK;
            this.expiry = expiry;
            this.ErrorMsg = ErrorMsg;
        }

        public string status { get; set; }
        public string bdoAuthtoken { get; set; }
        public string bdoSEK { get; set; }
        public string expiry { get; set; }
        public string ErrorMsg { get; set; }
    }

    public class EinvInvGEN
    {
        public EinvInvGEN(string Data)
        {
            this.Data = Data;
        }

        public string Data { get; set; }
    }
}