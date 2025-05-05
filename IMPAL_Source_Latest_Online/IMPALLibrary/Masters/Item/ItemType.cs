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
    public class ItemTypes
    {
        public List<ItemType> GetAllItemTypes()
        {
            List<ItemType> ItemType = new List<ItemType>();

            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "Select item_type_Code,Item_Type_Description from item_type_Master Order by Item_Type_Code";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    ItemType.Add(new ItemType(reader["item_type_Code"].ToString(), reader["Item_Type_Description"].ToString()));
                }
            }

            return ItemType;
        }
        public void AddNewItemType(string ItemTypeDescription)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_additemmaster");
            ImpalDB.AddInParameter(cmd, "@Item_Type_Description", DbType.String, ItemTypeDescription.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }

        public void UpdateItemType(string ItemTypeCode, string ItemTypeDescription)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_upditemmaster");

            //ImpalDB.AddInParameter(cmd, GLGroupCode, DbType.String);
            ImpalDB.AddInParameter(cmd, "@Item_Type_Code", DbType.String, ItemTypeCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Item_Type_Description", DbType.String, ItemTypeDescription.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }
    }
     
    public class ItemType
    {
       public ItemType(string ItemTypeCode, string ItemTypeDescription)
        {
            _ItemTypeCode = ItemTypeCode;
            _ItemTypeDescription = ItemTypeDescription;
        }
        private string _ItemTypeCode;
        private string _ItemTypeDescription;
        public string ItemTypeCode
        {
            get { return _ItemTypeCode; }
            set { _ItemTypeCode = value; }
        }
        public string ItemTypeDescription
        {
            get { return _ItemTypeDescription; }
            set { _ItemTypeDescription = value; }
        }

    }
}
