using CodeCapital.Bullhorn.Dtos;
using CodeCapital.Bullhorn.Helpers;
using CodeCapital.System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CodeCapital.Bullhorn.Api
{
    // Refactor according this 11th Minute https://channel9.msdn.com/Shows/XamarinShow/Azure-Active-Directory-B2C-Authentication-For-Mobile-with-Matthew-Soucoup
    // Call it Bullhorn.Identity, AcquireTokenAsync
    public class BullhornApi
    {
        public const int QueryCount = 500; // 500 max in BullhornApiJsonSerializerSettings

        private readonly HttpClient _httpClient;
        private readonly ApiSession _session;
        private readonly ILogger<BullhornApi> _logger;
        private readonly TimeSpan _defaultTimeout = TimeSpan.FromMinutes(5);
        private readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            AllowTrailingCommas = true,
            IgnoreNullValues = true,
            PropertyNameCaseInsensitive = true
        };

        private BullhornSettings _bullhornSettings;
        private int _apiCallCounter;

        public BullhornApi(ILogger<BullhornApi> logger, HttpClient httpClient, IOptions<BullhornSettings> settings)
        {

            httpClient.Timeout = _defaultTimeout;
            _logger = logger;
            _httpClient = httpClient;
            _bullhornSettings = settings.Value;
            _session = new ApiSession(httpClient, _bullhornSettings, logger);
        }

        public void SetAuthorizationMeta(BullhornSettings bullhornSettings) => _bullhornSettings = bullhornSettings;

        public async Task CheckConnectionAsync()
        {
            if (_bullhornSettings == null)
            {
                _logger.LogError("Make sure you have got BullhornSettings in your appsettings.json.");

                throw new NullReferenceException($"{nameof(BullhornSettings)}, Set the {nameof(BullhornSettings)} parameter before connecting!");
            }

            if (_session.LoginResponse != null) return;

            //BullhornApi.SetAuthorizationMeta(_bullhornSettings);

            await _session.ConnectAsync();
        }

        //public async Task ConnectAsync(int callsRefresh = 0)
        //{
        //    if (_bullhornSettings == null) throw new NullReferenceException($"{nameof(BullhornSettings)}, Set the {nameof(BullhornSettings)} parameter before connecting!");

        //    _logger.LogInformation("Connecting to Bullhorn");

        //    _authorization = new Authorization(_bullhornSettings, _httpClient, _logger);
        //    await _authorization.AuthorizeAsync();

        //    UpdateBhRestTokenHeader();
        //    Authorized = true;

        //    _logger.LogInformation("Connected to Bullhorn");
        //}

        //public async Task<DynamicQueryResponse> ApiQueryToDynamicAsync(string query, int count, int start = 0)
        //{
        //    query = $"query/{query}&start={start}&count={count}&showTotalMatched=true&usev2=true";

        //    var response = await ApiGetAsync(query);

        //    ApiCallCounter();

        //    return await response.Content.ReadAsAsync<DynamicQueryResponse>();
        //}

        public async Task<DynamicEntityResponse> GetEntityAsync(string query)
        {
            query = $"{query}&showTotalMatched=true&usev2=true";

            var apiResponse = await ApiGetAsync(query);

            var jsonString = await apiResponse.Content.ReadAsStringAsync();

            var response = JsonSerializer.Deserialize<DynamicEntityResponse>(jsonString);

            response.Json = jsonString;
            response.RequestUri = apiResponse.RequestMessage.RequestUri.ToString();

            if (string.IsNullOrWhiteSpace(response.ErrorMessage))
            {
                var flattener = new JsonFlattener();

                response.DynamicData = flattener.Flatten(jsonString);
            }

            return response;
        }

        //ToDo Test this
        public async Task<DynamicQueryResponse> ApiCallToDynamicAsync(string query, int count, int start = 0)
        {
            query = $"{query}&start={start}&count={count}&showTotalMatched=true&usev2=true";

            var apiResponse = await ApiGetAsync(query);

            var jsonString = await apiResponse.Content.ReadAsStringAsync();

            var response = JsonSerializer.Deserialize<DynamicQueryResponse>(jsonString);

            response.Json = jsonString;
            response.RequestUri = apiResponse.RequestMessage.RequestUri.ToString();

            if (string.IsNullOrWhiteSpace(response.ErrorMessage))
            {
                var flattener = new JsonFlattener();

                response.DynamicData = flattener.Flatten(jsonString);
            }

            return response;
        }

        //[Obsolete("Investigate if this should be removed", true)]
        //public async Task<QueryResponse> ApiQueryAsync(string query, int count, int start = 0)
        //{
        //    query = $"query/{query}&start={start}&count={count}&showTotalMatched=true&usev2=true";

        //    var response = await ApiGetAsync(query);

        //    return await DeserializeAsync<QueryResponse>(response);
        //}

        public async Task<QueryResponse<T>> ApiQueryAsync<T>(string query, int count, int start = 0)
        {
            query = $"query/{query}&start={start}&count={count}&showTotalMatched=true&usev2=true";

            var response = await ApiGetAsync(query);

            return await DeserializeAsync<QueryResponse<T>>(response);
        }

        //ToDo 
        //public async Task<SearchResponse<JObject>> ApiSearchAsync(string query, int count, int start = 0) => await ApiSearchAsync<JObject>(query, count, start);

        public async Task<SearchResponse<T>> ApiSearchAsync<T>(string query, int count, int start = 0)
        {
            query = $"search/{query}&start={start}&count={count}&showTotalMatched=true&usev2=true";

            var response = await ApiGetAsync(query);

            return await DeserializeAsync<SearchResponse<T>>(response);
        }

        /// <summary>
        /// Gives only total, only for search/queries
        /// </summary>
        /// <param name="query"></param>
        /// <returns>total number</returns>
        //public async Task<int> TotalAsync(string query)
        //{
        //    var response = await ApiSearchAsync(query, 1);

        //    return response.Total;
        //}

        public async Task<HttpResponseMessage> ApiGetAsync(string query)
        {
            await PingCheckAsync();

            var restUrl = $"{_session.LoginResponse!.RestUrl}{query}";

            _logger.LogDebug($"Request: {restUrl}");

            return await _httpClient.GetAsync(restUrl);
        }

        // This might be wrapped to ApiCreateEntity
        public async Task<HttpResponseMessage> ApiPutAsync(string query, HttpContent content)
        {
            await PingCheckAsync();

            var restUrl = $"{_session.LoginResponse!.RestUrl}{query}";

            return await _httpClient.PutAsync(restUrl, content);
        }

        public async Task<HttpResponseMessage> ApiPostAsync(string query, HttpContent content)
        {
            await PingCheckAsync();

            var restUrl = $"{_session.LoginResponse!.RestUrl}{query}";

            try
            {
                return await _httpClient.PostAsync(restUrl, content);
            }
            catch (Exception e)
            {
                _logger.LogError("BullhornAPI_ApiPostAsync", e);
            }

            return new HttpResponseMessage();
        }

        public async Task<HttpResponseMessage> ApiDeleteAsync(string query)
        {
            await PingCheckAsync();

            var restUrl = $"{_session.LoginResponse!.RestUrl}{query}";

            return await _httpClient.DeleteAsync(restUrl);
        }

        public async Task UpdateAsync<T>(int id, string entityName, T updateDto) => await ApiPostAsync($"entity/{entityName}/{id}?",
                new StringContent(JsonSerializer.Serialize<T>(updateDto, new JsonSerializerOptions
                {
                    AllowTrailingCommas = true,
                    IgnoreNullValues = true,
                    //ContractResolver = new EmptyStringResolver()
                    DefaultIgnoreCondition = JsonIgnoreCondition.Always
                }), Encoding.UTF8, "application/json"));

        public async Task MassUpdateAsync<T>(string entityName, T updateDto) => await ApiPostAsync($"massUpdate/{entityName}?",
                new StringContent(JsonSerializer.Serialize<T>(updateDto, new JsonSerializerOptions
                {
                    AllowTrailingCommas = true,
                    IgnoreNullValues = true,
                    //ContractResolver = new EmptyStringResolver()
                    DefaultIgnoreCondition = JsonIgnoreCondition.Always
                }), Encoding.UTF8, "application/json"));

        public async Task DeleteAsync(int id, string entityName) => await ApiDeleteAsync($"entity/{entityName}/{id}?");

        //ToDo 
        //public List<T> MapResults<T>(IEnumerable<JObject> data)
        //{
        //    var objects = data.Select(s => s.ToObject<T>()).ToList();

        //    return objects;
        //}

        public string GetQuotedString(IEnumerable<string> list) => string.Join(" OR ", list.Select(s => $"\"{s}\""));

        //[Obsolete("Use JobOrderApi instead", true)]
        //public async Task<List<JobOrderDto>> GetJobOrdersAsync(long timestampFrom)
        //{
        //    var query = $"JobOrder?fields=id,dateAdded,status,title,source,owner,isOpen,isDeleted,clientContact,clientCorporation&where=dateAdded>={timestampFrom}";

        //    var data = await QueryAsync<JobOrderDto>(query);

        //    return data;
        //}

        public Task<T?> DeserializeAsync<T>(HttpResponseMessage response)
            => response.DeserializeAsync<T>(_logger);

        public async Task<T> EntityAsync<T>(string query)
        {
            query = $"entity/{query}";

            var response = await ApiGetAsync(query);

            var entityResponse = await DeserializeAsync<EntityResponse<T>>(response);

            return entityResponse.Data;
        }

        public async Task<List<T>> QueryAsync<T>(string query)
        {
            var result = await ApiQueryAsync<T>(query, QueryCount);
            var data = result.Data;

            for (var i = result.Count; i < result.Total; i += result.Count)
            {
                result = await ApiQueryAsync<T>(query, QueryCount, i);
                data.AddRange(result.Data);
            }

            return data ?? new List<T>();
        }

        public async Task<List<T>> SearchAsync<T>(string query, int count = QueryCount, int total = 0)
        {
            var result = await ApiSearchAsync<T>(query, count);
            var data = result.Data;

            if (total != 0 && result.Count >= total) return data;

            for (var i = result.Count; i < result.Total; i += result.Count)
            {
                result = await ApiSearchAsync<T>(query, count, start: i);
                data.AddRange(result.Data);

                if (total != 0 && total <= i) break;
            }

            return data ?? new List<T>();
        }

        public void LogWarning(string text) => _logger.LogWarning(text);

        private async Task PingCheckAsync()
        {
            _logger.LogDebug("Next token refresh at {0}", _session.Ping.SessionExpiryDate);

            if (!_session.IsValid)
            {
                _logger.LogError("Not logged in yet.");
                throw new AuthenticationException("Not logged in yet.");
            }

            if (_session.Ping?.Valid ?? false) return;

            _apiCallCounter++;

            using var response = await _httpClient.GetAsync($"{_session.LoginResponse!.RestUrl}/ping");

            _session.Ping = await DeserializeAsync<PingDto>(response);

            _logger.LogDebug("Next token refresh at {0}", _session.Ping.SessionExpiryDate);

            if (_session.Ping.Valid) return;

            _logger.LogInformation($"Token refresh on {_apiCallCounter} API call.");

            await _session.RefreshTokenAsync();
        }

        //[Obsolete("Investigate if this should be removed", true)]
        //private static void LogList(List<JObject> list)
        //{
        //    foreach (var item in list)
        //    {
        //        Logger(JsonConvert.SerializeObject(item) + "\n");
        //    }
        //}

        //public string GetBullhornRestToken() => _authorization.LoginResponse?.BhRestToken ?? string.Empty;

        //[Obsolete("Investigate if this should be removed", true)]
        //private static void Logger(string content) => File.AppendAllText("temp-logfile.txt", content);

        //private string GetToken() => $"BhRestToken={_authorization.LoginResponse.BhRestToken}";

        //private void UpdateBhRestTokenHeader()
        //{
        //    _httpClient.DefaultRequestHeaders.Remove("BhRestToken");
        //    _httpClient.DefaultRequestHeaders.Add("BhRestToken", _authorization.LoginResponse.BhRestToken);
        //}
    }
}


