using CodeCapital.Bullhorn.Dtos;
using CodeCapital.Bullhorn.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeCapital.Bullhorn.Api
{
    public class CandidateWorkHistoryApi
    {
        private readonly BullhornApi _bullhornApi;
        public static readonly string DefaultFields = "id,dateAdded,isDeleted,candidate(id)";

        public CandidateWorkHistoryApi(BullhornApi bullhornApi) => _bullhornApi = bullhornApi;

        public async Task<List<CandidateWorkHistoryDto>> GetDeletedAsync(DateTime dateAddedFrom, string? fields = null)
        {
            var query = $"CandidateWorkHistory?fields={fields ?? DefaultFields}&where=isDeleted=true AND dateAdded>{dateAddedFrom.Timestamp()}";

            return await _bullhornApi.QueryAsync<CandidateWorkHistoryDto>(query);
        }
    }
}
