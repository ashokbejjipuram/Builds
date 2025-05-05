using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.ServiceLocation;
using System.Data.Common;
using System.Collections;

namespace IMPALLibrary
{
    public class Users
    {
        public List<UserInfo> GetAllUsersDetails()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            List<UserInfo> UserInfos = new List<UserInfo>();
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL = "select UserID, UserName, BranchCode, UserRoleID, RoleName, Rolecode, UPPER(B.Branch_Name) Branch_Name from users U inner join UserRole UR On U.UserRoleID = UR.RoleID and U.Password is not null inner join Branch_Master B On B.Branch_Code=U.BranchCode order by U.UserRoleID Desc, UserName";
                DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
                cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
                {
                    while (reader.Read())
                    {
                        UserInfos.Add(new UserInfo((string)reader["UserID"], (string)reader["UserName"], reader["BranchCode"].ToString(), (int)reader["UserRoleID"], (string)reader["RoleName"], (string)reader["RoleCode"], (string)reader["Branch_Name"]));
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return UserInfos;
        }

        public int GetUserId(string UserId)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;           
            int Cnt = 0;

            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL = "Select Count(UserID) from Users U inner join UserRole UR On U.UserRoleID = UR.RoleID and U.UserId= '" + UserId + "' and U.Status='A'";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                Cnt = Convert.ToInt16(ImpalDB.ExecuteScalar(cmd).ToString());
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return Cnt;
        }

        public int GetUserIpAddress(string UserIpAddress)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            int Cnt = 0;

            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL = "Select Count(ipAddress) from IpAddress_Master Where ipAddress = '" + UserIpAddress + "' and Status='A'";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                Cnt = Convert.ToInt16(ImpalDB.ExecuteScalar(cmd).ToString());
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return Cnt;
        }

