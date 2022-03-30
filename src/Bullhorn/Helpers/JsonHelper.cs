using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace CodeCapital.Bullhorn.Helpers
{
    //https://stackoverflow.com/questions/58512542/read-a-json-file-and-generate-string-keys-with-values-in-a-dictionary-like-objec
    //https://stackoverflow.com/questions/7394551/c-sharp-flattening-json-structure
    public static class JsonHelper
    {
        private static readonly string[] FlatEntities = { "candidate", "candidateReference", "personReference", "clientContact", "clientContactReference", "clientCorporation", "jobOrder", "placement", "jobSubmission", "owner", "user", "source", "editHistory" };
        private static readonly string[] DateTimeFields = { "dateAdded", "dateAvailable", "dateBegin", "dateClosed", "dateEnd", "dateLastModified", "dateLastComment", "dateLastVisit", "customDate1", "customDate2", "customDate3", "userDateAdded", "dateLastPublished" };

        public static async Task<T?> DeserializeAsync<T>(this HttpResponseMessage response, ILogger? logger = null)
        {
            var options = new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                IgnoreNullValues = true,
                PropertyNameCaseInsensitive = true
            };

            await using var stream = await response.Content.ReadAsStreamAsync();

            try
            {
                return await JsonSerializer.DeserializeAsync<T>(stream, options);
            }
            catch (Exception e)
            {
                logger?.LogError(e, "Deserialize Error at {uri}", response.RequestMessage?.RequestUri);
                throw;
            }
        }
    }
}