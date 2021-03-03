using System;

namespace CodeCapital.Bullhorn.Dtos
{
    public class EditHistoryClientContactDto
    {
        public DateTime DateAdded { get; set; }

        public int ClientContactId { get; set; }

        public int UpdatingUserId { get; set; }

        public string ColumnName { get; set; } = "";

        public string NewValue { get; set; } = "";

        public string OldValue { get; set; } = "";
    }
}
