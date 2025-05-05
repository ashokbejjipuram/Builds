using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMPALLibrary;
using IMPALLibrary.Masters;
using IMPALLibrary.Common;
using System.Data;

namespace IMPALWeb.Masters.SLB
{

    public partial class SLBQuery : System.Web.UI.Page
    {
        DataSet dsSLB = new DataSet();
        private string pstrBranch = default(string);

        protected void Page_Init()
        {
            try
            {
                SLBFormView.EditItemTemplate = SLBFormView.ItemTemplate;
                SLBFormView.InsertItemTemplate = SLBFormView.ItemTemplate;
            }
            catch (Exception)
            {

                throw;
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            pstrBranch = Session["BranchCode"].ToString();
            if (!IsPostBack)
            {
                fnPopulateSupplierLine(ddlSupplierLineCode);
                fnPopulateBranch(ddlBranch);
                SLBFormView.DefaultMode = FormViewMode.Insert;
                if (pstrBranch == "CRP")
                {
                    fnPopulateBranch((DropDownList)SLBFormView.FindControl("ddlBranchCode"));
                    fnPopulateSupplierLine((DropDownList)SLBFormView.FindControl("ddlSLC"));
                    fnPopulateItemCode((DropDownList)SLBFormView.FindControl("ddlItemCode"), ((DropDownList)SLBFormView.FindControl("ddlSLC")).SelectedValue);

                    fnPopulateDropDown("ADisc1Ind", (DropDownList)SLBFormView.FindControl("ddlADisc1"));
                    fnPopulateDropDown("ADisc2Ind", (DropDownList)SLBFormView.FindControl("ddlADisc2"));
                    fnPopulateDropDown("ADisc3Ind", (DropDownList)SLBFormView.FindControl("ddlADisc3"));
                    fnPopulateDropDown("ADisc4Ind", (DropDownList)SLBFormView.FindControl("ddlADisc4"));
                    fnPopulateDropDown("ADisc5Ind", (DropDownList)SLBFormView.FindControl("ddlADisc5"));
                    fnPopulateDropDown("ExciseDutyInd", (DropDownList)SLBFormView.FindControl("ddlExciseDuty"));
                    fnPopulateDropDown("AdditionalSurchargeInd", (DropDownList)SLBFormView.FindControl("ddlAdditionalSurchargeInd"));
                    fnPopulateDropDown("CESSCalculationIndicator", (DropDownList)SLBFormView.FindControl("ddlCESSCalculationIndicator"));
                    fnPopulateDropDown("InsuranceIndicator", (DropDownList)SLBFormView.FindControl("ddlInsuranceIndicator"));
                    fnPopulateDropDown("WarehouseSCIndicator", (DropDownList)SLBFormView.FindControl("ddlWarehouseSCIndicator"));
                    fnPopulateDropDown("FreightCalculationIndicator", (DropDownList)SLBFormView.FindControl("ddlFreightCalculationIndicator"));
                    fnPopulateDropDown("TOTInd", (DropDownList)SLBFormView.FindControl("ddlTOTind"));
                    fnPopulateDropDown("BonusCalculationInd", (DropDownList)SLBFormView.FindControl("ddlBonusCalculationInd"));
                    fnPopulateDropDown("CouponInd", (DropDownList)SLBFormView.FindControl("ddlCoupleInd"));
                    fnPopulateDropDown("SplTODInd", (DropDownList)SLBFormView.FindControl("ddlSplTODInd"));
                    fnPopulateDropDown("CalculationIndicator", (DropDownList)SLBFormView.FindControl("ddlCalculationIndicator"));
                    fnPopulateDropDown("HCInd", (DropDownList)SLBFormView.FindControl("ddlHCInd"));
                    fnPopulateDropDown("SLBRoundingOffInd", (DropDownList)SLBFormView.FindControl("ddlSLBRoundingOff"));
                    fnPopulateDropDown("EDForSellingPriceInd", (DropDownList)SLBFormView.FindControl("ddlEDForSellingPriceInd"));
                    fnPopulateDropDown("SplDisc1Ind", (DropDownList)SLBFormView.FindControl("ddlSPLDisc1Ind"));
                    fnPopulateDropDown("SplDisc1Ind", (DropDownList)SLBFormView.FindControl("ddlSPLDisc2Ind"));

                    ((TextBox)SLBFormView.FindControl("txtListPrice")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtPurchaseDiscount")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtAddDisc1")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtAddDisc2")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtAddDisc3")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtAddDisc4")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtAddDisc5")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtExciseDuty")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtSalesTax")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtSurchage")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtAdditinalSurchage")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtEntryTax")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtOctroi")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtCess")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtOtherLevies1")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtOtherLevies2")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtInsurance")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtWarehouseSurcharge")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtFreight")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtTurnoverTax")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtBonus")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtCouponCharges")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtGrossProfit")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtTurnoverDiscount")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtSpecialTurnoverDiscount")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtDealersDiscount")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtHandlingCharges")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtSLBText")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtEDforSellingPrice")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("TxtSpecialDiscount1")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("TxtSpecialDiscount2")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtCostPrice")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtSellingPrice")).Text = "0.00"; ;
                    ((TextBox)SLBFormView.FindControl("txtRegularGrossProfit")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtGP1")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtGP2")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtGP3")).Text = "0.00";

                    BtnSubmit.Enabled = false;
                    BtnSubmit.Text = "Add";
                    btnCheck.Enabled = true;
                }
                else
                {
                    ddlBranch.SelectedValue = pstrBranch;
                    PnlSLB.Enabled = false;
                    ((TextBox)SLBFormView.FindControl("txtListPrice")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtPurchaseDiscount")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtAddDisc1")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtAddDisc2")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtAddDisc3")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtAddDisc4")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtAddDisc5")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtExciseDuty")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtSalesTax")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtSurchage")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtAdditinalSurchage")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtEntryTax")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtOctroi")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtCess")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtOtherLevies1")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtOtherLevies2")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtInsurance")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtWarehouseSurcharge")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtFreight")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtTurnoverTax")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtBonus")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtCouponCharges")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtGrossProfit")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtTurnoverDiscount")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtSpecialTurnoverDiscount")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtDealersDiscount")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtHandlingCharges")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtSLBText")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtEDforSellingPrice")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("TxtSpecialDiscount1")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("TxtSpecialDiscount2")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtCostPrice")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtSellingPrice")).Text = "0.00"; ;
                    ((TextBox)SLBFormView.FindControl("txtRegularGrossProfit")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtGP1")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtGP2")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtGP3")).Text = "0.00";
                    BtnSubmit.Enabled = false;
                    BtnSubmit.Text = "Add";
                    btnCheck.Enabled = false;
                }

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if ((string)Session["RoleCode"] == "BEDP")
            {
                SLBFormView.ChangeMode(FormViewMode.ReadOnly);
                PnlSLB.Visible = true;
                PnlSLB.Enabled = false;
                GetSLBQuery();
                if (SLBFormView.DataItemCount == 0)
                {
                    ((Label)SLBFormView.FindControl("lblNoResult")).Visible = true;
                    btnCheck.Enabled = false;
                }
                else
                {
                    //((Label)SLBFormView.FindControl("lblNoResult")).Visible = false;
                    btnCheck.Enabled = true;
                }

            }
            else
            {
                SLBFormView.ChangeMode(FormViewMode.Edit);
                PnlSLB.Visible = true;
                PnlSLB.Enabled = true;
                GetSLBQuery();
                if (SLBFormView.DataItemCount == 0)
                {
                    ((Label)SLBFormView.FindControl("lblNoResult")).Visible = true;
                    btnCheck.Enabled = false;
                }
                else
                {
                    ((DropDownList)SLBFormView.FindControl("ddlBranchCode")).Enabled = false;
                    ((DropDownList)SLBFormView.FindControl("ddlSLC")).Enabled = false;
                    ((DropDownList)SLBFormView.FindControl("ddlItemCode")).Enabled = false;
                    //((Label)SLBFormView.FindControl("lblNoResult")).Visible = false;
                    btnCheck.Enabled = true;
                }
                BtnSubmit.Enabled = false;
                BtnSubmit.Text = "Update";
            }
        }

        protected void GetSLBQuery()
        {
            SLBQueries objSLB = new SLBQueries();

            try
            {
                dsSLB = objSLB.GetSLBQueryDetails(ddlBranch.SelectedValue, ddlSupplierLineCode.SelectedValue, ddlLCode.SelectedValue);
                SLBFormView.DataSource = dsSLB;
                SLBFormView.DataBind();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
            finally
            {
                objSLB = null;
            }
        }
        protected void fnPopulateDropDown(string strType, DropDownList ddlList)
        {

            try
            {
                ImpalLibrary lib = new ImpalLibrary();
                List<DropDownListValue> drop = new List<DropDownListValue>();
                drop = lib.GetDropDownListValues(strType);
                ddlList.DataSource = drop;
                ddlList.DataValueField = "DisplayValue";
                ddlList.DataTextField = "DisplayText";
                ddlList.DataBind();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        protected void fnPopulateBranch(DropDownList dropdownlist)
        {
            Branches objSLB = new Branches();
            try
            {
                dropdownlist.DataSource = objSLB.GetAllLinewiseBranches("", "OnlyBranch", Session["BranchCode"].ToString());
                dropdownlist.DataValueField = "BranchCode";
                dropdownlist.DataTextField = "BranchName";
                dropdownlist.DataBind();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
            finally
            {
                objSLB = null;
            }
        }


        protected void fnPopulateSupplierLine(DropDownList dropdownlist)
        {
            SLBs objSLB = new SLBs();
            try
            {
                dropdownlist.DataSource = objSLB.GetSupplierLineDetails();
                dropdownlist.DataValueField = "SLBCode";
                dropdownlist.DataTextField = "Description";
                dropdownlist.DataBind();

                dropdownlist.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
            finally
            {
                objSLB = null;
            }
        }

        protected DataTable fnGetDiscountDetails(string strSuppliercode)
        {
            SLBQueries objSLB = new SLBQueries();
            DataTable dtSLB;
            try
            {
                return dtSLB = objSLB.GetAdditionalDiscounts(strSuppliercode);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
            finally
            {
                objSLB = null;
            }
        }

        protected string fnGetExciseDuty(string strItemCode)
        {
            SLBQueries objSLB = new SLBQueries();
            try
            {
                return objSLB.GetExciseDuty(strItemCode);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
            finally
            {
                objSLB = null;
            }
        }

        protected void fnPopulateExciseDuty()
        {
            string strExciseDuty = fnGetExciseDuty(((DropDownList)SLBFormView.FindControl("ddlItemCode")).SelectedValue);
            if (!string.IsNullOrEmpty(strExciseDuty))
                ((TextBox)SLBFormView.FindControl("txtExciseDuty")).Text = string.Format("{0:0.00}", Convert.ToDecimal(strExciseDuty));
            else
                ((TextBox)SLBFormView.FindControl("txtExciseDuty")).Text = "0.00";
        }

        protected void fnPopulateDiscountDetails()
        {
            DataTable dtSLB;
            try
            {
                dtSLB = fnGetDiscountDetails(((DropDownList)SLBFormView.FindControl("ddlSLC")).SelectedValue);
                if (dtSLB != null)
                {
                    if (dtSLB.Rows.Count > 0)
                    {
                        ((TextBox)SLBFormView.FindControl("txtAddDisc1")).Text = string.Format("{0:0.00}", Convert.ToDecimal(dtSLB.Rows[0]["Additional_Discount1"]));
                        ((TextBox)SLBFormView.FindControl("txtAddDisc2")).Text = string.Format("{0:0.00}", Convert.ToDecimal(dtSLB.Rows[0]["Additional_Discount2"]));
                        ((TextBox)SLBFormView.FindControl("txtAddDisc3")).Text = string.Format("{0:0.00}", Convert.ToDecimal(dtSLB.Rows[0]["Additional_Discount3"]));
                        ((TextBox)SLBFormView.FindControl("txtAddDisc4")).Text = string.Format("{0:0.00}", Convert.ToDecimal(dtSLB.Rows[0]["Additional_Discount4"]));
                        ((TextBox)SLBFormView.FindControl("txtAddDisc5")).Text = string.Format("{0:0.00}", Convert.ToDecimal(dtSLB.Rows[0]["Additional_Discount5"]));

                    }
                    else
                    {
                        ((TextBox)SLBFormView.FindControl("txtAddDisc1")).Text = "0.00";
                        ((TextBox)SLBFormView.FindControl("txtAddDisc2")).Text = "0.00";
                        ((TextBox)SLBFormView.FindControl("txtAddDisc3")).Text = "0.00";
                        ((TextBox)SLBFormView.FindControl("txtAddDisc4")).Text = "0.00";
                        ((TextBox)SLBFormView.FindControl("txtAddDisc5")).Text = "0.00";

                    }
                }
                else
                {
                    ((TextBox)SLBFormView.FindControl("txtAddDisc1")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtAddDisc2")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtAddDisc3")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtAddDisc4")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtAddDisc5")).Text = "0.00";
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        protected void CheckValidation()
        {
            try
            {
                SLBQueries objSLB = new SLBQueries();
                Decimal decDiscount = 0.00M;
                Decimal decCalPercentage = 0.00M;
                Decimal decAd;
                Decimal decAd1;
                Decimal decAad1;
                Decimal decVal1 = 0.00M;
                Decimal decListPriceValue = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtListPrice")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtListPrice")).Text);
                Decimal decPurchaseDiscount = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtPurchaseDiscount")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtPurchaseDiscount")).Text);


                string strZone = objSLB.GetZone(((DropDownList)SLBFormView.FindControl("ddlBranchCode")).SelectedValue);
                string strZoneType = default(string);
                if (strZone.Equals("1") || strZone.Equals("3"))
                    strZoneType = "WS";
                else if (strZone.Equals("2") || strZone.Equals("4"))
                    strZoneType = "NE";
                else
                    strZoneType = "";

                string strAdditionDiscount1SelectedValue = ((DropDownList)SLBFormView.FindControl("ddlADisc1")).SelectedValue;
                decDiscount = Math.Round((decListPriceValue * decPurchaseDiscount) / 100);
                decAd = decListPriceValue - decDiscount;

                if (strAdditionDiscount1SelectedValue == "1")
                {
                    decVal1 = decListPriceValue;
                }
                else if (strAdditionDiscount1SelectedValue == "2")
                {
                    decVal1 = decAd;
                }
                Decimal decAdditionDiscount1 = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtAddDisc1")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtAddDisc1")).Text);
                decAd1 = Math.Round(decVal1 * decAdditionDiscount1) / 100;
                decAad1 = decAd - decAd1;

                string strAdditionDiscount2SelectedValue = ((DropDownList)SLBFormView.FindControl("ddlADisc2")).SelectedValue;
                if (strAdditionDiscount2SelectedValue == "1")
                    decVal1 = decListPriceValue;
                else if (strAdditionDiscount2SelectedValue == "2")
                    decVal1 = decAd;
                else if (strAdditionDiscount2SelectedValue == "3")
                    decVal1 = decAad1;

                Decimal decAdditionDiscount2 = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtAddDisc2")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtAddDisc2")).Text);
                Decimal decAd2 = Math.Round(decVal1 * decAdditionDiscount2) / 100;
                Decimal decAad2 = decAad1 - decAd2;



                string strAdditionDiscount3SelectedValue = ((DropDownList)SLBFormView.FindControl("ddlADisc3")).SelectedValue;
                if (strAdditionDiscount3SelectedValue == "1")
                    decVal1 = decListPriceValue;
                else if (strAdditionDiscount3SelectedValue == "2")
                    decVal1 = decAd;
                else if (strAdditionDiscount3SelectedValue == "3")
                    decVal1 = decAad1;
                else if (strAdditionDiscount3SelectedValue == "4")
                    decVal1 = decAad2;

                Decimal decAdditionDiscount3 = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtAddDisc3")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtAddDisc3")).Text);
                Decimal decAd3 = Math.Round(decVal1 * decAdditionDiscount3) / 100;
                Decimal decAad3 = decAad2 - decAd3;

                String strAdditionDiscount4SelectedValue = ((DropDownList)SLBFormView.FindControl("ddlADisc4")).SelectedValue;
                if (strAdditionDiscount4SelectedValue == "1")
                    decVal1 = decListPriceValue;
                else if (strAdditionDiscount4SelectedValue == "2")
                    decVal1 = decAd;
                else if (strAdditionDiscount4SelectedValue == "3")
                    decVal1 = decAad1;
                else if (strAdditionDiscount4SelectedValue == "4")
                    decVal1 = decAad2;
                else if (strAdditionDiscount4SelectedValue == "5")
                    decVal1 = decAad3;

                Decimal decAdditionDiscount4 = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtAddDisc4")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtAddDisc4")).Text);
                Decimal decAd4 = Math.Round(decVal1 * decAdditionDiscount4) / 100;
                Decimal decAad4 = decAad3 - decAd4;


                String strAdditionDiscount5SelectedValue = ((DropDownList)SLBFormView.FindControl("ddlADisc5")).SelectedValue;
                if (strAdditionDiscount5SelectedValue == "1")
                    decVal1 = decListPriceValue;
                else if (strAdditionDiscount5SelectedValue == "2")
                    decVal1 = decAd;
                else if (strAdditionDiscount5SelectedValue == "3")
                    decVal1 = decAad1;
                else if (strAdditionDiscount5SelectedValue == "4")
                    decVal1 = decAad2;
                else if (strAdditionDiscount5SelectedValue == "5")
                    decVal1 = decAad3;
                else if (strAdditionDiscount5SelectedValue == "6")
                    decVal1 = decAad4;

                Decimal decAdditionDiscount5 = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtAddDisc5")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtAddDisc5")).Text);
                Decimal decAd5 = Math.Round(decVal1 * decAdditionDiscount4) / 100;
                Decimal decAad5 = decAad4 - decAd5;


                String strExciseDutySelectedValue = ((DropDownList)SLBFormView.FindControl("ddlExciseDuty")).SelectedValue;
                if (strExciseDutySelectedValue == "1")
                    decVal1 = decListPriceValue;
                else if (strExciseDutySelectedValue == "2")
                    decVal1 = decAd;
                else if (strExciseDutySelectedValue == "3")
                    decVal1 = decAad1;
                else if (strExciseDutySelectedValue == "4")
                    decVal1 = decAad2;
                else if (strExciseDutySelectedValue == "5")
                    decVal1 = decAad3;
                else if (strExciseDutySelectedValue == "6")
                    decVal1 = decAad4;
                else if (strExciseDutySelectedValue == "7")
                    decVal1 = decAad5;

                Decimal decExciseDuty = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtExciseDuty")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtExciseDuty")).Text);
                Decimal decEd = Math.Round(decVal1 * decExciseDuty) / 100;
                Decimal decAed = decAad5 + decEd;

                if (strExciseDutySelectedValue == "8")
                {
                    decEd = decExciseDuty;
                    decAed = decAad5 + decEd;
                }

                Decimal DecSalesTaxValue = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtSalesTax")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtSalesTax")).Text);//txtSalesTax
                Decimal DecSurchargeValue = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtSurchage")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtSurchage")).Text);
                Decimal decST = Math.Round(decAed * DecSalesTaxValue) / 100;
                Decimal decAst = decAed + decST;
                Decimal decSC = Math.Round(decST * DecSurchargeValue) / 100;
                Decimal decAftsc = decAst + decSC;

                String strAdditionalSurchargeIndSelectedValue = ((DropDownList)SLBFormView.FindControl("ddlAdditionalSurchargeInd")).SelectedValue;
                if (strAdditionalSurchargeIndSelectedValue == "1")
                    decVal1 = decST;
                else if (strAdditionalSurchargeIndSelectedValue == "2")
                    decVal1 = decSC;

                Decimal decAdditionalSurchage = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtAdditinalSurchage")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtAdditinalSurchage")).Text);
                Decimal decEntryTax = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtEntryTax")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtEntryTax")).Text);
                Decimal decOctroi = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtOctroi")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtOctroi")).Text);//
                Decimal decAsc = Math.Round(decVal1 * decAdditionalSurchage) / 100;
                Decimal decAftasc = decAftsc + decAsc;
                Decimal decEt = Math.Round(decAftasc * decEntryTax) / 100;
                Decimal decAet = decAftasc + decEt;
                Decimal decOct = Math.Round(decAet * decOctroi) / 100;
                Decimal decAoct = decAet + decOct;

                String strCESSIndSelectedValue = ((DropDownList)SLBFormView.FindControl("ddlCESSCalculationIndicator")).SelectedValue;
                Decimal decCESSValue = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtCess")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtCess")).Text);
                if (strCESSIndSelectedValue == "1")
                    decVal1 = decAoct;
                else if (strCESSIndSelectedValue == "2")
                {
                    decVal1 = 0;
                    decCalPercentage += decCESSValue;
                }

                Decimal decCESS = Math.Round(decVal1 * decCESSValue) / 100;
                Decimal decAcess = decAoct + decCESS;
                Decimal decOtherLevies1 = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtOtherLevies1")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtOtherLevies1")).Text);
                Decimal decOl1 = Math.Round(decAcess * decOtherLevies1) / 100;
                Decimal decAol1 = decAcess + decOl1;
                Decimal decOtherLevies2 = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtOtherLevies2")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtOtherLevies2")).Text);
                Decimal decOl2 = Math.Round(decAol1 * decOtherLevies2) / 100;
                Decimal decAol2 = decAol1 + decOl2;

                String strInsuranceIndicatorSelectedValue = ((DropDownList)SLBFormView.FindControl("ddlInsuranceIndicator")).SelectedValue;
                Decimal decInsuranceValue = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtInsurance")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtInsurance")).Text);
                if (strInsuranceIndicatorSelectedValue == "1")
                    decVal1 = decAol2;
                else if (strInsuranceIndicatorSelectedValue == "2")
                {
                    decVal1 = 0;
                    decCalPercentage += decInsuranceValue;
                }

                Decimal decIns = Math.Round(decVal1 * decInsuranceValue) / 100;
                Decimal decAins = decVal1 + decIns;

                String strWarehouseSCIndicatorSelectedValue = ((DropDownList)SLBFormView.FindControl("ddlWarehouseSCIndicator")).SelectedValue;

                if (strWarehouseSCIndicatorSelectedValue == "1")
                    decVal1 = decAed;
                else if (strWarehouseSCIndicatorSelectedValue == "2")
                    decVal1 = decAftasc;
                else if (strWarehouseSCIndicatorSelectedValue == "3")
                    decVal1 = decAins;

                Decimal decWSCValue = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtWarehouseSurcharge")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtWarehouseSurcharge")).Text);
                Decimal decWsc = Math.Round(decVal1 * decWSCValue) / 100;
                Decimal decAwsc = decIns + decWsc;

                String strFreightCalIndSelectedValue = ((DropDownList)SLBFormView.FindControl("ddlFreightCalculationIndicator")).SelectedValue;
                Decimal decFreightValue = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtFreight")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtFreight")).Text);
                if (strFreightCalIndSelectedValue == "1")
                    decVal1 = decAwsc;
                else if (strFreightCalIndSelectedValue == "2")
                {
                    decVal1 = 0;
                    decCalPercentage += decFreightValue;
                }

                Decimal decFrt = Math.Round(decVal1 * decFreightValue) / 100;
                Decimal decAfrt = decAwsc + decFrt;

                if (strFreightCalIndSelectedValue == "3")
                    decAfrt = decAwsc + decFreightValue;


                String strTotIndSelectedValue = ((DropDownList)SLBFormView.FindControl("ddlTOTind")).SelectedValue;
                Decimal decTotValue = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtTurnoverTax")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtTurnoverTax")).Text);
                if (strTotIndSelectedValue == "1")
                    decVal1 = decAfrt;
                else if (strTotIndSelectedValue == "2")
                {
                    decVal1 = 0;
                    decCalPercentage += decTotValue;
                }
                Decimal decTot = Math.Round(decVal1 * decTotValue) / 100;
                Decimal decAtot = decAfrt + decTot;

                String strBonusCalIndSelectedValue = ((DropDownList)SLBFormView.FindControl("ddlBonusCalculationInd")).SelectedValue;
                if (strBonusCalIndSelectedValue == "1")
                    decVal1 = decListPriceValue;
                else if (strBonusCalIndSelectedValue == "2")
                    decVal1 = decAd;
                else if (strBonusCalIndSelectedValue == "3")
                    decVal1 = decAad1;
                else if (strBonusCalIndSelectedValue == "4")
                    decVal1 = decAad2;
                else if (strBonusCalIndSelectedValue == "5")
                    decVal1 = decAad3;
                else if (strBonusCalIndSelectedValue == "6")
                    decVal1 = decAad4;
                else if (strBonusCalIndSelectedValue == "7")
                    decVal1 = decAad5;

                Decimal decBonValue = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtBonus")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtBonus")).Text);
                Decimal decBon = Math.Round(decVal1 * decBonValue) / 100;
                Decimal decAbon = decAtot - decBon;

                if (strBonusCalIndSelectedValue == "8")
                    decAbon = decAtot - decBonValue;

                String strCoupleIndSelectedValue = ((DropDownList)SLBFormView.FindControl("ddlCoupleInd")).SelectedValue;
                if (strCoupleIndSelectedValue == "1")
                    decVal1 = decListPriceValue;
                else if (strCoupleIndSelectedValue == "2")
                    decVal1 = decAd;
                else if (strCoupleIndSelectedValue == "3")
                    decVal1 = decAad1;
                else if (strCoupleIndSelectedValue == "4")
                    decVal1 = decAad2;
                else if (strCoupleIndSelectedValue == "5")
                    decVal1 = decAad3;
                else if (strCoupleIndSelectedValue == "6")
                    decVal1 = decAad4;
                else if (strCoupleIndSelectedValue == "7")
                    decVal1 = decAad5;

                Decimal decCoupleValue = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtCouponCharges")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtCouponCharges")).Text);
                Decimal decCou = Math.Round(decVal1 * decCoupleValue) / 100;
                Decimal decAcou = decAbon - decCou;

                if (strCoupleIndSelectedValue == "8")
                    decAcou = decAbon - decCoupleValue;


                String strCalculationIndSelectedValue = ((DropDownList)SLBFormView.FindControl("ddlCalculationIndicator")).SelectedValue;
                Decimal decGrossProfitValue = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtGrossProfit")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtGrossProfit")).Text);
                Decimal decSLBValue = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtSLBText")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtSLBText")).Text);

                Decimal decGP = 0.00M;
                Decimal decSpne = 0.00M;
                Decimal decCP;
                Decimal decCoune;
                Decimal decAcoune;
                if (strCalculationIndSelectedValue == "1" || strCalculationIndSelectedValue == "3")
                    decGP = decGrossProfitValue;
                else if (strCalculationIndSelectedValue == "2")
                {
                    decSpne = Math.Round((decListPriceValue * ((100 + decSLBValue) / 100)) * 100) / 100;
                    decGP = Math.Round((((decSpne - decAtot) / decSpne) * 100) * 100) / 100;
                }

                if (decCalPercentage == 0)
                    decSpne = Math.Round(decAbon / (100 - decGP)) / 100;
                else if (decCalPercentage > 0)
                {
                    decCalPercentage += decGP;
                    decCP = 100 - decCalPercentage;
                    decSpne = Math.Round(decAtot * 100 / decCP);
                    if (strCESSIndSelectedValue == "2")
                    {
                        decCESS = Math.Round((decSpne * Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtCess")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtCess")).Text))) / 100;
                        decAtot = Math.Round(decAtot + decCESS);
                    }
                    if (strInsuranceIndicatorSelectedValue == "2")
                    {
                        decIns = Math.Round(decSpne * decInsuranceValue) / 100;
                        decAtot = Math.Round(decAtot + decIns);
                    }

                    if (strFreightCalIndSelectedValue == "2")
                    {
                        decFrt = Math.Round(decSpne * decFreightValue) / 100;
                        decAfrt = Math.Round(decAtot + decFrt);
                    }
                    if (strTotIndSelectedValue == "2")
                    {
                        decTot = Math.Round(decSpne * decTotValue) / 100;
                        decAtot = Math.Round(decAtot + decTot);
                        decAbon = decAtot - decBon;
                        decAcou = decAbon - decCou;
                    }

                    if (strCoupleIndSelectedValue == "9")
                    {
                        decCoune = decSpne * decCoupleValue / 100;
                        decAcoune = decAbon - decCoune;
                    }
                }
                Decimal decDealerDiscountValue = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtDealersDiscount")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtDealersDiscount")).Text);
                Decimal decDD = Math.Round(decListPriceValue * decDealerDiscountValue) / 100;
                Decimal decAdd = decListPriceValue - decDD;
                Decimal decEdsp = 0.00M;
                string strEDForSellingPriceIndSelectedValue = ((DropDownList)SLBFormView.FindControl("ddlEDForSellingPriceInd")).SelectedValue;//ddlEDForSellingPriceInd
                Decimal decEdForSellingPriceValue = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtEDforSellingPrice")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtEDforSellingPrice")).Text);//
                if (strEDForSellingPriceIndSelectedValue == "1")
                    decEdsp = decEd;
                else if (strEDForSellingPriceIndSelectedValue == "2")
                    decEdsp = decEdForSellingPriceValue;


                Decimal decSaed = decAdd + decEdsp;
                String strHCIndSelectedValue = ((DropDownList)SLBFormView.FindControl("ddlHCInd")).SelectedValue;
                if (strHCIndSelectedValue == "1")
                    decVal1 = decListPriceValue;
                else if (strHCIndSelectedValue == "2")
                    decVal1 = decSaed;

                Decimal decHc = 0.00M;
                Decimal decHcper = 0.00M;
                Decimal decAhc = 0.00M;
                Decimal decSpws;
                Decimal decSlbws;
                Decimal decHcValue = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtHandlingCharges")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtHandlingCharges")).Text);
                string strSLBRoundOffSelectedValue = ((DropDownList)SLBFormView.FindControl("ddlSLBRoundingOff")).SelectedValue;//
                if (strCalculationIndSelectedValue == "1")
                {

                    decHc = decHcValue;
                    decHcper = decHcValue;
                    decHc = Math.Round(decVal1 * decHcValue) / 100;
                    decAhc = decSaed + decHc;
                    decSpws = decAhc;
                    decSlbws = (decListPriceValue == 0.00M ? 0.00M : ((decSpws - decListPriceValue) / decListPriceValue) * 100);

                    if (strSLBRoundOffSelectedValue == "1")
                    {

                        decSlbws = Math.Round(decSlbws);
                        decSlbws = Convert.ToInt32(decSlbws + 0.50M);
                    }
                }
                else if (strCalculationIndSelectedValue == "2")
                {
                    decSpws = Math.Round(decListPriceValue * (100 + decSLBValue)) / 100;
                    decHcper = decSpws / decSaed;
                    decVal1 = decSaed;
                    decAhc = decSaed * decHcper;
                }
                else if (strCalculationIndSelectedValue == "3")
                {
                    decSpws = decSpne;
                    decHcper = decSpws / decSaed;
                    decVal1 = decSaed;
                    decAhc = decSaed * decHcper;
                }

                decSpws = decAhc;
                Decimal decGpws = Math.Round((decAhc == 0.00M ? 0.00M : (((decAhc - decAtot) / decAhc) * 100) * 100) / 100);
                Decimal decSlbne = Math.Round((decListPriceValue == 0.00M ? 0.00M : ((decSpne - decListPriceValue) / decListPriceValue) * 100),2);
                decSlbws = Math.Round((decListPriceValue == 0.00M ? 0.00M : ((decSpws - decListPriceValue) / decListPriceValue) * 100),2);
                decHcper = Math.Round(((decHcper * 100) - 100) * 100) / 100;

                if (strZoneType == "NE")
                {
                    ((TextBox)SLBFormView.FindControl("txtCostPrice")).Text = Convert.ToString(decAbon);
                    ((TextBox)SLBFormView.FindControl("txtSellingPrice")).Text = Convert.ToString(decSpne);
                    ((TextBox)SLBFormView.FindControl("txtSLBText")).Text = Convert.ToString(decSlbne);
                    ((TextBox)SLBFormView.FindControl("txtAddDisc1")).Text = Convert.ToString(decAad1);
                }

                else if (strZoneType == "WS")
                {
                    ((TextBox)SLBFormView.FindControl("txtCostPrice")).Text = Convert.ToString(decAbon);
                    ((TextBox)SLBFormView.FindControl("txtSellingPrice")).Text = Convert.ToString(decAhc);
                    ((TextBox)SLBFormView.FindControl("txtSLBText")).Text = Convert.ToString(decSlbws);
                }

                string strSplTODInd = ((DropDownList)SLBFormView.FindControl("ddlSplTODInd")).SelectedValue;
                Decimal decSpecialTurnOverDiscountValue = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtSpecialTurnoverDiscount")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtSpecialTurnoverDiscount")).Text);
                Decimal decTurnoverDiscountValue = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtTurnoverDiscount")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtTurnoverDiscount")).Text); //txtTurnoverDiscount
                if (decSlbne < 0)
                {
                    if (strSplTODInd == "1")
                        decVal1 = decListPriceValue;
                    else if (strSplTODInd == "2")
                        decVal1 = decAdd;
                    else if (strSplTODInd == "3")
                        decVal1 = decSpne;
                }

                Decimal decSpneatod = Math.Round((decSpne - (decVal1 * decSpecialTurnOverDiscountValue / 100)),2);
                decSpneatod = Math.Round((decSpneatod - (decListPriceValue * decTurnoverDiscountValue / 100)),2);
                if (decSlbne >= 0)
                {
                    decSpneatod = Math.Round((decSpne - (decListPriceValue * decTurnoverDiscountValue / 100)),2);
                    decSpneatod = Math.Round((decSpneatod - (decListPriceValue * decTurnoverDiscountValue / 100)),2);
                }

                if (decSlbws < 0)
                {
                    if (strSplTODInd == "1")
                    {
                        decVal1 = decListPriceValue;
                    }
                    if (strSplTODInd == "2")
                    {
                        decVal1 = decAdd;
                    }
                    if (strSplTODInd == "3")
                    {
                        decVal1 = decSpws;
                    }
                }

                Decimal decSpwsatod = Math.Round((decSpws - (decVal1 * decSpecialTurnOverDiscountValue / 100)),2);
                decSpwsatod = Math.Round((decSpwsatod - (decListPriceValue * decTurnoverDiscountValue / 100)),2);
                if (decSlbws >= 0)
                {
                    decSpwsatod = Math.Round((decSpws - (decListPriceValue * decTurnoverDiscountValue / 100)),2);
                    decSpwsatod = Math.Round((decSpwsatod - (decListPriceValue * decSpecialTurnOverDiscountValue / 100)),2);
                }
                Decimal decGp1ne;

                if (decSpne == 0.00M)
                    decGp1ne = 0.00M;
                else
                    decGp1ne = Math.Round(((decSpne - decAtot) / decSpne) * 100,2);

                Decimal decGp1ws = Math.Round((decSpws == 0.00M ? 0.00M : ((decSpws - decAtot) / decSpws) * 100),2);

                Decimal decGp2ne = Math.Round((decSpne == 0.00M ? 0.00M : ((decSpne - decAbon) / decSpne) * 100),2);
                Decimal decGp2ws = Math.Round((decSpws == 0.00M ? 0.00M : ((decSpws - decAbon) / decSpws) * 100),2);

                Decimal decGp3ne = Math.Round((decSpneatod == 0.00M ? 0.00M : ((decSpneatod - decAtot) / decSpneatod) * 100),2);
                Decimal decGp3ws = Math.Round((decSpwsatod == 0.00M ? 0.00M : ((decSpwsatod - decAtot) / decSpwsatod) * 100),2);

                Decimal decGp4ne = Math.Round((decSpneatod == 0.00M ? 0.00M : ((decSpneatod - decAbon) / decSpneatod) * 100),2);
                Decimal decGp4ws = Math.Round((decSpwsatod == 0.00M ? 0.00M : ((decSpwsatod - decAbon) / decSpwsatod) * 100),2);

                if (strZoneType == "NE")
                {
                    ((TextBox)SLBFormView.FindControl("txtRegularGrossProfit")).Text = Convert.ToString(decGp1ne);
                    ((TextBox)SLBFormView.FindControl("txtGP1")).Text = Convert.ToString(decGp2ne);
                    ((TextBox)SLBFormView.FindControl("txtGP2")).Text = Convert.ToString(decGp3ne);
                    ((TextBox)SLBFormView.FindControl("txtGP3")).Text = Convert.ToString(decGp4ne);
                    ((TextBox)SLBFormView.FindControl("txtSpecialTurnoverDiscount")).Text = Convert.ToString(decSpneatod);
                    ViewState["AfterSplTOD"] = decSpneatod;
                }
                else if (strZoneType == "WS")
                {
                    ((TextBox)SLBFormView.FindControl("txtRegularGrossProfit")).Text = Convert.ToString(decGp1ws);
                    ((TextBox)SLBFormView.FindControl("txtGP1")).Text = Convert.ToString(decGp2ws);
                    ((TextBox)SLBFormView.FindControl("txtGP2")).Text = Convert.ToString(decGp3ws);
                    ((TextBox)SLBFormView.FindControl("txtGP3")).Text = Convert.ToString(decGp4ws);
                    ((TextBox)SLBFormView.FindControl("txtSpecialTurnoverDiscount")).Text = Convert.ToString(decSpwsatod);
                    ViewState["AfterSplTOD"] = decSpwsatod;
                }

                ViewState["AfterDiscount"] = decAd;
                ViewState["AfterAdditionalDiscount1"] = decAad1;
                ViewState["AfterAdditionalDiscount2"] = decAad2;
                ViewState["AfterAdditionalDiscount3"] = decAad3;
                ViewState["AfterAdditionalDiscount4"] = decAad4;
                ViewState["AfterAdditionalDiscount5"] = decAad5;
                ViewState["AfterExciseDuty"] = decAed;
                ViewState["AfterSalesTax"] = decAst;
                ViewState["AfterSurcharge"] = decAftsc;
                ViewState["AfterAdditionalSurcharge"] = decAftasc;
                ViewState["AfterEntryTax"] = decAet;
                ViewState["AfterOctroi"] = decAoct;
                ViewState["AfterCESS"] = decCESS;
                ViewState["AfterOtherLevies1"] = decOl1;
                ViewState["AfterOtherLevies2"] = decOl2;
                ViewState["AfterInsurance"] = decIns;
                ViewState["AfterWSC"] = decWsc;
                ViewState["AfterFreight"] = decFrt;

                ViewState["AfterTurnOverTax"] = decTot;
                ViewState["AfterBON"] = decBon;
                ViewState["AfterCoupon"] = decCou;
                //ViewState["AfterSplTOD"] = dec;
                ViewState["AfterHC"] = decHc;
                ViewState["AfterAdd"] = decAdd;

                BtnSubmit.Enabled = true;
            }
            catch (Exception exp)
            {
                BtnSubmit.Enabled = false;
                throw new Exception(exp.Message);
            }

        }



        protected void SLBFormView_DataBound(object sender, EventArgs e)
        {
            try
            {
                if (SLBFormView.DataItemCount > 0 || SLBFormView.CurrentMode == FormViewMode.Insert)
                {
                    fnPopulateBranch((DropDownList)SLBFormView.FindControl("ddlBranchCode"));
                    fnPopulateSupplierLine((DropDownList)SLBFormView.FindControl("ddlSLC"));
                    fnPopulateItemCode((DropDownList)SLBFormView.FindControl("ddlItemCode"), ((DropDownList)SLBFormView.FindControl("ddlSLC")).SelectedValue);

                    fnPopulateDropDown("ADisc1Ind", (DropDownList)SLBFormView.FindControl("ddlADisc1"));
                    fnPopulateDropDown("ADisc2Ind", (DropDownList)SLBFormView.FindControl("ddlADisc2"));
                    fnPopulateDropDown("ADisc3Ind", (DropDownList)SLBFormView.FindControl("ddlADisc3"));
                    fnPopulateDropDown("ADisc4Ind", (DropDownList)SLBFormView.FindControl("ddlADisc4"));
                    fnPopulateDropDown("ADisc5Ind", (DropDownList)SLBFormView.FindControl("ddlADisc5"));
                    fnPopulateDropDown("ExciseDutyInd", (DropDownList)SLBFormView.FindControl("ddlExciseDuty"));
                    fnPopulateDropDown("AdditionalSurchargeInd", (DropDownList)SLBFormView.FindControl("ddlAdditionalSurchargeInd"));
                    fnPopulateDropDown("CESSCalculationIndicator", (DropDownList)SLBFormView.FindControl("ddlCESSCalculationIndicator"));
                    fnPopulateDropDown("InsuranceIndicator", (DropDownList)SLBFormView.FindControl("ddlInsuranceIndicator"));
                    fnPopulateDropDown("WarehouseSCIndicator", (DropDownList)SLBFormView.FindControl("ddlWarehouseSCIndicator"));
                    fnPopulateDropDown("FreightCalculationIndicator", (DropDownList)SLBFormView.FindControl("ddlFreightCalculationIndicator"));
                    fnPopulateDropDown("TOTInd", (DropDownList)SLBFormView.FindControl("ddlTOTind"));
                    fnPopulateDropDown("BonusCalculationInd", (DropDownList)SLBFormView.FindControl("ddlBonusCalculationInd"));
                    fnPopulateDropDown("CouponInd", (DropDownList)SLBFormView.FindControl("ddlCoupleInd"));
                    fnPopulateDropDown("SplTODInd", (DropDownList)SLBFormView.FindControl("ddlSplTODInd"));
                    fnPopulateDropDown("CalculationIndicator", (DropDownList)SLBFormView.FindControl("ddlCalculationIndicator"));
                    fnPopulateDropDown("HCInd", (DropDownList)SLBFormView.FindControl("ddlHCInd"));
                    fnPopulateDropDown("SLBRoundingOffInd", (DropDownList)SLBFormView.FindControl("ddlSLBRoundingOff"));
                    fnPopulateDropDown("EDForSellingPriceInd", (DropDownList)SLBFormView.FindControl("ddlEDForSellingPriceInd"));
                    fnPopulateDropDown("SplDisc1Ind", (DropDownList)SLBFormView.FindControl("ddlSPLDisc1Ind"));
                    fnPopulateDropDown("SplDisc1Ind", (DropDownList)SLBFormView.FindControl("ddlSPLDisc2Ind"));

                    fnPopulateDiscountDetails();
                    fnPopulateExciseDuty();


                    if (dsSLB != null && dsSLB.Tables.Count > 0)
                    {
                        ((DropDownList)SLBFormView.FindControl("ddlBranchCode")).SelectedValue = ddlBranch.SelectedValue;
                        ((DropDownList)SLBFormView.FindControl("ddlSLC")).SelectedValue = ddlSupplierLineCode.SelectedValue;
                        if (!string.IsNullOrEmpty(ddlSupplierLineCode.SelectedValue))
                            fnPopulateItemCode((DropDownList)SLBFormView.FindControl("ddlItemCode"), ((DropDownList)SLBFormView.FindControl("ddlSLC")).SelectedValue);
                        ((DropDownList)SLBFormView.FindControl("ddlItemCode")).SelectedValue = ddlLCode.SelectedValue;
                        ((DropDownList)SLBFormView.FindControl("ddlADisc1")).SelectedValue = Convert.ToString(dsSLB.Tables[0].Rows[0]["Addition_discount_1_ind"]);
                        ((DropDownList)SLBFormView.FindControl("ddlADisc2")).SelectedValue = Convert.ToString(dsSLB.Tables[0].Rows[0]["Addition_discount_2_ind"]);
                        ((DropDownList)SLBFormView.FindControl("ddlADisc3")).SelectedValue = Convert.ToString(dsSLB.Tables[0].Rows[0]["Addition_discount_3_ind"]);
                        ((DropDownList)SLBFormView.FindControl("ddlADisc4")).SelectedValue = Convert.ToString(dsSLB.Tables[0].Rows[0]["Addition_discount_4_ind"]);
                        ((DropDownList)SLBFormView.FindControl("ddlADisc5")).SelectedValue = Convert.ToString(dsSLB.Tables[0].Rows[0]["Addition_discount_5_ind"]);
                        ((DropDownList)SLBFormView.FindControl("ddlExciseDuty")).SelectedValue = Convert.ToString(dsSLB.Tables[0].Rows[0]["Excise_duty_indicator"]);
                        ((DropDownList)SLBFormView.FindControl("ddlAdditionalSurchargeInd")).SelectedValue = Convert.ToString(dsSLB.Tables[0].Rows[0]["Addition_Surcharge_Ind"]);
                        ((DropDownList)SLBFormView.FindControl("ddlCESSCalculationIndicator")).SelectedValue = Convert.ToString(dsSLB.Tables[0].Rows[0]["Cess_Indicator"]);
                        ((DropDownList)SLBFormView.FindControl("ddlInsuranceIndicator")).SelectedValue = Convert.ToString(dsSLB.Tables[0].Rows[0]["Insurance_Indicator"]);
                        ((DropDownList)SLBFormView.FindControl("ddlWarehouseSCIndicator")).SelectedValue = Convert.ToString(dsSLB.Tables[0].Rows[0]["Warehouse_Surcharge_Ind"]);
                        ((DropDownList)SLBFormView.FindControl("ddlFreightCalculationIndicator")).SelectedValue = Convert.ToString(dsSLB.Tables[0].Rows[0]["Freight_Ind"]);
                        ((DropDownList)SLBFormView.FindControl("ddlTOTind")).SelectedValue = Convert.ToString(dsSLB.Tables[0].Rows[0]["Turnover_Tax_Ind"]);
                        ((DropDownList)SLBFormView.FindControl("ddlBonusCalculationInd")).SelectedValue = Convert.ToString(dsSLB.Tables[0].Rows[0]["Bonus_Ind"]);

                        ((DropDownList)SLBFormView.FindControl("ddlCoupleInd")).SelectedValue = Convert.ToString(dsSLB.Tables[0].Rows[0]["Coupon_Ind"]);
                        ((DropDownList)SLBFormView.FindControl("ddlSplTODInd")).SelectedValue = Convert.ToString(dsSLB.Tables[0].Rows[0]["spl_TOD_Ind"]);
                        ((DropDownList)SLBFormView.FindControl("ddlCalculationIndicator")).SelectedValue = Convert.ToString(dsSLB.Tables[0].Rows[0]["Calculation_Ind"]);

                        ((DropDownList)SLBFormView.FindControl("ddlHCInd")).SelectedValue = Convert.ToString(dsSLB.Tables[0].Rows[0]["HC_Ind"]);
                        ((DropDownList)SLBFormView.FindControl("ddlSLBRoundingOff")).SelectedValue = Convert.ToString(dsSLB.Tables[0].Rows[0]["SLB_Roundoff_Ind"]);
                        ((DropDownList)SLBFormView.FindControl("ddlEDForSellingPriceInd")).SelectedValue = Convert.ToString(dsSLB.Tables[0].Rows[0]["ED_Selling_Price_Ind"]);
                        ((DropDownList)SLBFormView.FindControl("ddlSPLDisc1Ind")).SelectedValue = Convert.ToString(dsSLB.Tables[0].Rows[0]["Spl_Disc_1_Ind"]);
                        ((DropDownList)SLBFormView.FindControl("ddlSPLDisc2Ind")).SelectedValue = Convert.ToString(dsSLB.Tables[0].Rows[0]["Spl_Disc_2_Ind"]);

                    }
                    else
                    {
                        ((TextBox)SLBFormView.FindControl("txtListPrice")).Text = "0.00";
                        ((TextBox)SLBFormView.FindControl("txtPurchaseDiscount")).Text = "0.00";
                        ((TextBox)SLBFormView.FindControl("txtAddDisc1")).Text = "0.00";
                        ((TextBox)SLBFormView.FindControl("txtAddDisc2")).Text = "0.00";
                        ((TextBox)SLBFormView.FindControl("txtAddDisc3")).Text = "0.00";
                        ((TextBox)SLBFormView.FindControl("txtAddDisc4")).Text = "0.00";
                        ((TextBox)SLBFormView.FindControl("txtAddDisc5")).Text = "0.00";
                        ((TextBox)SLBFormView.FindControl("txtExciseDuty")).Text = "0.00";
                        ((TextBox)SLBFormView.FindControl("txtSalesTax")).Text = "0.00";
                        ((TextBox)SLBFormView.FindControl("txtSurchage")).Text = "0.00";
                        ((TextBox)SLBFormView.FindControl("txtAdditinalSurchage")).Text = "0.00";
                        ((TextBox)SLBFormView.FindControl("txtEntryTax")).Text = "0.00";
                        ((TextBox)SLBFormView.FindControl("txtOctroi")).Text = "0.00";
                        ((TextBox)SLBFormView.FindControl("txtCess")).Text = "0.00";
                        ((TextBox)SLBFormView.FindControl("txtOtherLevies1")).Text = "0.00";
                        ((TextBox)SLBFormView.FindControl("txtOtherLevies2")).Text = "0.00";
                        ((TextBox)SLBFormView.FindControl("txtInsurance")).Text = "0.00";
                        ((TextBox)SLBFormView.FindControl("txtWarehouseSurcharge")).Text = "0.00";
                        ((TextBox)SLBFormView.FindControl("txtFreight")).Text = "0.00";
                        ((TextBox)SLBFormView.FindControl("txtTurnoverTax")).Text = "0.00";
                        ((TextBox)SLBFormView.FindControl("txtBonus")).Text = "0.00";
                        ((TextBox)SLBFormView.FindControl("txtCouponCharges")).Text = "0.00";
                        ((TextBox)SLBFormView.FindControl("txtGrossProfit")).Text = "0.00";
                        ((TextBox)SLBFormView.FindControl("txtTurnoverDiscount")).Text = "0.00";
                        ((TextBox)SLBFormView.FindControl("txtSpecialTurnoverDiscount")).Text = "0.00";
                        ((TextBox)SLBFormView.FindControl("txtDealersDiscount")).Text = "0.00";
                        ((TextBox)SLBFormView.FindControl("txtHandlingCharges")).Text = "0.00";
                        ((TextBox)SLBFormView.FindControl("txtSLBText")).Text = "0.00";
                        ((TextBox)SLBFormView.FindControl("txtEDforSellingPrice")).Text = "0.00";
                        ((TextBox)SLBFormView.FindControl("TxtSpecialDiscount1")).Text = "0.00";
                        ((TextBox)SLBFormView.FindControl("TxtSpecialDiscount2")).Text = "0.00";
                        ((TextBox)SLBFormView.FindControl("txtCostPrice")).Text = "0.00";
                        ((TextBox)SLBFormView.FindControl("txtSellingPrice")).Text = "0.00"; ;
                        ((TextBox)SLBFormView.FindControl("txtRegularGrossProfit")).Text = "0.00";
                        ((TextBox)SLBFormView.FindControl("txtGP1")).Text = "0.00";
                        ((TextBox)SLBFormView.FindControl("txtGP2")).Text = "0.00";
                        ((TextBox)SLBFormView.FindControl("txtGP3")).Text = "0.00";
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void fnPopulateItemCode(DropDownList ddlDropdownlist, string strSupplierLineCode)
        {
            ItemMasters objItem = new ItemMasters();
            try
            {

                ddlDropdownlist.DataSource = objItem.GetAllItemCode(strSupplierLineCode);
                ddlDropdownlist.DataTextField = "Supplierpartno";
                ddlDropdownlist.DataValueField = "itemcode";
                ddlDropdownlist.DataBind();

                //ddlSupplierLineCode.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
            finally
            {
                objItem = null;
            }
        }

        protected void ddlSupplierLineCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            fnPopulateItemCode(ddlLCode, ddlSupplierLineCode.SelectedValue);
        }

        protected void ddlSLC_SelectedIndexChanged(object sender, EventArgs e)
        {
            fnPopulateItemCode((DropDownList)SLBFormView.FindControl("ddlItemCode"), ((DropDownList)SLBFormView.FindControl("ddlSLC")).SelectedValue);
            fnPopulateDiscountDetails();
        }

        protected void ddlBranchCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            SLBQueries objSLB = new SLBQueries();
            string strPriceList = objSLB.GetPriceList(((DropDownList)SLBFormView.FindControl("ddlBranchCode")).SelectedValue, ((DropDownList)SLBFormView.FindControl("ddlItemCode")).SelectedValue);

            if (string.IsNullOrEmpty(strPriceList) && ((DropDownList)SLBFormView.FindControl("ddlBranchCode")).SelectedValue != "0" && ((DropDownList)SLBFormView.FindControl("ddlItemCode")).SelectedValue != "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('No List price is available for this Item');", true);
                ((DropDownList)SLBFormView.FindControl("ddlItemCode")).SelectedIndex = -1;
            }
            else if (!string.IsNullOrEmpty(strPriceList) && ((DropDownList)SLBFormView.FindControl("ddlBranchCode")).SelectedValue != "0" && ((DropDownList)SLBFormView.FindControl("ddlItemCode")).SelectedValue != "")
                ((TextBox)SLBFormView.FindControl("txtListPrice")).Text = string.Format("{0:0.00}", Convert.ToDecimal(strPriceList));

        }




        protected void ddlItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            SLBQueries objSLB = new SLBQueries();
            Int32 intDiscount = 0;

            string strDiscountPrice = objSLB.GetPurchaseDiscount(((DropDownList)SLBFormView.FindControl("ddlItemCode")).SelectedValue);

            string strPriceList = objSLB.GetPriceList(((DropDownList)SLBFormView.FindControl("ddlBranchCode")).SelectedValue, ((DropDownList)SLBFormView.FindControl("ddlItemCode")).SelectedValue);

            if (string.IsNullOrEmpty(strPriceList) && ((DropDownList)SLBFormView.FindControl("ddlBranchCode")).SelectedValue != "0" && ((DropDownList)SLBFormView.FindControl("ddlItemCode")).SelectedValue != "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "alert('No List price is available for this Item');", true);
                ((DropDownList)SLBFormView.FindControl("ddlItemCode")).SelectedIndex = -1;
                ((TextBox)SLBFormView.FindControl("txtListPrice")).Text = string.Empty;
            }
            else if (!string.IsNullOrEmpty(strPriceList) && ((DropDownList)SLBFormView.FindControl("ddlBranchCode")).SelectedValue != "0" && ((DropDownList)SLBFormView.FindControl("ddlItemCode")).SelectedValue != "")
                ((TextBox)SLBFormView.FindControl("txtListPrice")).Text = string.Format("{0:0.00}", Convert.ToDecimal(strPriceList));

            if (Int32.TryParse(strDiscountPrice, out intDiscount))
                ((TextBox)SLBFormView.FindControl("txtPurchaseDiscount")).Text = intDiscount.ToString("0.00");
            else
                ((TextBox)SLBFormView.FindControl("txtPurchaseDiscount")).Text = "0.00";

            if (((DropDownList)SLBFormView.FindControl("ddlItemCode")).SelectedValue != null)
                fnPopulateExciseDuty();
            else
                ((TextBox)SLBFormView.FindControl("txtExciseDuty")).Text = "0.00";
        }

        protected void SLBFormView_PageIndexChanging(object sender, FormViewPageEventArgs e)
        {

        }

        protected void AddSLB()
        {
            SLBQueries objSLB = new SLBQueries();
            objSLB.BranchCode = ((DropDownList)SLBFormView.FindControl("ddlBranchCode")).SelectedValue;
            objSLB.SupplierLineCode = ((DropDownList)SLBFormView.FindControl("ddlSLC")).SelectedValue;
            objSLB.ItemCode = ((DropDownList)SLBFormView.FindControl("ddlItemCode")).SelectedValue;

            objSLB.AdditionDiscount_1_ind = ((DropDownList)SLBFormView.FindControl("ddlADisc1")).SelectedValue;
            objSLB.AdditionDiscount1 = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtAddDisc1")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtAddDisc1")).Text);
            objSLB.AdditionDiscount_2_ind = ((DropDownList)SLBFormView.FindControl("ddlADisc2")).SelectedValue;
            objSLB.AdditionDiscount2 = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtAddDisc2")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtAddDisc2")).Text); ;
            objSLB.AdditionDiscount_3_ind = ((DropDownList)SLBFormView.FindControl("ddlADisc3")).SelectedValue;
            objSLB.AdditionDiscount3 = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtAddDisc3")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtAddDisc3")).Text); ;
            objSLB.AdditionDiscount_4_ind = ((DropDownList)SLBFormView.FindControl("ddlADisc4")).SelectedValue;
            objSLB.AdditionDiscount4 = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtAddDisc4")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtAddDisc4")).Text);
            objSLB.AdditionDiscount_5_ind = ((DropDownList)SLBFormView.FindControl("ddlADisc5")).SelectedValue;
            objSLB.AdditionDiscount5 = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtAddDisc5")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtAddDisc5")).Text);
            objSLB.ExciseDutyIndicator = ((DropDownList)SLBFormView.FindControl("ddlExciseDuty")).SelectedValue;
            objSLB.ExciseDutyPercentage = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtExciseDuty")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtExciseDuty")).Text);

            objSLB.SalesTaxPercentage = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtSalesTax")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtSalesTax")).Text);
            objSLB.SurchargePercentage = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtSurchage")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtSurchage")).Text);
            objSLB.AdditionSurchargeInd = ((DropDownList)SLBFormView.FindControl("ddlAdditionalSurchargeInd")).SelectedValue;
            objSLB.AdditionalSurcharge = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtAdditinalSurchage")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtAdditinalSurchage")).Text);
            objSLB.EntryTax = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtEntryTax")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtEntryTax")).Text);
            objSLB.Octroi = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtOctroi")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtOctroi")).Text);
            objSLB.CessIndicator = ((DropDownList)SLBFormView.FindControl("ddlCESSCalculationIndicator")).SelectedValue;
            objSLB.CESS = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtCess")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtCess")).Text);

            objSLB.OtherLevies1 = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtOtherLevies1")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtOtherLevies1")).Text);
            objSLB.OtherLevies2 = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtOtherLevies2")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtOtherLevies2")).Text);
            objSLB.InsuranceIndicator = ((DropDownList)SLBFormView.FindControl("ddlInsuranceIndicator")).SelectedValue;
            objSLB.Insurance = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtInsurance")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtInsurance")).Text);
            objSLB.WarehouseSurchargeInd = ((DropDownList)SLBFormView.FindControl("ddlWarehouseSCIndicator")).SelectedValue;
            objSLB.WarehouseSurcharge = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtWarehouseSurcharge")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtWarehouseSurcharge")).Text);
            objSLB.FreightInd = ((DropDownList)SLBFormView.FindControl("ddlFreightCalculationIndicator")).SelectedValue;
            objSLB.Freight = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtFreight")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtFreight")).Text);


            objSLB.TurnoverTaxInd = ((DropDownList)SLBFormView.FindControl("ddlTOTind")).SelectedValue;
            objSLB.TurnoverTax = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtTurnoverTax")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtTurnoverTax")).Text);
            objSLB.BonusInd = ((DropDownList)SLBFormView.FindControl("ddlBonusCalculationInd")).SelectedValue;
            objSLB.Bonus = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtBonus")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtBonus")).Text);
            objSLB.CouponInd = ((DropDownList)SLBFormView.FindControl("ddlCoupleInd")).SelectedValue;
            objSLB.CouponCharges = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtCouponCharges")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtCouponCharges")).Text);
            objSLB.GrossProfitPercentage = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtGrossProfit")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtGrossProfit")).Text);
            objSLB.TurnoverDiscount = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtTurnoverDiscount")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtTurnoverDiscount")).Text);


            objSLB.SplTODInd = ((DropDownList)SLBFormView.FindControl("ddlSplTODInd")).SelectedValue;
            objSLB.SplTurnoverDiscount = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtSpecialTurnoverDiscount")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtSpecialTurnoverDiscount")).Text);
            objSLB.CalculationInd = ((DropDownList)SLBFormView.FindControl("ddlCalculationIndicator")).SelectedValue;
            objSLB.DealersDiscount = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtDealersDiscount")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtDealersDiscount")).Text);
            objSLB.HCInd = ((DropDownList)SLBFormView.FindControl("ddlHCInd")).SelectedValue;
            objSLB.HandlingChargesPercentage = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtHandlingCharges")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtHandlingCharges")).Text);
            objSLB.SLBRoundoffInd = ((DropDownList)SLBFormView.FindControl("ddlSLBRoundingOff")).SelectedValue;
            objSLB.SLBValue = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtSLBText")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtSLBText")).Text);

            objSLB.EDSellingPriceInd = ((DropDownList)SLBFormView.FindControl("ddlEDForSellingPriceInd")).SelectedValue;
            objSLB.EDForSellingPrice = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtEDforSellingPrice")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtEDforSellingPrice")).Text);
            objSLB.SplDisc_1_Ind = ((DropDownList)SLBFormView.FindControl("ddlSPLDisc1Ind")).SelectedValue;
            objSLB.SpecialDiscount1 = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("TxtSpecialDiscount1")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("TxtSpecialDiscount1")).Text);
            objSLB.SplDisc_2_Ind = ((DropDownList)SLBFormView.FindControl("ddlSPLDisc2Ind")).SelectedValue;
            objSLB.SpecialDiscount2 = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("TxtSpecialDiscount2")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("TxtSpecialDiscount2")).Text);
            objSLB.CostPrice = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtCostPrice")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtCostPrice")).Text);
            objSLB.SellingPrice = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtSellingPrice")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtSellingPrice")).Text);

            objSLB.RegularGP = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtRegularGrossProfit")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtRegularGrossProfit")).Text);
            objSLB.GPTOD = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtGP1")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtGP1")).Text);
            objSLB.GPBonus = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtGP2")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtGP2")).Text);
            objSLB.GPTODBONUS = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtGP3")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtGP3")).Text);

            objSLB.AfterDiscount = (Decimal)ViewState["AfterDiscount"];
            objSLB.AfterAddDisc1 = (Decimal)ViewState["AfterAdditionalDiscount1"];
            objSLB.AfterAddDisc2 = (Decimal)ViewState["AfterAdditionalDiscount2"];
            objSLB.AfterAddDisc3 = (Decimal)ViewState["AfterAdditionalDiscount3"];
            objSLB.AfterAddDisc4 = (Decimal)ViewState["AfterAdditionalDiscount4"];
            objSLB.AfterAddDisc5 = (Decimal)ViewState["AfterAdditionalDiscount5"];

            objSLB.AfterED = (Decimal)ViewState["AfterExciseDuty"];
            objSLB.AfterST = (Decimal)ViewState["AfterSalesTax"];
            objSLB.AfterSC = (Decimal)ViewState["AfterSurcharge"];
            objSLB.AfterASC = (Decimal)ViewState["AfterAdditionalSurcharge"];
            objSLB.AfterET = (Decimal)ViewState["AfterEntryTax"];
            objSLB.AfterOct = (Decimal)ViewState["AfterOctroi"];
            objSLB.AfterCess = (Decimal)ViewState["AfterCESS"];
            objSLB.AfterOl1 = (Decimal)ViewState["AfterOtherLevies1"];
            objSLB.AfterOl2 = (Decimal)ViewState["AfterOtherLevies2"];
            objSLB.AfterIns = (Decimal)ViewState["AfterInsurance"];

            objSLB.AfterWsc = (Decimal)ViewState["AfterWSC"];
            objSLB.AfterFrt = (Decimal)ViewState["AfterFreight"];
            objSLB.AfterTot = (Decimal)ViewState["AfterTurnOverTax"];
            objSLB.AfterBon = (Decimal)ViewState["AfterBON"];
            objSLB.AfterCoupon = (Decimal)ViewState["AfterCoupon"];
            objSLB.AfterSplTOD = (Decimal)ViewState["AfterSplTOD"];
            objSLB.AfterHC = (Decimal)ViewState["AfterHC"];
            objSLB.AfterAdd = (Decimal)ViewState["AfterAdd"];

            Int32 AddCount = objSLB.AddSLBQueryDetails();
            if (AddCount == -1)
            {
                string display = "Added Successfully!";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
            }
            else
            {
                string display = "Error in Adding records!";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
            }
            //SLBFormView.ChangeMode(FormViewMode.Insert);
        }

        protected void UpdateSLB()
        {
            try
            {
                SLBQueries objSLB = new SLBQueries();
                objSLB.BranchCode = ((DropDownList)SLBFormView.FindControl("ddlBranchCode")).SelectedValue;
                objSLB.SupplierLineCode = ((DropDownList)SLBFormView.FindControl("ddlSLC")).SelectedValue;
                objSLB.ItemCode = ((DropDownList)SLBFormView.FindControl("ddlItemCode")).SelectedValue;

                objSLB.AdditionDiscount_1_ind = ((DropDownList)SLBFormView.FindControl("ddlADisc1")).SelectedValue;
                objSLB.AdditionDiscount1 = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtAddDisc1")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtAddDisc1")).Text);
                objSLB.AdditionDiscount_2_ind = ((DropDownList)SLBFormView.FindControl("ddlADisc2")).SelectedValue;
                objSLB.AdditionDiscount2 = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtAddDisc2")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtAddDisc2")).Text); ;
                objSLB.AdditionDiscount_3_ind = ((DropDownList)SLBFormView.FindControl("ddlADisc3")).SelectedValue;
                objSLB.AdditionDiscount3 = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtAddDisc3")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtAddDisc3")).Text); ;
                objSLB.AdditionDiscount_4_ind = ((DropDownList)SLBFormView.FindControl("ddlADisc4")).SelectedValue;
                objSLB.AdditionDiscount4 = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtAddDisc4")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtAddDisc4")).Text);
                objSLB.AdditionDiscount_5_ind = ((DropDownList)SLBFormView.FindControl("ddlADisc5")).SelectedValue;
                objSLB.AdditionDiscount5 = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtAddDisc5")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtAddDisc5")).Text);
                objSLB.ExciseDutyIndicator = ((DropDownList)SLBFormView.FindControl("ddlExciseDuty")).SelectedValue;
                objSLB.ExciseDutyPercentage = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtExciseDuty")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtExciseDuty")).Text);

                objSLB.SalesTaxPercentage = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtSalesTax")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtSalesTax")).Text);
                objSLB.SurchargePercentage = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtSurchage")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtSurchage")).Text);
                objSLB.AdditionSurchargeInd = ((DropDownList)SLBFormView.FindControl("ddlAdditionalSurchargeInd")).SelectedValue;
                objSLB.AdditionalSurcharge = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtAdditinalSurchage")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtAdditinalSurchage")).Text);
                objSLB.EntryTax = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtEntryTax")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtEntryTax")).Text);
                objSLB.Octroi = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtOctroi")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtOctroi")).Text);
                objSLB.CessIndicator = ((DropDownList)SLBFormView.FindControl("ddlCESSCalculationIndicator")).SelectedValue;
                objSLB.CESS = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtCess")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtCess")).Text);

                objSLB.OtherLevies1 = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtOtherLevies1")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtOtherLevies1")).Text);
                objSLB.OtherLevies2 = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtOtherLevies2")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtOtherLevies2")).Text);
                objSLB.InsuranceIndicator = ((DropDownList)SLBFormView.FindControl("ddlInsuranceIndicator")).SelectedValue;
                objSLB.Insurance = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtInsurance")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtInsurance")).Text);
                objSLB.WarehouseSurchargeInd = ((DropDownList)SLBFormView.FindControl("ddlWarehouseSCIndicator")).SelectedValue;
                objSLB.WarehouseSurcharge = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtWarehouseSurcharge")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtWarehouseSurcharge")).Text);
                objSLB.FreightInd = ((DropDownList)SLBFormView.FindControl("ddlFreightCalculationIndicator")).SelectedValue;
                objSLB.Freight = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtFreight")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtFreight")).Text);


                objSLB.TurnoverTaxInd = ((DropDownList)SLBFormView.FindControl("ddlTOTind")).SelectedValue;
                objSLB.TurnoverTax = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtTurnoverTax")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtTurnoverTax")).Text);
                objSLB.BonusInd = ((DropDownList)SLBFormView.FindControl("ddlBonusCalculationInd")).SelectedValue;
                objSLB.Bonus = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtBonus")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtBonus")).Text);
                objSLB.CouponInd = ((DropDownList)SLBFormView.FindControl("ddlCoupleInd")).SelectedValue;
                objSLB.CouponCharges = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtCouponCharges")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtCouponCharges")).Text);
                objSLB.GrossProfitPercentage = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtGrossProfit")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtGrossProfit")).Text);
                objSLB.TurnoverDiscount = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtTurnoverDiscount")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtTurnoverDiscount")).Text);


                objSLB.SplTODInd = ((DropDownList)SLBFormView.FindControl("ddlSplTODInd")).SelectedValue;
                objSLB.SplTurnoverDiscount = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtSpecialTurnoverDiscount")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtSpecialTurnoverDiscount")).Text);
                objSLB.CalculationInd = ((DropDownList)SLBFormView.FindControl("ddlCalculationIndicator")).SelectedValue;
                objSLB.DealersDiscount = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtDealersDiscount")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtDealersDiscount")).Text);
                objSLB.HCInd = ((DropDownList)SLBFormView.FindControl("ddlHCInd")).SelectedValue;
                objSLB.HandlingChargesPercentage = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtHandlingCharges")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtHandlingCharges")).Text);
                objSLB.SLBRoundoffInd = ((DropDownList)SLBFormView.FindControl("ddlSLBRoundingOff")).SelectedValue;
                objSLB.SLBValue = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtSLBText")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtSLBText")).Text);

                objSLB.EDSellingPriceInd = ((DropDownList)SLBFormView.FindControl("ddlEDForSellingPriceInd")).SelectedValue;
                objSLB.EDForSellingPrice = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtEDforSellingPrice")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtEDforSellingPrice")).Text);
                objSLB.SplDisc_1_Ind = ((DropDownList)SLBFormView.FindControl("ddlSPLDisc1Ind")).SelectedValue;
                objSLB.SpecialDiscount1 = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("TxtSpecialDiscount1")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("TxtSpecialDiscount1")).Text);
                objSLB.SplDisc_2_Ind = ((DropDownList)SLBFormView.FindControl("ddlSPLDisc2Ind")).SelectedValue;
                objSLB.SpecialDiscount2 = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("TxtSpecialDiscount2")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("TxtSpecialDiscount2")).Text);
                objSLB.CostPrice = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtCostPrice")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtCostPrice")).Text);
                objSLB.SellingPrice = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtSellingPrice")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtSellingPrice")).Text);

                objSLB.RegularGP = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtRegularGrossProfit")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtRegularGrossProfit")).Text);
                objSLB.GPTOD = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtGP1")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtGP1")).Text);
                objSLB.GPBonus = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtGP2")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtGP2")).Text);
                objSLB.GPTODBONUS = Convert.ToDecimal(((TextBox)SLBFormView.FindControl("txtGP3")).Text == "" ? "0.00" : ((TextBox)SLBFormView.FindControl("txtGP3")).Text);

                objSLB.AfterDiscount = (Decimal)ViewState["AfterDiscount"];
                objSLB.AfterAddDisc1 = (Decimal)ViewState["AfterAdditionalDiscount1"];
                objSLB.AfterAddDisc2 = (Decimal)ViewState["AfterAdditionalDiscount2"];
                objSLB.AfterAddDisc3 = (Decimal)ViewState["AfterAdditionalDiscount3"];
                objSLB.AfterAddDisc4 = (Decimal)ViewState["AfterAdditionalDiscount4"];
                objSLB.AfterAddDisc5 = (Decimal)ViewState["AfterAdditionalDiscount5"];

                objSLB.AfterED = (Decimal)ViewState["AfterExciseDuty"];
                objSLB.AfterST = (Decimal)ViewState["AfterSalesTax"];
                objSLB.AfterSC = (Decimal)ViewState["AfterSurcharge"];
                objSLB.AfterASC = (Decimal)ViewState["AfterAdditionalSurcharge"];
                objSLB.AfterET = (Decimal)ViewState["AfterEntryTax"];
                objSLB.AfterOct = (Decimal)ViewState["AfterOctroi"];
                objSLB.AfterCess = (Decimal)ViewState["AfterCESS"];
                objSLB.AfterOl1 = (Decimal)ViewState["AfterOtherLevies1"];
                objSLB.AfterOl2 = (Decimal)ViewState["AfterOtherLevies2"];
                objSLB.AfterIns = (Decimal)ViewState["AfterInsurance"];

                objSLB.AfterWsc = (Decimal)ViewState["AfterWSC"];
                objSLB.AfterFrt = (Decimal)ViewState["AfterFreight"];
                objSLB.AfterTot = (Decimal)ViewState["AfterTurnOverTax"];
                objSLB.AfterBon = (Decimal)ViewState["AfterBON"];
                objSLB.AfterCoupon = (Decimal)ViewState["AfterCoupon"];
                objSLB.AfterSplTOD = (Decimal)ViewState["AfterSplTOD"];
                objSLB.AfterHC = (Decimal)ViewState["AfterHC"];
                objSLB.AfterAdd = (Decimal)ViewState["AfterAdd"];

                Int32 Updcount = objSLB.UpdateSLBQueryDetails();
                if (Updcount == -1)
                {
                    string display = "Updated Successfully!";
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
                }
                else
                {
                    string display = "Error in Updating records!";
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);

                }
                //SLBFormView.ChangeMode(FormViewMode.Insert);
            }
            catch (Exception exp)
            {
                throw exp;
            }

        }
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (BtnSubmit.Text == "Add")
            {
                AddSLB();
                BtnSubmit.Enabled = false;
            }
            else if (BtnSubmit.Text == "Update")
            {
                UpdateSLB();
                BtnSubmit.Enabled = false;
            }
        }

        protected void btnCheck_Click(object sender, EventArgs e)
        {
            CheckValidation();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            if (pstrBranch == "CRP")
            {
                ddlBranch.SelectedIndex = 0;
                ddlSupplierLineCode.SelectedIndex = 0;
                for (int i = 1; i <= ddlLCode.Items.Count; i++)
                {
                    ddlLCode.Items.Clear();
                }
                if (SLBFormView.DataItemCount > 0)
                {
                    //SLBFormView.ChangeMode(FormViewMode.Insert);
                    ((DropDownList)SLBFormView.FindControl("ddlBranchCode")).SelectedIndex = 0;
                    ((DropDownList)SLBFormView.FindControl("ddlSLC")).SelectedIndex = 0;
                    for (int i = 1; i <= ((DropDownList)SLBFormView.FindControl("ddlItemCode")).Items.Count; i++)
                    {
                        ((DropDownList)SLBFormView.FindControl("ddlItemCode")).Items.Clear();
                    }
                    ((DropDownList)SLBFormView.FindControl("ddlADisc1")).SelectedIndex = 0;
                    ((DropDownList)SLBFormView.FindControl("ddlADisc2")).SelectedIndex = 0;
                    ((DropDownList)SLBFormView.FindControl("ddlADisc3")).SelectedIndex = 0;
                    ((DropDownList)SLBFormView.FindControl("ddlADisc4")).SelectedIndex = 0;
                    ((DropDownList)SLBFormView.FindControl("ddlADisc5")).SelectedIndex = 0;
                    ((DropDownList)SLBFormView.FindControl("ddlExciseDuty")).SelectedIndex = 0;
                    ((DropDownList)SLBFormView.FindControl("ddlAdditionalSurchargeInd")).SelectedIndex = 0;
                    ((DropDownList)SLBFormView.FindControl("ddlCESSCalculationIndicator")).SelectedIndex = 0;
                    ((DropDownList)SLBFormView.FindControl("ddlInsuranceIndicator")).SelectedIndex = 0;
                    ((DropDownList)SLBFormView.FindControl("ddlWarehouseSCIndicator")).SelectedIndex = 0;
                    ((DropDownList)SLBFormView.FindControl("ddlFreightCalculationIndicator")).SelectedIndex = 0;
                    ((DropDownList)SLBFormView.FindControl("ddlTOTind")).SelectedIndex = 0;
                    ((DropDownList)SLBFormView.FindControl("ddlBonusCalculationInd")).SelectedIndex = 0;
                    ((DropDownList)SLBFormView.FindControl("ddlCoupleInd")).SelectedIndex = 0;
                    ((DropDownList)SLBFormView.FindControl("ddlSplTODInd")).SelectedIndex = 0;
                    ((DropDownList)SLBFormView.FindControl("ddlCalculationIndicator")).SelectedIndex = 0;
                    ((DropDownList)SLBFormView.FindControl("ddlHCInd")).SelectedIndex = 0;
                    ((DropDownList)SLBFormView.FindControl("ddlSLBRoundingOff")).SelectedIndex = 0;
                    ((DropDownList)SLBFormView.FindControl("ddlEDForSellingPriceInd")).SelectedIndex = 0;
                    ((DropDownList)SLBFormView.FindControl("ddlSPLDisc1Ind")).SelectedIndex = 0;
                    ((DropDownList)SLBFormView.FindControl("ddlSPLDisc2Ind")).SelectedIndex = 0;

                    ((TextBox)SLBFormView.FindControl("txtListPrice")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtPurchaseDiscount")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtAddDisc1")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtAddDisc2")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtAddDisc3")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtAddDisc4")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtAddDisc5")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtExciseDuty")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtSalesTax")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtSurchage")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtAdditinalSurchage")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtEntryTax")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtOctroi")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtCess")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtOtherLevies1")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtOtherLevies2")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtInsurance")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtWarehouseSurcharge")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtFreight")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtTurnoverTax")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtBonus")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtCouponCharges")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtGrossProfit")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtTurnoverDiscount")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtSpecialTurnoverDiscount")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtDealersDiscount")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtHandlingCharges")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtSLBText")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtEDforSellingPrice")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("TxtSpecialDiscount1")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("TxtSpecialDiscount2")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtCostPrice")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtSellingPrice")).Text = "0.00"; ;
                    ((TextBox)SLBFormView.FindControl("txtRegularGrossProfit")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtGP1")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtGP2")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtGP3")).Text = "0.00";
                }
                else
                {
                    SLBFormView.ChangeMode(FormViewMode.Insert);
                    ((DropDownList)SLBFormView.FindControl("ddlBranchCode")).SelectedIndex = 0;
                    ((DropDownList)SLBFormView.FindControl("ddlSLC")).SelectedIndex = 0;
                    for (int i = 1; i <= ((DropDownList)SLBFormView.FindControl("ddlItemCode")).Items.Count; i++)
                    {
                        ((DropDownList)SLBFormView.FindControl("ddlItemCode")).Items.Clear();
                    }
                    ((DropDownList)SLBFormView.FindControl("ddlADisc1")).SelectedIndex = 0;
                    ((DropDownList)SLBFormView.FindControl("ddlADisc2")).SelectedIndex = 0;
                    ((DropDownList)SLBFormView.FindControl("ddlADisc3")).SelectedIndex = 0;
                    ((DropDownList)SLBFormView.FindControl("ddlADisc4")).SelectedIndex = 0;
                    ((DropDownList)SLBFormView.FindControl("ddlADisc5")).SelectedIndex = 0;
                    ((DropDownList)SLBFormView.FindControl("ddlExciseDuty")).SelectedIndex = 0;
                    ((DropDownList)SLBFormView.FindControl("ddlAdditionalSurchargeInd")).SelectedIndex = 0;
                    ((DropDownList)SLBFormView.FindControl("ddlCESSCalculationIndicator")).SelectedIndex = 0;
                    ((DropDownList)SLBFormView.FindControl("ddlInsuranceIndicator")).SelectedIndex = 0;
                    ((DropDownList)SLBFormView.FindControl("ddlWarehouseSCIndicator")).SelectedIndex = 0;
                    ((DropDownList)SLBFormView.FindControl("ddlFreightCalculationIndicator")).SelectedIndex = 0;
                    ((DropDownList)SLBFormView.FindControl("ddlTOTind")).SelectedIndex = 0;
                    ((DropDownList)SLBFormView.FindControl("ddlBonusCalculationInd")).SelectedIndex = 0;
                    ((DropDownList)SLBFormView.FindControl("ddlCoupleInd")).SelectedIndex = 0;
                    ((DropDownList)SLBFormView.FindControl("ddlSplTODInd")).SelectedIndex = 0;
                    ((DropDownList)SLBFormView.FindControl("ddlCalculationIndicator")).SelectedIndex = 0;
                    ((DropDownList)SLBFormView.FindControl("ddlHCInd")).SelectedIndex = 0;
                    ((DropDownList)SLBFormView.FindControl("ddlSLBRoundingOff")).SelectedIndex = 0;
                    ((DropDownList)SLBFormView.FindControl("ddlEDForSellingPriceInd")).SelectedIndex = 0;
                    ((DropDownList)SLBFormView.FindControl("ddlSPLDisc1Ind")).SelectedIndex = 0;
                    ((DropDownList)SLBFormView.FindControl("ddlSPLDisc2Ind")).SelectedIndex = 0;

                    ((TextBox)SLBFormView.FindControl("txtListPrice")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtPurchaseDiscount")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtAddDisc1")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtAddDisc2")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtAddDisc3")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtAddDisc4")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtAddDisc5")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtExciseDuty")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtSalesTax")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtSurchage")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtAdditinalSurchage")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtEntryTax")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtOctroi")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtCess")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtOtherLevies1")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtOtherLevies2")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtInsurance")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtWarehouseSurcharge")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtFreight")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtTurnoverTax")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtBonus")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtCouponCharges")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtGrossProfit")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtTurnoverDiscount")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtSpecialTurnoverDiscount")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtDealersDiscount")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtHandlingCharges")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtSLBText")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtEDforSellingPrice")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("TxtSpecialDiscount1")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("TxtSpecialDiscount2")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtCostPrice")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtSellingPrice")).Text = "0.00"; ;
                    ((TextBox)SLBFormView.FindControl("txtRegularGrossProfit")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtGP1")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtGP2")).Text = "0.00";
                    ((TextBox)SLBFormView.FindControl("txtGP3")).Text = "0.00";
                    //((Label)SLBFormView.FindControl("lblNoResult")).Visible = false;
                }
                ((DropDownList)SLBFormView.FindControl("ddlBranchCode")).Enabled = true;
                ((DropDownList)SLBFormView.FindControl("ddlSLC")).Enabled = true;
                ((DropDownList)SLBFormView.FindControl("ddlItemCode")).Enabled = true;
                BtnSubmit.Enabled = false;
                BtnSubmit.Text = "Add";
               
            }
            else
            {
                ddlBranch.SelectedValue = pstrBranch;
                ddlSupplierLineCode.SelectedIndex = 0;
                for (int i = 1; i <= ddlLCode.Items.Count; i++)
                {
                    ddlLCode.Items.Clear();
                }
                SLBFormView.ChangeMode(FormViewMode.Insert);

                ((TextBox)SLBFormView.FindControl("txtListPrice")).Text = "0.00";
                ((TextBox)SLBFormView.FindControl("txtPurchaseDiscount")).Text = "0.00";
                ((TextBox)SLBFormView.FindControl("txtAddDisc1")).Text = "0.00";
                ((TextBox)SLBFormView.FindControl("txtAddDisc2")).Text = "0.00";
                ((TextBox)SLBFormView.FindControl("txtAddDisc3")).Text = "0.00";
                ((TextBox)SLBFormView.FindControl("txtAddDisc4")).Text = "0.00";
                ((TextBox)SLBFormView.FindControl("txtAddDisc5")).Text = "0.00";
                ((TextBox)SLBFormView.FindControl("txtExciseDuty")).Text = "0.00";
                ((TextBox)SLBFormView.FindControl("txtSalesTax")).Text = "0.00";
                ((TextBox)SLBFormView.FindControl("txtSurchage")).Text = "0.00";
                ((TextBox)SLBFormView.FindControl("txtAdditinalSurchage")).Text = "0.00";
                ((TextBox)SLBFormView.FindControl("txtEntryTax")).Text = "0.00";
                ((TextBox)SLBFormView.FindControl("txtOctroi")).Text = "0.00";
                ((TextBox)SLBFormView.FindControl("txtCess")).Text = "0.00";
                ((TextBox)SLBFormView.FindControl("txtOtherLevies1")).Text = "0.00";
                ((TextBox)SLBFormView.FindControl("txtOtherLevies2")).Text = "0.00";
                ((TextBox)SLBFormView.FindControl("txtInsurance")).Text = "0.00";
                ((TextBox)SLBFormView.FindControl("txtWarehouseSurcharge")).Text = "0.00";
                ((TextBox)SLBFormView.FindControl("txtFreight")).Text = "0.00";
                ((TextBox)SLBFormView.FindControl("txtTurnoverTax")).Text = "0.00";
                ((TextBox)SLBFormView.FindControl("txtBonus")).Text = "0.00";
                ((TextBox)SLBFormView.FindControl("txtCouponCharges")).Text = "0.00";
                ((TextBox)SLBFormView.FindControl("txtGrossProfit")).Text = "0.00";
                ((TextBox)SLBFormView.FindControl("txtTurnoverDiscount")).Text = "0.00";
                ((TextBox)SLBFormView.FindControl("txtSpecialTurnoverDiscount")).Text = "0.00";
                ((TextBox)SLBFormView.FindControl("txtDealersDiscount")).Text = "0.00";
                ((TextBox)SLBFormView.FindControl("txtHandlingCharges")).Text = "0.00";
                ((TextBox)SLBFormView.FindControl("txtSLBText")).Text = "0.00";
                ((TextBox)SLBFormView.FindControl("txtEDforSellingPrice")).Text = "0.00";
                ((TextBox)SLBFormView.FindControl("TxtSpecialDiscount1")).Text = "0.00";
                ((TextBox)SLBFormView.FindControl("TxtSpecialDiscount2")).Text = "0.00";
                ((TextBox)SLBFormView.FindControl("txtCostPrice")).Text = "0.00";
                ((TextBox)SLBFormView.FindControl("txtSellingPrice")).Text = "0.00"; ;
                ((TextBox)SLBFormView.FindControl("txtRegularGrossProfit")).Text = "0.00";
                ((TextBox)SLBFormView.FindControl("txtGP1")).Text = "0.00";
                ((TextBox)SLBFormView.FindControl("txtGP2")).Text = "0.00";
                ((TextBox)SLBFormView.FindControl("txtGP3")).Text = "0.00";
                PnlSLB.Visible = true;
                PnlSLB.Enabled = false;
                BtnSubmit.Enabled = false;
                btnCheck.Enabled = false;
                //((Label)SLBFormView.FindControl("lblNoResult")).Visible = false;

            }
        }


    }
}
