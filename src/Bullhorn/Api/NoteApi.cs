using CodeCapital.Bullhorn.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CodeCapital.Bullhorn.Api
{
    public class NoteApi
    {
        private readonly BullhornApi _bullhornApi;

        private const string AllFields = "id,action,commentingPerson,dateAdded,dateLastModified,isDeleted,comments,minutesSpent,personReference";

        public NoteApi(BullhornApi bullhornApi) => _bullhornApi = bullhornApi;

        //ToDo Note that this might not work because camelCase needs to be setup in Newtonsoft settings
        public async Task AddAsync(NoteDto noteDto) => await _bullhornApi.ApiPutAsync("entity/Note", new StringContent(JsonConvert.SerializeObject(noteDto), Encoding.UTF8, "application/json"));

        public async Task<List<NoteDto>> GetFromAsync(DateTime dateTime)
        {
            var query = $"Note?fields={AllFields}&query=dateAdded:[{dateTime:yyyyMMddHHmmss} TO *]";

            return await _bullhornApi.SearchAsync<NoteDto>(query);
        }

        public async Task<List<NoteDto>> GetNewAndUpdatedFromAsync(DateTime dateTime)
        {
            var query = $"Note?fields={AllFields}&query=dateAdded:[{dateTime:yyyyMMddHHmmss} TO *] OR dateLastModified:[{dateTime:yyyyMMddHHmmss} TO *]";

            return await _bullhornApi.SearchAsync<NoteDto>(query) ?? new List<NoteDto>();
        }

        public async Task<List<NoteDto>> GetFromToAsync(DateTime dateTimeFrom, DateTime dateTimeTo)
        {
            var query = $"Note?fields={AllFields}&query=dateAdded:[{dateTimeFrom:yyyyMMddHHmmss} TO {dateTimeTo:yyyyMMddHHmmss}]";

            return await _bullhornApi.SearchAsync<NoteDto>(query);
        }

        public async Task<List<NoteDto>> GetNotesAsync(string userQuery, string fields = AllFields)
        {
            var query = $"Note?fields={fields}&query={userQuery}&sort=noteID"; // case must be noteID

            return await _bullhornApi.SearchAsync<NoteDto>(query);
        }
    }
}
