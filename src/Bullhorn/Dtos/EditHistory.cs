namespace CodeCapital.Bullhorn.Dtos
{
    public class EditHistory
    {
        public string TransactionId { get; set; } = "";

        public long DateAdded { get; set; }

        public TargetEntityDto TargetEntity { get; set; } = new TargetEntityDto();

        public UserDto ModifyingPerson { get; set; } = new UserDto();
    }
}