using CodeCapital.Bullhorn.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeCapital.Bullhorn.Api
{
    public class CorporationUserApi
    {
        private readonly BullhornApi _bullhornApi;

        public CorporationUserApi(BullhornApi bullhornApi) => _bullhornApi = bullhornApi;

        /// <summary>
        /// Returns all users
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserDto>> GetAsync()
        {
            const string query = "CorporateUser?fields=id,firstName,lastName,name,isDeleted,departments&where=id>0";

            return await _bullhornApi.QueryAsync<UserDto>(query);
        }
    }
}
