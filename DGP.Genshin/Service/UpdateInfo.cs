using DGP.Genshin.Helper;

namespace DGP.Genshin.Service
{
    public class UpdateInfo : Observable
    {
        private string title;
        private string detail;
        private string progressText;
        private double progress;

        public string Title { get => title; set => Set(ref title, value); }
        public string Detail { get => detail; set => Set(ref detail, value); }
        public string ProgressText { get => progressText; set => Set(ref progressText, value); }
        public double Progress { get => progress; set => Set(ref progress, value); }
    }
}
