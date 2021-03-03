namespace CodeCapital.Bullhorn.Dtos
{
    public class EditHistoryFieldChangeDto
    {
        public EditHistory EditHistory { get; set; } = new EditHistory();

        public int Id { get; set; }

        public string ColumnName { get; set; } = "";

        public string NewValue { get; set; } = "";

        public string OldValue { get; set; } = "";
    }
}
