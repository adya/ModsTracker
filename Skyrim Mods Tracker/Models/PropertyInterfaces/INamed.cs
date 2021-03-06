﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.Models.PropertyInterfaces
{
    /// <summary>
    /// Entity with name.
    /// </summary>
    interface INamed
    {
        string Name { get; set; }
        bool HasValidName { get; }
    }
}
