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

        }
    }
}