// Other query examples
//search/Note?fields=id,dateAdded,action,commentingPerson&query=dateAdded:[20210101000000 TO *] AND action:'Phone Call'&sort=-dateAdded
//[ContestType.GdprWithDrawn] = "Candidate?fields=id&query=notes.id:\"^^action:(\\\"gdpr withdrawn\\\") AND isDeleted:false\""
//            var query = "Candidate?fields=id,status,firstName,dateAdded,owner,email,email2,email3,phone,phone2,phone3,mobile,workPhone,placements[0](id),sendouts(id,dateAdded),fileAttachments(id)&query=isDeleted:0 AND -status:\"Archive\" AND -email:[\"\" TO *] AND -email2:[\"\" TO *] AND -email3:[\"\" TO *] AND -mobile:[\"\" TO *] AND -phone:[\"\" TO *] AND -placements.id:[0 TO 99999999999] AND -interviews.id:[0 TO 99999999999] AND -fileAttachments.id:[0 TO 99999999999] AND -notes.id:\"^^action:(\\\"Parse Failed Remove\\\") AND isDeleted:false\" AND -notes.id:\"^^action:(\\\"Parse Failed Keep\\\") AND isDeleted:false\" &sort=-dateAdded";
//            var query = "Candidate?fields=id,status,email,email2,email3,mobile,phone,notes(id,action)&query=isDeleted:0 AND notes.id:\"^^action:(\\\"No Files Remove\\\") AND isDeleted:false\" AND (email:[\"\" TO *] OR email2:[\"\" TO *] OR email3:[\"\" TO *] OR mobile:[\"\" TO *]  OR phone:[\"\" TO *] ) AND -phone:\"44 01702460010\"