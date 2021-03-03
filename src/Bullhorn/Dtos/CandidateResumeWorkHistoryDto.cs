namespace CodeCapital.Bullhorn.Dtos
{
    public class CandidateResumeWorkHistoryDto
    {
        public int CandidateId { get; set; }
        public long StarDate { get; set; }
        public long EndDate { get; set; }
        public string CompanyName { get; set; } = "";
        public string Title { get; set; } = "";
        public string Comments { get; set; } = "";
    }
}