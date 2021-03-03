using CodeCapital.Bullhorn.Dtos;
using CodeCapital.Bullhorn.Helpers;
using System.Threading.Tasks;

namespace CodeCapital.Bullhorn.Api
{
    public class FileApi
    {
        private readonly BullhornApi _bullhornApi;

        public FileApi(BullhornApi bullhornApi) => _bullhornApi = bullhornApi;

        public async Task<FileDto> GetFileAsync(string entityType, int entityId, int fileId)
        {
            var query = $"file/{entityType}/{entityId}/{fileId}?";

            var response = await _bullhornApi.ApiGetAsync(query);

            var fileResponse = await response.DeserializeAsync<FileResponse>();

            return fileResponse.File;
        }
    }
}
