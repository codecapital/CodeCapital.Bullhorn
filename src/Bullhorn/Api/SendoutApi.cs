using CodeCapital.Bullhorn.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeCapital.Bullhorn.Api
{
    public class SendoutApi
    {
        private readonly BullhornApi _bullhornApi;

        public SendoutApi(BullhornApi bullhornApi) => _bullhornApi = bullhornApi;

        /// <summary>
        /// Get all from a specific timestamp
        /// </summary>
        /// <param name="timestampFrom"></param>
        /// <returns></returns>
        public async Task<List<SendoutDto>> GetAsync(long timestampFrom)
        {
            var query = $"Sendout?fields=id,candidate,user,dateAdded,jobOrder,clientContact,clientCorporation&where=dateAdded>={timestampFrom}";

            return await _bullhornApi.QueryAsync<SendoutDto>(query);
        }
    }
}
