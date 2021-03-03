namespace CodeCapital.Bullhorn.Dtos
{
    public class CandidateEducationDto
    {
        public int CandidateId { get; set; }
        public long StarDate { get; set; }
        public long EndDate { get; set; }
        public long GraduationDate { get; set; }
        public string School { get; set; } = "";
        public string Major { get; set; } = "";
        public string Degree { get; set; } = "";
        public string City { get; set; } = "";
        public string State { get; set; } = "";
        public string Certification { get; set; } = "";
        public string Comments { get; set; } = "";
    }
}