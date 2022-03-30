using CodeCapital.Bullhorn.Api;
using CodeCapital.Bullhorn.Dtos;

namespace CodeCapital.Bullhorn.Services
{
    /// <summary>
    /// Register this as a Singleton in DI if you want to maintain the connection
    /// </summary>
    public class BullhornService
    {
        public BullhornApi BullhornApi { get; }

        public BullhornService(BullhornApi bullhornApi) => BullhornApi = bullhornApi;

        /// <summary>
        /// Checks Bullhorn data connection is working. Get latest Candidates added in last X hours.
        /// </summary>
        /// <param name="hours"></param>
        /// <returns></returns>
        public async Task<List<CandidateDto>> BullhornCheck(int hours = 2)
        {
            await BullhornApi.CheckConnectionAsync();

            var candidateApi = new CandidateApi(BullhornApi);

            var newCandidates = await candidateApi.GetNewFromAsync(DateTime.Now.AddHours(-hours));

            return newCandidates;
        }

        public Task CheckConnectionAsync() => BullhornApi.CheckConnectionAsync();
    }
}
