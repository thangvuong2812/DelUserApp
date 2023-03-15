using DataAccess.PortalMethods;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
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
    /// Interaction logic for View02.xaml
    /// </summary>
    public partial class LoginView : UserControl, INotifyPropertyChanged
    {
        private bool _isEnabledError = false;
        public bool IsEnabledError { get => _isEnabledError; set { _isEnabledError = value; OnPropertyChanged(nameof(IsEnabledError)); } }

        private string _errorMessage = string.Empty;
        public string ErrorMessage { get => _errorMessage; set { _errorMessage = value; OnPropertyChanged(nameof(ErrorMessage));} }
        public LoginView()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
        private void loginButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            string userName = txtUserName.Text;
            string passWord = txtPass.Password;

            int isExistAdminUser = PortalMethod.GetInstance().CheckLogin(userName, passWord);

            if (isExistAdminUser != 0)
            {
                
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                //Close LoginWindow
                Window window = Window.GetWindow(this);
                window.Close();

            }
            else
            {
                //txtError.Dispatcher.Invoke(() =>
                //{
                //    txtError.Text = "Tài khoản hoặc mật khẩu đăng nhập không hợp lệ!";
                //    txtError.IsEnabled = true;
                //});
                ErrorMessage = "Tài khoản hoặc mật khẩu đăng nhập không hợp lệ!";
                IsEnabledError = true;
            }
        }
    }
}
