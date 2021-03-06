﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS2018.Attributes
{
    /// <summary>
    /// Specifies to a field or property that it's value always has to be unique
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class DBMSUniqueAttribute : DBMSIncludeAttribute
    {
    }
}
