using CodeCapital.Bullhorn.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeCapital.Bullhorn.Api
{
    public class CorporationDepartmentApi
    {
        private readonly BullhornApi _bullhornApi;
        public static readonly string DefaultFields = "id,dateAdded,enabled,name";

        public CorporationDepartmentApi(BullhornApi bullhornApi) => _bullhornApi = bullhornApi;

        /// <summary>
        /// Returns all departments
        /// </summary>
        /// <returns></returns>
        public async Task<List<CorporationDepartmentDto>> GetAsync(string? fields = null)
        {
            var query = $"CorporationDepartment?fields={fields ?? DefaultFields}&where=id>0";

            return await _bullhornApi.QueryAsync<CorporationDepartmentDto>(query) ?? new List<CorporationDepartmentDto>();
        }
    }
}
