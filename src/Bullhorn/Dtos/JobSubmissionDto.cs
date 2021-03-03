namespace CodeCapital.Bullhorn.Dtos
{
    public class JobSubmissionDto
    {
        public int Id { get; set; }

        public CandidateDto Candidate { get; set; } = new CandidateDto();

        public long DateAdded { get; set; }

        public long DateLastModified { get; set; }

        public bool IsDeleted { get; set; }

        public JobOrderDto JobOrder { get; set; } = new JobOrderDto();

        public string Status { get; set; } = "";

        public UserDto SendingUser { get; set; } = new UserDto();
    }
}
