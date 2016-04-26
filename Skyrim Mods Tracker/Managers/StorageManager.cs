using SMT.Models;
using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SMT.Managers
{
    static class StorageManager
    {
        private const string STORAGE_FILE = "smt";
        private const string STORAGE_EXT = ".json";
        private const string STORAGE_VERSION = "0.3";

        private const string STORAGE_SERVERS_KEY = "Servers";
        private const string STORAGE_MODS_KEY = "Mods";
        private const string STORAGE_VERSION_KEY = "Version";
        private const string STORAGE_DATE_KEY = "Updated";

        private static Dictionary<string, dynamic> data;
        public static string StorageFilePath { get { return Path.Combine(Environment.CurrentDirectory, STORAGE_FILE + STORAGE_EXT); } }

        static StorageManager()
        {
            data = new Dictionary<string, dynamic>();
            data.Set(STORAGE_SERVERS_KEY, new HashSet<Server>());
            data.Set(STORAGE_MODS_KEY, new HashSet<Mod>());
            data.Set(STORAGE_DATE_KEY, DateTime.Now.ToString("MM/dd/yyyy HH:mm"));
            data.Set(STORAGE_VERSION_KEY, STORAGE_VERSION);

            if (!File.Exists(StorageFilePath))
                Sync();
            string json = File.ReadAllText(StorageFilePath);

            Dictionary<string, dynamic> storageData = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(json);
            dynamic value;
            if (storageData.TryGetValue(STORAGE_SERVERS_KEY, out value))
            {
                storageData.Set(STORAGE_SERVERS_KEY, (value as JArray).ToObject<HashSet<Server>>());
                storageData.TryCopy(STORAGE_SERVERS_KEY, data);
            }
            if (storageData.TryGetValue(STORAGE_MODS_KEY, out value))
            { 
                storageData.Set(STORAGE_MODS_KEY, (value as JArray).ToObject<HashSet<Mod>>());
                storageData.TryCopy(STORAGE_MODS_KEY, data);
            }
            
            storageData.TryCopy(STORAGE_DATE_KEY, data);
            storageData.TryCopy(STORAGE_VERSION_KEY, data);

        }

        /// <summary>
        /// Loads set of specified items.
        /// </summary>
        /// <typeparam name="T">Target type.</typeparam>
        /// <returns>Returns HashSet of stored items of specified type. </returns>
        public static HashSet<T> Get<T>() { return data[KeyForType(typeof(T))]; }

        public static void Set<T>(HashSet<T> items)
        {
            if (items == null) return;
            string key = KeyForType(typeof(T));
            data.Set(key, new HashSet<T>(items));
        }

       

        public static void Sync()
        {
            data.Set(STORAGE_DATE_KEY, DateTime.Now.ToString("MM/dd/yyyy HH:mm"));
            data.Set(STORAGE_VERSION_KEY, STORAGE_VERSION);
            string res = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(StorageFilePath, res);
        }

        private static string KeyForType(Type t)
        {
            if (t.Equals(typeof(Mod)))
                return STORAGE_MODS_KEY;
            else if (t.Equals(typeof(Server)))
                return STORAGE_SERVERS_KEY;
            else
                throw new ArgumentException("No defined key for type " + t.ToString());
        }

        #region Dictionary Extensions
        private static void Set<TKey, TValue>(this Dictionary<TKey, TValue> dic, TKey key, TValue value)
        {
            if (key == null || value == null) return;
            if (dic.ContainsKey(key))
                dic.Remove(key);
            dic.Add(key, value);
        }
        private static bool TryCopy<TKey, TValue>(this Dictionary<TKey, TValue> src, TKey key, Dictionary<TKey, TValue> dst)
        {
            bool success = src.ContainsKey(key);
            if (success)
                dst.Set(key, src[key]);
            return success;
        }
        #endregion
    }
}
