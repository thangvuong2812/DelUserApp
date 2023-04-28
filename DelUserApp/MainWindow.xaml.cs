using ControlzEx.Theming;
using DelUserApp.Properties;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DelUserApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {

        public ReadOnlyObservableCollection<string> MyColorSchemes = ThemeManager.Current.ColorSchemes;

       
        public ReadOnlyObservableCollection<string> MyBaseColors = ThemeManager.Current.BaseColors;

        public string SelectedStringColor { get; set; }
        public string SelectedStringTheme { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            cbStyle.ItemsSource = MyColorSchemes;
            cbThemes.ItemsSource = MyBaseColors;
            SelectedStringTheme = Settings.Default.ColorScheme;
            SelectedStringColor = Settings.Default.ColorBase;
            DataContext = this;

        }

        //private void cbStyle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    ComboBox cb = sender as ComboBox;
        //    string color = cb.SelectedItem.ToString();
        //    string theme = SelectedStringTheme;
        //    //ChangeTheme(cbThemes.SelectedItem.ToString(), cb.SelectedItem.ToString());
        //    Debug.WriteLine(cbThemes);

        //}

        //private void cbThemes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    ComboBox cb = sender as ComboBox;
        //    cb.SelectedItem.ToString();
        //    string color = SelectedStringColor;
        //    //ChangeTheme(cb.SelectedItem.ToString(), cbStyle.SelectedItem.ToString());
        //    Debug.WriteLine(cbStyle);
        //}

        private void ChangeTheme(string theme, string style)
        {
            string themeName = theme + "." + style;
            ThemeManager.Current.ChangeTheme(App.Current, themeName);
        }

        private void btnChangeTheme_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.ColorScheme = SelectedStringTheme;
            Settings.Default.ColorBase = SelectedStringColor;
            ChangeTheme(SelectedStringTheme, SelectedStringColor);
            Settings.Default.Save();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginManager.Logout();
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            Window window = Window.GetWindow(this);
            window.Close();
        }
    }
}
