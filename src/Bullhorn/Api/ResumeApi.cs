using CodeCapital.Bullhorn.Dtos;
using CodeCapital.Bullhorn.Helpers;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CodeCapital.Bullhorn.Api
{
    public class ResumeApi
    {
        private readonly BullhornApi _bullhornApi;

        public ResumeApi(BullhornApi bullhornApi) => _bullhornApi = bullhornApi;

        public async Task<ResumeDto> ParseAsync(FileDto fileDto)
        {
            var query = "resume/parseToCandidate?format=text&populateDescription=html&";

            var content = new MultipartFormDataContent();
            content.Add(new ByteArrayContent(Convert.FromBase64String(fileDto.FileContent)), "resume", string.IsNullOrWhiteSpace(fileDto.Name) ? "temp-file-name" : fileDto.Name);

            var response = await _bullhornApi.ApiPostAsync(query, content);

            return await response.DeserializeAsync<ResumeDto>();
        }
    }
}
