using Microsoft.WindowsAzure.Storage.Blob;

namespace AzureBlobUtility
{
    public class BlobUtility : IBlobUtility
    {
        private readonly CloudBlobClient _cloudBlobClient;

        public BlobUtility(CloudBlobClient cloudBlobClient)
        {
            _cloudBlobClient = cloudBlobClient;
        }

        public async Task<string> GetContent(string blobName, string container)
        {
            string content = string.Empty;
            try
            {
                var blobReference = GetContainerReference(blobName, container);

                if (blobReference != null)
                    content = await blobReference.DownloadTextAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return content;
        }

        public async Task<bool> SaveContent(string blobName, string container, string content)
        {
            try
            {
                var blobReference = GetContainerReference(blobName, container);

                await blobReference.DeleteIfExistsAsync();

                if (blobReference != null)
                    await blobReference.UploadTextAsync(content);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<string>> GetFileNames(string container, string path)
        {
            var fileList = new List<string>();

            try
            {
                CloudBlobContainer containerReference = _cloudBlobClient.GetContainerReference(container);
                BlobResultSegment resultSegment = await containerReference.ListBlobsSegmentedAsync(path, true, new BlobListingDetails(), null, null, null, null);
                IEnumerable<IListBlobItem> files = resultSegment.Results;

                foreach (var file in files)
                {
                    fileList.Add(string.Concat(path + "/" + file.Uri.Segments.Last()));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return fileList;
        }

        private CloudAppendBlob GetContainerReference(string blobName, string container)
        {
            var containerReference = _cloudBlobClient.GetContainerReference(container);
            return containerReference.GetAppendBlobReference(blobName);
        }
    }
}