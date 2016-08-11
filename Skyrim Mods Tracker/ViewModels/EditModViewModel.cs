using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMT.Models;
using System.Windows.Media;
using System.ComponentModel;
using SMT.Managers;

namespace SMT.ViewModels
{
    class EditModViewModel : BaseViewModel, IDataErrorInfo
    {
        private Mod mod;

        public EditModViewModel(Mod mod)
        {
            this.mod = mod;
        }

        public string Name { get { return mod.Name; } set { mod.Name = value; OnPropertyChanged(); } }
        public string Version { get { return mod.Version.Value; } set { mod.Version.Value = value; OnPropertyChanged(); } }
        public Language Language { get { return mod.Language; } set { mod.Language = value; OnPropertyChanged(); } }
        public Language[] AvailableLanguages { get { return new Language[] { Models.Language.Russian, Models.Language.English, Models.Language.None }; } }

        string IDataErrorInfo.Error { get { return null; } }

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                string validationMessage = string.Empty;
                switch (columnName)
                {
                    case "Name": 
                        if (!mod.HasValidName) validationMessage = "Name must not be empty.";
                        else if (!mod.HasUniqueName()) validationMessage = "Name must be unique.";
                        break;
                    case "Version":
                        if (!mod.HasValidVersion) validationMessage = "Version must not be empty.";
                        break;
                }
                return validationMessage;

            }
        }
    }
}
