using System.Collections.Generic;

namespace CodeCapital.Bullhorn.Dtos
{
    public class EventDto
    {
        public string EventId { get; set; } = "";
        public string EventType { get; set; } = "";
        public long EventTimestamp { get; set; }
        public Dictionary<string, string> EventMetaData { get; set; } = new Dictionary<string, string>();
        public string EntityName { get; set; } = "";
        public int EntityId { get; set; }
        public string EntityEventType { get; set; } = "";
        public string[] UpdatedProperties { get; set; }
    }
}
