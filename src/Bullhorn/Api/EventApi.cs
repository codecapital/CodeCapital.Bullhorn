using CodeCapital.Bullhorn.Dtos;
using CodeCapital.Bullhorn.Helpers;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CodeCapital.Bullhorn.Api
{
    public class EventApi
    {
        private readonly BullhornApi _bullhornApi;

        public EventApi(BullhornApi bullhornApi) => _bullhornApi = bullhornApi;


        /// <summary>
        /// Lets you subscribe to Bullhorn event types
        /// </summary>
        /// <param name="subscriptionId">Used to retrieve changes, remember this id.</param>
        /// <param name="entityNames">Comma separated entities e.g. "Candidate,ClientContact"</param>
        /// <param name="eventTypes">Comma separated events e.g. "inserted,updated,deleted"</param>
        /// <returns></returns>
        public async Task<EventSubscribeDto?> SubscribeAsync(string subscriptionId, string entityNames, string eventTypes)
        {
            if (string.IsNullOrWhiteSpace(subscriptionId)) throw new ArgumentException(nameof(subscriptionId));

            if (string.IsNullOrWhiteSpace(entityNames)) throw new ArgumentException(nameof(entityNames));

            if (string.IsNullOrWhiteSpace(eventTypes)) throw new ArgumentException(nameof(eventTypes));

            var query = $"event/subscription/{subscriptionId}?type=entity&names={entityNames}&eventTypes={eventTypes}";

            var response = await _bullhornApi.ApiPutAsync(query, new StringContent(string.Empty));

            return await response.DeserializeAsync<EventSubscribeDto>();
        }

        public async Task<EventSubscribeDto?> ReSubscribeAsync(string subscriptionId, string entityNames, string eventTypes)
        {
            var unsubscribed = await UnSubscribeAsync(subscriptionId);

            if (unsubscribed != null && unsubscribed.Result) return await SubscribeAsync(subscriptionId, entityNames, eventTypes);

            return null;
        }

        public async Task<EventUnSubscribeDto?> UnSubscribeAsync(string subscriptionId)
        {
            if (string.IsNullOrWhiteSpace(subscriptionId)) throw new ArgumentException(nameof(subscriptionId));

            var query = $"event/subscription/{subscriptionId}";

            var response = await _bullhornApi.ApiDeleteAsync(query);

            return await response.DeserializeAsync<EventUnSubscribeDto>();
        }

        /// <summary>
        /// Use try catch when calling this method. The subscription returns empty string if no data which is not standard and so it crashes.
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <returns></returns>
        public async Task<EventsDto?> GetAsync(string subscriptionId)
        {
            var query = $"event/subscription/{subscriptionId}?maxEvents=100";

            _bullhornApi.LogWarning("Placement Event Subscription: This call might throw an error if expected the input to start with a no valid JSON token (empty string), meaning no data in this case.");

            var response = await _bullhornApi.ApiGetAsync(query);

            return await response.DeserializeAsync<EventsDto>();
        }
    }
}
