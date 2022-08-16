using System.ComponentModel;

namespace MancalaAssessment.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string bannerText = "Click \"New Game\" to get started!";
        public string BannerText
        {
            get => bannerText;
            set
            {
                if(bannerText != value)
                {
                    bannerText = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BannerText)));
                }
            }
        }
    }
}
