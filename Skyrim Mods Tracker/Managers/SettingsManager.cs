using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMT.Utils;
using System.IO;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace SMT.Managers
{
    static class SettingsManager
    {
        private const string SETTINGS_FILENAME = "smt_config";
        private const string SETTINGS_EXT = ".json";

        public const string SETTINGS_SCAN_MODS_FLAG = "AutoScanMods";
        public const string SETTINGS_MODS_LOCATION_PATH = "ModsLocation";
        public const string SETTINGS_PATTERN_NAMING_FLAG = "UsePatterns";
        public const string SETTINGS_NAMING_PATTERN = "NamePattern";
        public const string SETTINGS_AUTO_RENAME_FLAG = "AutoUpdateNames";
        public const string SETTINGS_USE_BACKUPS_FLAG = "UseBackups";
        public const string SETTINGS_BACKUPS_LEVEL = "MaxBackupsLevel";

        private const bool DEFAULT_SCAN_MODS_FLAG = false;
        private const string DEFAULT_MODS_LOCATION = "";
        private const bool DEFAULT_PATTERN_NAMING = false;
        private const string DEFAULT_PATTERN = "(?<{0}>.+)\\s*\\(\\s* v?\\s*(?<{1}>.+)\\s*\\)\\s*\\(\\s*(?<{2}>.+)\\s*\\)";
        private const bool DEFAULT_AUTO_RENAME = false;
        private const bool DEFAULT_USE_BACKUPS = true;
        private const int DEFAULT_BACKUPS_LEVEL = 2;

        public const string PATTERN_NAME_GROUP = "name";
        public const string PATTERN_VERSION_GROUP = "version";
        public const string PATTERN_LANGUAGE_GROUP = "language";

        public static string SettingsFilePath { get { return Path.Combine(Environment.CurrentDirectory, SETTINGS_FILENAME + SETTINGS_EXT); } }
        public static string DefaultNamePattern { get { return string.Format(DEFAULT_PATTERN, PATTERN_NAME_GROUP, PATTERN_VERSION_GROUP, PATTERN_LANGUAGE_GROUP); } }

        private static Dictionary<string, dynamic> settings;

        static SettingsManager()
        {
            settings = new Dictionary<string, dynamic>();
            LoadDefaults();
            if (!File.Exists(SettingsFilePath))
                Sync();
            LoadCustom();
        }

        private static void LoadDefaults()
        {
            settings.Set(SETTINGS_SCAN_MODS_FLAG, DEFAULT_SCAN_MODS_FLAG);
            settings.Set(SETTINGS_MODS_LOCATION_PATH, DEFAULT_MODS_LOCATION);
            settings.Set(SETTINGS_PATTERN_NAMING_FLAG, DEFAULT_PATTERN_NAMING);
            settings.Set(SETTINGS_NAMING_PATTERN, DefaultNamePattern);
            settings.Set(SETTINGS_AUTO_RENAME_FLAG, DEFAULT_AUTO_RENAME);
            settings.Set(SETTINGS_USE_BACKUPS_FLAG, DEFAULT_USE_BACKUPS);
            settings.Set(SETTINGS_BACKUPS_LEVEL, DEFAULT_BACKUPS_LEVEL);
        }

        private static void LoadCustom()
        {
            string json = File.ReadAllText(SettingsFilePath);
            Dictionary<string, dynamic> storageSettings = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(json);
            storageSettings.TryCopy(SETTINGS_SCAN_MODS_FLAG, settings);
            storageSettings.TryCopy(SETTINGS_MODS_LOCATION_PATH, settings);
            storageSettings.TryCopy(SETTINGS_PATTERN_NAMING_FLAG, settings);
            storageSettings.TryCopy(SETTINGS_NAMING_PATTERN, settings);
            storageSettings.TryCopy(SETTINGS_AUTO_RENAME_FLAG, settings);
            storageSettings.TryCopy(SETTINGS_USE_BACKUPS_FLAG, settings);
            storageSettings.TryCopy(SETTINGS_BACKUPS_LEVEL, settings);
        }

        public static void Sync()
        {
            string res = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(SettingsFilePath, res);
        }

        public static T Get<T>(string key)
        {
            object obj;
            if (!settings.TryGetValue(key, out obj)) return default(T);
            return (T)obj;
        }

        public static void Set<T>(string key, T obj)
        {
            settings.Set(key, obj);
        }

        public static bool AutoScanMods
        {
            get { return (bool)settings[SETTINGS_SCAN_MODS_FLAG]; }
            set { settings[SETTINGS_SCAN_MODS_FLAG] = value; }
        }
        public static string ModsLocation
        {
            get { return (string)settings[SETTINGS_MODS_LOCATION_PATH]; }
            set { settings[SETTINGS_MODS_LOCATION_PATH] = (value != null ? value : ""); }
        }

        public static bool HasValidModsLocation {
            get {
                if (string.IsNullOrWhiteSpace(ModsLocation))
                    return false;
                return Directory.Exists(ModsLocation);
            }
        }

        public static bool PatternNaming
        {
            get { return (bool)settings[SETTINGS_PATTERN_NAMING_FLAG]; }
            set { settings[SETTINGS_PATTERN_NAMING_FLAG] = value; }
        }
        public static string NamePattern
        {
            get { return (string)settings[SETTINGS_NAMING_PATTERN]; }
            set { settings[SETTINGS_NAMING_PATTERN] = (value != null ? value : ""); }
        }

        public static bool HasValidNamePattern
        {
            get
            {
                if (string.IsNullOrWhiteSpace(NamePattern))
                    return false;
                try {
                    Regex.IsMatch("", NamePattern); return true; }
                catch (ArgumentException) { return false; }
            }
        }

        public static bool HasFullNamePattern
        {
            get
            {
                string format = "?<{0}>";
                return (NamePattern.Contains(string.Format(format, PATTERN_NAME_GROUP)) &&
                        NamePattern.Contains(string.Format(format, PATTERN_VERSION_GROUP)) 
                        //&& NamePattern.Contains(string.Format(format, PATTERN_LANGUAGE_GROUP))
                        );
            }
        }

        public static bool AutoRename
        {
            get { return (bool)settings[SETTINGS_AUTO_RENAME_FLAG]; }
            set { settings[SETTINGS_AUTO_RENAME_FLAG] = value; }
        }

        public static bool UseBackups
        {
            get { return (bool)settings[SETTINGS_USE_BACKUPS_FLAG]; }
            set { settings[SETTINGS_USE_BACKUPS_FLAG] = value; }
        }
        public static int BackupsLevel
        {
            get { return (int)settings[SETTINGS_BACKUPS_LEVEL]; }
            set { settings[SETTINGS_BACKUPS_LEVEL] = (value <= 0 ? 1 : value); }
        }

        public static bool IsValid { get { return (!AutoScanMods || HasValidModsLocation) && (!PatternNaming || HasValidNamePattern); } }
    }
}
