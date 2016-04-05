using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Sogeti.Academy.Persistence.Storage
{
    public interface IBlobStorage
    {
        Task<byte[]> GetBlob(string id);
        Task UploadBlob(string id, byte[] bytes);
        Task DeleteBlob(string id);
    }

    public class BlobStorage : IBlobStorage
    {
        private readonly string _connectionString;
        private readonly string _container;

        public BlobStorage(string connectionString, string container)
        {
            _connectionString = connectionString;
            _container = container;
        }

        public async Task<byte[]> GetBlob(string id)
        {
            var blob = await GetBlobForId(id);
            using (var stream = await blob.OpenReadAsync())
            {
                var buffer = new byte[stream.Length];
                await stream.ReadAsync(buffer, 0, (int) stream.Length);
                return buffer;
            }
        }

        public async Task UploadBlob(string id, byte[] bytes)
        {
            var blob = await GetBlobForId(id);
            await blob.UploadFromByteArrayAsync(bytes, 0, bytes.Length);
        }

        public async Task DeleteBlob(string id)
        {
            var blob = await GetBlobForId(id);
            await blob.DeleteAsync();
        }

        private async Task<CloudBlockBlob> GetBlobForId(string id)
        {
            var account = CloudStorageAccount.Parse(_connectionString);
            var client = account.CreateCloudBlobClient();

            var container = client.GetContainerReference(_container);
            await container.CreateIfNotExistsAsync();

            var blob = container.GetBlockBlobReference(id);
            return blob;
        }
    }
}
