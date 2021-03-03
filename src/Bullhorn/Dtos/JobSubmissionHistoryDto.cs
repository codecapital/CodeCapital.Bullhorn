namespace CodeCapital.Bullhorn.Dtos
{
    public class JobSubmissionHistoryDto
    {
        public int Id { get; set; }

        public long DateAdded { get; set; }

        public JobSubmissionDto JobSubmission { get; set; } = new JobSubmissionDto();

        public string Status { get; set; } = "";

        public UserDto ModifyingUser { get; set; } = new UserDto();
    }
}
