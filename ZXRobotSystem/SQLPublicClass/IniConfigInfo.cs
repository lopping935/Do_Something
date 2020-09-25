using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLPublicClass;
namespace SQLPublicClass
{
    public class IniSqlConfigInfo
    {
        INIClass ini ;
        public bool Enabled = false;
        public IniSqlConfigInfo(string iniPath)
        {
            ini = new INIClass(iniPath);
            if(!ini.ExistINIFile())
            {
               return;
            }
            else
                Enabled = true;
        }
        public string  GetConnectionString(string Section,string SDKPass="")
        {
            string sql_Ip,sql_User,sql_Pass,sql_DBName,sql_OutTime;

            
            if (string.IsNullOrEmpty(SDKPass))
            {
                SDKPass = "123456789";
            }
            if (string.IsNullOrEmpty(SDKPass))
            {
                sql_Ip = SDKSecurityService.AESDecrypt(ini.IniReadValue(Section, "DBIp"), SDKPass);
                sql_User = SDKSecurityService.AESDecrypt(ini.IniReadValue(Section, "DBUser"), SDKPass);
                sql_Pass = SDKSecurityService.AESDecrypt(ini.IniReadValue(Section, "DBPass"), SDKPass);
                sql_DBName = SDKSecurityService.AESDecrypt(ini.IniReadValue(Section, "DBName"), SDKPass);
                sql_OutTime = SDKSecurityService.AESDecrypt(ini.IniReadValue(Section, "OutTime"), SDKPass);
            }
            else
            {
                sql_Ip = ini.IniReadValue(Section, "DBIp");
                sql_User = ini.IniReadValue(Section, "DBUser");
                sql_Pass = ini.IniReadValue(Section, "DBPass");
                sql_DBName = ini.IniReadValue(Section, "DBName");
                sql_OutTime = ini.IniReadValue(Section, "OutTime");
            }
            if (string.IsNullOrEmpty(sql_Ip) || string.IsNullOrEmpty(sql_DBName) || string.IsNullOrEmpty(sql_User) ||
                string.IsNullOrEmpty(sql_Pass) || string.IsNullOrEmpty(sql_OutTime))
            {
                //\\SL
                return "Data Source=.;Initial Catalog=ZX_Robot_DB;User ID=sa; Password=6923263;Enlist=true;Pooling=true;Max Pool Size=300;Min Pool Size=0;Connection Lifetime=300;packet size=1000";
            }

            string ConnectionString = "Data Source={0};Initial Catalog={1};User ID={2}; password={3};Enlist=true;Pooling=true;Max Pool Size = 512; Min Pool Size=0; Connection Lifetime = {4}";
            return string.Format(ConnectionString, sql_Ip, sql_DBName, sql_User, sql_Pass, sql_OutTime);
        }
    }
}
