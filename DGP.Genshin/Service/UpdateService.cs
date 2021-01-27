using DGP.Genshin.Helper;
using DGP.Genshin.Models.Github;
using DGP.Snap.Framework.Net.Download;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DGP.Genshin.Service
{
    internal class UpdateService
    {
        public Uri PackageUri { get; set; }
        public Version NewVersion { get; set; }
        public Release ReleaseInfo { get; set; }
        public UpdateInfo UpdateInfo { get; set; }
        public Version CurrentVersion => Assembly.GetExecutingAssembly().GetName().Version;

        public IFileDownloader InnerFileDownloader { get; set; }

        private const string GithubUrl = @"https://api.github.com/repos/DGP-Studio/Snap.Genshin/releases/latest";
        private const string GiteeUrl = @"https://gitee.com/api/v5/repos/Lightczx/Snap.Genshin/releases/latest";

        public UpdateAvailability CheckUpdateAvailability()
        {
            try
            {
                ReleaseInfo = Json.GetWebRequestObject<Release>(GiteeUrl);
                UpdateInfo.Title = ReleaseInfo.Name;
                UpdateInfo.Detail = ReleaseInfo.Body;
                string newVersion = ReleaseInfo.TagName;
                NewVersion = new Version(ReleaseInfo.TagName);

                if (new Version(newVersion) > CurrentVersion)//有新版本
                {
                    PackageUri = new Uri(ReleaseInfo.Assets[0].BrowserDownloadUrl);
                    return UpdateAvailability.NeedUpdate;
                }
                else
                {
                    if (new Version(newVersion) == CurrentVersion)
                        return UpdateAvailability.IsNewestRelease;
                    else
                        return UpdateAvailability.IsInsiderVersion;
                }
            }
            catch (Exception e)
            {
                return UpdateAvailability.NotAvailable;
            }
        }

        public void DownloadAndInstallPackage()
        {
            InnerFileDownloader = new FileDownloader();
            InnerFileDownloader.DownloadProgressChanged += OnDownloadProgressChanged;
            InnerFileDownloader.DownloadFileCompleted += OnDownloadFileCompleted;

            string destinationPath = AppDomain.CurrentDomain.BaseDirectory + @"\Package.zip";
            InnerFileDownloader.DownloadFileAsync(PackageUri, destinationPath);
        }

        internal void OnDownloadProgressChanged(object sender, DownloadFileProgressChangedArgs args)
        {
            double percent = Math.Round((double)args.BytesReceived / args.TotalBytesToReceive, 2);
            UpdateInfo.Progress = percent;
            UpdateInfo.ProgressText = $@"{percent*100}% - {args.BytesReceived / 1024}KB / {args.TotalBytesToReceive / 1024}KB";
        }
        internal void OnDownloadFileCompleted(object sender, DownloadFileCompletedArgs eventArgs)
        {
            //InnerFileDownloader.DownloadFileCompleted -= OnDownloadFileCompleted;
            //InnerFileDownloader.Dispose();
            if (eventArgs.State == CompletedState.Succeeded)
                StartUpdateInstall();
        }
        public static void StartUpdateInstall()
        {
            if (File.Exists("OldUpdater.exe"))
                File.Delete("OldUpdater.exe");
            File.Move("DGP.Snap.Updater.exe", "OldUpdater.exe");
            Process.Start("OldUpdater.exe");
            App.Current.Shutdown();
        }
        #region 单例
        private static UpdateService instance;
        private static object _lock = new object();
        private UpdateService()
        {

        }
        public static UpdateService GetInstance()
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new UpdateService();
                    }
                }
            }
            return instance;
        }
        #endregion
    }

    public enum UpdateAvailability
    {
        NeedUpdate = 0,
        IsNewestRelease = 1,
        IsInsiderVersion = 2,
        NotAvailable = 3
    }

    public class UpdateInfo : Observable
    {
        private string title;
        private string detail;
        private string progressText;
        private double progress;

        public string Title { get => title; set => Set(ref title, value); }
        public string Detail { get => detail; set => Set(ref detail, value); }
        public string ProgressText { get => progressText; set => Set(ref progressText, value); }
        public double Progress { get => progress; set => Set(ref progress,value); }
    }
}
