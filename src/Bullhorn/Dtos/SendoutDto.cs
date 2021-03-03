namespace CodeCapital.Bullhorn.Dtos
{
    public class SendoutDto
    {
        public long DateAdded { get; set; }
        public int Id { get; set; }
        public JobOrderDto JobOrder { get; set; } = new JobOrderDto();
        public ClientContactDto ClientContact { get; set; } = new ClientContactDto();
        public ClientCorporationDto ClientCorporation { get; set; } = new ClientCorporationDto();
        public CandidateDto Candidate { get; set; } = new CandidateDto();
        public UserDto User { get; set; } = new UserDto();

        public SendoutDto()
        {

        }
    }
}