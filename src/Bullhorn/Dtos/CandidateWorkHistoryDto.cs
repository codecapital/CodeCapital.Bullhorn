namespace CodeCapital.Bullhorn.Dtos
{
    public class CandidateWorkHistoryDto
    {
        public int Id { get; set; }
        public double Bonus { get; set; }
        public CandidateDto? Candidate { get; set; }
        public ClientCorporationDto? ClientCorporation { get; set; }
        public string? Comments { get; set; }
        public double Commission { get; set; }
        public string? CompanyName { get; set; }
        //public long CustomDate1 { get; set; }
        //public long CustomDate2 { get; set; }
        //public long CustomDate3 { get; set; }
        //public long CustomDate4 { get; set; }
        //public long CustomDate5 { get; set; }
        //public float CustomFloat1 { get; set; }
        //public float CustomFloat2 { get; set; }
        //public float CustomFloat3 { get; set; }
        //public float CustomFloat4 { get; set; }
        //public float CustomFloat5 { get; set; }
        //public int CustomInt1 { get; set; }
        //public int CustomInt2 { get; set; }
        //public int CustomInt3 { get; set; }
        //public int CustomInt4 { get; set; }
        //public int CustomInt5 { get; set; }
        //public string? CustomText1 { get; set; }
        //public string? CustomText2 { get; set; }
        //public string? CustomText3 { get; set; }
        //public string? CustomText4 { get; set; }
        //public string? CustomText5 { get; set; }
        //public string? CustomTextBlock1 { get; set; }
        //public string? CustomTextBlock2 { get; set; }
        //public string? CustomTextBlock3 { get; set; }
        public long DateAdded { get; set; }
        public long EndDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsLastJob { get; set; }
        public JobOrderDto? JobOrder { get; set; }
        public string? MigrateGuid { get; set; }
        public PlacementDto? Placement { get; set; }
        public decimal Salary1 { get; set; }
        public decimal Salary2 { get; set; }
        public string? SalaryType { get; set; }
        public long StartDate { get; set; }
        public string? TerminationReason { get; set; }
        public string? Title { get; set; }
    }
}
