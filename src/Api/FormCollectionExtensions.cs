using Microsoft.AspNet.Http;

namespace Sogeti.Academy.Api
{
    public static class FormCollectionExtensions
    {
        public static string GetStringOrNull(this IFormCollection formCollection, string key)
        {
            return formCollection.ContainsKey(key)
                ? (string)formCollection[key]
                : null;
        }
    }
}
