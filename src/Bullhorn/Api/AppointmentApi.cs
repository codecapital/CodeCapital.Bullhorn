using CodeCapital.Bullhorn.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeCapital.Bullhorn.Api
{
    public class AppointmentApi
    {
        private readonly BullhornApi _bullhornApi;

        public AppointmentApi(BullhornApi bullhornApi) => _bullhornApi = bullhornApi;
        public readonly string DefaultFields = "id,candidateReference,clientContactReference,dateAdded,dateBegin,dateLastModified,type,isDeleted,jobOrder,owner";

        public async Task<List<AppointmentDto>> GetAsync(long timestampFrom)
        {
            var query = $"Appointment?fields={DefaultFields}&where=dateAdded>{timestampFrom} AND candidateReference IS NOT NULL";

            return await _bullhornApi.QueryAsync<AppointmentDto>(query);
        }

        public async Task<List<AppointmentDto>> GetAsync(long timestampFrom, long timestampTo)
        {
            var query = $"Appointment?fields={DefaultFields}&where=dateAdded>{timestampFrom} AND dateAdded<{timestampTo} AND candidateReference IS NOT NULL";

            return await _bullhornApi.QueryAsync<AppointmentDto>(query);
        }

        public async Task<List<AppointmentDto>> GetNewAndUpdatedFromAsync(long timestampFrom)
        {
            var query = $"Appointment?fields={DefaultFields}&where=(dateAdded>{timestampFrom} OR dateLastModified>{timestampFrom}) AND candidateReference IS NOT NULL";

            return await _bullhornApi.QueryAsync<AppointmentDto>(query);
        }
    }
}
