using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS2018.Table
{
    public class ColumnInformation
    {
        #region "Fields"

        private string name;
        private Type type;
        private bool unique;
        private bool primaryKey;
        private bool nullable;
        private bool autoIncrement;

        #endregion

        #region "Constructors"

        public ColumnInformation(string name, Type type, bool unique = false, bool nullable = true, bool primaryKey = false, bool autoIncrement = false)
        {
            this.name = name;
            this.type = type;
            this.unique = unique;
            this.nullable = nullable;
            this.primaryKey = primaryKey;
            this.autoIncrement = autoIncrement;
        }

        public ColumnInformation(string name, Type type, bool primaryKey = false)
        {
            this.name = name;
            this.type = type;
            if (primaryKey)
            {
                this.unique = true;
                this.nullable = false;
                this.primaryKey = true;
                this.autoIncrement = true;
            }
        }

        #endregion

        #region "Properties"

        public string Name => name;

        public Type Type => type;

        public bool Unique => unique;

        public bool Nullable => nullable;

        public bool PrimaryKey => primaryKey;

        #endregion

        #region "Methods"



        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"

        public override string ToString()
        {
            return "COLUMN {" + Name + ", " + Type.Name + "}";
        }

        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
