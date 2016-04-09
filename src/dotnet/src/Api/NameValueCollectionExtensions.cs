using System.Collections.Specialized;
using System.Linq;

namespace Sogeti.Academy.Api
{
    public static class NameValueCollectionExtensions
    {
        public static string GetStringOrDefault(this NameValueCollection collection, string key)
        {
            return collection.AllKeys.Contains(key)
                ? collection.Get(key)
                : null;
        }
    }
}
