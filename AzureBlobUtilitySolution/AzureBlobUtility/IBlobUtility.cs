namespace AzureBlobUtility
{
    public interface IBlobUtility
    {
        Task<string> GetContent(string blobName, string container);
        Task<bool> SaveContent(string blobName, string container, string content);
        Task<List<string>> GetFileNames(string container, string path);
    }
}