using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using Sogeti.Academy.Application.Presentations.Commands.Edit;

namespace Sogeti.Academy.Api.Presentations.Factories
{
    public interface IEditFileViewModelFactory
    {
        EditFileViewModel[] Create(MultipartFormDataStreamProvider provider);
    }

    public class EditFileViewModelFactory : IEditFileViewModelFactory
    {
        public EditFileViewModel[] Create(MultipartFormDataStreamProvider provider)
        {
            var existingFiles = GetExistingFileViewModels(provider);
            return provider.FileData
                .Select(GetFileViewModel)
                .Concat(existingFiles)
                .ToArray();
        }

        private static EditFileViewModel[] GetExistingFileViewModels(MultipartFormDataStreamProvider provider)
        {
            return provider.FormData.AllKeys.Where(k => k.Contains("files"))
                .GroupBy(GetGroupKey)
                .Select(g => MapExistingFile(g, provider.FormData))
                .ToArray();
        }

        private static EditFileViewModel MapExistingFile(IGrouping<string, string> grouping, NameValueCollection form)
        {
            var idKey = grouping.FirstOrDefault(v => v.Contains("id"));
            var nameKey = grouping.FirstOrDefault(v => v.Contains("name"));
            var typeKey = grouping.FirstOrDefault(v => v.Contains("type"));
            return new EditFileViewModel
            {
                Id = form.GetStringOrDefault(idKey),
                Name = form.GetStringOrDefault(nameKey),
                Type = form.GetStringOrDefault(typeKey)
            };
        }

        private static string GetGroupKey(string key)
        {
            var startIndex = key.IndexOf("[", StringComparison.Ordinal);
            var length = key.IndexOf("]", StringComparison.Ordinal) - startIndex + 1;
            return key.Substring(startIndex, length);
        }

        private EditFileViewModel GetFileViewModel(MultipartFileData fileData)
        {
            return new EditFileViewModel
            {
                Name = fileData.Headers.ContentDisposition.FileName,
                Type = fileData.Headers.ContentType.MediaType,
                Bytes = System.IO.File.ReadAllBytes(fileData.LocalFileName)
            };
        }
    }
}
