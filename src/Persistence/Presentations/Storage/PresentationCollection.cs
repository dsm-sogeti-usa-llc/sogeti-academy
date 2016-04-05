using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sogeti.Academy.Application.Presentations.Models;
using Sogeti.Academy.Persistence.Storage;

namespace Sogeti.Academy.Persistence.Presentations.Storage
{
    public class PresentationCollection : DocumentCollection<Presentation> 
    {
        private readonly IBlobStorage _blobStorage;
        
        public PresentationCollection(string endpointUrl, string authKey, IBlobStorage blobStorage)
            : base(endpointUrl, authKey)
        {
            _blobStorage = blobStorage;
        }

        public override async Task<Presentation> GetByIdAsync(string id)
        {
            var presentation = await base.GetByIdAsync(id);
            return await GetPresentationWithFileStreams(presentation);
        }

        public override async Task<IEnumerable<Presentation>> GetAllAsync()
        {
            var presentations = await base.GetAllAsync();
            var tasks = presentations.Select(GetPresentationWithFileStreams);
            var results = await Task.WhenAll(tasks);
            return results;
        }

        public override async Task<string> CreateAsync(Presentation item)
        {
            var id = await base.CreateAsync(item);
            await UploadFiles(item.Files);
            return id;
        }

        public override async Task UpdateAsync(Presentation item)
        {
            var existing = await GetByIdAsync(item.Id);
            await base.UpdateAsync(item);
            await UploadFiles(item.Files);
            await DeleteOldFiles(item.Files, existing.Files);
        }

        private async Task<Presentation> GetPresentationWithFileStreams(Presentation presentation)
        {
            return new Presentation
            {
                Id = presentation.Id,
                Description = presentation.Description,
                Topic = presentation.Topic,
                Files = await GetFilesWithStreams(presentation.Files)
            };
        }

        private async Task<List<File>> GetFilesWithStreams(IEnumerable<File> files)
        {
            var tasks = files.Select(GetFileWithStream);
            var results = await Task.WhenAll(tasks);
            return results.ToList();
        }

        private async Task<File> GetFileWithStream(File file)
        {
            return new File
            {
                Id = file.Id,
                Type = file.Type,
                Name = file.Name,
                Size = file.Size,
                Bytes = await _blobStorage.GetBlob(file.Id)
            };
        }

        private async Task UploadFiles(IEnumerable<File> files)
        {
            foreach (var file in files.Where(f => f.Bytes != null))
                await _blobStorage.UploadBlob(file.Id, file.Bytes);
        }

        private async Task DeleteOldFiles(List<File> newFiles, List<File> oldFiles)
        {
            foreach (var oldFile in oldFiles)
                if (newFiles.All(f => f.Id != oldFile.Id))
                    await _blobStorage.DeleteBlob(oldFile.Id);
        }
    }
}
