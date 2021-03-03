namespace CodeCapital.Bullhorn.Dtos
{
    public class AppointmentDto : EntityBaseDto
    {
        public CandidateDto CandidateReference { get; set; }
        public ClientContactDto ClientContactReference { get; set; } = new ClientContactDto();
        public string Type { get; set; } = "";
        public bool IsDeleted { get; set; }
        public JobOrderDto JobOrder { get; set; }
        public UserDto Owner { get; set; }
        public long DateBegin { get; set; }

        public AppointmentDto()
        {
            CandidateReference = new CandidateDto();
            JobOrder = new JobOrderDto();
            Owner = new UserDto();
        }
    }
}
