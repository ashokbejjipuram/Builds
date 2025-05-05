using ClosedXML.Excel;
using IMPALLibrary.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;

namespace IMPALWeb.Reports
{
    public class CommonDataMembers
    {
        #region
        public static void ExportGridToExcel(GridView grd,string strReportName)
        {

            DataTable dt = new DataTable("GridView_Data");
            foreach (TableCell cell in grd.HeaderRow.Cells)
            {  
                dt.Columns.Add(cell.Text); 
            }
            foreach (GridViewRow row in grd.Rows)
            {
                dt.Rows.Add();
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    dt.Rows[dt.Rows.Count - 1][i] = row.Cells[i].Text; 
                }
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + strReportName + ".xls");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(HttpContext.Current.Response.OutputStream);
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.End();
                }
            }
        }
        #endregion

        public static DateTime GetFormatedDate(string currentformatdate)
        {
            string[] dateparts = currentformatdate.Split('/');
            DateTime expextedDate = new DateTime(Convert.ToInt32(dateparts[2]), Convert.ToInt32(dateparts[1]), Convert.ToInt32(dateparts[0]));
            return expextedDate;
        }

        public static string[] GetNumericColumns(string Column)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //List<DropDownListValue> ddlList = new List<DropDownListValue>();

            System.Collections.ArrayList myList = new System.Collections.ArrayList();
             
            XmlDocument docs = new XmlDocument();

            XDocument doc = XDocument.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"XML\Numeric_Columns.xml"));

            docs.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"XML\Numeric_Columns.xml"));

            docs.SelectSingleNode("//root/rootitem[@Type='" + Column + "']");

            XmlNodeList xlist = (docs.SelectNodes("root/rootnode[@Type='" + Column + "']/rootitem"));

            foreach (XmlNode xNode in xlist)
            {
                myList.Add(xNode.Attributes.GetNamedItem("Column").Value);  
            }
            return  myList.ToArray(typeof(string)) as string[]; 
        }
    }
}