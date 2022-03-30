using CodeCapital.Bullhorn.Dtos;

namespace CodeCapital.Bullhorn.Api.Entities
{
    public class EntityEditHistory
    {
        private readonly BullhornApi _bullhornApi;

        public EntityEditHistory(BullhornApi bullhornApi)
        {
            _bullhornApi = bullhornApi;
        }

        public async Task<List<EditHistoryFieldChangeDto>> GetAsync(EntityType entityType, long timestampFrom)
        {
            var query = $"{entityType}EditHistoryFieldChange?fields=id,display,columnName,newValue,oldValue,editHistory(dateAdded,modifyingPerson,targetEntity)&where=editHistory.dateAdded>{timestampFrom}";

            return await _bullhornApi.QueryAsync<EditHistoryFieldChangeDto>(query);
        }
    }
}
