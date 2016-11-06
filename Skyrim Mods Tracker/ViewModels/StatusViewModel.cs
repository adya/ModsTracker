using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.ViewModels
{
    class StatusViewModel : BaseViewModel
    {
        private bool isVisible;
        private bool isProgressVisible;
        private string status;
        private int currentProgress;

        public int CurrentProgress { get { return currentProgress; } set { currentProgress = value; OnPropertyChanged(); }}
        public int MinimumProgress { get { return 0; } }
        public int MaximumProgress { get { return 100; } }

        public string Status { get { return status; }
            set
            {
                if (IsProgressVisible)
                {
                    status = string.Format("{0}% {1}", CurrentProgress, value);
                }
                else {
                    status = value;
                }
                OnPropertyChanged();
            }
        }

        public bool IsVisible { get { return isVisible; } set { isVisible = value; OnPropertyChanged(); OnPropertyChanged("IsProgressVisible"); } }

        public bool IsProgressVisible { get { return IsVisible && isProgressVisible; } set { isProgressVisible = value; OnPropertyChanged(); } }
    }
}
