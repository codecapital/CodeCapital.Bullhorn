namespace CodeCapital.Bullhorn.Dtos
{
    public class PlacementDto : EntityBaseDto
    {
        public ClientContactDto BillingClientContact { get; set; }
        public CandidateDto Candidate { get; set; }
        public long DateBegin { get; set; }
        public long? DateEnd { get; set; }
        public string EmployeeType { get; set; } = "";
        public string CorrelatedCustomText1 { get; set; } = "";
        public double Fee { get; set; }
        public JobOrderDto JobOrder { get; set; }
        public decimal PayRate { get; set; }
        public decimal Salary { get; set; }
        public decimal FlatFee { get; set; }
        public string SalaryUnit { get; set; } = "";
        public string Status { get; set; } = "";
        public string EmploymentType { get; set; } = "";

        public PlacementDto()
        {
            BillingClientContact = new ClientContactDto();
            Candidate = new CandidateDto();
            JobOrder = new JobOrderDto();
        }
    }
}
