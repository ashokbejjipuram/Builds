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
    public class VehilcleTypes
    {
        public List<VehilcleType> GetAllVehilcleTypes()
        {
            List<VehilcleType> VehicleTypeLst = new List<VehilcleType>();

            Database ImpalDB = DataAccess.GetDatabase();

            string sSQL = "select Vehicle_Type_Code, Vehicle_Type_Description from Vehicle_Type_Master Order by Vehicle_Type_Code DESC " ;
            DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
            cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
            using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
            {
                while (reader.Read())
                {
                    VehicleTypeLst.Add(new VehilcleType(reader["Vehicle_Type_Code"].ToString(), reader["Vehicle_Type_Description"].ToString()));
                }
            }

            return VehicleTypeLst;
        }

        public bool FindExists(string VehicleType)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            int BranchCode = 0;
            string ssql = "select 1 from vehicle_Type_master where vehicle_type_code = '" + VehicleType + "'";

            using (DbCommand sqlCmd = ImpalDB.GetSqlStringCommand(ssql))
            {

                BranchCode = (int)ImpalDB.ExecuteScalar(sqlCmd);
            }

            if (BranchCode == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void AddNewVehilcleType(string VehicleTypeCode, string VehicleTypeDescription)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_AddVehicleMaster");
            ImpalDB.AddInParameter(cmd, "@Vehicle_Type_Code", DbType.String, VehicleTypeCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Vehicle_Type_Description", DbType.String, VehicleTypeDescription.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }

        public void UpdateVehilcleType(string VehicleTypeCode, string VehicleTypeDescription)
        {
            Database ImpalDB = DataAccess.GetDatabase();

            // Create command to execute the stored procedure and add the parameters.
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_updvehiclemaster");
            ImpalDB.AddInParameter(cmd, "@Vehicle_Type_Code", DbType.String, VehicleTypeCode.Trim());
            ImpalDB.AddInParameter(cmd, "@Vehicle_Type_Description", DbType.String, VehicleTypeDescription.Trim());
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            ImpalDB.ExecuteNonQuery(cmd);
        }
    }
   



    public class VehilcleType
    {

        public VehilcleType(string VehicleTypeCode, string VehicleTypeDescription)
        {
            _VehicleTypeCode = VehicleTypeCode;
            _VehicleTypeDescription = VehicleTypeDescription;
        }
        private string _VehicleTypeCode;
        private string _VehicleTypeDescription;

        public string VehicleTypeCode
        {
            get { return _VehicleTypeCode; }
            set { _VehicleTypeCode = value; }
        }
        public string VehicleTypeDescription
        {
            get { return _VehicleTypeDescription; }
            set { _VehicleTypeDescription = value; }
        }

    }
}
