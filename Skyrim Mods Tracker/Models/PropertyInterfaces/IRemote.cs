﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.Models
{
    interface IRemote
    {
        string URL { get; set; }
        bool HasValidURL { get; }
    }
}
