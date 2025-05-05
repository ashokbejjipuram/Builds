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
using System.Xml;
using System.IO;
using System.Xml.XPath;
using System.Xml.Linq;

namespace IMPALLibrary.Common
{
    public class ImpalLibrary
    {
        public string XMLFilePath { get; set; }
        public string PopulateList { get; set; }
        /*******************************************************************************
         * Method Name : GetReportFileName
         * Patermeters : Category of the report, Report Name key from the XML File
         * Return Type : string
         * Description : This method is read the xml file based on the Report category, Report Name key and the XML Path 
         *               and assign the original report name and its report path in the appropriate property
         * ******************************************************************************/
        public string GetReportFileName(string strReportName)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            string strXMLpath = XMLFilePath;
            string strReportFilePath = default(string);

            try
            {
                if (!string.IsNullOrEmpty(strReportName))
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(strXMLpath);

                    XmlNodeList xmlNodeReportList = xmlDoc.GetElementsByTagName("Report");
                    foreach (XmlNode nod in xmlNodeReportList)
                    {
                        XmlElement XEleValue = (XmlElement)nod;
                        if (strReportName == XEleValue.Attributes["ReportName"].Value.ToString())
                        {
                            strReportFilePath = XEleValue.Attributes["ReportPath"].Value.ToString();
                        }
                    }
                }
                return strReportFilePath;
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return strReportFilePath;
        }

        public List<DropDownListValue> GetDropDownListValues(string strType)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            List<DropDownListValue> ddlList = new List<DropDownListValue>();
            XmlDocument docs = new XmlDocument();

            //XDocument doc = XDocument.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"XML\Reporting_common.xml"));

            docs.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"XML\Reporting_common.xml"));

            docs.SelectSingleNode("//root/rootitem[@Type='" + strType + "']");

            XmlNodeList xlist = (docs.SelectNodes("root/rootnode[@Type='" + strType + "']/rootitem"));

            foreach (XmlNode xNode in xlist)
            {
                DropDownListValue ddlValue = new DropDownListValue();
                ddlValue.DisplayText = xNode.Attributes.GetNamedItem("Description").Value;
                ddlValue.DisplayValue = xNode.Attributes.GetNamedItem("Value").Value;
                ddlList.Add(ddlValue);
            }

            return ddlList;
        }

        //public int GetUPBranches(string Branchcode)
        //{
        //    int Branchcnt = 0;
        //    Database ImpalDB = DataAccess.GetDatabase();
        //    string sSQL = "select 1 from Branch_Master b inner join State_Master s on b.Branch_Code='" + Branchcode + "' and b.State_Code=s.State_Code and s.State_Code=33";
        //    DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
        //    cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
        //    Branchcnt = Convert.ToInt32(ImpalDB.ExecuteScalar(cmd));

        //    return Branchcnt;
        //}

        public int GetDelhiBranches(string Branchcode)
        {
            int Branchcnt = 0;
            Database ImpalDB = DataAccess.GetDatabase();
            string sSQL = "select 1 from Branch_Master b inner join State_Master s on b.Branch_Code='" + Branchcode + "' and b.State_Code=s.State_Code and s.State_Code in (10,13)";
            DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            Branchcnt = Convert.ToInt32(ImpalDB.ExecuteScalar(cmd));

            return Branchcnt;
        }

    }

    public class DropDownListValue
    {
        public string DisplayText { get; set; }
        public string DisplayValue { get; set; }
    }

    //public enum ExecuteType
    //{
    //    Insert = 1,
    //    StoredProcedure = 2
    //}
    //# region to populate aging dropdown in Reports->Ordering->stock->Stock List-Branches Page

    //public class Agings
    //{
    //    public List<Aging_xml> age()
    //    {
    //        List<Aging_xml> age = new List<Aging_xml>();
    //        XmlDocument docs = new XmlDocument();

    //        XDocument doc = XDocument.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"XML\Reporting_common.xml"));

    //        docs.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"XML\Reporting_common.xml"));

    //        docs.SelectSingleNode("//root/rootitem[@Type='Time']");

    //        XmlNodeList nlist = (docs.SelectNodes("root/rootnode[@Type='Time']/rootitem"));

    //        foreach (XmlNode x in nlist)
    //        {
    //            Aging_xml ag = new Aging_xml();
    //            ag.time_age = x.Attributes.GetNamedItem("Value").Value;
    //            age.Add(ag);
    //        }

    //        return age;
    //    }
    //    //public class Aging_xml
    //    //{
    //    //    public string time_age { get; set; }
    //    //}
    //}
    //#endregion


//  #region  to populate list dropdown of Reports->Ordering->stock-> stocklist aspx page

//    public class stocklist_lists
//    {
       
//        public List<stock_list> stocklist()
//        {
//            List<stock_list> stlist = new List<stock_list>();
//            XmlDocument docs = new XmlDocument();
//            XDocument doc = XDocument.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"XML\Reporting_common.xml")); ;
//            docs.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"XML\Reporting_common.xml"));
//            docs.SelectSingleNode("//root/rootitem[@Type='List']");
//            XmlNodeList nlist = (docs.SelectNodes("root/rootnode[@Type='List']/rootitem"));
//           // var query = doc.Root.Elements("rootnode").ElementAt(1).Elements("rootitem");

//            foreach (XmlNode x in nlist)
//            {
//                stock_list SL = new stock_list();
//                SL.StockName = x.Attributes.GetNamedItem("Value").Value;
//                stlist.Add(SL);
//            }


//            return stlist;
//        }
//        public class stock_list
//        {
//            public string StockName { get; set; }
//        }
//    }
  
//#endregion 

}
