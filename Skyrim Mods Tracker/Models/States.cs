using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SMT.Models
{
    public enum ModState
    {
        /// <summary>
        /// Indeterminate state.
        /// </summary>
        [Description("Unknown")]
        Undefined,

        /// <summary>
        /// Mod has invalid root path.
        /// </summary>
        [Description("Invalid File")]
        InvlaidFilePath,

        /// <summary>
        /// File specified at mod's root was not found.
        /// </summary>
        [Description("File not found")]
        MissedFile,

        /// <summary>
        /// Mod hasn't got any sources.
        /// </summary>
        [Description("Not tracking")]
        NotTracking,

        /// <summary>
        /// Mod has some sources which has never been updated.
        /// </summary>
        [Description("Not checked")]
        NotChecked,

        /// <summary>
        /// Mod has one or more sources with different version.
        /// </summary>
        [Description("Has Update")]
        Outdated,

        /// <summary>
        /// Mod has the latest available version on all sources.
        /// </summary>
        [Description("OK")]
        UpToDate
    }

    public enum SourceState
    {
        /// <summary>
        /// Indeterminate state.
        /// </summary>
        [Description("Unknown")]
        Undefined,

        /// <summary>
        /// Server was not found in the list of known servers.
        /// </summary>
        [Description("Unknown server")]
        UnknownServer,

        /// <summary>
        /// Server configuration is broken.
        /// </summary>
        [Description("Bas server")]
        BrokenServer,

        /// <summary>
        /// Page provided by the source can't be reached.
        /// </summary>
        [Description("Page not found")]
        UnreachablePage,

        /// <summary>
        /// Version pattern hasn't found any matches.
        /// </summary>
        [Description("Version not found")]
        UnavailableVersion,

        /// <summary>
        /// Source is valid and available.
        /// </summary>
        [Description("OK")]
        Available,

        /// <summary>
        /// Source has a greater version than version of the source's mod.
        /// </summary>
        [Description("Has Update")]
        Update,

        /// <summary>
        /// Source has a smaller version than version of the source's mod.
        /// </summary>
        [Description("Outdated")]
        Outdated
    }

    public static class EnumDescriptionExtension
    {
        public static string GetDescription<T>(this T enumerationValue)
                where T : struct
        {
            Type type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");
            }

            //Tries to find a DescriptionAttribute for a potential friendly name
            //for the enum
            MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    //Pull out the description value
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            //If we have no description attribute, just return the ToString of the enum
            return enumerationValue.ToString();

        }
    }
}
