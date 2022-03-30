using CodeCapital.Bullhorn.Dtos;

namespace CodeCapital.Bullhorn.Api
{
    public class PlacementApi
    {
        private readonly BullhornApi _bullhornApi;

        public PlacementApi(BullhornApi bullhornApi) => _bullhornApi = bullhornApi;

        public async Task<List<PlacementDto>> GetFromAsync(long timestampFrom)
        {
            var query = $"Placement?fields=id,billingClientContact,candidate,dateAdded,dateLastModified,dateBegin,dateEnd,employeeType,employmentType,fee,flatFee,jobOrder,payRate,correlatedCustomText1,salary,salaryUnit,status&where=dateAdded>{timestampFrom}";

            return await _bullhornApi.QueryAsync<PlacementDto>(query);
        }

        public async Task<List<PlacementDto>> GetNewAndUpdatedFromAsync(long timestampFrom)
        {
            var query = $"Placement?fields=id,billingClientContact,candidate,dateAdded,dateLastModified,dateBegin,dateEnd,employeeType,employmentType,fee,flatFee,jobOrder,payRate,correlatedCustomText1,salary,salaryUnit,status&where=dateAdded>{timestampFrom} OR dateLastModified>{timestampFrom}";

            return await _bullhornApi.QueryAsync<PlacementDto>(query);
        }
    }
}
