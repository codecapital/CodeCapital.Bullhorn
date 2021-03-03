using System;

namespace CodeCapital.Bullhorn.Dtos
{
    public class EditHistoryClientCorporationDto
    {
        public DateTime DateAdded { get; set; }

        public int ClientCorporationId { get; set; }

        public int UpdatingUserId { get; set; }

        public string ColumnName { get; set; } = "";

        public string NewValue { get; set; } = "";

        public string OldValue { get; set; } = "";
    }
}
