using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS2018.Domain
{
    class CacheObject
    {
        #region "Fields"

        private readonly uint _uid;
        private readonly object _obj;

        #endregion

        #region "Constructors"

        /// <summary>
        /// Creates a new object to be placed in the object cache
        /// </summary>
        /// <param name="obj">The object to put into the cache</param>
        public CacheObject(object obj)
        {
            this._obj = obj;
        }

        #endregion

        #region "Properties"

        /// <summary>
        /// Gets the unique UID of this object
        /// </summary>
        public uint UID => _uid;

        /// <summary>
        /// Gets the object linked to the UID
        /// </summary>
        public object Object => _obj;

        #endregion

        #region "Methods"



        #endregion
    }
}
