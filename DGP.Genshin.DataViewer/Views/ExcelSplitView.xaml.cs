using DGP.Genshin.DataViewer.Helper;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace DGP.Genshin.DataViewer.Views
{
    /// <summary>
    /// ExcelSplitView.xaml 的交互逻辑
    /// </summary>
    public partial class ExcelSplitView : UserControl, INotifyPropertyChanged
    {
        public ExcelSplitView()
        {
            DataContext = this;
            InitializeComponent();
        }

        private string ReadFile(FileEx file)
        {
            string json;
            using (StreamReader sr = new StreamReader(file.FullPath))
            {
                json = sr.ReadToEnd();
            }
            return json;
        }

        #region Property
        private string workingFolderPath;
        private IEnumerable<FileEx> excelConfigDataCollection;
        private FileEx selectedFile;
        private JArray presentDataTable;

        public string WorkingFolderPath
        {
            get => workingFolderPath; set
            {
                ExcelConfigDataCollection = Directory.GetFiles(value + @"\Excel\").Select(f => new FileEx(f));
                Set(ref workingFolderPath, value);
            }
        }
        public IEnumerable<FileEx> ExcelConfigDataCollection
        {
            get => excelConfigDataCollection; set
            {
                Set(ref excelConfigDataCollection, value);
            }
        }
        public FileEx SelectedFile
        {
            get => selectedFile; set
            {
                PresentDataTable = Json.ToObject<JArray>(ReadFile(value));
                foreach (JObject o in PresentDataTable)
                {
                    foreach(JProperty p in o.Properties())
                    {
                        if (p.Name.Contains("TextMapHash"))
                            p.Value = MainWindow.GetMapTextBy(p.Value.ToString());
                    }
                }
                Set(ref selectedFile, value);
            }
        }
        public JArray PresentDataTable
        {
            get => presentDataTable; set
            {
                Set(ref presentDataTable, value);
            }
        }
        #endregion

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
