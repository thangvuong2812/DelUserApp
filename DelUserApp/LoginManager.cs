using DataAccess.PortalMethods;
using DelUserApp.Properties;
using System;
using System.Collections.Generic;
using System.Text;

namespace DelUserApp
{
    public class LoginManager
    {
        private string _username;
        private string _password;
        private int isExistAdminUser = 0;
        public LoginManager()
        {

        }

        public LoginManager(string userName, string passWord)
        {
            this._username = userName;
            this._password = passWord;
            
        }


        public bool CheckUser()
        {
            isExistAdminUser = PortalMethod.GetInstance().CheckLogin(_username, _password);
            if (isExistAdminUser == 1)
                return true;
            return false;
        }

        public void KeepLogin()
        {
            Settings.Default.UserName = LocalStorageManagement.Protect(_username);
            Settings.Default.PassWord = LocalStorageManagement.Protect(_password);
            Settings.Default.ExpireLoginTime = DateTime.Now.ToString();

            Settings.Default.Save();
        }

        public static void Logout()
        {
            
            Settings.Default.UserName = string.Empty;
            Settings.Default.PassWord = string.Empty;
            Settings.Default.ExpireLoginTime = string.Empty;
            Settings.Default.Save();
        }
    }
}
