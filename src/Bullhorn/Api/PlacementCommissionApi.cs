using CodeCapital.Bullhorn.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeCapital.Bullhorn.Api
{
    public class PlacementCommissionApi
    {
        private readonly BullhornApi _bullhornApi;

        public PlacementCommissionApi(BullhornApi bullhornApi) => _bullhornApi = bullhornApi;

        public async Task<List<PlacementCommissionDto>> GetFromAsync(long timestampFrom)
        {
            var query = $"PlacementCommission?fields=id,commissionPercentage,dateAdded,dateLastModified,placement(id),user(id),status,role&where=dateAdded>{timestampFrom}";

            return await _bullhornApi.QueryAsync<PlacementCommissionDto>(query);
        }

        public async Task<List<PlacementCommissionDto>> GetNewAndUpdatedFromAsync(long timestampFrom)
        {
            var query = $"PlacementCommission?fields=id,commissionPercentage,dateAdded,dateLastModified,placement(id),user(id),status,role&where=dateAdded>{timestampFrom} OR dateLastModified>{timestampFrom}";

            return await _bullhornApi.QueryAsync<PlacementCommissionDto>(query);
        }
    }
}
