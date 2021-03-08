using CodeCapital.Bullhorn.Dtos;
using CodeCapital.Bullhorn.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CodeCapital.Bullhorn.Api
{
    public class CandidateApi
    {
        private readonly BullhornApi _bullhornApi;
        public static readonly string DefaultFields = "id,status,isDeleted,firstName,lastName,email,dateAdded,dateLastModified,source,owner";

        public CandidateApi(BullhornApi bullhornApi) => _bullhornApi = bullhornApi;

        public async Task AddAsync(CandidateDto dto)
            => await _bullhornApi.ApiPutAsync("entity/Candidate", new StringContent(JsonSerializer.Serialize(dto, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }), Encoding.UTF8, "application/json"));

        public async Task<CandidateDto> GetAsync(int id, string? fields = null)
        {
            var query = $"Candidate/{id}?fields={fields ?? DefaultFields}";

            return await _bullhornApi.EntityAsync<CandidateDto>(query);
        }

        public async Task<List<CandidateDto>> GetAsync(List<int> ids, string? fields = null)
        {
            if (ids.Count == 1)
                return new List<CandidateDto> { await GetAsync(ids[0], fields ?? DefaultFields) };

            var query = $"Candidate/{string.Join(",", ids)}?fields={fields ?? DefaultFields}";

            return await _bullhornApi.EntityAsync<List<CandidateDto>>(query);
        }

        public async Task<int> GetFilesCount(int id)
        {
            var query = $"entity/Candidate/{id}/fileAttachments?fields=id";

            var response = await _bullhornApi.ApiGetAsync(query);

            //var entityResponse = JsonConvert.DeserializeObject<QueryResponse>(await response.Content.ReadAsStringAsync(),
            //    new JsonSerializerSettings
            //    {
            //        MissingMemberHandling = MissingMemberHandling.Ignore,
            //        NullValueHandling = NullValueHandling.Ignore
            //    });

            //return (await BullhornApi.DeserializeAsync<QueryResponse>(response)).Total;

            //if (response == null) return 0;

            return (await response.DeserializeAsync<QueryResponse>()).Total;
        }

        public async Task<List<CandidateDto>> GetNewFromAsync(DateTime dateTime)
        {
            var query = $"Candidate?fields=id,status,isDeleted,firstName,lastName,email,dateAdded,source,owner&query=dateAdded:[{dateTime:yyyyMMddHHmmss} TO *]";

            return await _bullhornApi.SearchAsync<CandidateDto>(query);
        }

        public async Task<List<CandidateDto>> GetNewAndUpdatedFromAsync(DateTime dateTime)
        {
            var query = $"Candidate?fields={DefaultFields}&query=dateAdded:[{dateTime:yyyyMMddHHmmss} TO *] OR dateLastModified:[{dateTime:yyyyMMddHHmmss} TO *]";

            return await _bullhornApi.SearchAsync<CandidateDto>(query);
        }

        public async Task<List<CandidateDto>> GetUpdatedCandidatesFromAsync(DateTime dateTime)
        {
            var query = $"Candidate?fields=id&query=dateLastModified:[{dateTime:yyyyMMdd} TO *]";

            return await _bullhornApi.SearchAsync<CandidateDto>(query);
        }

        public async Task<CandidateDto?> FindCandidateIdByEmailAsync(string email)
        {
            var query = $"search/Candidate?fields=id,firstName,lastName&query=email:\"{email}\" AND isDeleted:0";

            var response = await _bullhornApi.ApiGetAsync(query);

            //var searchResponse = JsonConvert.DeserializeObject<SearchResponse>(
            //    await response.Content.ReadAsStringAsync());

            var searchResponse = await response.DeserializeAsync<SearchResponse2<CandidateDto>>();

            //https://stackoverflow.com/questions/58138793/system-text-json-jsonelement-toobject-workaround
            return searchResponse?.Data?.FirstOrDefault();
        }

        public async Task<List<CandidateDto>> FindCandidateIdByEmailAsync(List<string> emails)
        {
            var query = $"Candidate?fields=id,firstName,lastName,email&query=email:({_bullhornApi.GetQuotedString(emails)}) AND isDeleted:0";

            return await _bullhornApi.SearchAsync<CandidateDto>(query);

            //var result = await _bullhornApi.ApiSearchAsync(query, BullhornApi.QueryCount);

            //return _bullhornApi.MapResults<CandidateDto>(result.Data);
        }
    }
}
