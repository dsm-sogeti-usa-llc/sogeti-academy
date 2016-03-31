using System.Linq;
using Microsoft.AspNet.Http;

namespace Sogeti.Academy.Api
{
    public static class FormFileExtensions
    {
        public static string GetFilename(this IFormFile formFile)
        {
            return formFile.ContentDisposition.Split(';')
                .Where(p => p.Contains("filename"))
                .Select(p => p.Split('=')[1].Replace("\"", ""))
                .FirstOrDefault();
        }
    }
}
