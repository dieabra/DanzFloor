using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DanzFloor.Web.Helpers
{
    public class CacheHelper
    {
        public static void InsertKey(string key, string value)
        {
            System.Web.HttpContext.Current.Cache.Insert(key, value, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(1));
        }

        public static string GetKey(string key)
        {
            var value = System.Web.HttpContext.Current.Cache.Get(key);
            return (value == null ? "" : value.ToString());
        }

        public static void RemoveKey(string key)
        {
            System.Web.HttpContext.Current.Cache.Remove(key);
        }

        public static string GetNotLoggedUserKey()
        {
            return "key";
        }
    }
}