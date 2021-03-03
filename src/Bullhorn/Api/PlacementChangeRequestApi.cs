using CodeCapital.Bullhorn.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeCapital.Bullhorn.Api
{
    public class PlacementChangeRequestApi
    {
        private readonly BullhornApi _bullhornApi;

        public PlacementChangeRequestApi(BullhornApi bullhornApi) => _bullhornApi = bullhornApi;

        public async Task<List<PlacementChangeRequestDto>> GetNewAndUpdatedFromAsync(long timestampFrom)
        {
            var query = $"PlacementChangeRequest?fields=id,dateAdded,dateLastModified,requestStatus,requestType,placement(id),customText12&where=dateAdded>{timestampFrom} OR dateLastModified>{timestampFrom}";

            return await _bullhornApi.QueryAsync<PlacementChangeRequestDto>(query);
        }
    }
}
