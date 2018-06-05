using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS2018.Attributes
{
    public class DBMSNullableAttribute : Attribute
    {
        private bool _nullable = true;

        public bool Nullable
        {
            get => _nullable;
            set => _nullable = value;
        }
    }
}
