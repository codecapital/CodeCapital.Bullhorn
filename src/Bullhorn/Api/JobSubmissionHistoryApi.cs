using CodeCapital.Bullhorn.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeCapital.Bullhorn.Api
{
    public class JobSubmissionHistoryApi
    {
        private readonly BullhornApi _bullhornApi;
        public static readonly string DefaultFields = "id,dateAdded,status,modifyingUser,jobSubmission";

        public JobSubmissionHistoryApi(BullhornApi bullhornApi) => _bullhornApi = bullhornApi;

        public Task<List<JobSubmissionHistoryDto>> GetByCandidateIdAsync(int id)
        {
            var query = $"JobSubmissionHistory?fields=id,dateAdded,status,jobSubmission(id,status,jobOrder(id))&where=jobSubmission.candidate.id={id}";

            return _bullhornApi.QueryAsync<JobSubmissionHistoryDto>(query);
        }

        public Task<List<JobSubmissionHistoryDto>> GetNewAndUpdatedFromAsync(long timestampFrom)
        {
            var query = $"JobSubmissionHistory?fields={DefaultFields}&where=dateAdded>{timestampFrom}";

            return _bullhornApi.QueryAsync<JobSubmissionHistoryDto>(query);
        }
    }
}
