using CodeCapital.Bullhorn.Api;
using CodeCapital.Bullhorn.Dtos;
using CodeCapital.Bullhorn.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Bullhorn.CommandLine.Services
{
    public class UpdateFieldService
    {
        private readonly ILogger<UpdateFieldService> _logger;
        private readonly BullhornApi _bullhornApi;

        public UpdateFieldService(ILogger<UpdateFieldService> logger, BullhornApi bullhornApi)
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
                _logger.LogError(e, nameof(TestApiAsync));

                throw;
            }
        }

        private async Task GetDepartmentsAsync()
        {
            var timestampFrom = DateTime.Now.AddDays(-1).Timestamp();
            var timestampTo = DateTime.Now.Timestamp();

            var testUrl = $"Placement?fields=id,customEncryptedText10,customText8&where=id=";
            //var testUrl = $"Placement?fields=id,customEncryptedText10,customText8&where=dateAdded>={timestampFrom} AND dateAdded<={timestampTo}";

            var result = await _bullhornApi.QueryAsync<DepartmentDto>(testUrl);

            _logger.LogInformation("Items: {0}", result.Count);
        }
    }
}
