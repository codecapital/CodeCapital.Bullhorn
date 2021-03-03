namespace CodeCapital.Bullhorn.Dtos
{
    public class NoteDto : EntityBaseDto
    {
        public string Action { get; set; } = "";
        public UserDto CommentingPerson { get; set; }
        public UserDto PersonReference { get; set; }
        public string Comments { get; set; } = "";
        public bool IsDeleted { get; set; }

        public NoteDto()
        {
            CommentingPerson = new UserDto();

            PersonReference = new UserDto();
        }
    }
}
