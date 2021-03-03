using CodeCapital.Bullhorn.Dtos;
using CodeCapital.Bullhorn.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeCapital.Bullhorn.Api
{
    public class ClientContactApi
    {
        private readonly BullhornApi _bullhornApi;
        public readonly string DefaultFields = "id,clientCorporation,isDeleted,firstName,lastName,email,dateAdded,dateLastModified,owner";

        public ClientContactApi(BullhornApi bullhornApi) => _bullhornApi = bullhornApi;

        public async Task<ClientContactDto> GetAsync(int id, string? fields = null)
        {
            var query = $"ClientContact/{id}?fields={fields ?? DefaultFields}";

            return await _bullhornApi.EntityAsync<ClientContactDto>(query);

            //var response = await _bullhornApi.ApiGetAsync(query);

            //var entityResponse = JsonConvert.DeserializeObject<EntityResponse<ClientContactDto>>(await response.Content.ReadAsStringAsync(),
            //    new JsonSerializerSettings
            //    {
            //        MissingMemberHandling = MissingMemberHandling.Ignore,
            //        NullValueHandling = NullValueHandling.Ignore
            //    });
            //return entityResponse.Data;
        }

        public async Task<List<ClientContactDto>> GetAsync(List<int> ids, string? fields = null)
        {
            if (ids.Count == 1)
            {
                return new List<ClientContactDto> { await GetAsync(ids[0], fields ?? DefaultFields) };
            }

            var query = $"ClientContact/{string.Join(",", ids)}?fields={fields ?? DefaultFields}";

            return await _bullhornApi.EntityAsync<List<ClientContactDto>>(query);

            //var response = await _bullhornApi.ApiGetAsync(query);

            //var entityResponse = JsonConvert.DeserializeObject<EntityResponse<List<ClientContactDto>>>(await response.Content.ReadAsStringAsync(),
            //    new JsonSerializerSettings
            //    {
            //        MissingMemberHandling = MissingMemberHandling.Ignore,
            //        NullValueHandling = NullValueHandling.Ignore
            //    });

            //return entityResponse.Data;
        }

        public async Task<List<ClientContactDto>> GetAsync(DateTime from, DateTime to)
        {
            var query = $"ClientContact?fields={DefaultFields}&query=dateAdded:[{from:yyyyMMdd} TO {to:yyyyMMdd}]";

            return await _bullhornApi.SearchAsync<ClientContactDto>(query);
        }

        public async Task<List<ClientContactDto>> GetNewClientContactsAsync(DateTime dateTime)
        {
            var query = $"ClientContact?fields=id,firstName,lastName,email&query=dateAdded:[{dateTime:yyyyMMdd} TO *]";

            return await _bullhornApi.SearchAsync<ClientContactDto>(query);
        }

        public async Task<List<ClientContactDto>> GetNewAndUpdatedFromAsync(DateTime dateTime)
        {
            var query = $"ClientContact?fields={DefaultFields}&query=dateAdded:[{dateTime:yyyyMMdd} TO *] OR dateLastModified:[{dateTime:yyyyMMddHHmmss} TO *]";

            return await _bullhornApi.SearchAsync<ClientContactDto>(query);
        }

        public async Task<List<ClientContactDto>> GetUpdatedClientContactsAsync(DateTime dateTime)
        {
            var query = $"ClientContact?fields=id&query=dateLastModified:[{dateTime:yyyyMMdd} TO *]";

            return await _bullhornApi.SearchAsync<ClientContactDto>(query);
        }

        //public async Task<ClientContactDto> FindClientContactIdByEmail2Async(string emailQuery)
        //{
        //    var query = $"search/ClientContact?fields=id,firstName,lastName&query={emailQuery} AND isDeleted:0";

        //    var response = await _bullhornApi.ApiGetAsync(query);

        //    var searchResponse = JsonConvert.DeserializeObject<SearchResponse>(
        //        await response.Content.ReadAsStringAsync());

        //    var clientContact = searchResponse?.Data?.FirstOrDefault()?.ToObject<ClientContactDto>();

        //    return clientContact;
        //}

        public async Task<ClientContactDto?> FindClientContactIdByEmailAsync(string email)
        {
            var query = $"search/ClientContact?fields=id,firstName,lastName&query=email:({email}) AND isDeleted:0";

            var response = await _bullhornApi.ApiGetAsync(query);

            //var searchResponse = JsonConvert.DeserializeObject<SearchResponse>(
            //    await response.Content.ReadAsStringAsync());

            var searchResponse = await response.DeserializeAsync<SearchResponse>();

            return searchResponse?.Data?.FirstOrDefault()?.ToObject<ClientContactDto>();
        }

        public async Task<List<ClientContactDto>> FindClientContactIdByEmailAsync(List<string> emails)
        {
            var query = $"ClientContact?fields=id,firstName,lastName,email&query=email:({_bullhornApi.GetQuotedString(emails)}) AND isDeleted:0";

            return await _bullhornApi.SearchAsync<ClientContactDto>(query);
        }
    }
}