        public int GetBiosSerialNumber(string SerialNo)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            int Cnt = 0;

            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL = "Select Count(System_SerialNo) from BIOS_Master Where System_SerialNo = '" + SerialNo + "'";
                DbCommand cmd = ImpalDB.GetSqlStringCommand(sSQL);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                Cnt = Convert.ToInt16(ImpalDB.ExecuteScalar(cmd).ToString());
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return Cnt;
        }

        public UserInfo GetUserInfo(string UserId, string UserPassword)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            UserInfo userinfo = new UserInfo();
            try
            {
                //UpdateEncodedPasswords();

                PasswordHash pwdhash = new PasswordHash();
                //create Random Salt
                string salt = PasswordHash.CreateSalt(6);
                //Convert the user password to byte array
                byte[] PasswordToVerify = Encoding.UTF8.GetBytes(UserPassword);

                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL = "Select U.UserID, U.UserName, U.BranchCode, UR.RoleID, UR.RoleName, UR.RoleCode, U.Salt, U.Password, UPPER(B.Branch_Name) Branch_Name from Users U inner join UserRole UR On U.UserRoleID = UR.RoleID and UserId= '" + UserId + "' and U.Status='A' inner join Branch_Master B On B.Branch_Code=U.BranchCode";
                DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
                cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
                {
                    while (reader.Read())
                    {
                        //convert the salt to byte array
                        byte[] saltToVerify = Encoding.UTF8.GetBytes(reader["Salt"].ToString());

                        //Generate Hashvalue for the user entered passowrd
                        byte[] pwdHash = pwdhash.ComputeHash(PasswordToVerify, saltToVerify);

                        //Get the hash value in string format
                        string UserPasswordHash = Convert.ToBase64String(pwdHash).Trim();

                        //Get the current password 
                        string CurrentPassword = reader["Password"].ToString().Trim();

                        // if both password are matching then set the IsValid value to true
                        if (String.Compare(UserPasswordHash, CurrentPassword, false) == 0)
                        {
                            userinfo.UserID = (string)reader["UserID"];
                            userinfo.UserName = (string)reader["UserName"];
                            userinfo.BranchCode = (string)reader["BranchCode"];
                            userinfo.RoleID = (int)reader["RoleID"];
                            userinfo.RoleName = (string)reader["RoleName"];
                            userinfo.RoleCode = (string)reader["RoleCode"];
                            userinfo.BranchName = (string)reader["Branch_Name"];
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return userinfo;
        }

        public bool IsPasswordValid(string BranchCode, string Password)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            bool IsValid = false;
            try
            {
                PasswordHash pwdhash = new PasswordHash();
                //Convert the user password to byte array
                byte[] PasswordToVerify = Encoding.UTF8.GetBytes(Password);
                string sSQL = " Select  Salt, Password " +
                              " from users  where  BranchCode= '" + BranchCode + "'";
                Database ImpalDB = DataAccess.GetDatabase();
                DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
                cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
                {
                    while (reader.Read())
                    {
                        //convert the salt to byte array
                        byte[] saltToVerify = Encoding.UTF8.GetBytes(reader["Salt"].ToString());

                        //Generate Hashvalue for the user entered passowrd

                        byte[] pwdHash = pwdhash.ComputeHash(PasswordToVerify, saltToVerify);

                        //Get the hash value in string format
                        string UserPasswordHash = Convert.ToBase64String(pwdHash).Trim();
                        //Get the current password 
                        string CurrentPassword = reader["Password"].ToString().Trim();
                        // if both password are   matching then set the IsValid value to true
                        if (String.Compare(UserPasswordHash, CurrentPassword, false) == 0)
                            IsValid = true;
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return IsValid;

        }

        public UserInfo UpdateEncodedPasswords()
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            UserInfo userinfo = new UserInfo();
            try
            {
                PasswordHash pwdhash = new PasswordHash();
                //create Random Salt
                string salt = PasswordHash.CreateSalt(6);
                //Convert the user password to byte array               

                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL = "Select UserName, Password from Final_Users";
                DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
                cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;
                using (IDataReader reader = ImpalDB.ExecuteReader(cmd1))
                {
                    while (reader.Read())
                    {
                        //convert the salt to byte array
                        byte[] saltToVerify = Encoding.UTF8.GetBytes("gLQ2skDM");

                        byte[] PasswordToVerify = Encoding.UTF8.GetBytes(reader["Password"].ToString());

                        //Generate Hashvalue for the user entered passowrd
                        byte[] pwdHash = pwdhash.ComputeHash(PasswordToVerify, saltToVerify);

                        //Get the hash value in string format
                        string UserPasswordHash = Convert.ToBase64String(pwdHash).Trim();

                        //Get the current password 
                        string CurrentPassword = reader["Password"].ToString().Trim();

                        string sSQL1 = "update Final_Users set EncodedPassword = '" + UserPasswordHash + "' where UserName='" + (string)reader["UserName"] + "'";
                        DbCommand cmd2 = ImpalDB.GetSqlStringCommand(sSQL1);
                        cmd2.CommandTimeout = ConnectionTimeOut.TimeOut;
                        ImpalDB.ExecuteNonQuery(cmd2);
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
            return userinfo;
        }

        public bool ResetPassword( string NewPassword, string BranchCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            bool IsValid = false;
            try
            {
               

                PasswordHash pwdhash = new PasswordHash();
                //create Random Salt
                string salt = PasswordHash.CreateSalt(6);
                //Convert the user password to byte array
                byte[] ByteNewPassword = Encoding.UTF8.GetBytes(NewPassword);
                //convert the salt to byte array
                byte[] Bytesalt  = Encoding.UTF8.GetBytes( salt);

                //Create hash for the new password
                byte[] pwdHash = pwdhash.ComputeHash(ByteNewPassword, Bytesalt);

                
                //Get the hash value in string format
                string NewPasswordHash= Convert.ToBase64String(pwdHash).Trim();

                //update the new password and salt into the database
                Database ImpalDB = DataAccess.GetDatabase();
                string sSQL = " Update users set Password = '"  +    NewPasswordHash + "',salt= '" + salt + "'" +
                               "   where   BranchCode= '" + BranchCode + "'";
                DbCommand cmd1 = ImpalDB.GetSqlStringCommand(sSQL);
                cmd1.CommandTimeout = ConnectionTimeOut.TimeOut;      
                int Count =  ImpalDB.ExecuteNonQuery(cmd1);

                if (Count >0 )
                IsValid = true;
              
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }

            return IsValid;
        }

        public void UpdateUserIPAddressDetails(string strBranchCode, string strUserid, string strServiceProvider, string strIpAddress, string strIpAddress1, string strCookie, string strstatus)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_UpdUser_IpAddress");
                ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, strBranchCode);
                ImpalDB.AddInParameter(cmd, "@Userid", DbType.String, strUserid);
                ImpalDB.AddInParameter(cmd, "@ServiceProvider", DbType.String, strServiceProvider);
                ImpalDB.AddInParameter(cmd, "@IpAddress", DbType.String, strIpAddress);
                ImpalDB.AddInParameter(cmd, "@IpAddress1", DbType.String, strIpAddress1);
                ImpalDB.AddInParameter(cmd, "@Cookie", DbType.String, strCookie);
                ImpalDB.AddInParameter(cmd, "@status", DbType.String, strstatus);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                ImpalDB.ExecuteNonQuery(cmd);

            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        public void UpdateUserDetails(int UserID, string UserName, int RoleID, string BranchCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();

                // Create command to execute the stored procedure and add the parameters.
                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_UpdateUserDetails");

                //ImpalDB.AddInParameter(cmd, GLGroupCode, DbType.String);
                ImpalDB.AddInParameter(cmd, "@UserID", DbType.Int16, UserID);
                ImpalDB.AddInParameter(cmd, "@UserName", DbType.String, UserName.Trim());
                ImpalDB.AddInParameter(cmd, "@RoleID", DbType.Int16, RoleID);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                ImpalDB.ExecuteNonQuery(cmd);
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        public void Updateqtyinitial(string strBranchCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_UpdateQty_Initial");
                ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranchCode);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                ImpalDB.ExecuteNonQuery(cmd);

            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        public void UpdateGeoLocation(string strBranch, string stripAddress, string strRegionCode, string strRegionName, string strCity, string strZipCode, string strMetroCode)
        {
            Type Source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            try
            {
                Database ImpalDB = DataAccess.GetDatabase();
                DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_Update_GeoLocation");
                ImpalDB.AddInParameter(cmd, "@Branch_Code", DbType.String, strBranch);
                ImpalDB.AddInParameter(cmd, "@ipAddress", DbType.String, stripAddress);
                ImpalDB.AddInParameter(cmd, "@RegionCode", DbType.String, strRegionCode);
                ImpalDB.AddInParameter(cmd, "@RegionName", DbType.String, strRegionName);
                ImpalDB.AddInParameter(cmd, "@City", DbType.String, strCity);
                ImpalDB.AddInParameter(cmd, "@ZipCode", DbType.String, strZipCode);
                ImpalDB.AddInParameter(cmd, "@MetroCode", DbType.String, strMetroCode);
                cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
                ImpalDB.ExecuteNonQuery(cmd);
            }
            catch (Exception exp)
            {
                Log.WriteException(Source, exp);
            }
        }

        public int ValidateLogin(string SystemName, string BranchCode)
        {
            Database ImpalDB = DataAccess.GetDatabase();
            DbCommand cmd = ImpalDB.GetStoredProcCommand("usp_validateLoginEntry");
            ImpalDB.AddInParameter(cmd, "@SystemName", DbType.String, SystemName);
            ImpalDB.AddInParameter(cmd, "@BranchCode", DbType.String, BranchCode);
            cmd.CommandTimeout = ConnectionTimeOut.TimeOut;
            int Result = Convert.ToInt32(ImpalDB.ExecuteScalar(cmd).ToString());
            return Result;
        }
    }

    public class UserInfo
    {
        public UserInfo()
        {}

        public UserInfo(string UserID, string UserName, string BranchCode, int RoleID)
        {
            _UserID = UserID;
            _UserName = UserName;
            _BranchCode = BranchCode;
            _RoleID = RoleID;
        }

        public UserInfo(string UserID, string UserName, string BranchCode, int RoleID, string RoleName, string RoleCode, string BranchName)
        {
            _UserID = UserID;
            _UserName = UserName;
            _BranchCode = BranchCode;
            _RoleID = RoleID;
            _RoleName = RoleName;
            _RoleCode = RoleCode;
            _BranchName = BranchName;
        }

        private string _UserID;
        private string _UserName = "";
        private string _BranchCode = "";
        private int _RoleID;
        private string _RoleName;
        private string _RoleCode;
        private string _BranchName;

        public string UserID
        {
            get
            {
                return _UserID;
            }
            set
            {
                _UserID = value;
            }
        }

        public int RoleID
        {
            get
            {
                return _RoleID;
            }
            set
            {
                _RoleID = value;
            }
        }

        public string UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                _UserName = value;
            }
        }

        public string BranchCode

        {
            get
            {
                return _BranchCode;
            }
            set
            {
                _BranchCode = value;
            }
        }

        public string BranchName
        {
            get
            {
                return _BranchName;
            }
            set
            {
                _BranchName = value;
            }
        }

        public string RoleName
        {
            get
            {
                return _RoleName;
            }
            set
            {
                _RoleName = value;
            }
        }

        public string RoleCode
        {
            get
            {
                return _RoleCode;
            }
            set
            {
                _RoleCode = value;
            }
        }

        public string Password { get; set; }
        public string SaltValue { get; set; }
       


    }



}


