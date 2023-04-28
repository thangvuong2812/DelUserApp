using ControlzEx.Theming;
using DelUserApp.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DelUserApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            string colorScheme = Settings.Default.ColorScheme; 
            string colorBase = Settings.Default.ColorBase;
            ThemeManager.Current.ChangeTheme(this, colorScheme + "." + colorBase);

            //Keep login at here

            string userName = LocalStorageManagement.UnProtect(Settings.Default.UserName);
            string passWord = LocalStorageManagement.UnProtect(Settings.Default.PassWord);
            LoginManager loginManager = new LoginManager(userName, passWord);
            bool isAuth = loginManager.CheckUser();
            if(string.IsNullOrEmpty(Settings.Default.ExpireLoginTime)) 
            { 
                MainWindow = new LoginWindow();
            }
            else
            {
                DateTime ExpireTime = DateTime.Parse(Settings.Default.ExpireLoginTime);
                if (isAuth && DateTime.Compare(ExpireTime, DateTime.Now) < 1)
                    MainWindow = new MainWindow();
                else
                MainWindow = new LoginWindow();
            }
            MainWindow.Show();
        }
    }
}
