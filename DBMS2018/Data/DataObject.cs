using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DBMS2018.Data
{
    public class DataObject
    {
        #region "Fields"

        private Table _table;
        private object[] _data;

        #endregion

        #region "Constructors"

        public DataObject(object data)
        {
            this._table = Table.ParseFromType(data.GetType());
            _data = new object[this._table.ColumnCount];

            int index = 0;
            foreach(Column column in this._table)
            {
                object value = (column._ClassRefence as FieldInfo)?.GetValue(data);
                if (value == null)
                    value = (column._ClassRefence as PropertyInfo)?.GetValue(data);

                _data[index] = value;
                index++;
            }
        }

        #endregion

        #region "Properties"



        #endregion

        #region "Methods"

        public T GetValue<T>()
        {
            return default(T);
        }

        #endregion
    }
}
