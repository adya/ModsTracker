using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.Utils
{
    static class StringUtils
    {
        public static string NonNull(string str) { return (str == null ? "" : str); }
    }
}
