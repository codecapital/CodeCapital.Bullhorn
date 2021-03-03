using CodeCapital.Bullhorn.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeCapital.Bullhorn.Api
{
    public class JobSubmissionApi
    {
        private readonly BullhornApi _bullhornApi;
        public static readonly string DefaultFields = "id,dateAdded,dateLastModified,status,isDeleted,candidate,jobOrder,sendingUser";

        public JobSubmissionApi(BullhornApi bullhornApi) => _bullhornApi = bullhornApi;

        public Task<JobSubmissionDto> GetAsync(int id, string? fields = null)
            => _bullhornApi.EntityAsync<JobSubmissionDto>($"JobSubmission/{id}?fields={fields ?? DefaultFields}");

        public Task<List<JobSubmissionDto>> GetNewAndUpdatedFromAsync(long timestampFrom)
        {
            var query = $"JobSubmission?fields={DefaultFields}&where=dateAdded>{timestampFrom} OR dateLastModified>{timestampFrom}";

            return _bullhornApi.QueryAsync<JobSubmissionDto>(query);
        }

        public async Task<List<JobSubmissionDto>> GetAsync(List<int> ids, string? fields = null)
        {
            if (ids.Count == 1)
                return new List<JobSubmissionDto> { await GetAsync(ids[0], fields ?? DefaultFields) };

            var query = $"JobSubmission/{string.Join(",", ids)}?fields={fields ?? DefaultFields}";

            return await _bullhornApi.EntityAsync<List<JobSubmissionDto>>(query);
        }
    }
}