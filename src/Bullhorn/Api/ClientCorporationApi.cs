using CodeCapital.Bullhorn.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeCapital.Bullhorn.Api
{
    public class ClientCorporationApi
    {
        private readonly BullhornApi _bullhornApi;

        public ClientCorporationApi(BullhornApi bullhornApi) => _bullhornApi = bullhornApi;

        public async Task<List<ClientCorporationDto>> GetNewClientCorporationsAsync(long timestampFrom)
        {
            var query = $"ClientCorporation?fields=id,name&where=dateAdded>{timestampFrom}";

            return await _bullhornApi.QueryAsync<ClientCorporationDto>(query);
        }

        public async Task<List<ClientCorporationDto>> GetUpdatedClientCorporationsAsync(long timestampFrom)
        {
            var query = $"ClientCorporation?fields=id&where=dateLastModified>{timestampFrom}";

            return await _bullhornApi.QueryAsync<ClientCorporationDto>(query);
        }
    }
}
