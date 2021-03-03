using CodeCapital.Bullhorn.Api;
using CodeCapital.Bullhorn.Dtos;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Bullhorn.CommandLine.Services
{
    public class PlayGroundService
    {
        private readonly ILogger<PlayGroundService> _logger;
        private readonly BullhornApi _bullhornApi;

        public PlayGroundService(ILogger<PlayGroundService> logger, BullhornApi bullhornApi)
        {
            _logger = logger;
            _bullhornApi = bullhornApi;
        }

        public async Task TestApiAsync()
        {
            try
            {
                await _bullhornApi.CheckConnectionAsync();
                await GetDepartmentsAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "PlayGroundService");

                throw;
            }
        }

        private async Task GetDepartmentsAsync()
        {
            var testUrl = "Department?fields=id,description,enabled,name&where=id>0";

            var result = await _bullhornApi.QueryAsync<DepartmentDto>(testUrl);

            _logger.LogInformation("Items: {0}", result.Count);
        }
    }
}
