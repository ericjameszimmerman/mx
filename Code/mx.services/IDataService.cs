﻿using mx.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mx.services
{
    public interface IDataService
    {
        RootGroup GetProjectHierarchy();
    }
}
