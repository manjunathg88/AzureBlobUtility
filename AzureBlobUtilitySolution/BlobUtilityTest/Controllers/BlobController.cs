using AzureBlobUtility;
using Microsoft.AspNetCore.Mvc;

namespace TestBlobUtilityAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlobController : ControllerBase
    {
        private readonly IBlobUtility _blobUtility;
        private readonly ILogger<BlobController> _logger;

        public BlobController(ILogger<BlobController> logger, IBlobUtility blobUtility)
        {
            _logger = logger;
            _blobUtility = blobUtility;
        }

        [Route("GetFileNames"), HttpGet]
        public async Task<IList<string>> GetFileNames([FromQuery] string path, [FromQuery] string containerName)
        {
            return await _blobUtility.GetFileNames(path.Trim('/'), containerName);
        }

        [Route("GetContent"), HttpGet]
        public async Task<string> GetContent([FromQuery] string blobName, [FromQuery] string containerName)
        {
            return await _blobUtility.GetContent(blobName, containerName);
        }

        [Route("SaveContent"), HttpPost]
        public async Task<bool> GetFileNames([FromBody] BlobUtilityTest.Model model)
        {
            return await _blobUtility.SaveContent(model.BlobName, model.Container, model.Content);
        }
    }
}