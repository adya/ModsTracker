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
            langNames.Add(Language.None, "NO");
            langNames.Add(Language.Russian, "RU");
            langNames.Add(Language.English, "EN");
        }

       
        public static Language GetLanguage(string name) { return (langNames.ContainsValue(name.ToUpper()) ? langNames.First(kvp => kvp.Value.Equals(name.ToUpper())).Key : Language.Unknown); }

        public static string GetName(this Language lang) { return langNames[lang]; }

        public static string ToShortString(this Language lang) { return lang.GetName(); }
    }


}
