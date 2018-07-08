using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBMS2018.Domain
{
    /// <summary>
    /// Creates a local cache of object that are stored in the database.
    /// </summary>
    class LocalCache
    {
        #region "Fields"

        private List<CacheObject> _cache;

        #endregion

        #region "Constructors"

        /// <summary>
        /// Creates a new object cache
        /// </summary>
        public LocalCache()
        {
            _cache = new List<CacheObject>();
        }

        #endregion

        #region "Properties"



        #endregion

        #region "Methods"

        /// <summary>
        /// Adds an object to the cache.
        /// If the object already exists it is not added
        /// </summary>
        /// <param name="obj">The object to add to the cache</param>
        public void AddObjectToCache(object obj)
        {
            if (!Exists(obj))
            {
                _cache.Add(new CacheObject(obj));
            }
        }

        /// <summary>
        /// Checks if the given object already exists in the cache
        /// </summary>
        /// <param name="obj">The object to check for</param>
        /// <returns>Returns true if the object exists; otherwise false</returns>
        private bool Exists(object obj)
        {
            return _cache.Any(x => x.Object == obj);
        }

        /// <summary>
        /// Gets the given object from the cache if it exists
        /// </summary>
        /// <param name="obj">The object to find</param>
        /// <returns>Returns the object if found; otherwise null</returns>
        private CacheObject FindObject(object obj)
        {
            return _cache.FirstOrDefault(x => x.Object == obj);
        }

        /// <summary>
        /// Tries to remove the given object from the cache
        /// </summary>
        /// <param name="obj">The object to remove from the cache</param>
        public void RemoveFromCache(object obj)
        {
            _cache.RemoveAll(x => x.Object == obj);
        }

        /// <summary>
        /// Removes all objects from the cache
        /// </summary>
        public void ClearCache()
        {
            _cache.Clear();
        }

        #endregion
    }
}
