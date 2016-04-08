using Microsoft.Owin;

namespace Sogeti.Academy.Api
{
    public static class FormCollectionExtensions
    {
        public static string GetStringOrNull(this IFormCollection formCollection, string key)
        {
            return formCollection.Get(key) != null
                ? formCollection[key]
                : null;
        }
    }
}
