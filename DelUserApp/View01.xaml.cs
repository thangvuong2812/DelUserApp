using DataAccess.BusinessObjects;
using DataAccess.PortalMethods;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for View01.xaml
    /// </summary>
    public partial class View01 : UserControl, INotifyPropertyChanged
    {
        private PortalMethod portalMethod = null;

        public event PropertyChangedEventHandler PropertyChanged;
        
        private static bool isConnected = false;

        private ObservableCollection<string> employeeCodeList = new ObservableCollection<string>();
        public ObservableCollection<string> EmployeeCodeList { get => employeeCodeList; set { employeeCodeList = value; OnPropertyChanged(); } }
        //private BindableStringBuilder _logs = null;
        //public BindableStringBuilder Logs { get => _logs; set { _logs = value; OnPropertyChanged(); }}



        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));

        }

        public View01()
        {
            InitializeComponent();

            // Thêm các đoạn văn bản với màu chữ khác nhau vào StringBuilder

            // Gán chuỗi văn bản cho TextBlock

            DataContext = this;
        }

        private void Init()
        {
            try
            {
                portalMethod = PortalMethod.GetInstance();
                WriteLog("Mời nhập mã nhân viên ...", TypeLog.Info);
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message, TypeLog.Error);
            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            string inputText = StringFromRichText(rtxtInput);
            string[] employeeCodes = GetListFromString(inputText);
            if(employeeCodes == null || employeeCodes.Length <= 0)
            {
                WriteLog("Không tồn tại danh sách mã nhân viên!", TypeLog.Info);
                return;
            }
            string message = "Bạn có chắc chắn muốn khóa những user này?";
            string caption = "Thông báo!";
            var messageBox = MessageBox.Show(message, caption, MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (messageBox != MessageBoxResult.OK)
                return;
            try
            {
                List<string> handleEmployeeCode = employeeCodes.Select(o => "0000" + o).ToList(); 
                BOUserId bOUserId = portalMethod.CheckExistUser(employeeCodes);
                List<int> userIdExist = bOUserId.ExistUserIdList;
                List<int> employeeCodeNotExist = bOUserId.NotExistEmployeeCodeList;
                Task handleExistUserTask = Task.Run(() => HandleExistUserIds(userIdExist, handleEmployeeCode));
                Task handleNotExistUserTask = Task.Run(() => HandleNotExisUserIds(employeeCodeNotExist));
                Task.WhenAll(handleExistUserTask, handleNotExistUserTask).ConfigureAwait(false).GetAwaiter();
                rtxtInput.Document.Blocks.Clear();
            }
            catch(Exception ex)
            {
                WriteLog(ex.Message, TypeLog.Error);
            }
            Debug.WriteLine(employeeCodes);
        }

        public enum TypeLog
        {
            Error,
            Warning,
            Info,
            Success
        }
        private void WriteLog(string message, TypeLog typeLog)
        {
            if (string.IsNullOrEmpty(message))
                return;
            

            //if (Logs == null)
            //    Logs = new BindableStringBuilder();

            //Logs.AppendLine(message, typeLog);
            txtLogs.Dispatcher.BeginInvoke((Action)(() => {
                Run newRun = new Run(message + "\n");
                switch (typeLog)
                {
                    case TypeLog.Error:
                        newRun.Foreground = Brushes.Red;
                        break;
                    case TypeLog.Info:
                        newRun.Foreground = Brushes.Cyan;

                        break;
                    case TypeLog.Success:
                        newRun.Foreground = Brushes.LightGreen;

                        break;
                    case TypeLog.Warning:
                        newRun.Foreground = Brushes.Gold;
                        break;
                    default:
                        break;
                }
                txtLogs.Inlines.Add(newRun);
            }));
            //txtLogs.Dispatcher.Invoke(() => txtLogs.Inlines.Add(newRun));
            //txtLogs.Dispatcher.Invoke(() => txtLogs.ScrollToEnd());
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if(!isConnected)
            {
                WriteLog("Đang kết nối!", TypeLog.Info);
                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Loaded, new Action(Init));

                Window window = Window.GetWindow(this);
                window.Closing += Window_Closing; 
            }
        }

        private void ClearRichText(RichTextBox rtb)
        {
            TextRange textRange  = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            string outputString = ReplaceSpaceOrTab(textRange.Text);
            return outputString;
        }
        private string StringFromRichText(RichTextBox rtb)
        {
            TextRange textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            string outputString = ReplaceSpaceOrTab(textRange.Text);
            return outputString;
        }

        private string ReplaceSpaceOrTab(string input)
        {
           
            //tab
            string line = input.Replace("\t", ",").Replace("\n", ",").Replace("\r",",");
            while(line.IndexOf("\t") >= 0 || line.IndexOf("\n") >= 0 || line.IndexOf("\r") >= 0)
            {
                line.Replace("\t", ",").Replace("\n", ",").Replace("\r", ",");
            }
            return line;
        }

        private string[] GetListFromString(string input)
        {
            string[] splitedList = input.Split(",");

            string[] output = splitedList.Where(o => new Regex("^\\d+$").IsMatch(o)).ToArray();

            return output;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            WriteLog("Đang đóng kết nối", TypeLog.Info);
            //Close connect to db at here
            try
            {
                Task.Run(async () => await portalMethod.CloseConnection());
            }
            catch(Exception ex)
            {
                WriteLog(ex.Message, TypeLog.Error);
            }
        }


        private void HandleExistUserIds(List<int> userIds, List<string> employeeCodes)
        {
            try
            {
                WriteLog("Đang xử lý! Vui lòng không tắt ứng dụng", TypeLog.Warning);
                portalMethod.Delete_UserGroups(userIds);
                var rowsEffect = portalMethod.Set_BlockGroupByUserId(userIds);
                //Exec Store Procedure: Core_UpdateUserPortalGroup(UserId,PortalId = 111, GroupId = 900)

                portalMethod.Update_UserPortalGroup(userIds);
                //Update HRM (3)
                portalMethod.Update_EmployeeUserPortalDept(employeeCodes);
                //ChangeStatus
                portalMethod.Update_EmployeeStatus(employeeCodes);
                WriteLog($"Hoàn thành: Đã block {rowsEffect} users!", TypeLog.Success);
            }
            catch(Exception ex)
            {
                WriteLog(ex.Message, TypeLog.Error);
            }
           
        }

        private void HandleNotExisUserIds(List<int> employeeCodes)
        {
            string listEmployeeCode = string.Empty;
            if(employeeCodes != null && employeeCodes.Count > 0)
            {
                employeeCodes.ForEach(empCode => listEmployeeCode += empCode +", ");
                WriteLog("Mã nhân viên không tồn tai: " + listEmployeeCode, TypeLog.Error);
            }
        }
    }
}
