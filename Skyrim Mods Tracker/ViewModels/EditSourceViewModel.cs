using SMT.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.ViewModels
{
    class EditSourceViewModel : BaseViewModel, IDataErrorInfo
    {
        private Source src;
        private bool isEditableVersion;

        public EditSourceViewModel(Source src)
        {
            this.src = src;
            isEditableVersion = false;
        }

        public string URL { get { return src.URL; } set { src.URL = value; OnPropertyChanged(); } }
        public string Version { get { return src.Version.Value; } set { src.Version.Value = value; OnPropertyChanged(); } }
        public Language Language { get { return src.Language; } set { src.Language = value; OnPropertyChanged(); } }
        public Language[] AvailableLanguages { get { return new Language[] { Models.Language.Russian, Models.Language.English, Models.Language.None }; } }

        public bool IsVersionEditable { get { return isEditableVersion; } set { isEditableVersion = value; OnPropertyChanged(); } }


        string IDataErrorInfo.Error { get { return null; } }

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                string validationMessage = string.Empty;
                switch (columnName)
                {
                    case "URL":
                        if (!src.HasValidURL) validationMessage = "Malformed URL.";
                        else if (!src.HasKnownServer) validationMessage = "Uknown server domain.";
                        break;
                    case "Version":
                        if (!src.HasValidVersion) validationMessage = "Version must not be empty.";
                        break;
                }
                return validationMessage;

            }
        }
    }
}
