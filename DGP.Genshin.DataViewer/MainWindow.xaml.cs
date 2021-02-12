using DGP.Genshin.DataViewer.Helper;
using DGP.Genshin.DataViewer.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace DGP.Genshin.DataViewer
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
        }

        private void OpenFolderRequested(object sender, RoutedEventArgs e)
        {
            path = WorkingFolderService.SelectWorkingFolder();
            ExcelDataView.WorkingFolderPath = path;
            TextMapCollection = Directory.GetFiles(path + @"\TextMap\").Select(f => new FileEx(f));
        }

        public static string GetMapTextBy(string value)
        {
            if(TextMap!=null)
                if(TextMap.TryGetValue(value, out string result))
                    return result;
            return value;
        }

        private static Dictionary<string, string> TextMap;
        private string path;
        

        private IEnumerable<FileEx> textMapCollection;
        private FileEx selectedTextMap;
        public IEnumerable<FileEx> TextMapCollection
        {
            get => textMapCollection; set
            {
                Set(ref textMapCollection, value);
            }
        }
        public FileEx SelectedTextMap
        {
            get => selectedTextMap; set
            {
                Set(ref selectedTextMap, value);
                TextMap = Json.ToObject<Dictionary<string, string>>(selectedTextMap.ReadFile());
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void Set<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }

        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}
