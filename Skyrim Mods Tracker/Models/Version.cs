using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SMT.Models
{
    class Version : IValidatable, IComparable<Version>, IComparable<string>
    {
        private const string VERSION_PATTERN = "^v?(?:(\\d+)\\.?)+\\s*([a-zA-Z]+)?\\s*$";

        private string rawValue;
        private int[] components;
        private string stage;

        public bool IsValid { get; private set; }

        public bool HasStage { get { return !string.IsNullOrWhiteSpace(stage); } }

        public Version() { rawValue = ""; }

        public Version(string version)
        {
            Value = version;
        }

        public string Value {
            get { return rawValue; }
            set { rawValue = value; ParseVersion(); }
        }

        private void ParseVersion()
        {
            Match m = Regex.Match(rawValue, VERSION_PATTERN);
            if (m.Success)
            {
                CaptureCollection comps = m.Groups[1].Captures;
                components = new int[comps.Count];
                for (int i = 0; i < comps.Count; i++)
                    int.TryParse(comps[i].Value, out components[i]);
                stage = (m.Groups[2].Captures.Count > 0 ? m.Groups[2].Captures[0].Value : "");
                rawValue = string.Join(".", components) + (HasStage ? " " + stage : "");
            }
            IsValid = m.Success;
        }

        public override bool Equals(object other)
        {
            if (other == null || !GetType().Equals(other.GetType())) return false;
            return (rawValue.Equals((other as Version).rawValue));
        }

        public static bool operator > (Version first, Version second)
        {
            if (first == null) return false;
            else if (second == null) return true;
            else return first.CompareTo(second) > 0;
        }
        public static bool operator < (Version first, Version second)
        {
            if (first == null) return false;
            else if (second == null) return true;
            else return first.CompareTo(second) < 0;
        }
        public static bool operator >= (Version first, Version second)
        {
            if (first == null) return false;
            else if (second == null) return true;
            else return first.CompareTo(second) >= 0;
        }
        public static bool operator <= (Version first, Version second)
        {
            if (first == null) return false;
            else if (second == null) return true;
            else return first.CompareTo(second) <= 0;
        }

        public static explicit operator string(Version vers) { return vers.rawValue; }
        public static explicit operator Version(string vers) { return new Version(vers); }

        public int CompareTo(string other) { return CompareTo(new Version(other)); }
        public int CompareTo(Version other)
        {
            if (other == null) return 1;
            if (Equals(other)) return 0;
            if (!IsValid || !other.IsValid) return Value.CompareTo(other.Value); // if one of the version has invalid format compare them as raw strings

            int[] comps1 = components;
            int[] comps2 = other.components;

            string stage1 = stage;
            string stage2 = other.stage;

            bool isDifLength = comps1.Length != comps2.Length;
            int minLength = Math.Min(comps1.Length, comps2.Length);

            for (int i = 0; i < minLength; i++)
            {
                if (comps1[i] > comps2[i]) return 1;
                else if (comps1[i] < comps2[i]) return -1;
            }

            if (isDifLength) return (comps1.Length > comps2.Length ? 1 : -1); // while common part is the same, but lengths are different - greater will be version with extra components.
            else if (stage1 != "" || stage2 != "") // if components are completely idential check stage parts if any.
                return stage1.CompareTo(stage2);
            else return 0;

        }

        public override string ToString()
        {
            return Value;
        }
    }
}
