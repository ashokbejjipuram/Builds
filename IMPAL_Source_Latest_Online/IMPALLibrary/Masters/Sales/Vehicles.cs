using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;

namespace IMPALLibrary.Masters.Sales
{
    public class Vehicles
    {
        public void UpdateAutomobileItem()
        {
            Database ImpalDB = DataAccess.GetDatabase();
            string sQuery = "INSERT INTO VEHICLE_TYPE_MASTER SELECT DISTINCT I.VEHICLE_TYPE_CODE,'AUTOMOBILE ITEMS','" + DateTime.Now.ToString() + "','D' FROM ITEM_MASTER I WITH (NOLOCK) LEFT OUTER JOIN VEHICLE_TYPE_MASTER V WITH (NOLOCK) ON I.VEHICLE_TYPE_CODE=V.VEHICLE_TYPE_CODE WHERE V.VEHICLE_TYPE_CODE IS NULL";
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sQuery);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
        }
    }
    public class Vehicle
    {
        public Vehicle()
        {
        }
    }
}
