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


namespace IMPALLibrary
{
    public class SLBs
    {
        public string SLBCode { get; set; }
        public string ItemCode { get; set; }
        public string BranchCode { get; set; }
        public string NewOSValue { get; set; }
        public string NewLSValue { get; set; }
        public string NewFDOValue { get; set; }
        public string NewLRValue { get; set; }
        public string OldOSValue { get; set; }
        public string OldLSValue { get; set; }
        public string OldFDOValue { get; set; }
        public string OldLRValue { get; set; }
        public string PurchaseDiscount { get; set; }
        public string Status { get; set; }

        public void AddNewSLBs(string SLBCode, string Description, string Indicator,string MinValue, string MaxValue)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addslb");

            //ImpalDB.AddInParameter(cmd, GLGroupCode, DbType.String);
            ImpalDB.AddInParameter(cmd, "@SLB_Code", DbType.String, SLBCode.Trim());
            ImpalDB.AddInParameter(cmd, "@SLB_Description", DbType.String, Description.Trim());
            ImpalDB.AddInParameter(cmd, "@Quantity_Value_Indicator", DbType.String, Indicator.Trim());
            ImpalDB.AddInParameter(cmd, "@Minimum_Value", DbType.Double, Convert.ToDouble(MinValue.Trim()));
            ImpalDB.AddInParameter(cmd, "@Maximum_Value", DbType.Double, Convert.ToDouble(MaxValue.Trim()));
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);

        }
        public void UpdateSLBs(string SLBCode, string Description, string Indicator,string MinValue,string MaxValue)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_Updslb");

            ImpalDB.AddInParameter(cmd, "@SLB_Code", DbType.String, SLBCode.Trim());
            ImpalDB.AddInParameter(cmd, "@SLB_Description", DbType.String, Description.Trim());
            ImpalDB.AddInParameter(cmd, "@Quantity_Value_Indicator", DbType.String, Indicator.Trim());
            ImpalDB.AddInParameter(cmd, "@Minimum_Value", DbType.Double, Convert.ToDouble(MinValue.Trim()));
            ImpalDB.AddInParameter(cmd, "@Maximum_Value", DbType.Double, Convert.ToDouble(MaxValue.Trim()));
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }
        public List<SLB> GetALLSLBs()
        {
            List<SLB> SlbLst = new List<SLB>();

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = " Select SLB_Code, SLB_Description,Quantity_Value_Indicator,Minimum_Value,Maximum_Value " +
                            " from SLB_Master";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    SlbLst.Add(new SLB(reader["SLB_Code"].ToString(), reader["SLB_Description"].ToString(), reader["Quantity_Value_Indicator"].ToString(), reader["Minimum_Value"].ToString(), reader["Maximum_Value"].ToString()));
                }
            }

            return SlbLst;
        }

        public List<SLB> GetSLBDetails(string strBranchCode)
        {
            List<SLB> SlbLst = new List<SLB>();

            Database ImpalDB = DataAccess.GetDatabase();
            //string sSQL = "Select distinct SLB_Code, SLB_Description from SLB_Master order by SLB_Code";
            string sSQL = "select distinct SM.SLB_Code,SLB_Description  from SLB_Master SM left outer join SLB_Item_Detail SID on SID.SLB_Code=SM.SLB_Code where SID.Branch_Code = '" + strBranchCode + "'";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    SlbLst.Add(new SLB(reader["SLB_Code"].ToString(), reader["SLB_Description"].ToString()));
                }
            }

            return SlbLst;
        }

        public DataTable GetSLB(string strBranch,string strSLB, string SupPartNo)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            try
            {
                string strQuery = "select slb.SLB_Code,mas.SLB_Description, slb.Branch_Code,bran.Branch_Name,slb.Item_Code,os_value,ls_value,fdo_value,lr_value,old_os_value,old_ls_value,old_fdo_value,old_lr_value from slb_item_detail slb inner join SLB_Master mas on slb.SLB_Code=mas.SLB_Code and slb.Branch_Code='" + strBranch + "' and slb.SLB_Code='" + strSLB + "'";
                strQuery = strQuery + " inner join branch_master bran on bran.Branch_Code=slb.Branch_Code inner join item_master im on im.item_code=slb.Item_Code";
                if (SupPartNo != "")
                    strQuery = strQuery + " and im.Supplier_Part_Number like '" + SupPartNo + "'";

                DbCommand cmd = ImpalDB.GetSqlStringCommand(strQuery);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                DataSet ds = ImpalDB.ExecuteDataSet(cmd);
                return ds.Tables[0];
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
            finally
            {
                ImpalDB = null;
            }

        }

        public bool CheckSLBDetailsExists(string strBranch, string strSLB, string strItemCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            try
            {
                string strQuery = "select 1 from SLB_Item_Detail where SLB_Code ='" + strSLB + "' and  Item_Code ='" + strItemCode + "' and Branch_Code='" + strBranch + "'";
                DbCommand cmd1 = ImpalDB.GetSqlStringCommand(strQuery);
                cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                object objCount = ImpalDB.ExecuteScalar(cmd1);
                Int32 intCount = 0;
                if (Int32.TryParse((string)objCount, out intCount))
                {
                    if (intCount > 0)
                        return false;
                    else
                        return true;
                }
                return true;
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
            finally
            {
                ImpalDB = null;
            }
            
        }

        public void InsertSLBDetails()
        {
            Database ImpalDB = DataAccess.GetDatabase();
            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_addSLBItemDetail");

            ImpalDB.AddInParameter(cmd, "@SLB_Code", DbType.Int32, SLBCode);
            ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, ItemCode);
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@OS_Value", DbType.Decimal, OldOSValue);
            ImpalDB.AddInParameter(cmd, "@LS_Value", DbType.Decimal, OldLSValue);
            ImpalDB.AddInParameter(cmd, "@FDO_Value", DbType.Decimal, OldFDOValue);
            ImpalDB.AddInParameter(cmd, "@LR_Value", DbType.Decimal, OldLRValue);
            ImpalDB.AddInParameter(cmd, "@NEW_OS_Value", DbType.Decimal, NewOSValue);
            ImpalDB.AddInParameter(cmd, "@NEW_LS_Value", DbType.Decimal, NewLSValue);
            ImpalDB.AddInParameter(cmd, "@NEW_FDO_Value", DbType.Decimal, NewFDOValue);
            ImpalDB.AddInParameter(cmd, "@NEW_LR_Value", DbType.Decimal, NewLRValue);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }

        public void UpdateSLBDetails()
        {
            Database ImpalDB = DataAccess.GetDatabase();
            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_updSLBItemDetail");

            ImpalDB.AddInParameter(cmd, "@SLB_Code", DbType.Int32, SLBCode);
            ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, ItemCode);
            ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
            ImpalDB.AddInParameter(cmd, "@OS_Value", DbType.Decimal, OldOSValue);
            ImpalDB.AddInParameter(cmd, "@LS_Value", DbType.Decimal, OldLSValue);
            ImpalDB.AddInParameter(cmd, "@FDO_Value", DbType.Decimal, OldFDOValue);
            ImpalDB.AddInParameter(cmd, "@LR_Value", DbType.Decimal, OldLRValue);
            ImpalDB.AddInParameter(cmd, "@OS_Value_NEW", DbType.Decimal, NewOSValue);
            ImpalDB.AddInParameter(cmd, "@LS_Value_NEW", DbType.Decimal, NewLSValue);
            ImpalDB.AddInParameter(cmd, "@FDO_Value_NEW", DbType.Decimal, NewFDOValue);
            ImpalDB.AddInParameter(cmd, "@LR_Value_NEW", DbType.Decimal, NewLRValue);
            ImpalDB.AddInParameter(cmd, "@Pur_Dis", DbType.Decimal,(PurchaseDiscount==string.Empty?"0.00":PurchaseDiscount));
            ImpalDB.AddInParameter(cmd, "@Status", DbType.String,Status);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }

        public DataTable GetPurchaseDiscount(string strItemCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            try
            {
                string strQuery = "select distinct Purchase_Discount from Item_Master where substring(Item_Code,1,3) = '" + strItemCode.Substring(0, 3) + "'";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(strQuery);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                DataSet ds = ImpalDB.ExecuteDataSet(cmd);
                return ds.Tables[0];
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
            finally
            {
                ImpalDB = null;
            }

        }

        public List<SLB> GetSupplierLineDetails()
        {
            List<SLB> SlbLst = new List<SLB>();

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select supplier_line_Code,short_description + '-' + Long_Description Supplier_Description from supplier_line_master order by short_description";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    SlbLst.Add(new SLB(reader["supplier_line_Code"].ToString(), reader["Supplier_Description"].ToString()));
                }
            }

            return SlbLst;
        }

        
    }

    public class SLB
    {
        public SLB(string SLBCode,  string Description, string Indicator, string MinValue,string MaxValue)
        {
            _SLBCode = SLBCode;
            _MaxValue = MaxValue;
            _Description = Description;
            _MinValue = MinValue;
            _Indicator = Indicator;
         
        }

        public SLB(string SLBCode, string Description)
        {
            _SLBCode = SLBCode;
            _Description = Description;
        }

       
        private string _SLBCode;
        private string _Description;
        private string _Indicator;
        private string _MaxValue;
        private string _MinValue;

        public string SLBCode 
        {
            get { return _SLBCode; }
            set { _SLBCode = value; }
        }
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        public string Indicator
        {
            get {
                return _Indicator;
            }
            set {
                _Indicator = value; 
            }
        }

        public string MinValue
        {
            get { return _MinValue; }
            set { _MinValue = value; }
        }
        public string MaxValue
        {
            get { return _MaxValue; }
            set { _MaxValue = value; }
        }
    }

    public class SLBGroups
    { 
          public List<SLBGroup> GetALLSLBNames()
        {
            List<SLBGroup> SlbNames = new List<SLBGroup>();

            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = " Select SLB_Code, SLB_Description from SLB_Master";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    SlbNames.Add(new SLBGroup(reader["SLB_Code"].ToString(), reader["SLB_Description"].ToString()));
                }

            }
            return SlbNames;
        }

       
    }
    }
    public class SLBGroup
    { 
          public SLBGroup(string Code,  string Desc)
        {
            _Code = Code;
            _Desc = Desc;
        }
          public SLBGroup()
        {
           
        }
        private string _Code;
        private string _Desc;

        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }
        public string Desc
        {
            get { return _Desc; }
            set { _Desc = value; }
        }
        
    }

    public class SLBQueries
    {
       


        public string BranchCode { get; set; }
        public string SupplierLineCode { get; set; }
        public string ItemCode { get; set; }

        public decimal ListPrice { get; set; }
        public decimal SupplierDiscount { get; set; }
        public decimal ExciseDutyPercentage { get; set; }
        public decimal SalesTaxPercentage { get; set; }
        public decimal SurchargePercentage { get; set; }
        public decimal AdditionalSurcharge { get; set; }
        public decimal TurnoverTax { get; set; }
        public decimal Octroi { get; set; }
        public decimal Insurance { get; set; }
        public decimal TurnoverDiscount { get; set; }
        public decimal Bonus { get; set; }
        public decimal DealersDiscount { get; set; }
        public decimal HandlingChargesPercentage { get; set; }
        public decimal GrossProfitPercentage { get; set; }
        public decimal SLBValue { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal SpecialDiscount1 { get; set; }

        public decimal SpecialDiscount2 { get; set; }
        public decimal AdditionDiscount1 { get; set; }
        public decimal AdditionDiscount2 { get; set; }
        public decimal AdditionDiscount3 { get; set; }
        public decimal AdditionDiscount4 { get; set; }
        public decimal AdditionDiscount5 { get; set; }
        public decimal EntryTax { get; set; }
        public decimal CESS { get; set; }
        public decimal WarehouseSurcharge { get; set; }
        public decimal Freight { get; set; }
        public decimal CouponCharges { get; set; }
        public decimal OtherLevies1 { get; set; }
        public decimal OtherLevies2 { get; set; }
        
        public decimal EDForSellingPrice { get; set; }
        public decimal RegularGP { get; set; }
        public decimal GPTOD { get; set; }
        public decimal GPBonus { get; set; }
        public decimal GPTODBONUS { get; set; }
        public decimal SplTurnoverDiscount { get; set; }
        
        public string AdditionDiscount_1_ind { get; set; }
        public string AdditionDiscount_2_ind { get; set; }
        public string AdditionDiscount_3_ind { get; set; }
        public string AdditionDiscount_4_ind { get; set; }
        public string AdditionDiscount_5_ind { get; set; }
        public string ExciseDutyIndicator { get; set; }
        public string CessIndicator { get; set; }
        public string InsuranceIndicator { get; set; }
        public string WarehouseSurchargeInd { get; set; }
        public string FreightInd { get; set; }
        public string TurnoverTaxInd { get; set; }
        public string BonusInd { get; set; }
        public string CouponInd { get; set; }
        public string SplTODInd { get; set; }
        public string HCInd { get; set; }
        public string SLBRoundoffInd { get; set; }

        public string EDSellingPriceInd { get; set; }
        public string SplDisc_1_Ind { get; set; }
        public string SplDisc_2_Ind { get; set; }
        public string CalculationInd { get; set; }
        public string AdditionSurchargeInd { get; set; }

        public decimal AfterDiscount { get; set; }
        public decimal AfterAddDisc1 { get; set; }
        public decimal AfterAddDisc2 { get; set; }
        public decimal AfterAddDisc3 { get; set; }
        public decimal AfterAddDisc4 { get; set; }
        public decimal AfterAddDisc5 { get; set; }

        public decimal AfterED { get; set; }
        public decimal AfterST { get; set; }
        public decimal AfterSC { get; set; }
        public decimal AfterASC { get; set; }
        public decimal AfterET { get; set; }
        public decimal AfterOct { get; set; }
        public decimal AfterCess { get; set; }
        public decimal AfterOl1 { get; set; }
        public decimal AfterOl2 { get; set; }
        public decimal AfterIns { get; set; }

        public decimal AfterWsc { get; set; }
        public decimal AfterFrt { get; set; }
        public decimal AfterTot { get; set; }
        public decimal AfterBon { get; set; }
        public decimal AfterCoupon { get; set; }
        public decimal AfterSplTOD { get; set; }
        public decimal AfterHC { get; set; }
        public decimal AfterAdd { get; set; }
        
        public string GetPriceList(string strBranch, string strItemCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            try
            {
                string strQuery = "select isnull(List_Price,0) from branch_item_price where branch_code ='" + strBranch + "'  and Item_code= '" + strItemCode + "'";
                DbCommand cmd1 = ImpalDB.GetSqlStringCommand(strQuery);
                cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                object objListPrice = ImpalDB.ExecuteScalar(cmd1);
                return Convert.ToString(objListPrice); 
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
            finally
            {
                ImpalDB = null;
            }

        }

        public string GetPurchaseDiscount(string strItemCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            try
            {
                string strQuery = "select Purchase_Discount from item_master where Item_code= '"+ strItemCode + "'";
                DbCommand cmd1 = ImpalDB.GetSqlStringCommand(strQuery);
                cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                object objPurchaseDiscount = ImpalDB.ExecuteScalar(cmd1);
                return Convert.ToString(objPurchaseDiscount);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
            finally
            {
                ImpalDB = null;
            }
        }

        public DataTable GetAdditionalDiscounts(string strSuppliercode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataTable dtDiscount;
            try
            {
                string strQuery = "select Additional_Discount1,Additional_Discount2,Additional_Discount3,Additional_Discount4,Additional_Discount5 from supplier_line_master where supplier_line_code = '" + strSuppliercode + "'";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(strQuery);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                return dtDiscount = ((DataSet)ImpalDB.ExecuteDataSet(cmd)).Tables[0];
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
            finally
            {
                ImpalDB = null;
            }
        }

        public string GetExciseDuty(string strItemCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            try
            {
                string strQuery = "select Excise_Duty_Value from item_master where Item_code= '" + strItemCode + "'";
                DbCommand cmd1 = ImpalDB.GetSqlStringCommand(strQuery);
                cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                object objExciseDuty = ImpalDB.ExecuteScalar(cmd1);
                return Convert.ToString(objExciseDuty);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
            finally
            {
                ImpalDB = null;
            }
        }

        public string GetZone(string strBranch)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            try
            {
                string strQuery = "select c.zone_code from branch_master a,state_master b,zone_master c where a.branch_code = '" + strBranch  + "' and a.state_code= b.state_code and b.zone_code= c.zone_code";
                DbCommand cmd1 = ImpalDB.GetSqlStringCommand(strQuery);
                cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                object objZone = ImpalDB.ExecuteScalar(cmd1);
                return Convert.ToString(objZone);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
            finally
            {
                ImpalDB = null;
            }
         
        }


    

        public DataSet GetSLBQueryDetails(string strBranch, string strSupplierLineCode, string strItemCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DataSet dsSLBQuery;
            try
            {
                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_GetSLBitemcalc");

                ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranch);
                ImpalDB.AddInParameter(cmd, "@Supplier_Line_Code", DbType.String, strSupplierLineCode);
                ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, strItemCode);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                return dsSLBQuery=ImpalDB.ExecuteDataSet(cmd);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
            finally
            {
                ImpalDB = null;
                dsSLBQuery = null; 
            }
        }

        public Int32 AddSLBQueryDetails()
        {
            Database ImpalDB = DataAccess.GetDatabase();

            try
            {
                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_AddSLBitemcal");

                ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
                ImpalDB.AddInParameter(cmd, "@Supplier_Line_Code", DbType.String, SupplierLineCode);
                ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, ItemCode);

                ImpalDB.AddInParameter(cmd, "@List_Price", DbType.Decimal, ListPrice);
                ImpalDB.AddInParameter(cmd, "@Supplier_Discount", DbType.Decimal, SupplierDiscount);
                ImpalDB.AddInParameter(cmd, "@Excise_Duty_Percentage", DbType.Decimal, ExciseDutyPercentage);
                ImpalDB.AddInParameter(cmd, "@Sales_Tax_Percentage", DbType.Decimal, SalesTaxPercentage);
                ImpalDB.AddInParameter(cmd, "@Surcharge_Percentage", DbType.Decimal, SurchargePercentage);

                ImpalDB.AddInParameter(cmd, "@Additional_Surcharge", DbType.Decimal, AdditionalSurcharge);
                ImpalDB.AddInParameter(cmd, "@Turnover_Tax", DbType.Decimal, TurnoverTax);
                ImpalDB.AddInParameter(cmd, "@Octroi", DbType.Decimal, Octroi);
                ImpalDB.AddInParameter(cmd, "@Insurance", DbType.Decimal, Insurance);
                ImpalDB.AddInParameter(cmd, "@Turnover_Discount", DbType.Decimal, TurnoverDiscount);
                ImpalDB.AddInParameter(cmd, "@Bonus", DbType.Decimal, Bonus);
                ImpalDB.AddInParameter(cmd, "@Dealers_Discount", DbType.Decimal, DealersDiscount);

                ImpalDB.AddInParameter(cmd, "@Handling_Charges_Percentage", DbType.Decimal, HandlingChargesPercentage);
                ImpalDB.AddInParameter(cmd, "@Gross_Profit_Percentage", DbType.Decimal, GrossProfitPercentage);
                ImpalDB.AddInParameter(cmd, "@SLB_Value", DbType.Decimal, SLBValue);
                ImpalDB.AddInParameter(cmd, "@Cost_Price", DbType.Decimal, CostPrice);
                ImpalDB.AddInParameter(cmd, "@Selling_Price", DbType.Decimal, SellingPrice);
                ImpalDB.AddInParameter(cmd, "@Special_Discount_1", DbType.Decimal, SpecialDiscount1);
                ImpalDB.AddInParameter(cmd, "@Special_Discount_2", DbType.Decimal, SpecialDiscount2);
                ImpalDB.AddInParameter(cmd, "@Addition_discount_1", DbType.Decimal, AdditionDiscount1);
                ImpalDB.AddInParameter(cmd, "@Addition_discount_2", DbType.Decimal, AdditionDiscount2);
                ImpalDB.AddInParameter(cmd, "@Addition_discount_3", DbType.Decimal, AdditionDiscount3);

                ImpalDB.AddInParameter(cmd, "@Addition_discount_4", DbType.Decimal, AdditionDiscount4);
                ImpalDB.AddInParameter(cmd, "@Addition_discount_5", DbType.Decimal, AdditionDiscount5);
                ImpalDB.AddInParameter(cmd, "@Entry_Tax", DbType.Decimal, EntryTax);
                ImpalDB.AddInParameter(cmd, "@CESS", DbType.Decimal, CESS);
                ImpalDB.AddInParameter(cmd, "@Warehouse_Surcharge", DbType.Decimal, WarehouseSurcharge);
                ImpalDB.AddInParameter(cmd, "@Freight", DbType.Decimal, Freight);

                ImpalDB.AddInParameter(cmd, "@Coupon_charges", DbType.Decimal, CouponCharges);
                ImpalDB.AddInParameter(cmd, "@Other_levies_1", DbType.Decimal, OtherLevies1);
                ImpalDB.AddInParameter(cmd, "@Other_levies_2", DbType.Decimal, OtherLevies2);

                ImpalDB.AddInParameter(cmd, "@ED_for_Selling_price", DbType.Decimal, EDForSellingPrice);
                ImpalDB.AddInParameter(cmd, "@Regular_GP", DbType.Decimal, RegularGP);
                ImpalDB.AddInParameter(cmd, "@GP_TOD", DbType.Decimal, GPTOD);
                ImpalDB.AddInParameter(cmd, "@GP_Bonus", DbType.Decimal, GPBonus);
                ImpalDB.AddInParameter(cmd, "@GP_TOD_BONUS", DbType.Decimal, GPTODBONUS);
                ImpalDB.AddInParameter(cmd, "@Spl_Turnover_Discount", DbType.Decimal, SplTurnoverDiscount);


                ImpalDB.AddInParameter(cmd, "@Addition_discount_1_ind", DbType.Decimal, AdditionDiscount_1_ind);
                ImpalDB.AddInParameter(cmd, "@Addition_discount_2_ind", DbType.Decimal, AdditionDiscount_2_ind);
                ImpalDB.AddInParameter(cmd, "@Addition_discount_3_ind", DbType.Decimal, AdditionDiscount_3_ind);
                ImpalDB.AddInParameter(cmd, "@Addition_discount_4_ind", DbType.Decimal, AdditionDiscount_4_ind);
                ImpalDB.AddInParameter(cmd, "@Addition_discount_5_ind", DbType.Decimal, AdditionDiscount_5_ind);

                ImpalDB.AddInParameter(cmd, "@Excise_duty_indicator", DbType.Decimal, ExciseDutyIndicator);
                ImpalDB.AddInParameter(cmd, "@Cess_Indicator", DbType.Decimal, CessIndicator);
                ImpalDB.AddInParameter(cmd, "@Insurance_Indicator", DbType.Decimal, InsuranceIndicator);
                ImpalDB.AddInParameter(cmd, "@Warehouse_Surcharge_Ind", DbType.Decimal, WarehouseSurchargeInd);
                ImpalDB.AddInParameter(cmd, "@Freight_Ind", DbType.Decimal, FreightInd);
                ImpalDB.AddInParameter(cmd, "@Turnover_Tax_Ind", DbType.Decimal, TurnoverTaxInd);
                ImpalDB.AddInParameter(cmd, "@Bonus_Ind", DbType.Decimal, BonusInd);
                ImpalDB.AddInParameter(cmd, "@Coupon_Ind", DbType.Decimal, CouponInd);
                ImpalDB.AddInParameter(cmd, "@Spl_TOD_Ind", DbType.Decimal, SplTODInd);
                ImpalDB.AddInParameter(cmd, "@HC_Ind", DbType.Decimal, HCInd);
                ImpalDB.AddInParameter(cmd, "@SLB_Roundoff_Ind", DbType.Decimal, SLBRoundoffInd);


                ImpalDB.AddInParameter(cmd, "@ED_Selling_Price_Ind", DbType.Decimal, EDSellingPriceInd);
                ImpalDB.AddInParameter(cmd, "@Spl_Disc_1_Ind", DbType.Decimal, SplDisc_1_Ind);
                ImpalDB.AddInParameter(cmd, "@Spl_Disc_2_Ind", DbType.Decimal, SplDisc_2_Ind);
                ImpalDB.AddInParameter(cmd, "@Calculation_Ind", DbType.Decimal, CalculationInd);
                ImpalDB.AddInParameter(cmd, "@Addition_Surcharge_Ind", DbType.Decimal, AdditionSurchargeInd);
                ImpalDB.AddInParameter(cmd, "@After_discount", DbType.Decimal, AfterDiscount);
                ImpalDB.AddInParameter(cmd, "@After_add_disc1", DbType.Decimal, AfterAddDisc1);
                ImpalDB.AddInParameter(cmd, "@After_add_disc2", DbType.Decimal, AfterAddDisc2);
                ImpalDB.AddInParameter(cmd, "@After_add_disc3", DbType.Decimal, AfterAddDisc3);
                ImpalDB.AddInParameter(cmd, "@After_add_disc4", DbType.Decimal, AfterAddDisc4);
                ImpalDB.AddInParameter(cmd, "@After_add_disc5", DbType.Decimal, AfterAddDisc5);
                ImpalDB.AddInParameter(cmd, "@After_ED", DbType.Decimal, AfterED);
                ImpalDB.AddInParameter(cmd, "@After_ST", DbType.Decimal, AfterST);
                ImpalDB.AddInParameter(cmd, "@After_SC", DbType.Decimal, AfterSC);
                ImpalDB.AddInParameter(cmd, "@After_ASC", DbType.Decimal, AfterASC);


                ImpalDB.AddInParameter(cmd, "@After_ET", DbType.Decimal, AfterET);
                ImpalDB.AddInParameter(cmd, "@After_Oct", DbType.Decimal, AfterOct);
                ImpalDB.AddInParameter(cmd, "@After_cess", DbType.Decimal, AfterCess);
                ImpalDB.AddInParameter(cmd, "@After_ol1", DbType.Decimal, AfterOl1);
                ImpalDB.AddInParameter(cmd, "@After_ol2", DbType.Decimal, AfterOl2);
                ImpalDB.AddInParameter(cmd, "@After_ins", DbType.Decimal, AfterIns);
                ImpalDB.AddInParameter(cmd, "@After_wsc", DbType.Decimal, AfterWsc);
                ImpalDB.AddInParameter(cmd, "@After_frt", DbType.Decimal, AfterFrt);
                ImpalDB.AddInParameter(cmd, "@After_tot", DbType.Decimal, AfterTot);
                ImpalDB.AddInParameter(cmd, "@After_bon", DbType.Decimal, AfterBon);
                ImpalDB.AddInParameter(cmd, "@After_coupon", DbType.Decimal, AfterCoupon);
                ImpalDB.AddInParameter(cmd, "@After_splTOD", DbType.Decimal, AfterSplTOD);
                ImpalDB.AddInParameter(cmd, "@After_HC", DbType.Decimal, AfterHC);
                ImpalDB.AddInParameter(cmd, "@After_add", DbType.Decimal, AfterAdd);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                int intCount = ImpalDB.ExecuteNonQuery(cmd);
                return intCount;
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
            finally
            {
                ImpalDB = null;

            }
        }

        public Int32 UpdateSLBQueryDetails()
        {
            Database ImpalDB = DataAccess.GetDatabase();

            try
            {
                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_UpdateSLBitemcal");

                ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, BranchCode);
                ImpalDB.AddInParameter(cmd, "@Supplier_Line_Code", DbType.String, SupplierLineCode);
                ImpalDB.AddInParameter(cmd, "@Item_Code", DbType.String, ItemCode);

                ImpalDB.AddInParameter(cmd, "@List_Price", DbType.Decimal, ListPrice);
                ImpalDB.AddInParameter(cmd, "@Supplier_Discount", DbType.Decimal, SupplierDiscount);
                ImpalDB.AddInParameter(cmd, "@Excise_Duty_Percentage", DbType.Decimal, ExciseDutyPercentage);
                ImpalDB.AddInParameter(cmd, "@Sales_Tax_Percentage", DbType.Decimal, SalesTaxPercentage);
                ImpalDB.AddInParameter(cmd, "@Surcharge_Percentage", DbType.Decimal, SurchargePercentage);

                ImpalDB.AddInParameter(cmd, "@Additional_Surcharge", DbType.Decimal, AdditionalSurcharge);
                ImpalDB.AddInParameter(cmd, "@Turnover_Tax", DbType.Decimal, TurnoverTax);
                ImpalDB.AddInParameter(cmd, "@Octroi", DbType.Decimal, Octroi);
                ImpalDB.AddInParameter(cmd, "@Insurance", DbType.Decimal, Insurance);
                ImpalDB.AddInParameter(cmd, "@Turnover_Discount", DbType.Decimal, TurnoverDiscount);
                ImpalDB.AddInParameter(cmd, "@Bonus", DbType.Decimal, Bonus);
                ImpalDB.AddInParameter(cmd, "@Dealers_Discount", DbType.Decimal, DealersDiscount);

                ImpalDB.AddInParameter(cmd, "@Handling_Charges_Percentage", DbType.Decimal, HandlingChargesPercentage);
                ImpalDB.AddInParameter(cmd, "@Gross_Profit_Percentage", DbType.Decimal, GrossProfitPercentage);
                ImpalDB.AddInParameter(cmd, "@SLB_Value", DbType.Decimal, SLBValue);
                ImpalDB.AddInParameter(cmd, "@Cost_Price", DbType.Decimal, CostPrice);
                ImpalDB.AddInParameter(cmd, "@Selling_Price", DbType.Decimal, SellingPrice);
                ImpalDB.AddInParameter(cmd, "@Special_Discount_1", DbType.Decimal, SpecialDiscount1);
                ImpalDB.AddInParameter(cmd, "@Special_Discount_2", DbType.Decimal, SpecialDiscount2);
                ImpalDB.AddInParameter(cmd, "@Addition_discount_1", DbType.Decimal, AdditionDiscount1);
                ImpalDB.AddInParameter(cmd, "@Addition_discount_2", DbType.Decimal, AdditionDiscount2);
                ImpalDB.AddInParameter(cmd, "@Addition_discount_3", DbType.Decimal, AdditionDiscount3);

                ImpalDB.AddInParameter(cmd, "@Addition_discount_4", DbType.Decimal, AdditionDiscount4);
                ImpalDB.AddInParameter(cmd, "@Addition_discount_5", DbType.Decimal, AdditionDiscount5);
                ImpalDB.AddInParameter(cmd, "@Entry_Tax", DbType.Decimal, EntryTax);
                ImpalDB.AddInParameter(cmd, "@CESS", DbType.Decimal, CESS);
                ImpalDB.AddInParameter(cmd, "@Warehouse_Surcharge", DbType.Decimal, WarehouseSurcharge);
                ImpalDB.AddInParameter(cmd, "@Freight", DbType.Decimal, Freight);

                ImpalDB.AddInParameter(cmd, "@Coupon_charges", DbType.Decimal, CouponCharges);
                ImpalDB.AddInParameter(cmd, "@Other_levies_1", DbType.Decimal, OtherLevies1);
                ImpalDB.AddInParameter(cmd, "@Other_levies_2", DbType.Decimal, OtherLevies2);

                ImpalDB.AddInParameter(cmd, "@ED_for_Selling_price", DbType.Decimal, EDForSellingPrice);
                ImpalDB.AddInParameter(cmd, "@Regular_GP", DbType.Decimal, RegularGP);
                ImpalDB.AddInParameter(cmd, "@GP_TOD", DbType.Decimal, GPTOD);
                ImpalDB.AddInParameter(cmd, "@GP_Bonus", DbType.Decimal, GPBonus);
                ImpalDB.AddInParameter(cmd, "@GP_TOD_BONUS", DbType.Decimal, GPTODBONUS);
                ImpalDB.AddInParameter(cmd, "@Spl_Turnover_Discount", DbType.Decimal, SplTurnoverDiscount);


                ImpalDB.AddInParameter(cmd, "@Addition_discount_1_ind", DbType.Decimal, AdditionDiscount_1_ind);
                ImpalDB.AddInParameter(cmd, "@Addition_discount_2_ind", DbType.Decimal, AdditionDiscount_2_ind);
                ImpalDB.AddInParameter(cmd, "@Addition_discount_3_ind", DbType.Decimal, AdditionDiscount_3_ind);
                ImpalDB.AddInParameter(cmd, "@Addition_discount_4_ind", DbType.Decimal, AdditionDiscount_4_ind);
                ImpalDB.AddInParameter(cmd, "@Addition_discount_5_ind", DbType.Decimal, AdditionDiscount_5_ind);

                ImpalDB.AddInParameter(cmd, "@Excise_duty_indicator", DbType.Decimal, ExciseDutyIndicator);
                ImpalDB.AddInParameter(cmd, "@Cess_Indicator", DbType.Decimal, CessIndicator);
                ImpalDB.AddInParameter(cmd, "@Insurance_Indicator", DbType.Decimal, InsuranceIndicator);
                ImpalDB.AddInParameter(cmd, "@Warehouse_Surcharge_Ind", DbType.Decimal, WarehouseSurchargeInd);
                ImpalDB.AddInParameter(cmd, "@Freight_Ind", DbType.Decimal, FreightInd);
                ImpalDB.AddInParameter(cmd, "@Turnover_Tax_Ind", DbType.Decimal, TurnoverTaxInd);
                ImpalDB.AddInParameter(cmd, "@Bonus_Ind", DbType.Decimal, BonusInd);
                ImpalDB.AddInParameter(cmd, "@Coupon_Ind", DbType.Decimal, CouponInd);
                ImpalDB.AddInParameter(cmd, "@Spl_TOD_Ind", DbType.Decimal, SplTODInd);
                ImpalDB.AddInParameter(cmd, "@HC_Ind", DbType.Decimal, HCInd);
                ImpalDB.AddInParameter(cmd, "@SLB_Roundoff_Ind", DbType.Decimal, SLBRoundoffInd);


                ImpalDB.AddInParameter(cmd, "@ED_Selling_Price_Ind", DbType.Decimal, EDSellingPriceInd);
                ImpalDB.AddInParameter(cmd, "@Spl_Disc_1_Ind", DbType.Decimal, SplDisc_1_Ind);
                ImpalDB.AddInParameter(cmd, "@Spl_Disc_2_Ind", DbType.Decimal, SplDisc_2_Ind);
                ImpalDB.AddInParameter(cmd, "@Calculation_Ind", DbType.Decimal, CalculationInd);
                ImpalDB.AddInParameter(cmd, "@Addition_Surcharge_Ind", DbType.Decimal, AdditionSurchargeInd);
                ImpalDB.AddInParameter(cmd, "@After_discount", DbType.Decimal, AfterDiscount);
                ImpalDB.AddInParameter(cmd, "@After_add_disc1", DbType.Decimal, AfterAddDisc1);
                ImpalDB.AddInParameter(cmd, "@After_add_disc2", DbType.Decimal, AfterAddDisc2);
                ImpalDB.AddInParameter(cmd, "@After_add_disc3", DbType.Decimal, AfterAddDisc3);
                ImpalDB.AddInParameter(cmd, "@After_add_disc4", DbType.Decimal, AfterAddDisc4);
                ImpalDB.AddInParameter(cmd, "@After_add_disc5", DbType.Decimal, AfterAddDisc5);
                ImpalDB.AddInParameter(cmd, "@After_ED", DbType.Decimal, AfterED);
                ImpalDB.AddInParameter(cmd, "@After_ST", DbType.Decimal, AfterST);
                ImpalDB.AddInParameter(cmd, "@After_SC", DbType.Decimal, AfterSC);
                ImpalDB.AddInParameter(cmd, "@After_ASC", DbType.Decimal, AfterASC);


                ImpalDB.AddInParameter(cmd, "@After_ET", DbType.Decimal, AfterET);
                ImpalDB.AddInParameter(cmd, "@After_Oct", DbType.Decimal, AfterOct);
                ImpalDB.AddInParameter(cmd, "@After_cess", DbType.Decimal, AfterCess);
                ImpalDB.AddInParameter(cmd, "@After_ol1", DbType.Decimal, AfterOl1);
                ImpalDB.AddInParameter(cmd, "@After_ol2", DbType.Decimal, AfterOl2);
                ImpalDB.AddInParameter(cmd, "@After_ins", DbType.Decimal, AfterIns);
                ImpalDB.AddInParameter(cmd, "@After_wsc", DbType.Decimal, AfterWsc);
                ImpalDB.AddInParameter(cmd, "@After_frt", DbType.Decimal, AfterFrt);
                ImpalDB.AddInParameter(cmd, "@After_tot", DbType.Decimal, AfterTot);
                ImpalDB.AddInParameter(cmd, "@After_bon", DbType.Decimal, AfterBon);
                ImpalDB.AddInParameter(cmd, "@After_coupon", DbType.Decimal, AfterCoupon);
                ImpalDB.AddInParameter(cmd, "@After_splTOD", DbType.Decimal, AfterSplTOD);
                ImpalDB.AddInParameter(cmd, "@After_HC", DbType.Decimal, AfterHC);
                ImpalDB.AddInParameter(cmd, "@After_add", DbType.Decimal, AfterAdd);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                int intCount = ImpalDB.ExecuteNonQuery(cmd);
                return intCount;
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
            finally
            {
                ImpalDB = null;

            }
        }
        
    }


