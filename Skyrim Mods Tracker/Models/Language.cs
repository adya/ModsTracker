using SMT.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMT.Models
{

    public enum Language
    {
        Unknown = -1,
        None,
        Russian,
        English
    }

    public static class LanguageNames
    {
        private static Dictionary<Language, string> langNames;

        static LanguageNames()
        {
            langNames = new Dictionary<Language, string>();
            langNames.Add(Language.Unknown, "XXX");
            langNames.Add(Language.None, "NO");
            langNames.Add(Language.Russian, "RU");
            langNames.Add(Language.English, "EN");
        }

       
        public static Language GetLanguage(string name) {
            Language value;
            if (!Enum.TryParse(name, out value))
                value = (langNames.ContainsValue(name.ToUpper()) ? langNames.First(kvp => kvp.Value.Equals(name.ToUpper())).Key : Language.Unknown);
            return value;
        }

        public static string GetName(this Language lang) { return langNames[lang]; }

        public static string ToShortString(this Language lang) { return lang.GetName(); }
    }


}
