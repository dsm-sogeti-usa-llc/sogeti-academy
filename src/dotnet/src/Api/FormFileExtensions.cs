namespace Sogeti.Academy.Api
{
    public static class FormFileExtensions
    {
        //public static string GetFilename(this IFormFile formFile)
        //{
        //    return formFile.ContentDisposition.Split(';')
        //        .Where(p => p.Contains("filename"))
        //        .Select(p => p.Split('=')[1].Replace("\"", ""))
        //        .FirstOrDefault();
        //}

        //public static async Task<T> AsViewModel<T>(this IFormFile file) where T : IFileViewModel, new()
        //{
        //    return new T
        //    {
        //        Name = file.GetFilename(),
        //        Type = file.ContentType,
        //        Bytes = await ReadStream(file)
        //    };
        //}

        //private static async Task<byte[]> ReadStream(IFormFile file)
        //{
        //    var buffer = new byte[file.Length];
        //    await file.OpenReadStream().ReadAsync(buffer, 0, (int) file.Length);
        //    return buffer;
        //}
    }
}
