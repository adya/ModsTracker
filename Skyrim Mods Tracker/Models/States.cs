using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.Models
{
    public enum ModState
    {
        /// <summary>
        /// Indeterminate state.
        /// </summary>
        Undefined,

        /// <summary>
        /// Mod has invalid root path.
        /// </summary>
        InvlaidRoot,

        /// <summary>
        /// File specified at mod's root was not found.
        /// </summary>
        MissedFile,

        /// <summary>
        /// Mod hasn't got any sources.
        /// </summary>
        NotTracking,

        /// <summary>
        /// Mod has one or more sources with different version.
        /// </summary>
        Outdated,

        /// <summary>
        /// Mod has the latest available version on all sources.
        /// </summary>
        UpToDate
    }

    public enum SourceState
    {
        /// <summary>
        /// Indeterminate state.
        /// </summary>
        Undefined,

        /// <summary>
        /// Server was not found in the list of known servers.
        /// </summary>
        UnknownServer,

        /// <summary>
        /// Server configuration is broken.
        /// </summary>
        BrokenServer,

        /// <summary>
        /// Page provided by the source can't be reached.
        /// </summary>
        UnreachablePage,

        /// <summary>
        /// Version pattern hasn't found any matches.
        /// </summary>
        UnavailableVersion,

        /// <summary>
        /// Source is valid and available.
        /// </summary>
        Available,

        /// <summary>
        /// Source has version different from version of the source's mod.
        /// </summary>
        Update
    }
}
