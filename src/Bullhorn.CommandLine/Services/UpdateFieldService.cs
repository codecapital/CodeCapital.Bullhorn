using CodeCapital.Bullhorn.Api;
using Microsoft.Extensions.Logging;

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

        public async Task UpdateAsync()
        {
            try
            {
                await _bullhornApi.CheckConnectionAsync();

                await UpdatePlacmentFieldAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(UpdateAsync));

                throw;
            }
        }

        private async Task UpdatePlacmentFieldAsync()
        {
            //var testUrl = $"Placement?fields=id,customEncryptedText10,customText8&where=id=6908";

            //var result = await _bullhornApi.QueryAsync<PlacementDto>(testUrl);

            // Important! Make sure you update only a field you want to update. Do not use Dtos with multiple fields which are not going to be updated because Bullhorn entity will be updated with defaults.
            try
            {
                await _bullhornApi.UpdateAsync(6911, "Placement", new { customText8 = "New" });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Update UpdatePlacmentFieldAsync");
            }

            //_logger.LogInformation("Items: {0}", result.Count);
        }
    }
}
